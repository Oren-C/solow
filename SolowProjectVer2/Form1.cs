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
using System.Threading;



namespace SolowProjectVer2
{
    public partial class Form1 : Form
    {
        //Globals
        private static System.Timers.Timer cTimer;

        int gintMid;
        int gintButtonsPushed = 0;

        Random rnd = new Random();
      
        bool gboolInitialAnimationComplete = false;
        bool gboolApplyChanges = false;
        bool gboolLessThanAnimation = false;
        bool gboolGuessButPushed = false;

        ManualResetEvent gMrse, gApplyMrse;

        readonly Color butColor = Color.Green;

        private Thread t;
        private Thread altThread;


        public Form1()
        {
            InitializeComponent();
            
        }

        //This formats the labels on the chart to only show 3 decimal places
        private void Form1_Load(object sender, EventArgs e)
        {
            chrtLines.ChartAreas[0].AxisY.LabelStyle.Format = "{#####.###}";
            chrtLines.ChartAreas[0].AxisX.LabelStyle.Format = "{#####.###}";
            lblMsgbox.Text = "Enter a value for the exponent on capital.Remember that our production function must exhibit constant returns to scale.Also, enter initial values for the saving rate and population growth rate.";

        }

        //To prevent the application being closed without the threads being killed
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(t != null)
            {
                if (t.IsAlive)
                {
                    t.Abort();
                }
            }

            if(altThread != null)
            {
                if (altThread.IsAlive)
                {
                    altThread.Abort();
                }
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            //Checks if the fraction that is currently entered is okay see that function (IsFractionOkay) it also sets some values
            if (IsFractionOkay())
            {

                TheChart chrt = TheChart.GetInstance();


                chrt.gdblN = (double)nudN.Value;
                chrt.gdblS = (double)nudS.Value;
                chrt.gdblDelta = 0.1;

                btnStart.Visible = false;
                txtKNumerator.Enabled = false;
                txtKDenominator.Enabled = false;
                DisableFields();

                t = new Thread(() => chrt.DrawAllLines(this, setAnswerText, btnAnswer.Enabled, setMaxAndMinBounds, MultiThreadDraw));
                t.Start();

                //Update UI
                ShowGuessButtons();
                btnOption1.Select();
                chrtC.ChartAreas[0].AxisX.Minimum = 0;
                chrtC.ChartAreas[0].AxisX.Maximum = 15;
                chrtI.ChartAreas[0].AxisX.Minimum = 0;
                chrtI.ChartAreas[0].AxisX.Maximum = 15;
                chrtY.ChartAreas[0].AxisX.Minimum = 0;
                chrtY.ChartAreas[0].AxisX.Maximum = 15;

                //Generates the buttons 
                altThread = new Thread(() => chrt.GenerateGuessButtons(this, PrintGuessButttons));
                altThread.Start();

                lblMsgbox.Text = "The four boxes you see represent potential choices for initial capital per worker. Click on a box and observe transition to steady state capital per worker. Repeat for each choice. Notice that each choice is not steady state capital per worker.";
            }
        }




       

        //Makes a timer that waits for the specified time and does not repeat
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
                
