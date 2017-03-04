using System;
using System.Collections.Generic;

namespace MathQuizEEPROMWriter
{
    class MainSerializer
    {
        public static List<byte> Serialize(string uDataStr, string qDataStr, string sdDataStr)
        {
            var uSerializer = new UserDataSerializer(uDataStr);
            var qSerializer = new QuestionsSerializer(qDataStr, uSerializer.UserCt);
            var sSerializer = new SoundSerializer(sdDataStr);

            var uBytes = uSerializer.Serialize();
            var qBytes = qSerializer.Serialize();
            var sdBytes = sSerializer.Serialize();

            var data = new List<byte>();

            UInt16 uDataAddr = 0x06;
            UInt16 qDataAddr = (UInt16)(uDataAddr + uBytes.Count);
            UInt16 sDataAddr = (UInt16)(qDataAddr + qBytes.Count);

            BaseSerializer.addUInt16(data, uDataAddr);
            BaseSerializer.addUInt16(data, qDataAddr);
            BaseSerializer.addUInt16(data, sDataAddr);

            data.AddRange(uBytes);
            data.AddRange(qBytes);
            data.AddRange(sdBytes);

            return data;
        }
    }
}
