using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace SolowProjectVer2
{
    class TheChart
    {

        //These are okay globals since they will be the same once the start button is pressed.
        //But they need to be reset if the reset button is pushed.
        private int gintNumerator;
        private int gintDenom;



        internal double gdblN;
        internal double gdblS;
        internal double gdblDelta;


        private double gdblMiniGraphXVal = 11.5;
        const double gdblXRate = 0.05;

        public bool gboolInterrupt = false;



        private static TheChart obj = new TheChart();
        

        private TheChart() { }

        public static TheChart GetInstance()
        {
            return obj;
        }

        public static void MakeNewInstance()
        {
            obj = new TheChart();
        }



        public void setNumAndDenom(int numerator, int denom)
        {
            this.gintNumerator = numerator;
            this.gintDenom = denom;
        }

        public void GenerateGuessButtons(Form f, Action<double> printGuessButtons)
        {
            double kStar = 0;
            double trashY = 0;
            Calculations.CalcKsandYs(gdblS, gdblN, gdblDelta, gintNumerator, gintDenom, ref kStar, ref trashY);
            f.Invoke(printGuessButtons, kStar);
        }

        public void ApplyChange(Form f, Action<double, double, double, double, double, double, ManualResetEvent> ApplyChangesMessage, double s, double n, Action<string> setAnswer, bool btnAnswerEnabled, Action<double, double> maxAndMinBounds, Action<double, double, double, double> draw, double chrtMax, Action<bool, double> finishApply)
        {
            double oldKStar = 0;
            double yStar = 0;

            Calculations.CalcKsandYs(gdblS, gdblN, gdblDelta, gintNumerator, gintDenom, ref oldKStar, ref yStar);

            double oldS = gdblS;
            double oldN = gdblN;
            //setParams(n, s, gdblDelta);
            this.gdblN = n;
            this.gdblS = s;
            double kStar = 0;
            Calculations.CalcKsandYs(gdblS, gdblN, gdblDelta, gintNumerator, gintDenom, ref kStar, ref yStar);

            ManualResetEvent msre = new ManualResetEvent(false);
            f.Invoke(ApplyChangesMessage, new object[] { n, oldN, s, oldS, kStar, oldKStar, msre});
            msre.WaitOne();

            bool lessThan = false;
            if(oldKStar < kStar)
            {
                DrawAllLines(f, setAnswer, btnAnswerEnabled, maxAndMinBounds, draw);
            }
            else
            {
                DrawLines(f, chrtMax, draw);
                lessThan = true;
            }

            f.Invoke(finishApply, new object[] { lessThan, oldKStar });


            msre.Dispose();
        }



        public void DrawAllLines(Form f, Action<string> setAnswer, bool btnAnswerEnabled, Action<double, double> maxAndMinBounds, Action<double, double, double, double> draw)
        {
            //Console.WriteLine("hllow ");
            double kStar = 0;
            double yStar = 0;
            //Console.WriteLine(gdblS + " " + gdblN + " " + gdblDelta);
            Calculations.CalcKsandYs(gdblS, gdblN, gdblDelta, gintNumerator, gintDenom, ref kStar, ref yStar);

            //At this point in time the kStar is still 0 for some reason I'll find out tomorrow

            if (btnAnswerEnabled)
            {
                f.Invoke(setAnswer, "k* = " + Calculations.RoundTo3Decimals(kStar) + "\n y* = " + Calculations.RoundTo3Decimals(yStar));
            }

            double maxK;

            if(kStar < 1)
            {
                maxK = Math.Pow(kStar, 2) + kStar;
            }
            else
            {
                maxK = 2 * Math.Sqrt(kStar) + kStar;
            }

            f.Invoke(maxAndMinBounds, new object[] { maxK, Calculations.Calcy(maxK, gintNumerator, gintDenom) });


            DrawLines(f, maxK, draw);
        }

        public void DrawLines(Form f, double maxK, Action<double, double, double, double> draw)
        {
            double tempK = 0;
            double capita, invest, brkEven;
            while(tempK < maxK)
            {
                capita = Calculations.Calcy(tempK, gintNumerator, gintDenom);
                invest = Calculations.CalcInvest(capita, gdblS);
                brkEven = Calculations.CalcDecay(gdblN, gdblDelta, tempK);

                f.BeginInvoke(draw, new object[] {tempK, capita, invest, brkEven });

                tempK += gdblXRate;
            }
        }


        //This will be called via the ui Thread as this can happen with not much time inbetween 
        //drawLine will draw the specified line with the data given
        public void DrawNewLine(Form f, Action<int, int, int, double, double, double> drawLine, int seriesNumber, double newVar)
        {
            /*
            f.Invoke(ClearLine, seriesNumber);

            double tempX = 0;

            if (seriesNumber == 5)
            {
                while (tempX < max)
                {
                    //chrtLines.Series[5].Points.AddXY(gdblTempX, Calculations.CalcInvest(Calculations.Calcy(gdblTempX, gintNumerator, gintDenom), gdblS));
                    f.Invoke(writePoint, new object[] { seriesNumber, tempX, Calculations.CalcInvest(Calculations.Calcy(tempX, gintNumerator, gintDenom), newVar) });
                    tempX += gdblXRate;
                }
            }
            else if (seriesNumber == 6)
            {
                while (tempX < max)
                {
                    //chrtLines.Series[6].Points.AddXY(gdblTempX, Calculations.CalcDecay(gdblN, gdblDelta, gdblTempX));
                    f.Invoke(writePoint, new object[] { seriesNumber, tempX, Calculations.CalcDecay(newVar, gdblDelta, tempX) });
                    tempX += gdblXRate;
                }
            }
            */
            f.Invoke(drawLine, new object[] { seriesNumber, gintNumerator, gintDenom, newVar, gdblXRate, gdblDelta });

        }


        //This comment block will explain what each Action needs to do for Animation
        /*
         * DrawPoint is the function that draws the cross hairs
         * increaseSubGraphs shifts the subgraphs
         * justSubs draws just the subgrapsh
         * finishAnimation happens after the animation is complete
         * startOkWait will start the process to wait for an ok button click which is the purpose of the ManualResetEvent
         * 
         */

        public void Animation(Form f, Action<double, double> DrawPoint, double k, Action<double> increaseSubGraphs, Action<double, double, double> justSubs, Action<double, double> finishAnimation, bool GuessButPushed, Action<double, double, ManualResetEvent> startOkWait)
        {
            double y = Calculations.Calcy(k, gintNumerator, gintDenom);
            double consum = Calculations.CalcConsum(gdblS, y);
            double invest = Calculations.CalcInvest(y, gdblS);
            //gdblDK = CalcDeltaTimesK();
            double changeK = Calculations.CalcChangeOfK(gdblS, y, gdblN, gdblDelta, k);


            double tempK;

            double kStar = 0;
            double yStar = 0;

            Calculations.CalcKsandYs(gdblS, gdblN, gdblDelta, gintNumerator, gintDenom, ref kStar, ref yStar);

            //Temp delete later
            //changeK = 1;
            ManualResetEvent mrse = new ManualResetEvent(false);

            while ((changeK > 0.001) || (changeK < -0.001))
            {
                if (gboolInterrupt)
                {
                    gboolInterrupt = false;
                    while ((changeK > 0.001) || (changeK < -0.001))
                    {
                        f.BeginInvoke(justSubs, new object[] { gdblMiniGraphXVal, consum, invest });
                        k += changeK;
                        gdblMiniGraphXVal += Math.Abs(changeK);
                        //increaseGraphs(Math.Abs(changeK));
                        f.BeginInvoke(increaseSubGraphs, Math.Abs(changeK));
                        y = Calculations.Calcy(k, gintNumerator, gintDenom);
                        consum = Calculations.CalcConsum(gdblS, y);
                        invest = Calculations.CalcInvest(y, gdblS);
                        //gdblDK = Calculations.CalcDeltaTimesK();
                        changeK = Calculations.CalcChangeOfK(gdblS, y, gdblN, gdblDelta, k);

                    }
                    f.BeginInvoke(DrawPoint, new object[] { k, y });


                }
                else
                {
                    //f.Invoke(crossLines(k, y, gdblMiniGraphXVal, consum, invest);
                    f.BeginInvoke(DrawPoint, new object[] { k, y});
                    f.BeginInvoke(justSubs, new object[] { gdblMiniGraphXVal, consum, invest });
                    tempK = k;
                    k += changeK;
                    gdblMiniGraphXVal += Math.Abs(changeK);
                    //increaseGraphs(Math.Abs(changeK));
                    f.BeginInvoke(increaseSubGraphs, Math.Abs(changeK));
                    
                    y = Calculations.Calcy(k, gintNumerator, gintDenom);
                    consum = Calculations.CalcConsum(gdblS, y);
                    invest = Calculations.CalcInvest(y, gdblS);
                    //gdblDK = Calculations.CalcDeltaTimesK();
                    changeK = Calculations.CalcChangeOfK(gdblS, y, gdblN, gdblDelta, k);

                    if (GuessButPushed)
                    {
                        GuessButPushed = false;
                        f.BeginInvoke(startOkWait, new object[] { kStar, tempK, mrse });
                        mrse.WaitOne();
                        //Thread.CurrentThread.Suspend();
                        
                    }


                    Thread.Sleep(90);
                }
                
            }

            mrse.Dispose();
            f.BeginInvoke(finishAnimation, new object[] { kStar, yStar });


            
        }
    }
}
