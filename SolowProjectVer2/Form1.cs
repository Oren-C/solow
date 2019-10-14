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
using System.Windows.Forms.DataVisualization.Charting;

namespace SolowProjectVer2
{
    public partial class Form1 : Form
    {
        private static System.Timers.Timer aTimer, bTimer, cTimer;
        double gdblK, gdblY;
        double gdblKStar, gdblYStar;
        double gdblTempX, gdblMaxK, gdblMinK, gdblTempY, gdblTempInvest, gdblTempDecay;
        double gdblS, gdblN, gdblDelta;
        double gdblConsum, gdblInvest, gdblDK, gdblChangeK, gdblDecay;
        //double gdblMedianMiniGrph = 5;
        double gdblZmMaxX, gdblZmMinX, gdblZmMaxY, gdblZmMinY;
        double gdblCurMaxX, gdblCurMinX, gdblCurMaxY, gdblCurMinY;
        double gdblOldMaxX, gdblOldMaxY;
        double gdblScalar;
        double gdblOldKStar;



        Random rnd = new Random();

        double gdblMiniGraphXVal = 11.5;
        int gintNumerator, gintDenom;
        int gintButtonsPushed = 0;

        //int[] gintArrLines = new int[] { 0, 1, 2, -1, -1 };
        
        bool gboolGoLeft = false;
        bool gboolInitialAnimationComplete = false;
        bool gboolZoomAnimationComplete = false;
        bool gboolInvestChanged = false;
        bool gboolDecayChanged = false;
        //bool gboolAnimationNeeded = false;
        bool gboolLessThanAnimation = false;

      

        bool gboolFirstChangeOcc = false;

        

        bool gboolInterrupt = false;

        double gdblXRate = .05;
        double gdblMinXRate = .05, gdblMaxXRate = 0.5, gdblMinYRate = 0.5, gdblMaxYRate = 0.5;
        const int TIMERATEINMILIS = 90;
        public Form1()
        {
            InitializeComponent();
            //MessageBox.Show("Enter a value for the exponent on capital.Remember that our production function must exhibit constant returns to scale.Also, enter initial values for the saving rate, population growth rate, and rate of depreciation.");


        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (IsFractionOkay())
            {
                btnStart.Enabled = false;
                txtKNumerator.Enabled = false;
                txtKDenominator.Enabled = false;
                DisableFields();
                DrawAllLines();
                ShowGuessButtons();
                GenerateGuessButtons();
                chrtC.ChartAreas[0].AxisX.Minimum = 0;
                chrtC.ChartAreas[0].AxisX.Maximum = 15;
                chrtI.ChartAreas[0].AxisX.Minimum = 0;
                chrtI.ChartAreas[0].AxisX.Maximum = 15;
                chrtY.ChartAreas[0].AxisX.Minimum = 0;
                chrtY.ChartAreas[0].AxisX.Maximum = 15;
                MessageBox.Show("The four boxes you see represent potential choices for initial capital per worker. Click on a box and observe transition to steady state capital per worker. Repeat for each choice.");
            }
        }

       

        private void Zoom(double kvalue)
        {
            gdblOldMaxX = chrtLines.ChartAreas[0].AxisX.Maximum;
            gdblOldMaxY = chrtLines.ChartAreas[0].AxisY.Maximum;

            SetZoomConstraints(kvalue);
            
            // I need to get the average of the two points and set the min and max by that instead of them themselves.

            SetTimerB(this);

        }

