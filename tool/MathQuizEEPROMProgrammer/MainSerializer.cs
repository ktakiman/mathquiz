using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathQuizEEPROMWriter
{
    class MainSerializer
    {
        public static List<byte> Serialize(string qDataStr, string sdDataStr, string secretKey)
        {
            var qSerializer = new QuestionsSerializer(qDataStr);
            var sSerializer = new SoundSerializer(sdDataStr);
            var lKSerializer = new LockKeySerializer(secretKey);

            var qBytes = qSerializer.Serialize();
            var sdBytes = sSerializer.Serialize();
            var lkBytes = lKSerializer.Serialize();

            var data = new List<byte>();

            UInt16 qDataAddr = 0x06;
            UInt16 sDataAddr = (UInt16)(qDataAddr + qBytes.Count);
            UInt16 lkDataAddr = (UInt16)(sDataAddr + sdBytes.Count);

            BaseSerializer.addUInt16(data, qDataAddr);
            BaseSerializer.addUInt16(data, sDataAddr);
            BaseSerializer.addUInt16(data, lkDataAddr);

            data.AddRange(qBytes);
            data.AddRange(sdBytes);
            data.AddRange(lkBytes);

            return data;
        }
    }
}
