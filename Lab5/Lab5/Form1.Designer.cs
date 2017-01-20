namespace Lab5
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toppanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.penWidth = new System.Windows.Forms.ListBox();
            this.fillColor = new System.Windows.Forms.ListBox();
            this.penColor = new System.Windows.Forms.ListBox();
            this.drawGroupBox = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lineButton = new System.Windows.Forms.RadioButton();
            this.rectangleButton = new System.Windows.Forms.RadioButton();
            this.EllipseButton = new System.Windows.Forms.RadioButton();
            this.textButton = new System.Windows.Forms.RadioButton();
            this.fillBox = new System.Windows.Forms.CheckBox();
            this.outlineBox = new System.Windows.Forms.CheckBox();
            this.paintpanel = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.toppanel.SuspendLayout();
            this.drawGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(607, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // toppanel
            // 
            this.toppanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.toppanel.Controls.Add(this.label3);
            this.toppanel.Controls.Add(this.label2);
            this.toppanel.Controls.Add(this.label1);
            this.toppanel.Controls.Add(this.penWidth);
            this.toppanel.Controls.Add(this.fillColor);
            this.toppanel.Controls.Add(this.penColor);
            this.toppanel.Controls.Add(this.drawGroupBox);
            this.toppanel.Controls.Add(this.fillBox);
            this.toppanel.Controls.Add(this.outlineBox);
            this.toppanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.toppanel.Location = new System.Drawing.Point(0, 24);
            this.toppanel.Name = "toppanel";
            this.toppanel.Size = new System.Drawing.Size(607, 245);
            this.toppanel.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(454, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Pen Width";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(341, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Fill Color";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(244, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Pen Color";
            // 
            // penWidth
            // 
            this.penWidth.FormattingEnabled = true;
            this.penWidth.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.penWidth.Location = new System.Drawing.Point(452, 51);
            this.penWidth.Name = "penWidth";
            this.penWidth.Size = new System.Drawing.Size(120, 134);
            this.penWidth.TabIndex = 9;
            this.penWidth.SelectedIndexChanged += new System.EventHandler(this.penWidth_SelectedIndexChanged);
            // 
            // fillColor
            // 
            this.fillColor.FormattingEnabled = true;
            this.fillColor.Items.AddRange(new object[] {
            "White",
            "Black",
            "Red",
            "Blue",
            "Green"});
            this.fillColor.Location = new System.Drawing.Point(344, 51);
            this.fillColor.Name = "fillColor";
            this.fillColor.Size = new System.Drawing.Size(70, 69);
            this.fillColor.TabIndex = 8;
            this.fillColor.SelectedIndexChanged += new System.EventHandler(this.fillColor_SelectedIndexChanged);
            // 
            // penColor
            // 
            this.penColor.FormattingEnabled = true;
            this.penColor.Items.AddRange(new object[] {
            "Black",
            "Red",
            "Blue",
            "Green"});
            this.penColor.Location = new System.Drawing.Point(247, 51);
            this.penColor.Name = "penColor";
            this.penColor.Size = new System.Drawing.Size(70, 69);
            this.penColor.TabIndex = 7;
            this.penColor.SelectedIndexChanged += new System.EventHandler(this.penColor_SelectedIndexChanged);
            // 
            // drawGroupBox
            // 
            this.drawGroupBox.Controls.Add(this.textBox1);
            this.drawGroupBox.Controls.Add(this.lineButton);
            this.drawGroupBox.Controls.Add(this.rectangleButton);
            this.drawGroupBox.Controls.Add(this.EllipseButton);
            this.drawGroupBox.Controls.Add(this.textButton);
            this.drawGroupBox.Location = new System.Drawing.Point(21, 39);
            this.drawGroupBox.Name = "drawGroupBox";
            this.drawGroupBox.Size = new System.Drawing.Size(217, 188);
            this.drawGroupBox.TabIndex = 1;
            this.drawGroupBox.TabStop = false;
            this.drawGroupBox.Text = "Draw";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(32, 125);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(185, 63);
            this.textBox1.TabIndex = 6;
            this.textBox1.WordWrap = false;
            // 
            // lineButton
            // 
            this.lineButton.AutoSize = true;
            this.lineButton.Location = new System.Drawing.Point(32, 26);
            this.lineButton.Name = "lineButton";
            this.lineButton.Size = new System.Drawing.Size(45, 17);
            this.lineButton.TabIndex = 2;
            this.lineButton.TabStop = true;
            this.lineButton.Text = "Line";
            this.lineButton.UseVisualStyleBackColor = true;
            // 
            // rectangleButton
            // 
            this.rectangleButton.AutoSize = true;
            this.rectangleButton.Location = new System.Drawing.Point(32, 49);
            this.rectangleButton.Name = "rectangleButton";
            this.rectangleButton.Size = new System.Drawing.Size(74, 17);
            this.rectangleButton.TabIndex = 3;
            this.rectangleButton.TabStop = true;
            this.rectangleButton.Text = "Rectangle";
            this.rectangleButton.UseVisualStyleBackColor = true;
            // 
            // EllipseButton
            // 
            this.EllipseButton.AutoSize = true;
            this.EllipseButton.Location = new System.Drawing.Point(32, 72);
            this.EllipseButton.Name = "EllipseButton";
            this.EllipseButton.Size = new System.Drawing.Size(55, 17);
            this.EllipseButton.TabIndex = 4;
            this.EllipseButton.TabStop = true;
            this.EllipseButton.Text = "Ellipse";
            this.EllipseButton.UseVisualStyleBackColor = true;
            // 
            // textButton
            // 
            this.textButton.AutoSize = true;
            this.textButton.Location = new System.Drawing.Point(32, 95);
            this.textButton.Name = "textButton";
            this.textButton.Size = new System.Drawing.Size(46, 17);
            this.textButton.TabIndex = 5;
            this.textButton.TabStop = true;
            this.textButton.Text = "Text";
            this.textButton.UseVisualStyleBackColor = true;
            // 
            // fillBox
            // 
            this.fillBox.AutoSize = true;
            this.fillBox.Location = new System.Drawing.Point(318, 155);
            this.fillBox.Name = "fillBox";
            this.fillBox.Size = new System.Drawing.Size(38, 17);
            this.fillBox.TabIndex = 0;
            this.fillBox.Text = "Fill";
            this.fillBox.UseVisualStyleBackColor = true;
            // 
            // outlineBox
            // 
            this.outlineBox.AutoSize = true;
            this.outlineBox.Location = new System.Drawing.Point(318, 179);
            this.outlineBox.Name = "outlineBox";
            this.outlineBox.Size = new System.Drawing.Size(59, 17);
            this.outlineBox.TabIndex = 0;
            this.outlineBox.Text = "Outline";
            this.outlineBox.UseVisualStyleBackColor = true;
            // 
            // paintpanel
            // 
            this.paintpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paintpanel.Location = new System.Drawing.Point(0, 269);
            this.paintpanel.Name = "paintpanel";
            this.paintpanel.Size = new System.Drawing.Size(607, 260);
            this.paintpanel.TabIndex = 0;
            this.paintpanel.Paint += new System.Windows.Forms.PaintEventHandler(this.paintpanel_Paint);
            this.paintpanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.paintpanel_MouseDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 529);
            this.Controls.Add(this.paintpanel);
            this.Controls.Add(this.toppanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Lab 5 by Shivani Bhatia";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toppanel.ResumeLayout(false);
            this.toppanel.PerformLayout();
            this.drawGroupBox.ResumeLayout(false);
            this.drawGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.Panel toppanel;
        private System.Windows.Forms.Panel paintpanel;
        private System.Windows.Forms.CheckBox outlineBox;
        private System.Windows.Forms.RadioButton textButton;
        private System.Windows.Forms.RadioButton EllipseButton;
        private System.Windows.Forms.RadioButton rectangleButton;
        private System.Windows.Forms.RadioButton lineButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox drawGroupBox;
        private System.Windows.Forms.ListBox penColor;
        private System.Windows.Forms.ListBox fillColor;
        private System.Windows.Forms.ListBox penWidth;
        private System.Windows.Forms.CheckBox fillBox;
    }
}

