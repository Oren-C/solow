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


public struct Locs
{
    public static Point YK = new Point(13,33);
    public static Point Knum = new Point(61, 23);
    public static Point Kdev = new Point(97, 23);
    public static Point Kdenom = new Point(121, 23);
    public static Point L = new Point(157, 33);
    public static Point Lnum = new Point(176, 23);
    public static Point Ldev = new Point(212, 23);
    public static Point Ldenom = new Point(236, 23);
    public static Point yk = new Point(13, 91);
    public static Point knum = new Point(61,81);
    public static Point kdev = new Point(97, 81);
    public static Point kdenom = new Point(121, 81);
    public static Point delta = new Point(12, 125);
    public static Point dnum = new Point(56, 124);
    public static Point s = new Point(12, 197);
    public static Point nuds = new Point(52, 199);
    public static Point n = new Point(12, 247);
    public static Point nudn = new Point(52, 249);
    public static Point bApply = new Point(46, 289);
    public static Point bStart = new Point(197, 157);
    public static Point bSkip = new Point(197, 229);
    public static Point bReset = new Point(197, 301);
    public static Point bOp1 = new Point(354, 379);
    public static Point bOp2 = new Point(531, 379);
    public static Point bOp3 = new Point(889, 379);
    public static Point bOp4 = new Point(1056, 379);
    public static Point bAns = new Point(683, 380);
    public static Point bOk = new Point(97, 696);
    public static Point msg = new Point(16, 436);
    public static Point cLines = new Point(319, 10);
    public static Point cC = new Point(319, 436);
    public static Point cI = new Point(319, 566);
    public static Point cY = new Point(319, 696);

}