                if (gboolApplyChanges)
                {
                    //If it is in phase 2 of the program then this section will start when apply changes button is pressed
                    gboolApplyChanges = false;

                    RemoveLabels(gintMid);
                    ClearAllUpperLines();

                    //ApplyChanges();

                    //For multi use
                    gApplyMrse.Set();

                }
                else
                {
                    //This section is when one of the guess buttons are clicked
                    RemoveLabels(gintMid);
                    btnSkip.Visible = true;
                    btnSkip.Select();
                    //For multi use
                    gMrse.Set();
                }
                

            }
        }


        private void btnApplyChanges_Click(object sender, EventArgs e)
        {
            btnApplyChanges.Visible = false;
            DisableFields();


            //This is to make sure that the thread has compeleted its work
            if (t.IsAlive)
            {
                t.Join();
            }


            t = new Thread(() => TheChart.GetInstance().ApplyChange(this, ApplyChangesMessage, (double)nudS.Value, (double)nudN.Value ,setAnswerText, btnAnswer.Enabled, setMaxAndMinBounds, MultiThreadDraw, chrtLines.ChartAreas[0].AxisX.Maximum,FinishApplyChanges));
            t.Start();
        }

        // Determines what message to be displayed sets the mrse and makes the ok button visible.
        private void ApplyChangesMessage(double n, double oldN, double s, double oldS, double kStar, double oldKStar, ManualResetEvent mrse)
        {
            gApplyMrse = mrse;
            string message;
            if (s > oldS)
            {

                if (n> oldN)
                {
                    //Option 7
                    if (kStar > oldKStar)
                    {
                        message = "You have increased the saving rate and increased the population growth rate.  As a result, investment now exceeds its break-even point, and the economy will transition to a new steady state";

                    }
                    else if (kStar < oldKStar)
                    {
                        message = "You have increased the saving rate and increased the population growth rate.  As a result, investment is now less than its break-even point, and the economy will transition to a new steady state.";
                    }
                    else
                    {
                        message = "Error nothing changed";
                    }


                }
                else if (n < oldN)
                {
                    //Option 5
                    message = "You have increased the saving rate and decreased the population growth rate.  As a result, investment now exceeds its break-even point, and the economy will transition to a new steady state.";


                }
                else
                {
                    //Option 1
                    message = "You have increased the saving rate and left the population growth rate unchanged.  As a result, investment now exceeds its break-even point, and the economy will transition to a new steady state.";


                }



            }
            else if (s < oldS)
            {
                if (n > oldN)
                {
                    //Option 6
                    message = "You have decreased the saving rate and increased the population growth rate.  As a result, investment is now less than its break-even point, and the economy will transition to a new steady state.";



                }
                else if (n < oldN)
                {
                    //Option 8

                    if (kStar > oldKStar)
                    {
                        message = "You have decreased the saving rate and decreased the population growth rate.  As a result, investment now exceeds its break-even point, and the economy will transition to a new steady state";

                    }
                    else if (kStar < oldKStar)
                    {
                        message = "You have decreased the saving rate and decreased the population growth rate.  As a result, investment is now less than its break-even point, and the economy will transition to a new steady state.";
                    }
                    else
                    {
                        message = "Error nothing changed";
                    }
                }
                else
                {
                    //Option 2
                    message = "You have decreased the saving rate and left the population growth rate unchanged.  As a result, investment is now less than its break-even point, and the economy will transition to a new steady state.";


                }
            }
            else if (n > oldN)
            {
                // Option 3
                message = "You have left the saving rate unchanged and increased the population growth rate.  As a result, investment is now less than its break-even point, and the economy will transition to a new steady state.";

            }
            else if (n < oldN)
            {
                //Option 4
                message = "You have left the saving rate unchanged and increased the population growth rate.  As a result, investment now exceeds its break-even point, and the economy will transition to a new steady state.";

            }
            else
            {
                //No changes made
                message = "You did not make a change";
            }
            lblMsgbox.Text = message;




            //gintMid = AddLabels(chrtLines.ChartAreas[0].AxisX.Maximum / 2, chrtLines.ChartAreas[0].AxisX.Maximum);
            //gintMid = AddLabels((chrtLines.ChartAreas[0].AxisX.Maximum / 2 + chrtLines.ChartAreas[0].AxisX.Maximum) / 2); don't use
            gintMid = MakeLabelsAuto(oldKStar);
            gboolApplyChanges = true;
            btnOk.Visible = true;
            btnOk.Select();
        }

        //For multi use
        private void FinishApplyChanges(bool lessThanAnimation, double oldKStar)
        {
            gboolLessThanAnimation = lessThanAnimation;

            //btnSkip.Enabled = true;
            btnSkip.Visible = true;
            btnSkip.Select();
            TheChart chrt = TheChart.GetInstance();
            t = new Thread(() => chrt.Animation(this, DrawPoint, oldKStar, IncreaseSubgraphs, DrawSubGraphs, FinishAnimation, gboolGuessButPushed, StartMessageConfirm));
            t.Start();

        }


        //For multi use
        private void setAnswerText(string s)
        {
            btnAnswer.Text = s;
        }


        

        //for multi use
        private void MultiThreadDraw(double x, double capita, double invest, double brkEven)
        {
            chrtLines.Series[0].Points.AddXY(x, capita);
            chrtLines.Series[1].Points.AddXY(x, invest);
            chrtLines.Series[2].Points.AddXY(x, brkEven);
        }

        //For multi use
        private void setMaxAndMinBounds(double maxK, double maxY)
        {
            chrtLines.ChartAreas[0].AxisX.Minimum = 0;
            chrtLines.ChartAreas[0].AxisX.Maximum = maxK;
            chrtLines.ChartAreas[0].AxisY.Minimum = 0;
            chrtLines.ChartAreas[0].AxisY.Maximum = maxY;
        }

        private int MakeLabelsAuto(double k)
        {

            return AddLabels((chrtLines.ChartAreas[0].AxisX.Maximum / 2 + chrtLines.ChartAreas[0].AxisX.Maximum)/2, chrtLines.ChartAreas[0].AxisX.Maximum);
        }
        //Add the labels at the middle of the two min and max x values
        private int AddLabels(double min, double max)
        {

            System.Windows.Forms.DataVisualization.Charting.DataPoint[] bigArr;
            bigArr = chrtLines.Series[0].Points.ToArray();
            int mid = SearchDataArray(bigArr, 0, bigArr.Length - 1, (min + max) / 2);

            chrtLines.Series[0].Points[mid].Label = "PerCapita";

            if (chrtLines.Series[5].Points.Any())
            {
                chrtLines.Series[5].Points[mid].Label = "New Invest";
            }
            else
            {
                chrtLines.Series[1].Points[mid].Label = "Invest";
            }

            if (chrtLines.Series[6].Points.Any())
            {
                chrtLines.Series[6].Points[mid].Label = "New Break Even";
            }
            else
            {
                chrtLines.Series[2].Points[mid].Label = "Break Even";
            }
            


            return mid;
        }

        //Add the labels at that x value
        private int AddLabels(double value)
        {
            //Console.WriteLine("hey it activates");
            System.Windows.Forms.DataVisualization.Charting.DataPoint[] bigArr;
            //double[] bigArr;
            bigArr = chrtLines.Series[0].Points.ToArray();
            int mid = SearchDataArray(bigArr, 0, bigArr.Length - 1, value);
            //Console.WriteLine(mid);
            if (mid == -1)
            {
                //Console.WriteLine("Not found");
            }
            chrtLines.Series[0].Points[mid].Label = "PerCapita";

            if (chrtLines.Series[5].Points.Any())
            {
                chrtLines.Series[5].Points[mid].Label = "New Invest";
            }
            else
            {
                chrtLines.Series[1].Points[mid].Label = "Invest";
            }

            if (chrtLines.Series[6].Points.Any())
            {
                chrtLines.Series[6].Points[mid].Label = "New Break Even";
            }
            else
            {
                chrtLines.Series[2].Points[mid].Label = "Break Even";
            }


            return mid;
        }

        //This requires the x intger where the lables are currently at
        private void RemoveLabels(int mid)
        {
            chrtLines.Series[0].Points[mid].Label = "";
            chrtLines.Series[1].Points[mid].Label = "";
            chrtLines.Series[2].Points[mid].Label = "";
            if (chrtLines.Series[5].Points.Any())
            {
                chrtLines.Series[5].Points[mid].Label = "";
            }
            if (chrtLines.Series[6].Points.Any())
            {
                chrtLines.Series[6].Points[mid].Label = "";
            }
                
        }

        //Recursively finds the closest approximation of double x in the DataPoint Array
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



        //For multi
        //Will clear out the old line and draw a new line in its place.
        private void DrawNewLine(int seriesNumber, int numer, int denom, double newVar, double rate, double delta)
        {

            ClearLine(seriesNumber);

            double tempX = 0;
            double max = chrtLines.ChartAreas[0].AxisX.Maximum;
            if(seriesNumber == 5)
            {
                while(tempX < max)
                {
                    chrtLines.Series[5].Points.AddXY(tempX, Calculations.CalcInvest(Calculations.Calcy(tempX, numer, denom), newVar));
                    tempX += rate;
                }
            }
            if (seriesNumber == 6)
            {
                while (tempX < max)
                {

                    chrtLines.Series[6].Points.AddXY(tempX, Calculations.CalcDecay(newVar, delta, tempX));
                    tempX += rate;
                }
            }


        }

        //This section of functions just update the UI and do pretty much what they say

        private void ClearLine(int seriesNumber)
        {
            chrtLines.Series[seriesNumber].Points.Clear();
        }

        private void DisableFields()
        {
            nudS.Enabled = false;
            nudN.Enabled = false;
            //nudDelta.Enabled = false;
        }
        private void EnableFields()
        {
            nudS.Enabled = true;
            nudN.Enabled = true;
            //nudDelta.Enabled = true;
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

        //Clears every line but 3 and 4
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


       
        //Skip button sets the interrupt boolean inside the TheChart
        //If there is currently a timer going it will dispose of the timer and set the appropriate mrse

        private void BtnSkip_Click(object sender, EventArgs e)
        {
            btnSkip.Visible = false;

            //For multi use
            TheChart.GetInstance().gboolInterrupt = true;

            if (cTimer != null)
            {
                if (cTimer.Enabled)
                {
                    cTimer.Dispose();
                    if(gMrse != null)
                    {
                        gMrse.Set();
                    }else if(gApplyMrse != null)
                    {
                        gApplyMrse.Set();
                    }
                }
            }



        }

        //Sets all values back to default 
        private void BtnReset_Click(object sender, EventArgs e)
        {

            if (cTimer != null)
            {
                cTimer.Dispose();
            }

            if (t != null)
            {
                if (t.IsAlive)
                {
                    t.Abort();
                }
            }

            if (altThread != null)
            {
                if (altThread.IsAlive)
                {
                    altThread.Abort();
                }
            }
            txtKNumerator.Enabled = true;
            txtKDenominator.Enabled = true;
            txtKNumerator.Text = "";
            txtKDenominator.Text = "";
            txtLNumerator.Text = "";
            txtLDenominator.Text = "";
            txtSmlKNumerator.Text = "";
            txtSmlKDenominator.Text = "";
            nudS.Value = 0.1m;
            nudN.Value = 0m;



            btnStart.Visible = true;
            nudS.Enabled = true;
            nudN.Enabled = true;

            
            EnableButtons();
            //ShowGuessButtons();
            HideGuessButtons();
            btnOption1.BackColor = Color.FromArgb(224, 122, 95);
            btnOption2.BackColor = Color.FromArgb(224, 122, 95);
            btnOption3.BackColor = Color.FromArgb(224, 122, 95);
            btnOption4.BackColor = Color.FromArgb(224, 122, 95);

            btnAnswer.Visible = false;

            btnSkip.Visible = false;
            lblMsgbox.Text = "Enter a value for the exponent on capital.Remember that our production function must exhibit constant returns to scale.Also, enter initial values for the saving rate and population growth rate.";
            btnOk.Visible = false;
            btnApplyChanges.Visible = false;
            chrtC.Visible = false;
            chrtI.Visible = false;
            chrtY.Visible = false;

            ClearAllUpperLines();
            ClearLowerLines();

            gApplyMrse = null;
            gMrse = null;
            gboolApplyChanges = false;
            gboolGuessButPushed = false;
            gboolLessThanAnimation = false;
            gboolInitialAnimationComplete = false;
            gintMid = 0;
            gintButtonsPushed = 0;
            


            TheChart.MakeNewInstance();
            
            //This is a easier way of reseting but window size will revert to default.
            //Application.Restart();
        }

        //Calculates an appropriate space around k star so that the guess buttons values arent right on top of it.
        private double[] SpaceAroundKStar(double chrtMax, double kStar)
        {
            double[] minMax = new double[2];
            double variance = chrtMax - Math.Pow(chrtMax, (8 / 9d));
            Console.WriteLine("This is the chrtMax" + chrtMax);
            Console.WriteLine("here we have " + variance);
            minMax[0] = kStar - variance;
            minMax[1] = kStar + variance;


            return minMax;
        }



        //This needs work I sometimes see an error where both sets of values are less than kStar and also its not very random values are very close together.
        //This calculates random values to put on guess buttons buttons on the left 2 buttons are less than kstar and the right 2 are greater than
        private void PrintGuessButttons(double kStar)
        {
            double[] minMax = SpaceAroundKStar(chrtLines.ChartAreas[0].AxisX.Maximum, kStar);
            btnOption1.Text = Calculations.RoundTo3Decimals(RandomDoubleBetween(0, minMax[0])).ToString();
            btnOption2.Text = Calculations.RoundTo3Decimals(RandomDoubleBetween(0, minMax[0])).ToString();

            btnOption3.Text = Calculations.RoundTo3Decimals(RandomDoubleBetween(minMax[1], chrtLines.ChartAreas[0].AxisX.Maximum)).ToString();
            btnOption4.Text = Calculations.RoundTo3Decimals(RandomDoubleBetween(minMax[1], chrtLines.ChartAreas[0].AxisX.Maximum)).ToString();
        }

        //https://stackoverflow.com/questions/1064901/random-number-between-2-double-numbers
        private double RandomDoubleBetween(double min, double max)
        {
            //Random is not being very random so I'm going to spice it up.
            //return rnd.NextDouble() * (max - min) + min;
            double rand = 0;
            for(int i = 0; i < rnd.Next(1, 10); i++)
            {
                rand = rnd.NextDouble() * (max - min) + min;
            }
            return rand;
        }

        

        private void BtnOk_Click(object sender, EventArgs e)
        {
            btnOk.Visible = false;
            
            //Determine what phase of the program is in and then set the delay to resume animation
            if (gboolApplyChanges)
            {
                SetDelay(this, 2000);
            }
            else
            {
                RemoveLabels(gintMid);
                
                btnSkip.Visible = true;
                btnSkip.Select();
                
                lblMsgbox.Text = "Transition";
                SetDelay(this, 3000);
            }
            
            
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

            if (gboolInitialAnimationComplete)
            {

                btnApplyChanges.Visible = true;

                if (t.IsAlive)
                {
                    t.Join();
                }
                //TheChart chrt = TheChart.GetInstance();

                TheChart.GetInstance().DrawNewLine(this, DrawNewLine, 5, (double)nudS.Value);
            }

        }

        private void nudN_ValueChanged(object sender, EventArgs e)
        {
            if (gboolInitialAnimationComplete)
            {
                btnApplyChanges.Visible = true;
                if (t.IsAlive)
                {
                    t.Join();
                }
                //TheChart chrt = TheChart.GetInstance();


                TheChart.GetInstance().DrawNewLine(this, DrawNewLine, 6, (double)nudN.Value);
            }

        }





        private void buttonOption(Control c)
        {
            DisableButtons();
            if (c.BackColor != butColor)
            {
                gintButtonsPushed += 1;
                
                c.BackColor = butColor;

                
                if(gintButtonsPushed == 4)
                {
                    ShowAnswerButton();
                }
            }
            else
            {
                if(gintButtonsPushed >= 4)
                {
                    gintButtonsPushed++;
                }
                
            }

            gboolGuessButPushed = true;

            TheChart chrt = TheChart.GetInstance();
            t = new Thread(() => chrt.Animation(this, DrawPoint, double.Parse(c.Text), IncreaseSubgraphs, DrawSubGraphs, FinishAnimation, gboolGuessButPushed, StartMessageConfirm));
            t.Start();
        }
        private void btnOption1_Click(object sender, EventArgs e)
        {
            buttonOption(btnOption1);

        }

        private void btnOption2_Click(object sender, EventArgs e)
        {
            buttonOption(btnOption2);

        }

        private void btnOption3_Click(object sender, EventArgs e)
        {
            buttonOption(btnOption3);

        }

        private void btnOption4_Click(object sender, EventArgs e)
        {
            buttonOption(btnOption4);

        }

        private void btnAnswer_Click(object sender, EventArgs e)
        {
            gboolInitialAnimationComplete = true;
            gintButtonsPushed = 0;
            HideGuessButtons();
            btnAnswer.Enabled = false;
            EnableFields();
            //MessageBox.Show("Please make changes to the saving rate, population growth rate, and depreciation rate. Notice that the relevant functions will shift because of the changes you make. Think about how those changes will affect the steady state capital per worker and income per worker. Click “Apply Changes” and see if your intuition is correct. You may do this as many time as you would like.");
            lblMsgbox.Text = "Please make changes to the saving rate and/or the population growth rate. Notice that the relevant functions will shift because of the changes you make. Think about how those changes will affect the steady state capital per worker and income per worker. Click “Apply Changes” and see if your intuition is correct. You may do this as many time as you would like.";
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
           
        }
        


        //For multi use
        private void StartMessageConfirm(double kStar, double tempK, ManualResetEvent mrse)
        {
            //aTimer.Enabled = false;
            gboolGuessButPushed = false;
            //gintMid = AddLabels(gdblTempK);
            gintMid = MakeLabelsAuto(kStar);

            if (tempK > kStar)
            {
                //MessageBox.Show("Notice at the k selected, new investment in the economy falls below that which is required to break even.  Therefore, capital per worker will decrease as the economy transitions to steady-state.");
                lblMsgbox.Text = "Notice at the k selected, new investment in the economy falls below that which is required to break even.  Therefore, capital per worker will decrease as the economy transitions to steady-state.";
            }
            else
            {
                //MessageBox.Show("Notice at the k selected, new investment in the economy exceeds that which is required to break even.  Therefore, capital per worker will increase as the economy transitions to steady-state.");
                lblMsgbox.Text = "Notice at the k selected, new investment in the economy exceeds that which is required to break even.  Therefore, capital per worker will increase as the economy transitions to steady-state.";
            }
            gMrse = mrse;

            btnOk.Visible = true;
            btnOk.Select();
        }

        //For multi use
        private void DrawPoint(double k, double y)
        {
            chrtLines.Series[3].Points.Clear();
            chrtLines.Series[4].Points.Clear();

            chrtLines.Series[3].Points.AddXY(chrtLines.ChartAreas[0].AxisX.Minimum, y);
            chrtLines.Series[3].Points.AddXY(chrtLines.ChartAreas[0].AxisX.Maximum, y);
            chrtLines.Series[4].Points.AddXY(k, chrtLines.ChartAreas[0].AxisY.Minimum);
            chrtLines.Series[4].Points.AddXY(k, chrtLines.ChartAreas[0].AxisY.Maximum);

            //DrawSubGraphs(miniX, consum, invest);
        }

        //For multi use
        private void DrawSubGraphs(double miniX, double consum, double invest)
        {
            chrtC.Series[0].Points.AddXY(miniX, consum);
            //Console.WriteLine("C: " + gdblConsum);
            chrtI.Series[0].Points.AddXY(miniX, invest);
            //Console.WriteLine("I: " + gdblInvest);
            chrtY.Series[0].Points.AddXY(miniX, (consum + invest));
            //Console.WriteLine("Y: " + (gdblConsum + gdblInvest));
        }


        //For multi use
        private void FinishAnimation(double kStar, double yStar)
        {
            //btnSkip.Enabled = false;
            btnSkip.Visible = false;
            //aTimer.Enabled = false;
            //gboolZoomAnimationComplete = false;
            if (gboolLessThanAnimation)
            {
                ClearCertainLines();
                //gdblK = 0;
                //DrawAllLines();
                //TheChart.GetInstance().DrawAllLines(this, setAnswerText, btnAnswer.Enabled, setMaxAndMinBounds, MultiThreadDraw);
                t = new Thread(() => TheChart.GetInstance().DrawAllLines(this, setAnswerText, btnAnswer.Enabled, setMaxAndMinBounds, MultiThreadDraw));
                t.Start();
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
            //Console.WriteLine($"Here the value of buttons pushed is {gintButtonsPushed}");
            if (gintButtonsPushed == 4)
            {
                //MessageBox.Show("The middle box displays steady state capital per worker and income per worker for the production function you selected and the values for the saving rate, population growth rate, and depreciation rate you selected. Click on the box.");
                lblMsgbox.Text = "The middle box displays steady state capital per worker and income per worker for the production function you selected and the values for the saving rate and population growth rate you selected. Click on the box.";
            }
            else if (!btnOption1.Visible)
            {
                lblMsgbox.Text = "Notice that the economy has transitioned to new steady state level of capital per worker and income per worker. Convince yourself that the change in the parameter(s) you made resulted in the transition shown.";
            }
            else
            {
                lblMsgbox.Text = "Select another value for initial k.";
            }
            btnAnswer.Text = "k* = " + Calculations.RoundTo3Decimals(kStar) + "\n y* = " + Calculations.RoundTo3Decimals(yStar);
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
            int numerator, denom;
            if (!(string.IsNullOrEmpty(txtKNumerator.Text) || string.IsNullOrEmpty(txtKDenominator.Text))){
                if (int.TryParse(txtKNumerator.Text, out numerator) && int.TryParse(txtKDenominator.Text, out denom))
                {
                    if (numerator == 0 || denom == 0)
                    {
                        return false;
                    }
                    else if (numerator >= denom)
                    {
                        //Print message that the numberator must be smaller than denominator
                        return false;
                    }
                    else
                    {
                        Calculations.GreatestCommonD(ref numerator, ref denom);


                        TheChart.GetInstance().setNumAndDenom(numerator, denom);

                        txtKNumerator.Text = numerator.ToString();
                        txtKDenominator.Text = denom.ToString();
                        txtSmlKNumerator.Text = numerator.ToString();
                        txtSmlKDenominator.Text = denom.ToString();
                        txtLDenominator.Text = denom.ToString();
                        txtLNumerator.Text = (denom - numerator).ToString();
                        return true;
                    }
                }
                else
                {
                    //Print message that they are not numbers
                    //Maybe add a error box to display error messages
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

    }
}
