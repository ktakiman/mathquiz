#include <avr/io.h>
#include <avr/power.h>
#include <util/delay.h>
#include <avr/interrupt.h>
#include <avr/eeprom.h>

#include "../avrlib/lcd-pcd8544.h"
#include "../avrlib/i2c.h"


#define VOP_TO_USE 0x3a

#define MODE_QUESTIONING 0
#define MODE_FEEDBACK 1

#define LINE_LIFE 0
#define LINE_QUESTION 2
#define LINE_ANSWER 4
#define LINE_CURSOR 5

#define INPUT_DIGITS_MAX 4
#define INPUT_INDENT 2
#define INPUT_OK_CURSOR_POS 7

typedef uint8_t (*FUNC_SELECTUSER)(uint8_t, uint8_t*);

volatile uint8_t gBtn1Clicked = 0;
volatile uint8_t gBtn2Clicked = 0;
volatile uint8_t gBtn3Clicked = 0;

#define EEPROM_ADDR_STAGE (uint8_t*)0x01

// ----------------------------------------------------------------------------
volatile uint16_t gVal1;
volatile uint16_t gVal2;
volatile uint8_t gArithOp;

volatile uint8_t gStageCt;
volatile uint8_t gNumOfQsInStage;

volatile uint8_t gStage;
volatile uint8_t gQIndex;

volatile uint8_t gLifeCt;
volatile uint8_t gCursorPos;

uint8_t gBufLife[PCD8544_COL_MAX + 1];
uint8_t gBufQuestion[PCD8544_COL_MAX + 1];
uint8_t gBufAnswer[PCD8544_COL_MAX + 1];
uint8_t gBufCursor[PCD8544_COL_MAX + 1];
uint8_t gBufMsg[PCD8544_COL_MAX + 1];

#define EX_EEPROM_BUF_SIZE 128
struct I2C gI2C;
uint8_t gExEEPROMBuf[EX_EEPROM_BUF_SIZE];
uint8_t gExEEPROMBuf2[EX_EEPROM_BUF_SIZE];

uint16_t gExEEPROMAddrQ;
uint16_t gExEEPROMAddrS;
uint16_t gExEEPROMAddrStage;

uint8_t gNumOfTunes;
uint8_t gLockKey[11];

// ----------------------------------------------------------------------------
void playTune(uint8_t index);

