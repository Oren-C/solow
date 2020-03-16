using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolowProjectVer2
{
    static class Calculations
    {

        //Sets the K Star and Y Star passed by reference 
        static public void CalcKsandYs(double S, double N, double Delta, int numer, int denom, ref double dblKStar, ref double dblYStar)
        {
            double k, y;
            k = S / (N + Delta);
            y = k;
            double lNum = denom - numer;

            k = Math.Pow(k, (denom / lNum));
            y = Math.Pow(y, (numer / lNum));
            dblKStar = k;
            dblYStar = y;

        }

        //Returns y
        static public double Calcy(double K, int numer, int denom)
        {
            return Math.Pow(K, ((double)numer / denom));
        }

        //Returns Consumption
        static public double CalcConsum(double S, double Y)
        {
            return (1.0 - S) * Y;
        }


        //Returns Investment
        static public double CalcInvest(double y, double s)
        {
            return y * s;
        }

        //Returns what is says
        /*
        static public double CalcDeltaTimesK(double delta, double k)
        {
            return delta * k;
        }
        */
        static public double CalcChangeOfK(double s, double y, double n, double delta, double k)
        {
            return (s * y) - ((n + delta) * k);
        }

        static public double CalcDecay(double n, double delta, double k)
        {
            return (n + delta) * k;
        }

        static public void GreatestCommonD(ref int Numerator, ref int Denominator)
        {
            int greatestCommonD = 0;
            for (int x = 1; x <= Denominator; x++)
            {
                if ((Numerator % x == 0) && (Denominator % x == 0))
                    greatestCommonD = x;
            }
            if (greatestCommonD == 0)
            {
                return;
            }
            else
            {
                Numerator = Numerator / greatestCommonD;
                Denominator = Denominator / greatestCommonD;
            }
        }

        public static double RoundTo3Decimals(double num)
        {
            return Math.Round(num * 1000) / 1000d;
        }
    }

   







}
