using System;
using System.Security.Cryptography;

namespace Sys.Common.Helper
{
    public static class CommonHelper
    {
        public static int LengthPinCode = 6;

        public static int GenerateRandomInteger(int min = 0, int max = int.MaxValue)
        {
            var randomNumberBuffer = new byte[10];
            new RNGCryptoServiceProvider().GetBytes(randomNumberBuffer);
            return new Random(BitConverter.ToInt32(randomNumberBuffer, 0)).Next(min, max);
        }

        public static string GenerateRandomCode()
        {
            string pinCode = "";
            for (int i = 0; i < LengthPinCode; i++)
            {
                pinCode = string.Concat(pinCode, GenerateRandomInteger().ToString());
            }
            return pinCode;
        }
    }
}