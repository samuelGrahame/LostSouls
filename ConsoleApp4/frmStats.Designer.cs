﻿namespace ConsoleApp4
{
    partial class frmStats
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
            this.labelStrength = new System.Windows.Forms.Label();
            this.labelInteligence = new System.Windows.Forms.Label();
            this.labelDefence = new System.Windows.Forms.Label();
            this.labelOrens = new System.Windows.Forms.Label();
            this.labelHealth = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.labelPoints = new System.Windows.Forms.Label();
            this.labelLevel = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelRace = new System.Windows.Forms.Label();
            this.labelXP = new System.Windows.Forms.Label();
            this.labelGender = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataEquipt = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataInventory = new System.Windows.Forms.DataGridView();
            this.pictureBox1 = new ConsoleApp4.PictureBoxWithInterpolationMode();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataEquipt)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataInventory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelStrength
            // 
            this.labelStrength.AutoSize = true;
            this.labelStrength.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStrength.ForeColor = System.Drawing.Color.Navy;
            this.labelStrength.Location = new System.Drawing.Point(57, 36);
            this.labelStrength.Name = "labelStrength";
            this.labelStrength.Size = new System.Drawing.Size(75, 20);
            this.labelStrength.TabIndex = 0;
            this.labelStrength.Text = "Strength:";
            // 
            // labelInteligence
            // 
            this.labelInteligence.AutoSize = true;
            this.labelInteligence.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInteligence.ForeColor = System.Drawing.Color.Cyan;
            this.labelInteligence.Location = new System.Drawing.Point(57, 56);
            this.labelInteligence.Name = "labelInteligence";
            this.labelInteligence.Size = new System.Drawing.Size(91, 20);
            this.labelInteligence.TabIndex = 1;
            this.labelInteligence.Text = "Inteligence:";
            // 
            // labelDefence
            // 
            this.labelDefence.AutoSize = true;
            this.labelDefence.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDefence.ForeColor = System.Drawing.Color.Blue;
            this.labelDefence.Location = new System.Drawing.Point(57, 76);
            this.labelDefence.Name = "labelDefence";
            this.labelDefence.Size = new System.Drawing.Size(74, 20);
            this.labelDefence.TabIndex = 2;
            this.labelDefence.Text = "Defence:";
            // 
            // labelOrens
            // 
            this.labelOrens.AutoSize = true;
            this.labelOrens.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOrens.ForeColor = System.Drawing.Color.Gold;
            this.labelOrens.Location = new System.Drawing.Point(57, 95);
            this.labelOrens.Name = "labelOrens";
            this.labelOrens.Size = new System.Drawing.Size(56, 20);
            this.labelOrens.TabIndex = 3;
            this.labelOrens.Text = "Orens:";
            // 
            // labelHealth
            // 
            this.labelHealth.AutoSize = true;
            this.labelHealth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHealth.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelHealth.Location = new System.Drawing.Point(57, 16);
            this.labelHealth.Name = "labelHealth";
            this.labelHealth.Size = new System.Drawing.Size(60, 20);
            this.labelHealth.TabIndex = 4;
            this.labelHealth.Text = "Health:";
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.LimeGreen;
            this.button1.Location = new System.Drawing.Point(10, 36);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 20);
            this.button1.TabIndex = 5;
            this.button1.Text = "+";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.LimeGreen;
            this.button2.Location = new System.Drawing.Point(10, 56);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(32, 20);
            this.button2.TabIndex = 6;
            this.button2.Text = "+";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.LimeGreen;
            this.button3.Location = new System.Drawing.Point(10, 76);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(32, 20);
            this.button3.TabIndex = 7;
            this.button3.Text = "+";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // labelPoints
            // 
            this.labelPoints.AutoSize = true;
            this.labelPoints.Location = new System.Drawing.Point(17, 20);
            this.labelPoints.Name = "labelPoints";
            this.labelPoints.Size = new System.Drawing.Size(13, 13);
            this.labelPoints.TabIndex = 8;
            this.labelPoints.Text = "0";
            // 
            // labelLevel
            // 
            this.labelLevel.AutoSize = true;
            this.labelLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLevel.ForeColor = System.Drawing.Color.Magenta;
            this.labelLevel.Location = new System.Drawing.Point(57, 115);
            this.labelLevel.Name = "labelLevel";
            this.labelLevel.Size = new System.Drawing.Size(54, 20);
            this.labelLevel.TabIndex = 9;
            this.labelLevel.Text = "Level: ";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelName.ForeColor = System.Drawing.Color.Black;
            this.labelName.Location = new System.Drawing.Point(57, 153);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(59, 20);
            this.labelName.TabIndex = 10;
            this.labelName.Text = "Name: ";
            // 
            // labelRace
            // 
            this.labelRace.AutoSize = true;
            this.labelRace.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRace.ForeColor = System.Drawing.Color.Black;
            this.labelRace.Location = new System.Drawing.Point(57, 173);
            this.labelRace.Name = "labelRace";
            this.labelRace.Size = new System.Drawing.Size(55, 20);
            this.labelRace.TabIndex = 11;
            this.labelRace.Text = "Race: ";
            // 
            // labelXP
            // 
            this.labelXP.AutoSize = true;
            this.labelXP.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelXP.ForeColor = System.Drawing.Color.DarkGray;
            this.labelXP.Location = new System.Drawing.Point(76, 135);
            this.labelXP.Name = "labelXP";
            this.labelXP.Size = new System.Drawing.Size(72, 18);
            this.labelXP.TabIndex = 12;
            this.labelXP.Text = "XP: 0 / 84";
            // 
            // labelGender
            // 
            this.labelGender.AutoSize = true;
            this.labelGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGender.ForeColor = System.Drawing.Color.Black;
            this.labelGender.Location = new System.Drawing.Point(57, 193);
            this.labelGender.Name = "labelGender";
            this.labelGender.Size = new System.Drawing.Size(71, 20);
            this.labelGender.TabIndex = 13;
            this.labelGender.Text = "Gender: ";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(5, 234);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(363, 358);
            this.splitContainer1.SplitterDistance = 178;
            this.splitContainer1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataEquipt);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(363, 178);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Character Equipt";
            // 
            // dataEquipt
            // 
            this.dataEquipt.AllowUserToAddRows = false;
            this.dataEquipt.AllowUserToDeleteRows = false;
            this.dataEquipt.AllowUserToResizeRows = false;
            this.dataEquipt.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataEquipt.BackgroundColor = System.Drawing.Color.White;
            this.dataEquipt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataEquipt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataEquipt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataEquipt.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.dataEquipt.Location = new System.Drawing.Point(3, 16);
            this.dataEquipt.MultiSelect = false;
            this.dataEquipt.Name = "dataEquipt";
            this.dataEquipt.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataEquipt.Size = new System.Drawing.Size(357, 159);
            this.dataEquipt.TabIndex = 2;
            this.dataEquipt.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataEquipt_CellBeginEdit);
            this.dataEquipt.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataEquipt_CellMouseDoubleClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataInventory);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(363, 176);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Character Inventory";
            // 
            // dataInventory
            // 
            this.dataInventory.AllowUserToAddRows = false;
            this.dataInventory.AllowUserToDeleteRows = false;
            this.dataInventory.AllowUserToResizeRows = false;
            this.dataInventory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataInventory.BackgroundColor = System.Drawing.Color.White;
            this.dataInventory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataInventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataInventory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataInventory.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.dataInventory.Location = new System.Drawing.Point(3, 16);
            this.dataInventory.MultiSelect = false;
            this.dataInventory.Name = "dataInventory";
            this.dataInventory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataInventory.Size = new System.Drawing.Size(357, 157);
            this.dataInventory.TabIndex = 3;
            this.dataInventory.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataInventory_CellBeginEdit);
            this.dataInventory.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataInventory_CellMouseDoubleClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.pictureBox1.Location = new System.Drawing.Point(3, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(650, 571);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button16);
            this.groupBox3.Controls.Add(this.button17);
            this.groupBox3.Controls.Add(this.button8);
            this.groupBox3.Controls.Add(this.button7);
            this.groupBox3.Controls.Add(this.button5);
            this.groupBox3.Controls.Add(this.button6);
            this.groupBox3.Controls.Add(this.button15);
            this.groupBox3.Controls.Add(this.button13);
            this.groupBox3.Controls.Add(this.button4);
            this.groupBox3.Controls.Add(this.button11);
            this.groupBox3.Controls.Add(this.button10);
            this.groupBox3.Controls.Add(this.button9);
            this.groupBox3.Controls.Add(this.button18);
            this.groupBox3.Controls.Add(this.button14);
            this.groupBox3.Controls.Add(this.button12);
            this.groupBox3.Controls.Add(this.labelPoints);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.labelGender);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.labelStrength);
            this.groupBox3.Controls.Add(this.labelName);
            this.groupBox3.Controls.Add(this.labelRace);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.labelXP);
            this.groupBox3.Controls.Add(this.labelHealth);
            this.groupBox3.Controls.Add(this.labelInteligence);
            this.groupBox3.Controls.Add(this.labelLevel);
            this.groupBox3.Controls.Add(this.labelOrens);
            this.groupBox3.Controls.Add(this.labelDefence);
            this.groupBox3.Location = new System.Drawing.Point(5, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(363, 226);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Details";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.pictureBox1);
            this.groupBox4.Location = new System.Drawing.Point(374, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(656, 590);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Map";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(224, 173);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(45, 23);
            this.button6.TabIndex = 16;
            this.button6.Text = "West";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(268, 195);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(45, 23);
            this.button8.TabIndex = 18;
            this.button8.Text = "South";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(268, 151);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(45, 23);
            this.button4.TabIndex = 19;
            this.button4.Text = "North";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(313, 173);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(45, 23);
            this.button5.TabIndex = 20;
            this.button5.Text = "East";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button7
            // 
            this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.ForeColor = System.Drawing.Color.Red;
            this.button7.Location = new System.Drawing.Point(268, 173);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(46, 23);
            this.button7.TabIndex = 21;
            this.button7.Text = "Attack";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button9
            // 
            this.button9.ForeColor = System.Drawing.Color.Blue;
            this.button9.Location = new System.Drawing.Point(268, 129);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(45, 23);
            this.button9.TabIndex = 22;
            this.button9.Text = "Forest";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.ForeColor = System.Drawing.Color.Green;
            this.button10.Location = new System.Drawing.Point(224, 129);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(45, 23);
            this.button10.TabIndex = 23;
            this.button10.Text = "Yes";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.ForeColor = System.Drawing.Color.Red;
            this.button11.Location = new System.Drawing.Point(312, 129);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(45, 23);
            this.button11.TabIndex = 24;
            this.button11.Text = "No";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button12
            // 
            this.button12.ForeColor = System.Drawing.Color.Green;
            this.button12.Location = new System.Drawing.Point(224, 107);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(45, 23);
            this.button12.TabIndex = 25;
            this.button12.Text = "0";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button13
            // 
            this.button13.ForeColor = System.Drawing.Color.Green;
            this.button13.Location = new System.Drawing.Point(312, 151);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(45, 23);
            this.button13.TabIndex = 26;
            this.button13.Text = "3";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button14
            // 
            this.button14.ForeColor = System.Drawing.Color.Green;
            this.button14.Location = new System.Drawing.Point(312, 107);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(45, 23);
            this.button14.TabIndex = 27;
            this.button14.Text = "1";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // button15
            // 
            this.button15.ForeColor = System.Drawing.Color.Green;
            this.button15.Location = new System.Drawing.Point(224, 151);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(45, 23);
            this.button15.TabIndex = 28;
            this.button15.Text = "2";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // button16
            // 
            this.button16.ForeColor = System.Drawing.Color.Green;
            this.button16.Location = new System.Drawing.Point(224, 195);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(45, 23);
            this.button16.TabIndex = 31;
            this.button16.Text = "4";
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new System.EventHandler(this.button16_Click);
            // 
            // button17
            // 
            this.button17.ForeColor = System.Drawing.Color.Green;
            this.button17.Location = new System.Drawing.Point(312, 195);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(45, 23);
            this.button17.TabIndex = 30;
            this.button17.Text = "5";
            this.button17.UseVisualStyleBackColor = true;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // button18
            // 
            this.button18.ForeColor = System.Drawing.Color.Red;
            this.button18.Location = new System.Drawing.Point(268, 107);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(45, 23);
            this.button18.TabIndex = 29;
            this.button18.Text = "Flee";
            this.button18.UseVisualStyleBackColor = true;
            this.button18.Click += new System.EventHandler(this.button18_Click);
            // 
            // frmStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 604);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmStats";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Information Hud";
            this.Load += new System.EventHandler(this.frmStats_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataEquipt)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataInventory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label labelStrength;
        public System.Windows.Forms.Label labelInteligence;
        public System.Windows.Forms.Label labelDefence;
        public System.Windows.Forms.Label labelOrens;
        public System.Windows.Forms.Label labelHealth;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label labelPoints;
        public System.Windows.Forms.Label labelLevel;
        public System.Windows.Forms.Label labelName;
        public System.Windows.Forms.Label labelRace;
        public System.Windows.Forms.Label labelXP;
        public System.Windows.Forms.Label labelGender;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataEquipt;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataInventory;
        private ConsoleApp4.PictureBoxWithInterpolationMode pictureBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Button button18;
    }
}