// ----------------------------------------------------------------------------
void memSet(uint8_t buf[], uint8_t size, uint8_t val, uint8_t nullTerminate)
{
    uint8_t pos;
    for (pos = 0; pos < size; pos++) { buf[pos] = val; }
    if (nullTerminate) { buf[pos] = 0; }
}
// ----------------------------------------------------------------------------
uint8_t int2Chars(uint16_t num, uint8_t buf[], uint8_t startPos, uint8_t nullTerminate)
{
    uint8_t pos = startPos;
    
    if (num / 10000 > 0)
    {
        buf[pos++] = (num / 10000) + '0';
        num %= 10000;
    }
    
    if (num / 1000 > 0)
    {
        buf[pos++] = (num / 1000) + '0';
        num %= 1000;
    }
    else if (pos > startPos)
    {
        buf[pos++] = '0';
    }
    
    if (num / 100 > 0)
    {
        buf[pos++] = (num / 100) + '0';
        num %= 100;
    }
    else if (pos > startPos)
    {
        buf[pos++] = '0';
    }    
 
    if (num / 10 > 0)
    {
        buf[pos++] = (num / 10) + '0';
        num %= 10;
    }
    else if (pos > startPos)
    {
        buf[pos++] = '0';
    }  
    
    buf[pos++] = '0' + num;
    
    if (nullTerminate) { buf[pos] = 0; }
    
    return pos;
}
// ----------------------------------------------------------------------------
uint8_t writeToBuf(uint8_t s[], uint8_t buf[], uint8_t startPos, uint8_t nullTerminate)
{
    uint8_t pos = 0;
    while (1)
    {
        if (s[pos] == 0) { break; }
        
        buf[startPos + pos] = s[pos];
        pos++;
    }
    
    if (nullTerminate) { buf[startPos + pos] = 0; }
    
    return startPos + pos;
}
// ----------------------------------------------------------------------------
void memCopy(uint8_t srcBuf[], uint8_t tgtBuf[], uint8_t srcStartPos, uint8_t tgtStartPos, uint8_t len)
{
    for (uint8_t i = 0; i < len; i++)
    {
        tgtBuf[tgtStartPos + i] = srcBuf[srcStartPos + i];
    } 
}
// ----------------------------------------------------------------------------
uint16_t readUInt16(uint8_t buf[], uint8_t pos)
{
    uint16_t v;
    *((uint8_t*)&v + 1) = buf[pos];
    *((uint8_t*)&v) = buf[pos + 1];
    return v;
}
// ----------------------------------------------------------------------------
uint8_t selectUser(uint8_t numUsers, uint8_t* pData)
{
    uint8_t selected = 0;
    while(1)
    {
        uint8_t buf[13];
        buf[1] = ' ';
        buf[12] = 0;
        for (uint8_t i = 0; i < numUsers; i++)
        {
            buf[0] = i == selected ? '>' : ' '; 
            memCopy(pData, buf, i * 21, 2, 10);
            _pcd8544_setText(i, buf, 0);
        }

        _pcd8544_renderText();

        while (1)
        {
            if (gBtn2Clicked)
            {
                selected = selected == numUsers - 1 ? 0 : selected + 1;
                gBtn2Clicked = 0;
                break;
            }
            if (gBtn1Clicked)
            {
                gBtn1Clicked = 0;
                return selected;
            }
        }
    }

    return 0; // should never execute
}
// ----------------------------------------------------------------------------
void setupNextQuestionAndBuf()
{
    // expecting gQIndex incremented externally
    uint8_t index = gStage * gNumOfQsInStage + gQIndex;
    
    uint16_t addr = gExEEPROMAddrQ + 2 + index * 5;
    _eeprom_read(&gI2C, addr, (uint8_t*)gExEEPROMBuf, 0, 5);
    
    gVal1 = readUInt16(gExEEPROMBuf, 0);
    gVal2 = readUInt16(gExEEPROMBuf, 2);
    gArithOp = gExEEPROMBuf[4];
    
    memSet(gBufAnswer, PCD8544_COL_MAX, ' ', 1);
    memSet(gBufQuestion, PCD8544_COL_MAX, ' ', 1);
    uint8_t pos = int2Chars(gVal1, gBufQuestion, 0, 0);
    gBufQuestion[pos + 1] = gArithOp;
    pos += 3;
    pos = int2Chars(gVal2, gBufQuestion, pos, 0);
    gBufQuestion[pos + 1] = '=';
}
// ----------------------------------------------------------------------------
void updateLifeBuf()
{
    memSet(gBufLife, PCD8544_COL_MAX, ' ', 1);
    uint8_t i;
    for (i = 0; i < gLifeCt; i++) { gBufLife[i] = PCD8544_FONT_HEART; }
    
    // put stage/qindex
    uint8_t buf[8];
    i = int2Chars(gQIndex+ 1, buf, 0, 0);
    buf[i++] = ' ';
    buf[i++] = '|';
    buf[i++] = ' ';
    i = int2Chars(gStage + 1, buf, i, 1);
    writeToBuf(buf, gBufLife, PCD8544_COL_MAX - i, 0);    
}
// ----------------------------------------------------------------------------
void initAnswerBuf()
{
    memSet(gBufAnswer, PCD8544_COL_MAX, ' ', 1);
    gBufAnswer[INPUT_INDENT] = '0';
    gBufAnswer[INPUT_INDENT + INPUT_OK_CURSOR_POS] = 'O';
    gBufAnswer[INPUT_INDENT + INPUT_OK_CURSOR_POS + 1] = 'K';
}
// ----------------------------------------------------------------------------
void updateAnswerBuf()
{
    uint8_t newChar;
    uint8_t curChar = gBufAnswer[INPUT_INDENT + gCursorPos];
    switch (curChar)
    {
        case ' ': newChar = '0'; break;
        case '9': newChar = gCursorPos == 0 ? '0' : ' '; break;
        default: newChar = curChar + 1; break;
    }
    
    gBufAnswer[INPUT_INDENT + gCursorPos] = newChar;
}
// ----------------------------------------------------------------------------
void updateAnswerBufReverse()
{

    if (gCursorPos != INPUT_OK_CURSOR_POS)
    {
        uint8_t newChar;
        uint8_t curChar = gBufAnswer[INPUT_INDENT + gCursorPos]; 
        switch (curChar)
        {
            case ' ': newChar = '9'; break;
            case '0': newChar = gCursorPos == 0 ? '9' : ' '; break;
            default: newChar = curChar - 1; break; 
        }

        gBufAnswer[INPUT_INDENT + gCursorPos] = newChar;
    }
}
// ----------------------------------------------------------------------------
void updateCursorBuf(uint8_t shift)
{
    memSet(gBufCursor, PCD8544_COL_MAX, ' ', 1);
    
    if (shift)
    {
        switch (gCursorPos)
        {
            case INPUT_OK_CURSOR_POS: gCursorPos = 0; break;
            case INPUT_DIGITS_MAX - 1: gCursorPos = INPUT_OK_CURSOR_POS; break;
            default: gCursorPos++; break;
        }    
    }
    
    gBufCursor[INPUT_INDENT + gCursorPos] = '^';
}
// ----------------------------------------------------------------------------
uint8_t checkAnswer(uint8_t buf[])
{
    uint16_t calculated;
    uint16_t answered = 0;
    
    switch (gArithOp)
    {
        case '+': calculated = gVal1 + gVal2; break;
        case '-': calculated = gVal1 - gVal2; break;
        case '*': calculated = gVal1 * gVal2; break;
        case 'x': calculated = gVal1 * gVal2; break;
        case '/': calculated = gVal1 / gVal2; break;
    }
    
    for (uint8_t i = 0; i < 4; i++)
    {
        if (buf[2 + i] != ' ')
        {
            answered *= 10;
            answered += buf[2 + i] - '0';
        }
        else
        {
            break;
        }
    }
    
    return calculated == answered;
}
// ----------------------------------------------------------------------------
void showStageMsg(uint8_t playOpeningTune)
{
    _pcd8544_clearText(1);
     uint8_t msgPos = writeToBuf("  STAGE \0", gBufMsg, 0, 0);
    int2Chars(gStage + 1, gBufMsg, msgPos, 1);
    _pcd8544_setText(2, gBufMsg, 1);
   
    if (playOpeningTune)
    {
        playTune(2);
    }
    else
    {
        _delay_ms(3000);
    }
}
// ----------------------------------------------------------------------------
void showStageClearMsg()
{
    _pcd8544_clearText(1);
    uint8_t msgPos = writeToBuf("  STAGE \0", gBufMsg, 0, 0);
    int2Chars(gStage + 1, gBufMsg, msgPos, 1);
    _pcd8544_setText(1, gBufMsg, 0);
    _pcd8544_setText(3, "   CLEAR!!!\0", 1);
    
    if (gStage % 2 == 1)
    {
        playTune(4  + (gStage / 2) % (gNumOfTunes - 4));
    }
    else
    {
        _delay_ms(2000);
    }
}
// ----------------------------------------------------------------------------
void showFinale()
{
    _pcd8544_clearText(1);
    _pcd8544_setText(1, " CONGRATS!!!\0", 0);
    _pcd8544_setText(3, "SECRET KEY IS\0", 0);

    uint8_t pos = writeToBuf(" ", gBufMsg, 0, 0);
    pos = writeToBuf(gLockKey, gBufMsg, pos, 1);
    _pcd8544_setText(5, gBufMsg, 1);
    
    playTune(3);

    while (1);
}
// ----------------------------------------------------------------------------
void updateLcd()
{
    _pcd8544_clearText(0);
    _pcd8544_setText(LINE_LIFE, gBufLife, 0);
    _pcd8544_setText(LINE_QUESTION, gBufQuestion, 0);
    _pcd8544_setText(LINE_ANSWER, gBufAnswer, 0);
    _pcd8544_setText(LINE_CURSOR, gBufCursor, 1);
}
// ----------------------------------------------------------------------------
void dumpByte(uint8_t byte) // LCD used as debug dump
{
    _pcd8544_newLine(0);
    _pcd8544_appendByteAsHex(byte, 1);
}
// ----------------------------------------------------------------------------
void dumpWord(uint16_t word) // LCD used as debug dump
{
    _pcd8544_newLine(0);
    _pcd8544_appendWordAsHex(word, 1);
}
// ----------------------------------------------------------------------------
void initFromExEEPROMData(struct I2C* pI2C, FUNC_SELECTUSER selectUser)
{
    uint8_t* buf = (uint8_t*)gExEEPROMBuf;

    // read address for the main blocks
    _eeprom_read(pI2C, 0x0000, buf, 0, 6);

    uint16_t addrU = readUInt16(buf, 0);
    uint16_t addrQ = readUInt16(buf, 2);
    gExEEPROMAddrS = readUInt16(buf, 4);

    // read userinfo block
    uint8_t numUsrs;
    _eeprom_read(pI2C, addrU, &numUsrs, 0, 1);

    
    uint8_t usrIndex = 0;
    _eeprom_read(pI2C, addrU + 1, buf, 0, numUsrs * 21); // name = 10 bytes, stage = 1 byte, secret key = 10 bytes

    if (numUsrs > 1)
    {
        usrIndex = selectUser(numUsrs, buf);
    }

    gStage = buf[21 * usrIndex + 10]; // stage is at offset 10 
    gExEEPROMAddrStage = addrU + 1 + 21 * usrIndex + 10; // has to be a global address
    
    // read lock key number
    memCopy(buf, gLockKey, 21 * usrIndex + 11, 0, 10);
    gLockKey[10] = 0;

    // read question block header
    _eeprom_read(pI2C, addrQ + 2 * usrIndex, buf, 0, 2);
    gExEEPROMAddrQ = addrQ + readUInt16(buf, 0);

    _eeprom_read(pI2C, gExEEPROMAddrQ, buf, 0, 2);
    
    gStageCt = buf[0];
    gNumOfQsInStage = buf[1];

    if (gStage >= gStageCt)
    {
        gStage = 0;
    }

    // read song block header
    _eeprom_read(pI2C, gExEEPROMAddrS, &gNumOfTunes, 0, 1);
}
// ----------------------------------------------------------------------------
void i2cDump(uint8_t* pMsg)
{
    _pcd8544_appendText(pMsg, 1);
}

