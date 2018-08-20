using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class RandomNumber
    {
        public static int RandNumber()
        {
            int Low = 0;
            int High = 999999;
            Random rndNum = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            int rnd = rndNum.Next(Low, High);
            return rnd;
        }
    }
}