        private void SetZoomConstraints(double kvalue)
        {
            double incValue;
            if (kvalue < 1)
            {
                incValue = (Math.Pow(kvalue, 3) / 4d) + kvalue;
            }
            else
            {
                incValue = (Math.Sqrt(kvalue) / 4d) + kvalue;
            }
            //chrtLines.ChartAreas[0].AxisX.Maximum = incValue;
            Console.WriteLine("the k value is " + kvalue);
            gdblZmMaxX = incValue;
            Console.WriteLine("the Max x is " + gdblZmMaxX);

            if (kvalue < 1)
            {
                incValue = kvalue - (Math.Pow(kvalue, 3) / 4d);
            }
            else
            {
                incValue = kvalue - (Math.Sqrt(kvalue) / 4d);
            }
            //chrtLines.ChartAreas[0].AxisX.Minimum = incValue;
            gdblZmMinX = incValue;
            Console.WriteLine("'The min x is " + gdblZmMinX);
            double locInvest = CalcInvest(Calcy(kvalue), gdblS);
            double locDecay = CalcDecay(gdblN, gdblDelta, kvalue);

            Console.WriteLine("The locInvest value is " + locInvest);
            Console.WriteLine("the locDecay value is " + locDecay);

            double avg = (locInvest + locDecay) / 2;
            Console.WriteLine("The avg " + avg);
            double dist = Math.Abs(locInvest - avg);
            Console.WriteLine("the dist is " + dist);
            if (avg < 1)
            {
                incValue = avg + locInvest + (Math.Pow(avg + dist, 3) / 4d);
            }
            else
            {
                //incValue = avg + locInvest + (Math.Sqrt(avg + dist) / 8d);
                incValue = avg + 2 * dist;
            }
            gdblZmMaxY = incValue;
            Console.WriteLine("The max y" + gdblZmMaxY);
            if (avg < 1)
            {
                incValue = avg - (locInvest + (Math.Pow(avg + dist, 3) / 4d));
            }
            else
            {
                //incValue = avg - (locInvest + (Math.Sqrt(dist + avg) / 4d));
                incValue = avg - 2 * dist;
            }
            gdblZmMinY = incValue;
            Console.WriteLine("the min y " + gdblZmMinY);
        }
        private static void SetTimerB(Form1 daFrm)
        {
            bTimer = new System.Timers.Timer(40);
            bTimer.Elapsed += daFrm.AnimateZoom;
            bTimer.AutoReset = true;
            bTimer.Enabled = true;
        }