// ----------------------------------------------------------------------------
// Pizzo buzzer
// ----------------------------------------------------------------------------

// base frequency
//   processor 8Mhz
//   clock prescale 8
//   base frequency - 1,000,000
//   16 bit timer TOP value (half length of the period)

#define PT_NONE 0

#define PT_A3  2273
#define PT_A3S 2145
#define PT_B3  2025
#define PT_C4  1915
#define PT_C4S 1804
#define PT_D4  1703
#define PT_D4S 1607
#define PT_E4  1517
#define PT_F4  1432
#define PT_F4S 1351
#define PT_G4  1276
#define PT_G4S 1204
#define PT_A4  1136
#define PT_A4S 1073
#define PT_B4  1012
#define PT_C5   956
#define PT_C5S  902
#define PT_D5   851
#define PT_D5S  804
#define PT_E5   758
#define PT_F5   716
#define PT_F5S  676
#define PT_G5   638
#define PT_G5S  602
#define PT_A5   568
#define PT_A5S  536
#define PT_B5   506
#define PT_C6   478
#define PT_C6S  451
#define PT_D6   426
#define PT_D6S  402
#define PT_E6   379
#define PT_F6   358
#define PT_F6S  338
#define PT_G6   319
#define PT_G6S  301
#define PT_A6   284
#define PT_A6S  268
#define PT_B6   253

