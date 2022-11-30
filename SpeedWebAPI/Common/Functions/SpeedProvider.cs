using System;

namespace SpeedWebAPI.Common.Functions
{
    public class SpeedProvider
    {
        public const int GridSize = 100;

        private static float CalculateDistance(float X1, float Y1, float X2, float Y2)
        {
            double P1X = X1 * (Math.PI / 180);
            double P1Y = Y1 * (Math.PI / 180);
            double P2X = X2 * (Math.PI / 180);
            double P2Y = Y2 * (Math.PI / 180);

            double Kc = 0;
            double Temp = 0;

            if (X1 == X2 && Y1 == Y2) return 0;

            Kc = P2X - P1X;
            Temp = Math.Cos(Kc);
            Temp = Temp * Math.Cos(P2Y);
            Temp = Temp * Math.Cos(P1Y);

            Kc = Math.Sin(P1Y);
            Kc = Kc * Math.Sin(P2Y);
            Temp = Temp + Kc;
            Kc = Math.Acos(Temp);
            Kc = Kc * 6376000;

            //Hieu chinh quang duong km gps so voi thuc te
            //Kc = Kc * 1.0566;

            return (float)Kc;
        }

    }
}
