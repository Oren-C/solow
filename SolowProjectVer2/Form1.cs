using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

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

        Random rnd = new Random();

        double gdblMiniGraphXVal = 0;
        int gintNumerator, gintDenom;
        int gintButtonsPushed = 0;

        //int[] gintArrLines = new int[] { 0, 1, 2, -1, -1 };
        
        bool gboolGoLeft = false;
        bool gboolInitialAnimationComplete = false;
        bool gboolInvestChanged = false;
        bool gboolDecayChanged = false;

      

        bool gboolFirstChangeOcc = false;

        

        bool gboolInterrupt = false;

        double gdblXRate = .05;
        const int TIMERATEINMILIS = 90;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (IsFractionOkay())
            {
                btnStart.Enabled = false;
                DisableFields();
                DrawAllLines();
                ShowGuessButtons();
                GenerateGuessButtons();

            }
        }

        private void DisableFields()
        {
            nudS.Enabled = false;
            nudN.Enabled = false;
            nudDelta.Enabled = false;
        }

        private void EnableFields()
        {
            nudS.Enabled = true;
            nudN.Enabled = true;
            nudDelta.Enabled = true;
        }

        private void btnApplyChanges_Click(object sender, EventArgs e)
        {
            btnApplyChanges.Visible = false;
            double oldKStar = gdblKStar;
            ClearAllUpperLines();
            gdblK = 0;
            DrawAllLines();
            StartAnimation(oldKStar);
        }



        private void nudS_ValueChanged(object sender, EventArgs e)
        {
            //Console.WriteLine("its trying" + gboolInitialAnimationComplete);
            if (gboolInitialAnimationComplete)
            {
                //Console.WriteLine("Hellow it went thrugh");
                btnApplyChanges.Visible = true;
                gboolInvestChanged = true;
                gdblS = (double)nudS.Value;
                DrawNewLine(5);
            }
            
        }

        private void nudN_ValueChanged(object sender, EventArgs e)
        {
            if (gboolInitialAnimationComplete)
            {
                btnApplyChanges.Visible = true;
                gboolDecayChanged = true;
                gdblN = (double)nudN.Value;
                DrawNewLine(6);
            }
            
        }

        private void nudDelta_ValueChanged(object sender, EventArgs e)
        {
            if (gboolInitialAnimationComplete)
            {
                btnApplyChanges.Visible = true;
                gboolDecayChanged = true;
                gdblDelta = (double)nudDelta.Value;
                DrawNewLine(6);
            }
           
        }

        private void DrawAllLines()
        {
            gdblS = (double)nudS.Value;
            gdblN = (double)nudN.Value;
            gdblDelta = (double)nudDelta.Value;

            CalcKsandYs(gdblS, gdblN, gdblDelta);
            btnAnswer.Text = "K* = " + RoundTo3Decimals(gdblKStar)+"\n Y* = "+RoundTo3Decimals(gdblYStar);
            Console.WriteLine("K* = " + gdblKStar + "\n Y* = " + gdblYStar);

            // Calculate the initial window size
            if (gdblKStar < 1)
            {
                gdblMaxK = Math.Pow(gdblKStar, 2) + gdblKStar;
            }
            else
            {
                gdblMaxK = 2 * Math.Sqrt(gdblKStar) + gdblKStar;
            }
            chrtLines.ChartAreas[0].AxisX.Minimum = 0;
            chrtLines.ChartAreas[0].AxisX.Maximum = gdblMaxK;
            chrtLines.ChartAreas[0].AxisY.Minimum = 0;
            chrtLines.ChartAreas[0].AxisY.Maximum = Calcy(gdblMaxK);
            DrawLines();
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

        private void DrawNewLine(int seriesNumber)
        {
            chrtLines.Series[seriesNumber].Points.Clear();
            gdblTempX = 0;
            if(seriesNumber == 5)
            {
                while (gdblTempX < gdblMaxK)
                {
                    chrtLines.Series[5].Points.AddXY(gdblTempX, CalcInvest(Calcy(gdblTempX), gdblS));
                    gdblTempX += gdblXRate;
                }
            }else if( seriesNumber == 6)
            {
                while (gdblTempX < gdblMaxK)
                {
                    chrtLines.Series[6].Points.AddXY(gdblTempX, CalcDecay(gdblN, gdblDelta, gdblTempX));
                    gdblTempX += gdblXRate;
                }
            }
            
        }
       
        private void ShowGuessButtons()
        {
            btnOption1.Visible = true;
            btnOption2.Visible = true;
            btnOption3.Visible = true;
            btnOption4.Visible = true;
        }
        private void HideGuessButtons()
        {
            btnOption1.Visible = false;
            btnOption2.Visible = false;
            btnOption3.Visible = false;
            btnOption4.Visible = false;
        }
        private void ClearAllUpperLines()
        {
            for(int i = 0; i < 7; i++)
            {
                chrtLines.Series[i].Points.Clear();
            }
        }

        private void ClearLowerLines()
        {
            chrtC.Series[0].Points.Clear();
            chrtI.Series[0].Points.Clear();
            chrtY.Series[0].Points.Clear();
        }


        private static double RoundTo3Decimals(double num)
        {
            return Math.Round(num * 1000) / 1000d;
        }
        private void BtnSkip_Click(object sender, EventArgs e)
        {
            btnSkip.Enabled = false;
            gboolInterrupt = true;
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            /*
            btnReset.Enabled = false;
            ClearAllUpperLines();
            ClearLowerLines();
            aTimer.Dispose();
            nudS.Enabled = true;
            */
            Application.Restart();
        }

        private void GenerateGuessButtons()
        {
            btnOption1.Text = RandomDoubleBetween(0, gdblKStar).ToString();
            btnOption2.Text = RandomDoubleBetween(0, gdblKStar).ToString();

            btnOption3.Text = RandomDoubleBetween(gdblKStar + 0.01, gdblMaxK).ToString();
            btnOption4.Text = RandomDoubleBetween(gdblKStar + 0.01, gdblMaxK).ToString();

        }


        //https://stackoverflow.com/questions/1064901/random-number-between-2-double-numbers
        private double RandomDoubleBetween(double min, double max)
        {
            return rnd.NextDouble() * (max - min) + min;
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

        private void btnOption1_Click(object sender, EventArgs e)
        {
            if(btnOption1.BackColor != Color.Aqua)
            {
                gintButtonsPushed += 1;
                btnSkip.Enabled = true;
                btnOption1.BackColor = Color.Aqua;
                StartAnimation(double.Parse(btnOption1.Text));
                if(gintButtonsPushed == 4)
                {
                    ShowAnswerButton();
                }
            }
            else
            {
                btnSkip.Enabled = true;
                StartAnimation(double.Parse(btnOption1.Text));
            }
            
        }

        private void btnOption2_Click(object sender, EventArgs e)
        {

            if (btnOption2.BackColor != Color.Aqua)
            {
                gintButtonsPushed += 1;
                btnSkip.Enabled = true;
                btnOption2.BackColor = Color.Aqua;
                StartAnimation(double.Parse(btnOption2.Text));
                if (gintButtonsPushed == 4)
                {
                    ShowAnswerButton();
                }
            }
            else
            {
                btnSkip.Enabled = true;
                StartAnimation(double.Parse(btnOption2.Text));
            }
        }

        private void btnOption3_Click(object sender, EventArgs e)
        {
            if (btnOption3.BackColor != Color.Aqua)
            {
                gintButtonsPushed += 1;
                btnSkip.Enabled = true;
                btnOption3.BackColor = Color.Aqua;
                StartAnimation(double.Parse(btnOption3.Text));
                if (gintButtonsPushed == 4)
                {
                    ShowAnswerButton();
                }
            }
            else
            {
                btnSkip.Enabled = true;
                StartAnimation(double.Parse(btnOption3.Text));
            }
        }

        private void btnOption4_Click(object sender, EventArgs e)
        {
            if (btnOption4.BackColor != Color.Aqua)
            {
                gintButtonsPushed += 1;
                btnSkip.Enabled = true;
                btnOption4.BackColor = Color.Aqua;
                StartAnimation(double.Parse(btnOption4.Text));
                if (gintButtonsPushed == 4)
                {
                    ShowAnswerButton();
                }
            }
            else
            {
                btnSkip.Enabled = true;
                StartAnimation(double.Parse(btnOption4.Text));
            }
        }

        private void btnAnswer_Click(object sender, EventArgs e)
        {
            gboolInitialAnimationComplete = true;
            HideGuessButtons();
            btnAnswer.Enabled = false;
            EnableFields();
            
        }

        private void ShowAnswerButton()
        {
            btnAnswer.Visible = true;
        }
        private void BtnTestAnimation_Click(object sender, EventArgs e)
        {
            StartAnimation((double)nudTestAnimation.Value);
        }

        private void StartAnimation(double startK)
        {
            gdblK = startK;
            gdblY = Calcy(gdblK);
            gdblConsum = CalcConsum();
            gdblInvest = CalcInvest(gdblY, gdblS);
            gdblDK = CalcDeltaTimesK();
            gdblChangeK = CalcChangeOfK();
            gdblDecay = CalcDecay(gdblN, gdblDelta, gdblK);


            SetTimer(this);
        }
        private static void SetTimer(Form1 daFrm)
        {
            aTimer = new System.Timers.Timer(TIMERATEINMILIS);
            aTimer.Elapsed += daFrm.Animation;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
        delegate void ArgReturningVoidDelegate(object source, ElapsedEventArgs e);
        private void Animation(object source, ElapsedEventArgs e)
        {
            if (this.chrtLines.InvokeRequired)
            {
                ArgReturningVoidDelegate d = new ArgReturningVoidDelegate(Animation);
                try
                {
                    this.Invoke(d, new object[] { source, e });
                }
                catch (System.ObjectDisposedException)
                {

                }
            }
            else
            {
                if (gboolInterrupt)
                {
                    aTimer.Enabled = false;
                    gboolInterrupt = false;
                    while ((gdblChangeK > 0.001) || (gdblChangeK < -0.001))
                    {
                        chrtC.Series[0].Points.AddXY(gdblMiniGraphXVal, gdblConsum);
                        chrtI.Series[0].Points.AddXY(gdblMiniGraphXVal, gdblInvest);
                        chrtY.Series[0].Points.AddXY(gdblMiniGraphXVal, (gdblConsum + gdblInvest));

                        gdblK += gdblChangeK;

                        gdblMiniGraphXVal += Math.Abs(gdblChangeK);
                        gdblY = Calcy(gdblK);
                        gdblConsum = CalcConsum();
                        gdblInvest = CalcInvest(gdblY, gdblS);
                        gdblDK = CalcDeltaTimesK();
                        gdblChangeK = CalcChangeOfK();
                        gdblDecay = CalcDecay(gdblN, gdblDelta, gdblK);
                    }
                    chrtLines.Series[3].Points.Clear();
                    chrtLines.Series[4].Points.Clear();

                    chrtLines.Series[3].Points.AddXY(chrtLines.ChartAreas[0].AxisX.Minimum, gdblY);
                    chrtLines.Series[3].Points.AddXY(chrtLines.ChartAreas[0].AxisX.Maximum, gdblY);
                    chrtLines.Series[4].Points.AddXY(gdblK, chrtLines.ChartAreas[0].AxisY.Minimum);
                    chrtLines.Series[4].Points.AddXY(gdblK, chrtLines.ChartAreas[0].AxisY.Maximum);
                    
                }
                else
                {
                    if ((gdblChangeK > 0.001) || (gdblChangeK < -0.001))
                    {
                        /*
                        chrtLines.Series[0].Points.AddXY(gdblK, gdblY);
                        chrtLines.Series[1].Points.AddXY(gdblK, gdblInvest);
                        chrtLines.Series[2].Points.AddXY(gdblK, gdblDecay);
                        */
                        chrtLines.Series[3].Points.Clear();
                        chrtLines.Series[4].Points.Clear();

                        chrtLines.Series[3].Points.AddXY(chrtLines.ChartAreas[0].AxisX.Minimum, gdblY);
                        chrtLines.Series[3].Points.AddXY(chrtLines.ChartAreas[0].AxisX.Maximum, gdblY);
                        chrtLines.Series[4].Points.AddXY(gdblK, chrtLines.ChartAreas[0].AxisY.Minimum);
                        chrtLines.Series[4].Points.AddXY(gdblK, chrtLines.ChartAreas[0].AxisY.Maximum);

                        chrtC.Series[0].Points.AddXY(gdblMiniGraphXVal, gdblConsum);
                        chrtI.Series[0].Points.AddXY(gdblMiniGraphXVal, gdblInvest);
                        chrtY.Series[0].Points.AddXY(gdblMiniGraphXVal, (gdblConsum + gdblInvest));

                        gdblK += gdblChangeK;

                        gdblMiniGraphXVal += Math.Abs(gdblChangeK);
                        gdblY = Calcy(gdblK);
                        gdblConsum = CalcConsum();
                        gdblInvest = CalcInvest(gdblY, gdblS);
                        gdblDK = CalcDeltaTimesK();
                        gdblChangeK = CalcChangeOfK();
                        gdblDecay = CalcDecay(gdblN, gdblDelta, gdblK);
                    }
                    else
                    {
                        btnSkip.Enabled = false;
                        aTimer.Enabled = false;

                    }
                }
                
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