volatile uint8_t gDelayStatus;
volatile uint16_t gDelayCt;
volatile uint16_t gDelayTop;

#define DELAY_STATUS_CLEAR 0
#define DELAY_STATUS_WAITING 1

#define BASE_TUNE_SPEED 4096

// ----------------------------------------------------------------------------
void delay(uint16_t unit,void (*pIdleCallback)())
{
    gDelayTop = unit / 16;
    gDelayCt = 0;
    gDelayStatus = DELAY_STATUS_WAITING;
    TCNT1 = 0;
    TIMSK0 |= (1 << OCIE0A);

    while(gDelayStatus == DELAY_STATUS_WAITING)
    {
        if (pIdleCallback)
        {
            pIdleCallback();
            pIdleCallback = 0;
        }
    }
    
    TIMSK0 &= ~(1 << OCIE0A);
}
// ----------------------------------------------------------------------------
void playNote(uint16_t pitch, uint16_t duration, uint8_t noGap, void (*pIdleCallback)())
{
    //_pcd8544_newLine(0);
    //_pcd8544_appendWordAsInt(duration, 1);

    if (pitch != 0)
    {
        OCR1A = pitch;
        TCCR1A |= (1 << COM1A0); // toggle OC0A on comare match
    }
    delay(duration * 9 / 10, pIdleCallback);
    
    TCCR1A &= ~(1 << COM1A0); // toggle OC0A on comare match
    
    if (!noGap)
    {
        delay(duration / 10, 0);
    }
}
// ----------------------------------------------------------------------------
void playTuneBlock(uint8_t* buf, uint8_t numOfNotes, uint16_t baseDuration, uint8_t noGap, void (*pIdleCallback)())
{
    for (uint8_t i = 0;i < numOfNotes; i++)
    {
        uint16_t pitch = 0;
        uint16_t duration = 0;
        
        uint8_t scale = buf[i * 4 + 1];
        
        switch (buf[i * 4])
        {
            case 'C': pitch = scale == 6 ? PT_C6 : (scale == 5 ? PT_C5 : PT_C4); break;
            case 'c': pitch = scale == 6 ? PT_C6S : (scale == 5 ? PT_C5S : PT_C4S); break;
            case 'D': pitch = scale == 6 ? PT_D6 : (scale == 5 ? PT_D5 : PT_D4); break;
            case 'd': pitch = scale == 6 ? PT_D6S : (scale == 5 ? PT_D5S : PT_D4S); break;
            case 'E': pitch = scale == 6 ? PT_E6 : (scale == 5 ? PT_E5 : PT_E4); break;
            case 'F': pitch = scale == 6 ? PT_F6 : (scale == 5 ? PT_F5 : PT_F4); break;
            case 'f': pitch = scale == 6 ? PT_F6S : (scale == 5 ? PT_F5S : PT_F4S); break;
            case 'G': pitch = scale == 6 ? PT_G6 : (scale == 5 ? PT_G5 : PT_G4); break;
            case 'g': pitch = scale == 6 ? PT_G6S  : (scale == 5 ? PT_G5S : PT_G4S); break;
            case 'A': pitch = scale == 6 ? PT_A6  : (scale == 5 ? PT_A5 : (scale == 4 ? PT_A4 : PT_A3)); break;
            case 'a': pitch = scale == 6 ? PT_A6S  : (scale == 5 ? PT_A5S : (scale == 4 ? PT_A4S : PT_A3S)); break;
            case 'B': pitch = scale == 6 ? PT_B6  : (scale == 5 ? PT_B5 : (scale == 4 ? PT_B4 : PT_B3)); break;
        }

        uint16_t durEnu = buf[i * 4 + 2];
        uint16_t durDen = buf[i * 4 + 3];
        
        playNote(pitch, baseDuration * durEnu / durDen, noGap, pIdleCallback);
        pIdleCallback = 0;
    }
}
// ----------------------------------------------------------------------------
uint16_t gTuneAddr;
uint8_t gTuneSpeed;
uint8_t gTuneSize;
uint8_t gTuneBlockCt;
uint8_t gTuneBlockSize;
uint8_t gTuneLastBlockSize;
uint8_t gTuneCurrBlock;

