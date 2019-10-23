namespace SolowProjectVer2
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea21 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend21 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series51 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series52 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series53 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series54 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series55 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series56 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series57 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea22 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend22 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series58 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea23 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend23 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series59 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea24 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend24 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series60 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chrtLines = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.txtKNumerator = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtKDenominator = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLDenominator = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLNumerator = new System.Windows.Forms.TextBox();
            this.txtSmlKDenominator = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSmlKNumerator = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnSkip = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.chrtC = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chrtI = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chrtY = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.nudS = new System.Windows.Forms.NumericUpDown();
            this.nudN = new System.Windows.Forms.NumericUpDown();
            this.nudDelta = new System.Windows.Forms.NumericUpDown();
            this.btnOption1 = new System.Windows.Forms.Button();
            this.btnOption2 = new System.Windows.Forms.Button();
            this.btnOption3 = new System.Windows.Forms.Button();
            this.btnOption4 = new System.Windows.Forms.Button();
            this.btnApplyChanges = new System.Windows.Forms.Button();
            this.btnAnswer = new System.Windows.Forms.Button();
            this.lblDelta = new System.Windows.Forms.Label();
            this.lblN = new System.Windows.Forms.Label();
            this.lblS = new System.Windows.Forms.Label();
            this.lblMsgbox = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chrtLines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chrtC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chrtI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chrtY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelta)).BeginInit();
            this.SuspendLayout();
            // 
            // chrtLines
            // 
            chartArea21.AxisX.MajorGrid.Enabled = false;
            chartArea21.AxisY.MajorGrid.Enabled = false;
            chartArea21.Name = "ChartArea1";
            this.chrtLines.ChartAreas.Add(chartArea21);
            legend21.Enabled = false;
            legend21.Name = "Legend1";
            this.chrtLines.Legends.Add(legend21);
            this.chrtLines.Location = new System.Drawing.Point(319, 10);
            this.chrtLines.Name = "chrtLines";
            series51.ChartArea = "ChartArea1";
            series51.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series51.Color = System.Drawing.Color.Black;
            series51.IsVisibleInLegend = false;
            series51.Legend = "Legend1";
            series51.Name = "Series1";
            series52.ChartArea = "ChartArea1";
            series52.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series52.Color = System.Drawing.Color.Black;
            series52.IsVisibleInLegend = false;
            series52.Legend = "Legend1";
            series52.Name = "Series2";
            series53.ChartArea = "ChartArea1";
            series53.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series53.Color = System.Drawing.Color.Black;
            series53.IsVisibleInLegend = false;
            series53.Legend = "Legend1";
            series53.Name = "Series3";
            series54.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series54.BorderWidth = 3;
            series54.ChartArea = "ChartArea1";
            series54.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series54.Color = System.Drawing.Color.Lime;
            series54.Legend = "Legend1";
            series54.Name = "Series4";
            series55.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series55.BorderWidth = 3;
            series55.ChartArea = "ChartArea1";
            series55.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series55.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            series55.Legend = "Legend1";
            series55.Name = "Series5";
            series56.ChartArea = "ChartArea1";
            series56.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series56.Color = System.Drawing.Color.Aqua;
            series56.IsVisibleInLegend = false;
            series56.Legend = "Legend1";
            series56.Name = "Series6";
            series57.ChartArea = "ChartArea1";
            series57.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series57.Color = System.Drawing.Color.Red;
            series57.IsVisibleInLegend = false;
            series57.Legend = "Legend1";
            series57.Name = "Series7";
            series57.YValuesPerPoint = 2;
            this.chrtLines.Series.Add(series51);
            this.chrtLines.Series.Add(series52);
            this.chrtLines.Series.Add(series53);
            this.chrtLines.Series.Add(series54);
            this.chrtLines.Series.Add(series55);
            this.chrtLines.Series.Add(series56);
            this.chrtLines.Series.Add(series57);
            this.chrtLines.Size = new System.Drawing.Size(856, 363);
            this.chrtLines.TabIndex = 0;
            this.chrtLines.TabStop = false;
            this.chrtLines.Text = "chart1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Y=K";
            // 
            // txtKNumerator
            // 
            this.txtKNumerator.Location = new System.Drawing.Point(61, 23);
            this.txtKNumerator.Name = "txtKNumerator";
            this.txtKNumerator.Size = new System.Drawing.Size(30, 20);
            this.txtKNumerator.TabIndex = 0;
            this.txtKNumerator.Leave += new System.EventHandler(this.txtKNumerator_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(97, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "/";
            // 
            // txtKDenominator
            // 
            this.txtKDenominator.Location = new System.Drawing.Point(121, 23);
            this.txtKDenominator.Name = "txtKDenominator";
            this.txtKDenominator.Size = new System.Drawing.Size(30, 20);
            this.txtKDenominator.TabIndex = 1;
            this.txtKDenominator.Leave += new System.EventHandler(this.txtKDenominator_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(157, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "L";
            // 
            // txtLDenominator
            // 
            this.txtLDenominator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(241)))), ((int)(((byte)(222)))));
            this.txtLDenominator.Enabled = false;
            this.txtLDenominator.Location = new System.Drawing.Point(236, 23);
            this.txtLDenominator.Name = "txtLDenominator";
            this.txtLDenominator.Size = new System.Drawing.Size(30, 20);
            this.txtLDenominator.TabIndex = 8;
            this.txtLDenominator.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(212, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 25);
            this.label4.TabIndex = 7;
            this.label4.Text = "/";
            // 
            // txtLNumerator
            // 
            this.txtLNumerator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(241)))), ((int)(((byte)(222)))));
            this.txtLNumerator.Enabled = false;
            this.txtLNumerator.Location = new System.Drawing.Point(176, 23);
            this.txtLNumerator.Name = "txtLNumerator";
            this.txtLNumerator.Size = new System.Drawing.Size(30, 20);
            this.txtLNumerator.TabIndex = 6;
            this.txtLNumerator.TabStop = false;
            // 
            // txtSmlKDenominator
            // 
            this.txtSmlKDenominator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(241)))), ((int)(((byte)(222)))));
            this.txtSmlKDenominator.Enabled = false;
            this.txtSmlKDenominator.Location = new System.Drawing.Point(121, 81);
            this.txtSmlKDenominator.Name = "txtSmlKDenominator";
            this.txtSmlKDenominator.Size = new System.Drawing.Size(30, 20);
            this.txtSmlKDenominator.TabIndex = 12;
            this.txtSmlKDenominator.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(97, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 25);
            this.label5.TabIndex = 11;
            this.label5.Text = "/";
            // 
            // txtSmlKNumerator
            // 
            this.txtSmlKNumerator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(241)))), ((int)(((byte)(222)))));
            this.txtSmlKNumerator.Enabled = false;
            this.txtSmlKNumerator.Location = new System.Drawing.Point(61, 81);
            this.txtSmlKNumerator.Name = "txtSmlKNumerator";
            this.txtSmlKNumerator.Size = new System.Drawing.Size(30, 20);
            this.txtSmlKNumerator.TabIndex = 10;
            this.txtSmlKNumerator.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(13, 91);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 25);
            this.label6.TabIndex = 9;
            this.label6.Text = "y=k";
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(122)))), ((int)(((byte)(95)))));
            this.btnStart.Location = new System.Drawing.Point(197, 157);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(106, 66);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // btnSkip
            // 
            this.btnSkip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(122)))), ((int)(((byte)(95)))));
            this.btnSkip.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSkip.Enabled = false;
            this.btnSkip.Location = new System.Drawing.Point(197, 229);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(106, 66);
            this.btnSkip.TabIndex = 6;
            this.btnSkip.Text = "Skip";
            this.btnSkip.UseVisualStyleBackColor = false;
            this.btnSkip.Visible = false;
            this.btnSkip.Click += new System.EventHandler(this.BtnSkip_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(122)))), ((int)(((byte)(95)))));
            this.btnReset.Location = new System.Drawing.Point(197, 301);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(106, 66);
            this.btnReset.TabIndex = 13;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // chrtC
            // 
            chartArea22.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea22.AxisX.MajorGrid.Enabled = false;
            chartArea22.AxisY.MajorGrid.Enabled = false;
            chartArea22.Name = "ChartArea1";
            this.chrtC.ChartAreas.Add(chartArea22);
            legend22.Name = "Legend1";
            this.chrtC.Legends.Add(legend22);
            this.chrtC.Location = new System.Drawing.Point(319, 436);
            this.chrtC.Name = "chrtC";
            series58.ChartArea = "ChartArea1";
            series58.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series58.Color = System.Drawing.Color.Black;
            series58.Legend = "Legend1";
            series58.Name = "C";
            this.chrtC.Series.Add(series58);
            this.chrtC.Size = new System.Drawing.Size(855, 124);
            this.chrtC.TabIndex = 19;
            this.chrtC.TabStop = false;
            this.chrtC.Text = "chart2";
            this.chrtC.Visible = false;
            // 
            // chrtI
            // 
            chartArea23.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea23.AxisX2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea23.AxisY.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea23.AxisY.MajorGrid.Enabled = false;
            chartArea23.AxisY2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea23.Name = "ChartArea1";
            this.chrtI.ChartAreas.Add(chartArea23);
            legend23.Name = "Legend1";
            this.chrtI.Legends.Add(legend23);
            this.chrtI.Location = new System.Drawing.Point(319, 566);
            this.chrtI.Name = "chrtI";
            series59.ChartArea = "ChartArea1";
            series59.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series59.Color = System.Drawing.Color.Black;
            series59.Legend = "Legend1";
            series59.Name = "I";
            this.chrtI.Series.Add(series59);
            this.chrtI.Size = new System.Drawing.Size(855, 124);
            this.chrtI.TabIndex = 20;
            this.chrtI.TabStop = false;
            this.chrtI.Text = "chart3";
            this.chrtI.Visible = false;
            // 
            // chrtY
            // 
            chartArea24.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea24.AxisY.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea24.AxisY.MajorGrid.Enabled = false;
            chartArea24.Name = "ChartArea1";
            this.chrtY.ChartAreas.Add(chartArea24);
            legend24.Name = "Legend1";
            this.chrtY.Legends.Add(legend24);
            this.chrtY.Location = new System.Drawing.Point(319, 696);
            this.chrtY.Name = "chrtY";
            series60.ChartArea = "ChartArea1";
            series60.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series60.Color = System.Drawing.Color.Black;
            series60.Legend = "Legend1";
            series60.Name = "y";
            this.chrtY.Series.Add(series60);
            this.chrtY.Size = new System.Drawing.Size(855, 124);
            this.chrtY.TabIndex = 21;
            this.chrtY.TabStop = false;
            this.chrtY.Text = "chart4";
            this.chrtY.Visible = false;
            // 
            // nudS
            // 
            this.nudS.DecimalPlaces = 3;
            this.nudS.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.nudS.Location = new System.Drawing.Point(52, 157);
            this.nudS.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            65536});
            this.nudS.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudS.Name = "nudS";
            this.nudS.Size = new System.Drawing.Size(120, 20);
            this.nudS.TabIndex = 2;
            this.nudS.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudS.ValueChanged += new System.EventHandler(this.nudS_ValueChanged);
            // 
            // nudN
            // 
            this.nudN.DecimalPlaces = 3;
            this.nudN.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudN.Location = new System.Drawing.Point(52, 207);
            this.nudN.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.nudN.Name = "nudN";
            this.nudN.Size = new System.Drawing.Size(120, 20);
            this.nudN.TabIndex = 3;
            this.nudN.ValueChanged += new System.EventHandler(this.nudN_ValueChanged);
            // 
            // nudDelta
            // 
            this.nudDelta.DecimalPlaces = 3;
            this.nudDelta.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.nudDelta.Location = new System.Drawing.Point(52, 257);
            this.nudDelta.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            65536});
            this.nudDelta.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudDelta.Name = "nudDelta";
            this.nudDelta.Size = new System.Drawing.Size(120, 20);
            this.nudDelta.TabIndex = 4;
            this.nudDelta.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudDelta.ValueChanged += new System.EventHandler(this.nudDelta_ValueChanged);
            // 
            // btnOption1
            // 
            this.btnOption1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(122)))), ((int)(((byte)(95)))));
            this.btnOption1.Location = new System.Drawing.Point(354, 379);
            this.btnOption1.Name = "btnOption1";
            this.btnOption1.Size = new System.Drawing.Size(95, 51);
            this.btnOption1.TabIndex = 7;
            this.btnOption1.Text = "Option1";
            this.btnOption1.UseVisualStyleBackColor = false;
            this.btnOption1.Visible = false;
            this.btnOption1.Click += new System.EventHandler(this.btnOption1_Click);
            // 
            // btnOption2
            // 
            this.btnOption2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(122)))), ((int)(((byte)(95)))));
            this.btnOption2.Location = new System.Drawing.Point(531, 379);
            this.btnOption2.Name = "btnOption2";
            this.btnOption2.Size = new System.Drawing.Size(95, 51);
            this.btnOption2.TabIndex = 8;
            this.btnOption2.Text = "Option2";
            this.btnOption2.UseVisualStyleBackColor = false;
            this.btnOption2.Visible = false;
            this.btnOption2.Click += new System.EventHandler(this.btnOption2_Click);
            // 
            // btnOption3
            // 
            this.btnOption3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(122)))), ((int)(((byte)(95)))));
            this.btnOption3.Location = new System.Drawing.Point(889, 379);
            this.btnOption3.Name = "btnOption3";
            this.btnOption3.Size = new System.Drawing.Size(95, 51);
            this.btnOption3.TabIndex = 10;
            this.btnOption3.Text = "Option3";
            this.btnOption3.UseVisualStyleBackColor = false;
            this.btnOption3.Visible = false;
            this.btnOption3.Click += new System.EventHandler(this.btnOption3_Click);
            // 
            // btnOption4
            // 
            this.btnOption4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(122)))), ((int)(((byte)(95)))));
            this.btnOption4.Location = new System.Drawing.Point(1056, 379);
            this.btnOption4.Name = "btnOption4";
            this.btnOption4.Size = new System.Drawing.Size(95, 51);
            this.btnOption4.TabIndex = 11;
            this.btnOption4.Text = "Option4";
            this.btnOption4.UseVisualStyleBackColor = false;
            this.btnOption4.Visible = false;
            this.btnOption4.Click += new System.EventHandler(this.btnOption4_Click);
            // 
            // btnApplyChanges
            // 
            this.btnApplyChanges.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(122)))), ((int)(((byte)(95)))));
            this.btnApplyChanges.Location = new System.Drawing.Point(46, 289);
            this.btnApplyChanges.Name = "btnApplyChanges";
            this.btnApplyChanges.Size = new System.Drawing.Size(126, 81);
            this.btnApplyChanges.TabIndex = 12;
            this.btnApplyChanges.Text = "Apply Changes";
            this.btnApplyChanges.UseVisualStyleBackColor = false;
            this.btnApplyChanges.Visible = false;
            this.btnApplyChanges.Click += new System.EventHandler(this.btnApplyChanges_Click);
            // 
            // btnAnswer
            // 
            this.btnAnswer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(122)))), ((int)(((byte)(95)))));
            this.btnAnswer.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnswer.Location = new System.Drawing.Point(683, 380);
            this.btnAnswer.Name = "btnAnswer";
            this.btnAnswer.Size = new System.Drawing.Size(136, 50);
            this.btnAnswer.TabIndex = 9;
            this.btnAnswer.Text = "button1";
            this.btnAnswer.UseVisualStyleBackColor = false;
            this.btnAnswer.Visible = false;
            this.btnAnswer.Click += new System.EventHandler(this.btnAnswer_Click);
            // 
            // lblDelta
            // 
            this.lblDelta.AutoSize = true;
            this.lblDelta.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDelta.Location = new System.Drawing.Point(12, 251);
            this.lblDelta.Name = "lblDelta";
            this.lblDelta.Size = new System.Drawing.Size(26, 24);
            this.lblDelta.TabIndex = 34;
            this.lblDelta.Text = "δ:";
            // 
            // lblN
            // 
            this.lblN.AutoSize = true;
            this.lblN.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblN.Location = new System.Drawing.Point(12, 201);
            this.lblN.Name = "lblN";
            this.lblN.Size = new System.Drawing.Size(26, 24);
            this.lblN.TabIndex = 33;
            this.lblN.Text = "n:";
            // 
            // lblS
            // 
            this.lblS.AutoSize = true;
            this.lblS.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblS.Location = new System.Drawing.Point(12, 151);
            this.lblS.Name = "lblS";
            this.lblS.Size = new System.Drawing.Size(24, 24);
            this.lblS.TabIndex = 32;
            this.lblS.Text = "s:";
            // 
            // lblMsgbox
            // 
            this.lblMsgbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsgbox.Location = new System.Drawing.Point(16, 436);
            this.lblMsgbox.Name = "lblMsgbox";
            this.lblMsgbox.Size = new System.Drawing.Size(271, 244);
            this.lblMsgbox.TabIndex = 35;
            this.lblMsgbox.Text = "label7";
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(122)))), ((int)(((byte)(95)))));
            this.btnOk.Location = new System.Drawing.Point(97, 696);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 36;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Visible = false;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(21)))), ((int)(((byte)(135)))));
            this.CancelButton = this.btnSkip;
            this.ClientSize = new System.Drawing.Size(1189, 833);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblMsgbox);
            this.Controls.Add(this.lblDelta);
            this.Controls.Add(this.lblN);
            this.Controls.Add(this.lblS);
            this.Controls.Add(this.btnAnswer);
            this.Controls.Add(this.btnApplyChanges);
            this.Controls.Add(this.btnOption4);
            this.Controls.Add(this.btnOption3);
            this.Controls.Add(this.btnOption2);
            this.Controls.Add(this.btnOption1);
            this.Controls.Add(this.nudDelta);
            this.Controls.Add(this.nudN);
            this.Controls.Add(this.nudS);
            this.Controls.Add(this.chrtY);
            this.Controls.Add(this.chrtI);
            this.Controls.Add(this.chrtC);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnSkip);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtSmlKDenominator);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtSmlKNumerator);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtLDenominator);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtLNumerator);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtKDenominator);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtKNumerator);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chrtLines);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Solow";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.chrtLines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chrtC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chrtI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chrtY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelta)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chrtLines;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtKNumerator;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtKDenominator;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLDenominator;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLNumerator;
        private System.Windows.Forms.TextBox txtSmlKDenominator;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSmlKNumerator;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnSkip;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.DataVisualization.Charting.Chart chrtC;
        private System.Windows.Forms.DataVisualization.Charting.Chart chrtI;
        private System.Windows.Forms.DataVisualization.Charting.Chart chrtY;
        private System.Windows.Forms.NumericUpDown nudS;
        private System.Windows.Forms.NumericUpDown nudN;
        private System.Windows.Forms.NumericUpDown nudDelta;
        private System.Windows.Forms.Button btnOption1;
        private System.Windows.Forms.Button btnOption2;
        private System.Windows.Forms.Button btnOption3;
        private System.Windows.Forms.Button btnOption4;
        private System.Windows.Forms.Button btnApplyChanges;
        private System.Windows.Forms.Button btnAnswer;
        private System.Windows.Forms.Label lblDelta;
        private System.Windows.Forms.Label lblN;
        private System.Windows.Forms.Label lblS;
        private System.Windows.Forms.Label lblMsgbox;
        private System.Windows.Forms.Button btnOk;
    }
}