        delegate void ArgReturningVoidDelegateB(object source, ElapsedEventArgs e);
        private void AnimateZoom(object source, ElapsedEventArgs e)
        {
            if (this.chrtLines.InvokeRequired)
            {
                //Console.WriteLine("Invoke required");
                ArgReturningVoidDelegateB p = new ArgReturningVoidDelegateB(AnimateZoom);
                try
                {
                    this.Invoke(p, new object[] { source, e });
                }
                catch (System.ObjectDisposedException)
                {

                }
            }
            else
            {
                //Console.WriteLine("Invoke not required");
                if (!gboolInterrupt)
                {
                    gdblCurMaxX = chrtLines.ChartAreas[0].AxisX.Maximum;
                    gdblCurMinX = chrtLines.ChartAreas[0].AxisX.Minimum;
                    gdblCurMaxY = chrtLines.ChartAreas[0].AxisY.Maximum;
                    gdblCurMinY = chrtLines.ChartAreas[0].AxisY.Minimum;
                    //Console.WriteLine("MaxX: " + gdblCurMaxX);
                    //Console.WriteLine("MaxY: " + gdblCurMaxY);
                    //Console.WriteLine("MinX: " + gdblCurMinX);
                    //Console.WriteLine("MinY: " + gdblCurMinY);
                    //Console.WriteLine((gdblCurMaxX - gdblZmMaxX) > (gdblZmMinX - gdblCurMinX));
                    //Console.WriteLine("Is the problem here maybe");
                    if((gdblCurMaxX - gdblZmMaxX) > 5 || (gdblZmMinX - gdblCurMinX)>5)
                    {
                        gdblScalar = 12;
                    }
                    else
                    {
                        gdblScalar = 1;
                    }
                    if(Math.Abs((gdblCurMaxX - gdblZmMaxX) - (gdblZmMinX - gdblCurMinX))< 0.05*gdblScalar)
                    {
                        //Console.WriteLine("Both Similar");
                        gdblMaxXRate = 0.05*gdblScalar;
                        gdblMinXRate = 0.05*gdblScalar;
                    }
                    else if((gdblCurMaxX - gdblZmMaxX) > (gdblZmMinX - gdblCurMinX))
                    {
                        //Console.WriteLine("the second option");
                        
                        gdblMaxXRate = 0.06*gdblScalar;
                        gdblMinXRate = 0.0008*gdblScalar;
                        
                    }
                    else
                    {
                        //Console.WriteLine("the thrid option");
                        gdblMaxXRate = 0.0008*gdblScalar;
                        gdblMinXRate = 0.06*gdblScalar;
                    }

                    if(Math.Abs((gdblCurMaxY - gdblZmMaxX) - (gdblZmMinX - gdblCurMinX))< 0.05)
                    {
                        gdblMaxYRate = 0.05;
                        gdblMinYRate = 0.05;
                    }else if((gdblCurMaxY - gdblZmMaxY) > (gdblZmMinY - gdblCurMinY))
                    {
                        gdblMaxYRate = 0.06;
                        gdblMinYRate = 0.0008;
                    }
                    else
                    {
                        gdblMaxYRate = 0.0008;
                        gdblMinYRate = 0.06;
                    }

                    //Console.WriteLine("the timer works");
                    if (gdblCurMaxX > gdblZmMaxX + gdblMaxXRate || gdblCurMaxY > gdblZmMaxY + gdblMaxYRate || gdblCurMinX < gdblZmMinX - gdblMinXRate || gdblCurMinY < gdblZmMinY - gdblMinYRate)
                    {
                        try {
                            if (gdblCurMaxX > gdblZmMaxX + gdblMaxXRate)
                            {
                                //Console.WriteLine("changing max x rate is "+ gdblMaxXRate);
                                chrtLines.ChartAreas[0].AxisX.Maximum -= gdblMaxXRate;
                                //Console.WriteLine("changing max x after" + chrtLines.ChartAreas[0].AxisX.Maximum + "here is the zm max x" + gdblZmMaxX);
                            }
                            if (gdblCurMaxY > gdblZmMaxY + gdblMaxYRate)
                            {
                                //Console.WriteLine("Changing max y rate is "+ gdblMaxYRate);
                                chrtLines.ChartAreas[0].AxisY.Maximum -= gdblMaxYRate;
                                //Console.WriteLine("Changing max y after"+ chrtLines.ChartAreas[0].AxisY.Maximum + "here is the zm max y" + gdblZmMaxY);
                            }
                            if (gdblCurMinX < gdblZmMinX - gdblMinXRate)
                            {
                               //Console.WriteLine("Changing min x rate is "+ gdblMinXRate);
                                chrtLines.ChartAreas[0].AxisX.Minimum += gdblMinXRate;
                               // Console.WriteLine("Changing min x after"+ chrtLines.ChartAreas[0].AxisX.Minimum + "here is the zm min x" + gdblZmMinX);
                            }
                            if (gdblCurMinY < gdblZmMinY - gdblMinYRate)
                            {
                                //Console.WriteLine("Changing min y rate is "+ gdblMinYRate);
                                chrtLines.ChartAreas[0].AxisY.Minimum += gdblMinYRate;
                                //Console.WriteLine("Changing min y after"+ chrtLines.ChartAreas[0].AxisY.Minimum + "here is the zm min y" + gdblZmMinY);
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine(gdblCurMaxX+" "+gdblCurMaxY + " " + gdblCurMinX + " " + gdblCurMinY);
                        }
                        
                    }
                    else
                    {
                        bTimer.Enabled = false;
                        int mid = AddLabels(gdblZmMinX, gdblZmMaxX);
                        if (gdblK > gdblKStar)
                        {
                            MessageBox.Show("Notice at the k selected, new investment in the economy falls below that which is required to break even.  Therefore, capital per worker will decrease as the economy transitions to steady-state.");
                        }
                        else
                        {
                            MessageBox.Show("Notice at the k selected, new investment in the economy exceeds that which is required to break even.  Therefore, capital per worker will increase as the economy transitions to steady-state.");
                        }
                        //Zooms out 
                        RemoveLabels(mid);
                        chrtLines.ChartAreas[0].AxisX.Maximum = gdblOldMaxX;
                        chrtLines.ChartAreas[0].AxisX.Minimum = 0;
                        chrtLines.ChartAreas[0].AxisY.Maximum = gdblOldMaxY;
                        chrtLines.ChartAreas[0].AxisY.Minimum = 0;
                        Console.WriteLine("Hellppp mee pleeeassee");
                        //Should add a wait but it happens before the zoom out
                        //DoNothing(3000);



                        //System.Threading.Thread.Sleep(500);
                        // Edits need to happen here
                        // Set gdblk to 0
                        // Call draw all lines
                        // The b timer is stopped and then the a timer should be formally started
                        //gboolZoomAnimationComplete = true;
                        //aTimer.Enabled = true;
                        SetDelay(this, 2000);
                    }
                }
                else // Skip has been pressed
                {
                    bTimer.Enabled = false;
                    chrtLines.ChartAreas[0].AxisX.Maximum = gdblOldMaxX;
                    chrtLines.ChartAreas[0].AxisX.Minimum = 0;
                    chrtLines.ChartAreas[0].AxisY.Maximum = gdblOldMaxY;
                    chrtLines.ChartAreas[0].AxisY.Minimum = 0;
                    // Here is where edits need to happen
                    // Set gdblk to 0
                    // Call draw all lines
                    // The b timer should stop and then the a timer should be formally started
                    gboolZoomAnimationComplete = true;
                    aTimer.Enabled = true;
                }
                


            }
        }

        private static void SetDelay(Form1 daFrm, int waitTime)
        {
            cTimer = new System.Timers.Timer(waitTime);
            cTimer.Elapsed += daFrm.AfterWait;
            cTimer.AutoReset = false;
            cTimer.Enabled = true;
        }

        delegate void ArgReturningVoidDelegatec(object source, ElapsedEventArgs e);
        private void AfterWait(object source, ElapsedEventArgs e)
        {
            if (this.chrtLines.InvokeRequired)
            {
                ArgReturningVoidDelegatec d = new ArgReturningVoidDelegatec(AfterWait);
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
                Console.WriteLine("it executed afte timer");
                gboolZoomAnimationComplete = true;
                aTimer.Enabled = true;

            }
        }


                private void DoNothing(int miliSecs)
        {
            System.Threading.Thread.Sleep(miliSecs);
        }
        private void btnApplyChanges_Click(object sender, EventArgs e)
        {
            //gboolAnimationNeeded = true;
            btnApplyChanges.Visible = false;
            DisableFields();
            double oldKStar = gdblKStar;
            //gdblOldKStar = gdblKStar;

            // this will delete all the lines this is where the problem lies as we are adding multiple new lines and
            // by deleting them and redrawing them we are changing the max I could keep the current setup and add some checks in DrawAllLines possibly
            ClearAllUpperLines();
            gdblK = 0;
            //I need to zoom before I draw all !Wait maybe not

            gdblS = (double)nudS.Value;
            gdblN = (double)nudN.Value;
            gdblDelta = (double)nudDelta.Value;

            CalcKsandYs(gdblS, gdblN, gdblDelta);
            if (oldKStar < gdblKStar)
            {
                DrawAllLines();
                btnSkip.Enabled = true;
                StartAnimation(oldKStar);
            }
            else
            {
                DrawLines();
                gboolLessThanAnimation = true;
                btnSkip.Enabled = true;
                StartAnimation(oldKStar);
            }

            
        }



        

        //All this does is calculates the min and max of the axis and then calls the draw lines function
        private void DrawAllLines()
        {
            gdblS = (double)nudS.Value;
            gdblN = (double)nudN.Value;
            gdblDelta = (double)nudDelta.Value;

            CalcKsandYs(gdblS, gdblN, gdblDelta);
            btnAnswer.Text = "k* = " + RoundTo3Decimals(gdblKStar)+"\n y* = "+RoundTo3Decimals(gdblYStar);
            Console.WriteLine("k* = " + gdblKStar + "\n y* = " + gdblYStar);

            // Calculate the initial window size
            //If zoom needed do something 
            // Else do this 

            if (gdblKStar < 1)
            {
                // maybe the problem is here
                gdblMaxK = Math.Pow(gdblKStar, 2) + gdblKStar;
            }
            else
            {
                gdblMaxK = 2 * Math.Sqrt(gdblKStar) + gdblKStar;
            }

            // I need to put some kind of check here but then I would also need to know to go back to these values later
            // I could change the zoom somehow
            chrtLines.ChartAreas[0].AxisX.Minimum = 0;
            chrtLines.ChartAreas[0].AxisX.Maximum = gdblMaxK;
            chrtLines.ChartAreas[0].AxisY.Minimum = 0;
            chrtLines.ChartAreas[0].AxisY.Maximum = Calcy(gdblMaxK);

            
            DrawLines();
        }

        private void CalcMaxK()
        {
            if (gdblKStar < 1)
            {
                // maybe the problem is here
                gdblMaxK = Math.Pow(gdblKStar, 2) + gdblKStar;
            }
            else
            {
                gdblMaxK = 2 * Math.Sqrt(gdblKStar) + gdblKStar;
            }
        }

        private void DrawLines()
        {
            //double orgK = gdblK;
            while (gdblK < gdblMaxK)
            {
                chrtLines.Series[0].Points.AddXY(gdblK, Calcy(gdblK));
                chrtLines.Series[1].Points.AddXY(gdblK, CalcInvest(Calcy(gdblK), gdblS));
                chrtLines.Series[2].Points.AddXY(gdblK, CalcDecay(gdblN, gdblDelta, gdblK));
                gdblK += gdblXRate;
            }
            //AddLabels(orgK, gdblK);
            
            
        }

        private int AddLabels(double min, double max)
        {
            Console.WriteLine("hey it activates");
            System.Windows.Forms.DataVisualization.Charting.DataPoint[] bigArr;
            //double[] bigArr;
            bigArr = chrtLines.Series[0].Points.ToArray();
            int mid = SearchDataArray(bigArr, 0, bigArr.Length - 1, (min + max) / 2);
            Console.WriteLine(mid);
            if(mid == -1)
            {
                Console.WriteLine("Not found");
            }
            chrtLines.Series[0].Points[mid].Label = "PerCapita";
            chrtLines.Series[1].Points[mid].Label = "Invest";
            chrtLines.Series[2].Points[mid].Label = "Break Even";
            Console.WriteLine("The y value of capita is {0}", chrtLines.Series[0].Points[mid].YValues[0]);
            Console.WriteLine("The y value of invest is {0}", chrtLines.Series[1].Points[mid].YValues[0]);
            Console.WriteLine("The y value of decay is {0}", chrtLines.Series[2].Points[mid].YValues[0]);

            return mid;
        }

        private void RemoveLabels(int mid)
        {
            chrtLines.Series[0].Points[mid].Label = "";
            chrtLines.Series[1].Points[mid].Label = "";
            chrtLines.Series[2].Points[mid].Label = "";
        }


        private int SearchDataArray(System.Windows.Forms.DataVisualization.Charting.DataPoint[] arr, int l, int r, double x)
        {
            if (r >= l)
            {
                int mid = l + (r - l) / 2;

                if ((x - 0.05) <= arr[mid].XValue && arr[mid].XValue <= (x + 0.05))
                {
                    return mid;
                }

                if (arr[mid].XValue > x)
                {
                    return SearchDataArray(arr, l, mid - 1, x);
                }

                return SearchDataArray(arr, mid + 1, r, x);
            }
            

            return -1;
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

        private void DisableButtons()
        {
            btnOption1.Enabled = false;
            btnOption2.Enabled = false;
            btnOption3.Enabled = false;
            btnOption4.Enabled = false;

            btnAnswer.Enabled = false;
        }
        private void EnableButtons()
        {
            btnOption1.Enabled = true;
            btnOption2.Enabled = true;
            btnOption3.Enabled = true;
            btnOption4.Enabled = true;

            btnAnswer.Enabled = true;
        }
        private void ClearAllUpperLines()
        {
            for(int i = 0; i < 7; i++)
            {
                chrtLines.Series[i].Points.Clear();
            }
        }

        private void ClearCertainLines()
        {
            for (int i = 0; i < 7; i++)
            {
                if(i != 3 && i != 4)
                {
                    chrtLines.Series[i].Points.Clear();
                }
                
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
            //EnableFields();
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
            SetZoomConstraints(gdblKStar);
            btnOption1.Text = RoundTo3Decimals(RandomDoubleBetween(0, gdblZmMinX)).ToString();
            btnOption2.Text = RoundTo3Decimals(RandomDoubleBetween(0, gdblZmMinX)).ToString();

            btnOption3.Text = RoundTo3Decimals(RandomDoubleBetween(gdblZmMaxX, gdblMaxK)).ToString();
            btnOption4.Text = RoundTo3Decimals(RandomDoubleBetween(gdblZmMaxX, gdblMaxK)).ToString();

        }


        //https://stackoverflow.com/questions/1064901/random-number-between-2-double-numbers
        private double RandomDoubleBetween(double min, double max)
        {
            return rnd.NextDouble() * (max - min) + min;
        }

        private void BtnClearLablels_Click(object sender, EventArgs e)
        {
            chrtLines.Series[0].Label = "";
            chrtLines.Series[1].Label = "";
            chrtLines.Series[2].Label = "";

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            MessageBox.Show("Enter a value for the exponent on capital.Remember that our production function must exhibit constant returns to scale.Also, enter initial values for the saving rate, population growth rate, and rate of depreciation.");
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

        private void btnOption1_Click(object sender, EventArgs e)
        {
            DisableButtons();
            //gboolAnimationNeeded = true;
            if(btnOption1.BackColor != Color.Aqua)
            {
                gintButtonsPushed += 1;
                //DisableButtons();
                btnSkip.Enabled = true;
                btnOption1.BackColor = Color.Aqua;
                StartAnimation(double.Parse(btnOption1.Text));
                if (gintButtonsPushed == 4)
                {
                    ShowAnswerButton();
                }
            }
            else
            {
                if (gintButtonsPushed >= 4)
                {
                    gintButtonsPushed++;
                    //ShowAnswerButton();
                }
                btnSkip.Enabled = true;
                StartAnimation(double.Parse(btnOption1.Text));
                
            }
            
        }

        private void btnOption2_Click(object sender, EventArgs e)
        {
            DisableButtons();
            //gboolAnimationNeeded = true;
            if (btnOption2.BackColor != Color.Aqua)
            {
                gintButtonsPushed += 1;
                //DisableButtons();
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
                if (gintButtonsPushed >= 4)
                {
                    gintButtonsPushed++;
                    //ShowAnswerButton();
                }
                btnSkip.Enabled = true;
                StartAnimation(double.Parse(btnOption2.Text));
            }
        }

        private void btnOption3_Click(object sender, EventArgs e)
        {
            //gboolAnimationNeeded = true;
            if (btnOption3.BackColor != Color.Aqua)
            {
                DisableButtons();
                gintButtonsPushed += 1;
                //DisableButtons();
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
                if (gintButtonsPushed >= 4)
                {
                    gintButtonsPushed++;
                    //ShowAnswerButton();
                }
                btnSkip.Enabled = true;
                StartAnimation(double.Parse(btnOption3.Text));
            }
        }

        private void btnOption4_Click(object sender, EventArgs e)
        {
            DisableButtons();
            //gboolAnimationNeeded = true;
            if (btnOption4.BackColor != Color.Aqua)
            {
                gintButtonsPushed += 1;
                //DisableButtons();
                btnSkip.Enabled = true;
                btnOption4.BackColor = Color.Aqua;
                StartAnimation(double.Parse(btnOption4.Text));
                if(gintButtonsPushed == 4)
                {
                    ShowAnswerButton();
                }
            }
            else
            {
                if (gintButtonsPushed >= 4)
                {
                    gintButtonsPushed++;
                    //ShowAnswerButton();
                }
                btnSkip.Enabled = true;
                StartAnimation(double.Parse(btnOption4.Text));
            }
        }

        private void btnAnswer_Click(object sender, EventArgs e)
        {
            gboolInitialAnimationComplete = true;
            gintButtonsPushed = 0;
            HideGuessButtons();
            btnAnswer.Enabled = false;
            EnableFields();
            MessageBox.Show("“Please make changes to the saving rate, population growth rate, and depreciation rate. Notice that the relevant functions will shift because of the changes you make. Think about how those changes will affect the steady state capital per worker and income per worker. Click “Apply Changes” and see if your intuition is correct. You may do this as many time as you would like.");
            DataPoint[] lastArr = new DataPoint[3];
            lastArr[0] = chrtC.Series[0].Points.Last();
            lastArr[1] = chrtI.Series[0].Points.Last();
            lastArr[2] = chrtY.Series[0].Points.Last();
            chrtC.Series[0].Points.Clear();
            chrtI.Series[0].Points.Clear();
            chrtY.Series[0].Points.Clear();

            chrtC.Series[0].Points.AddXY(chrtC.ChartAreas[0].AxisX.Minimum, lastArr[0].YValues[0]);
            chrtC.Series[0].Points.AddXY(lastArr[0].XValue, lastArr[0].YValues[0]);

            chrtI.Series[0].Points.AddXY(chrtI.ChartAreas[0].AxisX.Minimum, lastArr[1].YValues[0]);
            chrtI.Series[0].Points.AddXY(lastArr[1].XValue, lastArr[1].YValues[0]);

            chrtY.Series[0].Points.AddXY(chrtY.ChartAreas[0].AxisX.Minimum, lastArr[2].YValues[0]);
            chrtY.Series[0].Points.AddXY(lastArr[2].XValue, lastArr[2].YValues[0]);

            chrtC.Visible = true;
            chrtI.Visible = true;
            chrtY.Visible = true;
        }

        private void ShowAnswerButton()
        {
            btnAnswer.Visible = true;
            //MessageBox.Show("The middle box displays steady state capital per worker and income per worker for the production function you selected and the values for the saving rate, population growth rate, and depreciation rate you selected. Click on the box.");
        }
        

        private void StartAnimation(double startK)
        {
            //Console.WriteLine("Before Zoom");
            //Zoom(startK);
            //Console.WriteLine("After Zoom");
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
                //Checks if the skip button has been pressed
                if (gboolInterrupt)
                {
                    aTimer.Enabled = false;
                    gboolInterrupt = false;
                    gboolZoomAnimationComplete = false;
                    while ((gdblChangeK > 0.001) || (gdblChangeK < -0.001))
                    {
                        chrtC.Series[0].Points.AddXY(gdblMiniGraphXVal, gdblConsum);
                        chrtI.Series[0].Points.AddXY(gdblMiniGraphXVal, gdblInvest);
                        chrtY.Series[0].Points.AddXY(gdblMiniGraphXVal, (gdblConsum + gdblInvest));

                        gdblK += gdblChangeK;

                        gdblMiniGraphXVal += Math.Abs(gdblChangeK);
                        IncreaseSubgraphs(Math.Abs(gdblChangeK));
                        //gdblMedianMiniGrph = (chrtC.ChartAreas[0].AxisX.Minimum + chrtC.ChartAreas[0].AxisX.Maximum) / 2d;
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
                    if (gboolLessThanAnimation)
                    {
                        ClearCertainLines();
                        gdblK = 0;
                        DrawAllLines();
                        gboolLessThanAnimation = false;
                    }

                    if (gboolInitialAnimationComplete)
                    {
                        EnableFields();

                    }
                    else
                    {
                        EnableButtons();
                    }

                    if (gintButtonsPushed == 4)
                    {
                        MessageBox.Show("The middle box displays steady state capital per worker and income per worker for the production function you selected and the values for the saving rate, population growth rate, and depreciation rate you selected. Click on the box.");
                    }
                }// Else the skip button has not been pressed
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
                        //Console.WriteLine("C: " + gdblConsum);
                        chrtI.Series[0].Points.AddXY(gdblMiniGraphXVal, gdblInvest);
                        //Console.WriteLine("I: " + gdblInvest);
                        chrtY.Series[0].Points.AddXY(gdblMiniGraphXVal, (gdblConsum + gdblInvest));
                        //Console.WriteLine("Y: " + (gdblConsum + gdblInvest));
                        if (!gboolZoomAnimationComplete)
                        {
                            aTimer.Enabled = false;
                            Zoom(gdblK);
                        }
                        gdblK += gdblChangeK;

                        gdblMiniGraphXVal += Math.Abs(gdblChangeK);
                        //Console.WriteLine("The value of gdblminigraphXval is " + gdblMiniGraphXVal);
                        IncreaseSubgraphs(Math.Abs(gdblChangeK));
                        //gdblMedianMiniGrph = (chrtC.ChartAreas[0].AxisX.Minimum + chrtC.ChartAreas[0].AxisX.Maximum) / 2d;
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
                        gboolZoomAnimationComplete = false;
                        if (gboolLessThanAnimation)
                        {
                            ClearCertainLines();
                            gdblK = 0;
                            DrawAllLines();
                            gboolLessThanAnimation = false;
                        }
                        if (gboolInitialAnimationComplete)
                        {
                            EnableFields();

                        }
                        else
                        {
                            EnableButtons();
                        }
                        Console.WriteLine($"Here the value of buttons pushed is {gintButtonsPushed}");
                        if (gintButtonsPushed == 4)
                        {
                            MessageBox.Show("The middle box displays steady state capital per worker and income per worker for the production function you selected and the values for the saving rate, population growth rate, and depreciation rate you selected. Click on the box.");
                        }
                    }
                }

                
            }
        }

        private void IncreaseSubgraphs(double increment)
        {
            chrtC.ChartAreas[0].AxisX.Minimum += increment;
            chrtC.ChartAreas[0].AxisX.Maximum += increment;

            chrtI.ChartAreas[0].AxisX.Minimum += increment;
            chrtI.ChartAreas[0].AxisX.Maximum += increment;

            chrtY.ChartAreas[0].AxisX.Minimum += increment;
            chrtY.ChartAreas[0].AxisX.Maximum += increment;
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