uint8_t gExEEPROMBufTOUse = 0;
uint8_t* gpTuneBufToUse;
uint8_t gNextTuneBlockSize;

// ----------------------------------------------------------------------------
void loadTuneBlock()
{
    if (gTuneCurrBlock < gTuneBlockCt)
    {
        if (gExEEPROMBufTOUse == 0)
        {   
            gpTuneBufToUse = gExEEPROMBuf;
            gExEEPROMBufTOUse = 1;
        }
        else
        {
            gpTuneBufToUse = gExEEPROMBuf2;
            gExEEPROMBufTOUse = 0;
        }
        
        gNextTuneBlockSize = gTuneCurrBlock == gTuneBlockCt - 1  ? gTuneLastBlockSize : gTuneBlockSize;

        _eeprom_read(&gI2C, gExEEPROMAddrS + gTuneAddr + 2 + EX_EEPROM_BUF_SIZE * gTuneCurrBlock, gpTuneBufToUse, 0, gNextTuneBlockSize * 4);    
    }
}
// ----------------------------------------------------------------------------
void playTune(uint8_t index)
{
    _eeprom_read(&gI2C, gExEEPROMAddrS + 1 + 2 * index, (uint8_t*)gExEEPROMBuf, 0, 2);
    gTuneAddr = readUInt16(gExEEPROMBuf, 0);
            
    _eeprom_read(&gI2C, gExEEPROMAddrS + gTuneAddr, (uint8_t*)&gTuneSpeed, 0, 1);
    _eeprom_read(&gI2C, gExEEPROMAddrS + gTuneAddr + 1, (uint8_t*)&gTuneSize, 0, 1);
            
    gTuneBlockSize = EX_EEPROM_BUF_SIZE / 4;
    
    if (gTuneSize % gTuneBlockSize > 0)
    {
        gTuneBlockCt = gTuneSize / gTuneBlockSize + 1;
        gTuneLastBlockSize = gTuneSize % gTuneBlockSize;
    }
    else
    {
        gTuneBlockCt = gTuneSize / gTuneBlockSize;
        gTuneLastBlockSize = gTuneBlockSize;
    }

    gTuneCurrBlock = 0;
    
    loadTuneBlock();

    while (gTuneCurrBlock < gTuneBlockCt)
    {
        gTuneCurrBlock++;
        playTuneBlock(gpTuneBufToUse, gNextTuneBlockSize, BASE_TUNE_SPEED / gTuneSpeed, 0, loadTuneBlock);      
    }
}
// ----------------------------------------------------------------------------
void setStage(uint8_t stage)
{
    _eeprom_write(&gI2C, gExEEPROMAddrStage, &stage, 0, 1, 1);
}
// ----------------------------------------------------------------------------
ISR(TIMER0_COMPA_vect)
{
    gDelayCt++;

    PORTC ^= (1 << PINC5);
    
    if (gDelayCt == gDelayTop)
    {
        gDelayStatus = DELAY_STATUS_CLEAR;
    }
}
// ----------------------------------------------------------------------------
ISR(PCINT1_vect)
{
    if (!(PINC & (1 << PINC0))) { gBtn1Clicked = 1; }
    else if (!(PINC & (1 << PINC1))) { gBtn2Clicked = 1; }
    else if (!(PINC & (1 << PINC2))) { gBtn3Clicked = 1; }
}
// ----------------------------------------------------------------------------
int main(void) {
    clock_prescale_set(clock_div_1);

    // LCD setup (PD0~PD4)
    DDRD |= (1 << PIND0 | 1 << PIND1 | 1 << PIND2 | 1 << PIND3 | 1 << PIND4);
    
    // Switch button setup - PC0(PCINT8), PC1(PCINT9), PC2(PCINT10)
    PORTC |= (1 << PINC0 | 1 << PINC1 | 1 << PINC2);

    // pin change interrupt for switch buttons
    PCICR |= (1 << PCIE1);
    PCMSK1 |= (1 << PCINT8 | 1 << PCINT9 | 1 << PCINT10);

    // Piezzo buzzer setup 
    // Timer 0
    TCCR0A |= (1 << WGM01); // Timer0 CTC Mode
    TCCR0B |= (1 << CS00 | 1 << CS02); // Timer0 prescaler - 1024
    OCR0A = 64; // 8ms
    //TIMSK0 |= (1 << OCIE0A);
    
    // Timer 1
    DDRB |= (1 << PINB1); // OC1A
    TCCR1B |= (1 << WGM12); // Timer1 CTC Mode
    TCCR1B |= (1 << CS11); // Timer1 prescaler - 1024
    
    // Reest EEPROM
    // PORTC |= (1 << PINC5); // enable pull-up

    _delay_ms(1000);
    
    sei();
    
    _pcd8544_init(&PORTD, PIND0, PIND1, PIND2, PIND3, PIND4, VOP_TO_USE);

    gI2C.pDDR = &DDRD;
    gI2C.pPort = &PORTD;
    gI2C.pPin = &PIND;
    gI2C.sda = PIND6;
    gI2C.scl = PIND7;
    gI2C.pDumpCallback = i2cDump;
    
    _i2c_init(&gI2C);    
    
    initFromExEEPROMData(&gI2C, selectUser);
   
    gQIndex = 0;
    
    gLifeCt = 5;
    gCursorPos = 0;

    updateLifeBuf();
    setupNextQuestionAndBuf();
    initAnswerBuf();
    updateCursorBuf(0);

    showStageMsg(1);
    
    updateLcd();

    while (1)
    {
        if (gBtn1Clicked)
        {
            updateCursorBuf(1);
            _pcd8544_setText(LINE_CURSOR, gBufCursor, 1);

            gBtn1Clicked = 0;        
        }
        if (gBtn2Clicked)
        {
            if (gCursorPos == INPUT_OK_CURSOR_POS)
            {
                if (checkAnswer(gBufAnswer))
                {
                    _pcd8544_clearText(1);
                    _pcd8544_setText(2, " CORRECT!!\0", 1);
                    playTune(0);
                    _delay_ms(1000);
                
                    if (gQIndex == gNumOfQsInStage - 1)
                    {
                        showStageClearMsg();
                        
                        gStage++;
                        gQIndex = 0;
                        gLifeCt = 5;

                        if (gStage == gStageCt)
                        {
                            setStage(0);
                            
                            _delay_ms(1000);
                            
                            showFinale();
                        }
                        else
                        {
                            setStage(gStage);
                        
                            showStageMsg(0);
                        }
                    }
                    else
                    {
                        // update question
                        gQIndex++;
                    }

                    setupNextQuestionAndBuf();   
                    updateLifeBuf(); // for stage/qindex indicator
                    
                    initAnswerBuf();
                    
                    gCursorPos = 0;
                    updateCursorBuf(0);
                    
                    updateLcd();
                }
                else
                {
                    _pcd8544_clearText(1);
                    _pcd8544_setText(2, "    UH-OH\0", 1);
                    playTune(1);
                    _delay_ms(1000);
                    
                    gLifeCt--; 
                    
                    if (gLifeCt == 0)
                    {
                        _pcd8544_clearText(0);
                        _pcd8544_setText(1, "  LET'S TRY\0", 0);
                        _pcd8544_setText(3, "    AGAIN\0", 1);
                        _delay_ms(2000);

                        gQIndex = 0;
                        gLifeCt = 5;
                        
                        showStageMsg(0);
                        
                        setupNextQuestionAndBuf();   
                        initAnswerBuf();
                    }
                    
                    updateLifeBuf();
                    updateCursorBuf(1);
                    updateLcd();
                }
            }
            else
            {
                updateAnswerBuf();
                _pcd8544_setText(LINE_ANSWER, gBufAnswer, 1);
            }

            gBtn2Clicked = 0;
        }
        if (gBtn3Clicked)
        {
            updateAnswerBufReverse();
            _pcd8544_setText(LINE_ANSWER, gBufAnswer, 1);

            gBtn3Clicked = 0;
        }
    }
}






