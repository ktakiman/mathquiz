CLS

SET avrincludepath="C:\Program Files (x86)\Arduino\hardware\tools\avr\avr\include\"
SET avrdudeconfpath="c:\Program Files (x86)\Arduino\hardware\tools\avr\etc\avrdude.conf"

SET proj=mathquiz

rem attiny13a
rem attiny84
rem attiny85
rem attiny4313
rem atmega8
rem atmega88p

SET mcugcc=atmega328p

rem t13
rem t84
rem t85
rem t4313  
rem m8
rem m88p
rem m328p

SET mcuavr=m328p


SET clock=8000000

avr-gcc -g -Os -c -mmcu=%mcugcc% -D"F_CPU=%clock%" avrlib\lcd-pcd8544.c -l%avrincludepath%
@IF ERRORLEVEL 1 GOTO errorHandling

avr-gcc -g -Os -c -mmcu=%mcugcc% -D"F_CPU=%clock%" avrlib\i2c.c -l%avrincludepath%
@IF ERRORLEVEL 1 GOTO errorHandling

avr-gcc -g -Os -c -mmcu=%mcugcc% -D"F_CPU=%clock%" src\%proj%.c -l%avrincludepath%
@IF ERRORLEVEL 1 GOTO errorHandling

avr-gcc -g -mmcu=%mcugcc% -o %proj%.elf %proj%.o lcd-pcd8544.o i2c.o
@IF ERRORLEVEL 1 GOTO errorHandling

avr-objcopy -j .text -j .data -O ihex %proj%.elf %proj%.hex
@IF ERRORLEVEL 1 GOTO errorHandling

avr-objdump -d %proj%.elf > %proj%.asm
@IF ERRORLEVEL 1 GOTO errorHandling

@echo.
@echo #### Build succeeded !!! ###
@echo.

avrdude -c avrisp -p %mcuavr% -P com4 -b 19200 -U flash:w:%proj%.hex -C %avrdudeconfpath%
@IF ERRORLEVEL 1 GOTO flashErrorHandling

@GOTO end

:errorHandling

@echo.
@echo #### Build failed ###
@echo.

@GOTO end

:flashErrorHandling

@echo.
@echo #### Failed to flash the chip ####
@echo #### Is programmer connected properly? A correct COM port specified? ####
@echo.

:end