using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolowProjectVer2
{
    public partial class Form1 : Form
    {
        private static System.Timers.Timer aTimer, bTimer;
        double gdblK, gdblY;
        double gdblKStar, gdblYStar;
        double gdblTempX, gdblMaxK, gdblMinK, gdblTempY, gdblTempInvest, gdblTempDecay;
        double gdblS, gdblN, gdblDelta;
        double gdblConsum, gdblInvest, gdblDK, gdblChangeK, gdblDecay;

        double gdblMiniGraphXVal = 0;
        int gintNumerator, gintDenom;

        //int[] gintArrLines = new int[] { 0, 1, 2, -1, -1 };
        
        bool gboolGoLeft = false;
        bool gboolInvestChanged = false;
        bool gboolDecayChanged = false;

      

        bool gboolFirstChangeOcc = false;

        

        bool gboolInterrupt = false;

        double gdblXRate = .05;
        const int TIMERATEINMILIS = 50;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (IsFractionOkay()){
                gdblS = (double)nudS.Value;
                gdblN = (double)nudN.Value;
                gdblDelta = (double)nudDelta.Value;

                CalcKsandYs(gdblS, gdblN, gdblDelta);

                // Calculate the initial window size
                if(gdblKStar < 1)
                {
                    gdblMaxK = Math.Pow(gdblKStar, 2) + gdblKStar;
                }
                else
                {
                    gdblMaxK = Math.Sqrt(gdblKStar) + gdblKStar;
                }
                chrtLines.ChartAreas[0].AxisX.Minimum = 0;
                chrtLines.ChartAreas[0].AxisX.Maximum = gdblMaxK;
                chrtLines.ChartAreas[0].AxisY.Minimum = 0;
                chrtLines.ChartAreas[0].AxisY.Maximum = Calcy(gdblMaxK);
                DrawLines();
                
            }
        }

        private void BtnSkip_Click(object sender, EventArgs e)
        {

        }

        private void BtnReset_Click(object sender, EventArgs e)
        {

        }

        private void txtKNumerator_Leave(object sender, EventArgs e)
        {
            if (!txtKDenominator.Focused)
            {
                IsFractionOkay();
            }
                
            
        }

        private void txtKDenominator_Leave(object sender, EventArgs e)
        {
            if (!txtKNumerator.Focused)
            {
                IsFractionOkay();
            }
            
        }

        private void DrawLines()
        {
            while (gdblK < gdblMaxK)
            {
                chrtLines.Series[0].Points.AddXY(gdblK, Calcy(gdblK));
                chrtLines.Series[1].Points.AddXY(gdblK, CalcInvest(Calcy(gdblK), gdblS));
                chrtLines.Series[2].Points.AddXY(gdblK, CalcDecay(gdblN, gdblDelta, gdblK));
                gdblK += gdblXRate;
            }
        }


        private bool IsFractionOkay()
        {
            if (!(string.IsNullOrEmpty(txtKNumerator.Text) || string.IsNullOrEmpty(txtKDenominator.Text))){
                if (int.TryParse(txtKNumerator.Text, out gintNumerator) && int.TryParse(txtKDenominator.Text, out gintDenom))
                {
                    if (gintNumerator == 0 || gintDenom == 0)
                    {
                        return false;
                    }
                    else if (gintNumerator >= gintDenom)
                    {
                        //Print message that the numberator must be smaller than denominator
                        return false;
                    }
                    else
                    {
                        GreatestCommonD(ref gintNumerator, ref gintDenom);
                        txtKNumerator.Text = gintNumerator.ToString();
                        txtKDenominator.Text = gintDenom.ToString();
                        txtSmlKNumerator.Text = gintNumerator.ToString();
                        txtSmlKDenominator.Text = gintDenom.ToString();
                        txtLDenominator.Text = gintDenom.ToString();
                        txtLNumerator.Text = (gintDenom - gintNumerator).ToString();
                        return true;
                    }
                }
                else
                {
                    //Print message that they are not numbers
                    return false;
                }
            }
            else
            {
                return false;
            }
        }







        private void CalcKsandYs(double S, double N, double Delta)
        {
            double k, y;
            k = S / (N + Delta);
            y = k;
            double lNum = gintDenom - gintNumerator;

            k = Math.Pow(k, (gintDenom / lNum));
            y = Math.Pow(y, (gintNumerator / lNum));
            gdblKStar = k;
            gdblYStar = y;
            /*
            txtK.Text = Math.Round(k, 2).ToString();
            txtY.Text = Math.Round(y, 2).ToString();
            */
        }
        private double Calcy(double K)
        {
            return Math.Pow(K, ((double)gintNumerator / gintDenom));
        }
        private double CalcConsum()
        {
            return (1.0 - gdblS) * gdblY;
        }

        private double CalcInvest(double y, double s)
        {
            return y * s;
        }

        private double CalcDeltaTimesK()
        {
            return gdblDelta * gdblK;
        }

        private double CalcChangeOfK()
        {
            return (gdblS * gdblY) - ((gdblN + gdblDelta) * gdblK);
        }

        private double CalcDecay(double n, double delta, double k)
        {
            return (n + delta) * k;
        }


        public static void GreatestCommonD(ref int Numerator, ref int Denominator)
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

    }
}