public struct Sizes
{
    public static Size YK = new Size(53,25);
    public static Size Knum = new Size(30,20);
    public static Size Kdev = new Size(18,25);
    public static Size Kdenom = new Size(30,20);
    public static Size L = new Size(24,25);
    public static Size Lnum = new Size(30,20);
    public static Size Ldev = new Size(18,25);
    public static Size Ldenom = new Size(30,20);
    public static Size yk = new Size(46,25);
    public static Size knum = new Size(30,20);
    public static Size kdev = new Size(18,25);
    public static Size kdenom = new Size(30,20);
    public static Size delta = new Size(26,24);
    public static Size dnum = new Size(42,25);
    public static Size s = new Size(24,24);
    public static Size nuds = new Size(120,24);
    public static Size n = new Size(26,24);
    public static Size nudn = new Size(120,24);
    public static Size bApply = new Size(126,81);
    public static Size bStart = new Size(106,66);
    public static Size bSkip = new Size(106,66);
    public static Size bReset = new Size(106,66);
    public static Size bOp = new Size(95, 51);
    public static Size bAns = new Size(136,40);
    public static Size bOk = new Size(109,60);
    public static Size msg = new Size(271,244);
    public static Size cLines = new Size(856, 363);
    public static Size subChart = new Size(855, 124);
}




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
        double gdblOldN, gdblOldS;
        int gintMid;



        Random rnd = new Random();

        double gdblMiniGraphXVal = 11.5;
        int gintNumerator, gintDenom;
        int gintButtonsPushed = 0;

        //int[] gintArrLines = new int[] { 0, 1, 2, -1, -1 };
        
        bool gboolGoLeft = false;
        bool gboolInitialAnimationComplete = false;
        bool gboolZoomAnimationComplete = false;
        bool gboolApplyChanges = false;
        bool gboolInvestChanged = false;
        bool gboolDecayChanged = false;
        //bool gboolAnimationNeeded = false;
        bool gboolLessThanAnimation = false;

      

        bool gboolFirstChangeOcc = false;

        

        bool gboolInterrupt = false;

        double gdblXRate = .05;
        double gdblMinXRate = .05, gdblMaxXRate = 0.5, gdblMinYRate = 0.5, gdblMaxYRate = 0.5;
        const int TIMERATEINMILIS = 90;

        Size[] sizArr;
        Point[] locArr;
        public Form1()
        {
            InitializeComponent();
            //MessageBox.Show("Enter a value for the exponent on capital.Remember that our production function must exhibit constant returns to scale.Also, enter initial values for the saving rate and population growth rate.");
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chrtLines.ChartAreas[0].AxisY.LabelStyle.Format = "{#####.###}";
            chrtLines.ChartAreas[0].AxisX.LabelStyle.Format = "{#####.###}";
            sizArr = new Size[base.Controls.Count];
            locArr = new Point[base.Controls.Count];
            for (int i = 0; i < sizArr.Length; i++)
            {
                sizArr[i] = base.Controls[i].Size;
                locArr[i] = base.Controls[i].Location;

            }
            Console.WriteLine(sizArr.Length);
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            /*
            float widthRatio = this.Width / 1205f;
            lblMsgbox.Text += " " + widthRatio;
            float heightRatio = this.Height / 800;

            for (int i = 0; i < Controls.Count; i++)
            {
                Controls[i].Size = new Size((int)(sizArr[i].Width * widthRatio), (int)(sizArr[i].Height * heightRatio));
            }
            */
        }

        protected override void WndProc(ref Message m)
        {
            

            if ( m.Msg == 0x0112)
            {

                


               /*
                if (m.WParam == new IntPtr( 0xF030 ))
                {
                    //The Window is being maximized
                    Console.WriteLine("the window is being maximized");
                    System.Drawing.Size size = new Size(20, 10);
                    chrtLines.Size += size;
                }
                else
                {
                    Console.WriteLine("the window is changed");

                    
                    lblYK.Size = Sizes.YK;
                    lblYK.Location = Locs.YK;
                    txtKNumerator.Size = Sizes.Knum;
                    txtKNumerator.Location = Locs.Knum;
                    lblDev1.Size = Sizes.Kdev;
                    lblDev1.Location = Locs.Kdev;
                    txtKDenominator.Size = Sizes.Kdenom;
                    txtKDenominator.Location = Locs.Kdenom;
                    lblL.Size = Sizes.L;
                    lblL.Location = Locs.L;
                    txtLNumerator.Size = Sizes.Lnum;
                    txtLNumerator.Location = Locs.Lnum;
                    lblDev2.Size = Sizes.Ldev;
                    lblDev2.Location = Locs.Ldev;
                    txtLDenominator.Size = Sizes.Ldenom;
                    txtLDenominator.Location = Locs.Ldenom;
                    lblLittleyk.Size = Sizes.yk;
                    lblLittleyk.Location = Locs.yk;
                    txtSmlKNumerator.Size = Sizes.knum;
                    txtSmlKNumerator.Location = Locs.knum;
                    lblDev3.Size = Sizes.kdev;
                    lblDev3.Location = Locs.kdev;
                    txtSmlKDenominator.Size = Sizes.kdenom;
                    txtSmlKDenominator.Location = Locs.kdenom;
                    lblDelta.Size = Sizes.delta;
                    lblDelta.Location = Locs.delta;
                    lblNDelta.Size = Sizes.dnum;
                    lblNDelta.Location = Locs.dnum;
                    lblS.Size = Sizes.s;
                    lblS.Location = Locs.s;
                    nudS.Size = Sizes.nuds;
                    nudS.Location = Locs.nuds;
                    lblN.Size = Sizes.n;
                    lblN.Location = Locs.n;
                    nudN.Size = Sizes.nudn;
                    nudN.Location = Locs.nudn;
                    btnApplyChanges.Size = Sizes.bApply;
                    btnApplyChanges.Location = Locs.bApply;
                    btnStart.Size = Sizes.bStart;
                    btnStart.Location = Locs.bStart;
                    btnSkip.Size = Sizes.bSkip;
                    btnSkip.Location = Locs.bSkip;
                    btnReset.Size = Sizes.bReset;
                    btnReset.Location = Locs.bReset;

                    btnOption1.Size = Sizes.bOp;
                    //btnOption1.Location = Locs.bOp1;

                    btnOption2.Size = Sizes.bOp;
                   // btnOption2.Location = Locs.bOp2;

                    btnOption1.Size = Sizes.bOp;
                   // btnOption2.Location = Locs.bOp2;

                    btnOption3.Size = Sizes.bOp;
                   // btnOption3.Location = Locs.bOp3;

                    btnOption4.Size = Sizes.bOp;
                   // btnOption4.Location = Locs.bOp4;

                    btnAnswer.Size = Sizes.bApply;
                    btnAnswer.Location = Locs.bApply;
                    btnOk.Size = Sizes.bOk;
                    btnOk.Location = Locs.bOk;

                    lblMsgbox.Size = Sizes.msg;
                    lblMsgbox.Location = Locs.msg;

                    chrtLines.Size = Sizes.cLines;
                    //chrtLines.Location = Locs.cLines;

                    chrtC.Size = Sizes.subChart;
                    //chrtC.Location = Locs.cC;

                    chrtI.Size = Sizes.subChart;
                    //chrtI.Location = Locs.cI;

                    chrtY.Size = Sizes.subChart;
                   // chrtY.Location = Locs.cY;
                    

                }
                */
            }
            base.WndProc(ref m);
        }
        
        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (IsFractionOkay())
            {
                btnStart.Enabled = false;
                btnStart.Visible = false;
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
                //MessageBox.Show("The four boxes you see represent potential choices for initial capital per worker. Click on a box and observe transition to steady state capital per worker. Repeat for each choice.");
                lblMsgbox.Text = "The four boxes you see represent potential choices for initial capital per worker. Click on a box and observe transition to steady state capital per worker. Repeat for each choice. Notice that each choice is not steady state capital per worker.";
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
                        gintMid = AddLabels(gdblZmMinX, gdblZmMaxX);
                        if (gdblK > gdblKStar)
                        {
                            //MessageBox.Show("Notice at the k selected, new investment in the economy falls below that which is required to break even.  Therefore, capital per worker will decrease as the economy transitions to steady-state.");
                            lblMsgbox.Text = "Notice at the k selected, new investment in the economy falls below that which is required to break even.  Therefore, capital per worker will decrease as the economy transitions to steady-state.";
                        }
                        else
                        {
                            //MessageBox.Show("Notice at the k selected, new investment in the economy exceeds that which is required to break even.  Therefore, capital per worker will increase as the economy transitions to steady-state.");
                            lblMsgbox.Text = "Notice at the k selected, new investment in the economy exceeds that which is required to break even.  Therefore, capital per worker will increase as the economy transitions to steady-state.";
                        }
                        btnSkip.Enabled = false;
                        btnSkip.Visible = false;
                        btnOk.Visible = true;
                        btnOk.Select();
                        //Zooms out 
                        /*
                        RemoveLabels(gintMid);
                        chrtLines.ChartAreas[0].AxisX.Maximum = gdblOldMaxX;
                        chrtLines.ChartAreas[0].AxisX.Minimum = 0;
                        chrtLines.ChartAreas[0].AxisY.Maximum = gdblOldMaxY;
                        chrtLines.ChartAreas[0].AxisY.Minimum = 0;
                        //Console.WriteLine("Hellppp mee pleeeassee");
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
                        */
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
        private void ContinueAfterMessage()
        {
            RemoveLabels(gintMid);
            chrtLines.ChartAreas[0].AxisX.Maximum = gdblOldMaxX;
            chrtLines.ChartAreas[0].AxisX.Minimum = 0;
            chrtLines.ChartAreas[0].AxisY.Maximum = gdblOldMaxY;
            chrtLines.ChartAreas[0].AxisY.Minimum = 0;
            //Console.WriteLine("Hellppp mee pleeeassee");
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
                btnSkip.Enabled = true;
                btnSkip.Visible = true;
                aTimer.Enabled = true;

            }
        }


                private void DoNothing(int miliSecs)
        {
            System.Threading.Thread.Sleep(miliSecs);
        }
        private void btnApplyChanges_Click(object sender, EventArgs e)
        {
            btnApplyChanges.Visible = false;
            DisableFields();
            

            //gboolAnimationNeeded = true;
            //btnApplyChanges.Visible = false;
            //DisableFields();
            gdblOldKStar = gdblKStar;
            //gdblOldKStar = gdblKStar;

            // this will delete all the lines this is where the problem lies as we are adding multiple new lines and
            // by deleting them and redrawing them we are changing the max I could keep the current setup and add some checks in DrawAllLines possibly
            ClearAllUpperLines();
            gdblK = 0;
            //I need to zoom before I draw all !Wait maybe not
            
            //gdblDelta = (double)nudDelta.Value;
            //Console.WriteLine($"here is the old s {gdblOldS} and here is the old n {gdblOldN}");
            gdblDelta = 0.1;

            CalcKsandYs(gdblS, gdblN, gdblDelta);

            if (gdblOldKStar < gdblKStar)
            {
                DrawAllLines();

            }
            else
            {
                DrawLines();
                gboolLessThanAnimation = true;

            }
            //Words Section

            string message = "";
            if(gdblS > gdblOldS)
            {
                
                if(gdblN > gdblOldN)
                {
                    //Option 7
                    if(gdblKStar > gdblOldKStar)
                    {
                        message = "You have increased the saving rate and increased the population growth rate.  As a result, investment now exceeds its break-even point, and the economy will transition to a new steady state";

                    }else if(gdblKStar < gdblOldKStar)
                    {
                        message = "You have increased the saving rate and increased the population growth rate.  As a result, investment is now less than its break-even point, and the economy will transition to a new steady state.";
                    }
                    else
                    {
                        message = "Error nothing changed";
                    }


                }else if(gdblN < gdblOldN)
                {
                    //Option 5
                    message = "You have increased the saving rate and decreased the population growth rate.  As a result, investment now exceeds its break-even point, and the economy will transition to a new steady state.";


                }
                else
                {
                    //Option 1
                    message = "You have increased the saving rate and left the population growth rate unchanged.  As a result, investment now exceeds its break-even point, and the economy will transition to a new steady state.";


                }

                
                
            }else if(gdblS < gdblOldS)
            {
                if (gdblN > gdblOldN)
                {
                    //Option 6
                    message = "You have decreased the saving rate and increased the population growth rate.  As a result, investment is now less than its break-even point, and the economy will transition to a new steady state.";



                }
                else if (gdblN < gdblOldN)
                {
                    //Option 8

                    if (gdblKStar > gdblOldKStar)
                    {
                        message = "You have decreased the saving rate and decreased the population growth rate.  As a result, investment now exceeds its break-even point, and the economy will transition to a new steady state";

                    }
                    else if (gdblKStar < gdblOldKStar)
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
            }else if(gdblN > gdblOldN)
            {
                // Option 3
                message = "You have left the saving rate unchanged and increased the population growth rate.  As a result, investment is now less than its break-even point, and the economy will transition to a new steady state.";

            }
            else if(gdblN < gdblOldN)
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





            gboolApplyChanges = true;
            btnOk.Visible = true;
            btnOk.Select();
            //ApplyChanges();

        }

        private void ApplyChanges()
        {
            
            
            
            btnSkip.Enabled = true;
            btnSkip.Visible = true;
            StartAnimation(gdblOldKStar);
        }

        

        //All this does is calculates the min and max of the axis and then calls the draw lines function
        private void DrawAllLines()
        {
            gdblS = (double)nudS.Value;
            gdblN = (double)nudN.Value;
            //gdblDelta = (double)nudDelta.Value;
            gdblDelta = 0.1;

            CalcKsandYs(gdblS, gdblN, gdblDelta);
            if (btnAnswer.Enabled)
            {
                btnAnswer.Text = "k* = " + RoundTo3Decimals(gdblKStar) + "\n y* = " + RoundTo3Decimals(gdblYStar);
            }
            //btnAnswer.Text = "k* = " + RoundTo3Decimals(gdblKStar)+"\n y* = "+RoundTo3Decimals(gdblYStar);
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
            btnSkip.Visible = false;
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

        

        private void btnPrintSize_Click(object sender, EventArgs e)
        {
            lblMsgbox.Text = chrtLines.Size.Width.ToString();
        }

        

        private void BtnOk_Click(object sender, EventArgs e)
        {
            btnOk.Visible = false;
            
            if (gboolApplyChanges)
            {
                gboolApplyChanges = false;
                ApplyChanges();
            }
            else
            {
                ContinueAfterMessage();
            }
            
            
        }

        

        private void Form1_Shown(object sender, EventArgs e)
        {
            //MessageBox.Show("Enter a value for the exponent on capital.Remember that our production function must exhibit constant returns to scale.Also, enter initial values for the saving rate, population growth rate, and rate of depreciation.");
            lblMsgbox.Text = "Enter a value for the exponent on capital.Remember that our production function must exhibit constant returns to scale.Also, enter initial values for the saving rate and population growth rate.";
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


        /*
        private void nudDelta_ValueChanged(object sender, EventArgs e)
        {
            if (gboolInitialAnimationComplete)
            {
                btnApplyChanges.Visible = true;
                gboolDecayChanged = true;
                //gdblDelta = (double)nudDelta.Value;
                gdblDelta = 0.1;
                DrawNewLine(6);
            }

        }
        */
        private void btnOption1_Click(object sender, EventArgs e)
        {
            DisableButtons();
            //gboolAnimationNeeded = true;
            if(btnOption1.BackColor != Color.Green)
            {
                gintButtonsPushed += 1;
                //DisableButtons();
                btnSkip.Enabled = true;
                btnSkip.Visible = true;
                btnOption1.BackColor = Color.Green;
                
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
                btnSkip.Visible = true;
                StartAnimation(double.Parse(btnOption1.Text));
                
            }
            
        }

        private void btnOption2_Click(object sender, EventArgs e)
        {
            DisableButtons();
            //gboolAnimationNeeded = true;
            if (btnOption2.BackColor != Color.Green)
            {
                gintButtonsPushed += 1;
                //DisableButtons();
                btnSkip.Enabled = true;
                btnSkip.Visible = true;
                btnOption2.BackColor = Color.Green;
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
                btnSkip.Visible = true;
                StartAnimation(double.Parse(btnOption2.Text));
            }
        }

        private void btnOption3_Click(object sender, EventArgs e)
        {
            //gboolAnimationNeeded = true;
            if (btnOption3.BackColor != Color.Green)
            {
                DisableButtons();
                gintButtonsPushed += 1;
                //DisableButtons();
                btnSkip.Enabled = true;
                btnSkip.Visible = true;
                btnOption3.BackColor = Color.Green;
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
                btnSkip.Visible = true;
                StartAnimation(double.Parse(btnOption3.Text));
            }
        }

        private void btnOption4_Click(object sender, EventArgs e)
        {
            DisableButtons();
            //gboolAnimationNeeded = true;
            if (btnOption4.BackColor != Color.Green)
            {
                gintButtonsPushed += 1;
                //DisableButtons();
                btnSkip.Enabled = true;
                btnSkip.Visible = true;
                btnOption4.BackColor = Color.Green;
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
                btnSkip.Visible = true;
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
            //MessageForm messageForm = new MessageForm();
            //messageForm.Show();

            gdblOldN = gdblN;
            gdblOldS = gdblS;

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
                        //MessageBox.Show("The middle box displays steady state capital per worker and income per worker for the production function you selected and the values for the saving rate, population growth rate, and depreciation rate you selected. Click on the box.");
                        lblMsgbox.Text = "The middle box displays steady state capital per worker and income per worker for the production function you selected and the values for the saving rate, population growth rate, and depreciation rate you selected. Click on the box.";
                    }else if (!btnOption1.Visible)
                    {
                        lblMsgbox.Text = "Notice that the economy has transitioned to new steady state level of capital per worker and income per worker. Convince yourself that the change in the parameter(s) you made resulted in the transition shown.";
                    }
                    btnAnswer.Text = "k* = " + RoundTo3Decimals(gdblKStar) + "\n y* = " + RoundTo3Decimals(gdblYStar);
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
                        btnSkip.Visible = false;
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
                            //MessageBox.Show("The middle box displays steady state capital per worker and income per worker for the production function you selected and the values for the saving rate, population growth rate, and depreciation rate you selected. Click on the box.");
                            lblMsgbox.Text = "The middle box displays steady state capital per worker and income per worker for the production function you selected and the values for the saving rate, population growth rate, and depreciation rate you selected. Click on the box.";
                        }
                        else if (!btnOption1.Visible)
                        {
                            lblMsgbox.Text = "Notice that the economy has transitioned to new steady state level of capital per worker and income per worker. Convince yourself that the change in the parameter(s) you made resulted in the transition shown.";
                        }
                        btnAnswer.Text = "k* = " + RoundTo3Decimals(gdblKStar) + "\n y* = " + RoundTo3Decimals(gdblYStar);
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
