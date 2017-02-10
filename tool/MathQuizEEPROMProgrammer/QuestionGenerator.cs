using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathQuizEEPROMWriter
{
    //interface IQuestionGenerator
    //{
    //    string Generate(int count);
    //}

    static class QuestionGenerator /*: IQuestionGenerator*/
    {
        static public string GenerateAddition(int min, int max, bool carry, int count)
        {
            var sb = new StringBuilder();
            var rd = new Random();

            for (int i = 0; i < count; i++)
            {
                int a = rd.Next(max - min) + min;

                if (carry)
                {
                    int v1 = rd.Next(a);
                    int v2 = a - v1;

                    sb.AppendFormat("{0} + {1}\r\n", v1, v2);
                }
                else
                {
                    int v1 = 0;
                    int v2 = 0;

                    int pos = 0;
                    while (a > 0)
                    {
                        int d = a % 10;

                        if (d != 0)
                        {
                            int d1 = rd.Next(d + 1);
                            int d2 = d - d1;

                            v1 += (int)Math.Pow(10, pos) * d1;
                            v2 += (int)Math.Pow(10, pos) * d2;
                        }

                        pos++;

                        a /= 10;
                    }

                    sb.AppendFormat("{0} + {1}\r\n", v1, v2);
                }
            }

            return sb.ToString();
        }

    }
}
