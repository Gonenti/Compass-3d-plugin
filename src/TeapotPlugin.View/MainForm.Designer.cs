namespace TeapotPlugin.View
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.LabelTeapotHeight = new System.Windows.Forms.Label();
            this.LabelBaseCircleRadius = new System.Windows.Forms.Label();
            this.LabelOuterCircleSpoutRadius = new System.Windows.Forms.Label();
            this.LabelSpoutLength = new System.Windows.Forms.Label();
            this.LabelInnerCircleSpout = new System.Windows.Forms.Label();
            this.LabelHandleThickness = new System.Windows.Forms.Label();
            this.TextBoxTeapotHeight = new System.Windows.Forms.TextBox();
            this.TextBoxBaseCircleRadius = new System.Windows.Forms.TextBox();
            this.TextBoxOuterCircleSpout = new System.Windows.Forms.TextBox();
            this.TextBoxLabelSpoutLength = new System.Windows.Forms.TextBox();
            this.TextBoxInnerCircleSpout = new System.Windows.Forms.TextBox();
            this.TextBoxHandleThickness = new System.Windows.Forms.TextBox();
            this.LabelHeightLimits = new System.Windows.Forms.Label();
            this.LabelBaseCircleRadiusLimits = new System.Windows.Forms.Label();
            this.LabelOuterCircleSpoutCircleRadiusLimits = new System.Windows.Forms.Label();
            this.LabelSpoutLengthLimits = new System.Windows.Forms.Label();
            this.LabelInnerCircleSpoutLimists = new System.Windows.Forms.Label();
            this.LabelHandleThicknessLimits = new System.Windows.Forms.Label();
            this.ButtonBuildFigure = new System.Windows.Forms.Button();
            this.LableHandleType = new System.Windows.Forms.Label();
            this.СomboBoxHandleType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // LabelTeapotHeight
            // 
            this.LabelTeapotHeight.AutoSize = true;
            this.LabelTeapotHeight.Location = new System.Drawing.Point(12, 46);
            this.LabelTeapotHeight.Name = "LabelTeapotHeight";
            this.LabelTeapotHeight.Size = new System.Drawing.Size(56, 20);
            this.LabelTeapotHeight.TabIndex = 0;
            this.LabelTeapotHeight.Text = "Height";
            // 
            // LabelBaseCircleRadius
            // 
            this.LabelBaseCircleRadius.AutoSize = true;
            this.LabelBaseCircleRadius.Location = new System.Drawing.Point(12, 78);
            this.LabelBaseCircleRadius.Name = "LabelBaseCircleRadius";
            this.LabelBaseCircleRadius.Size = new System.Drawing.Size(183, 20);
            this.LabelBaseCircleRadius.TabIndex = 1;
            this.LabelBaseCircleRadius.Text = "Radius of the base circle";
            // 
            // LabelOuterCircleSpoutRadius
            // 
            this.LabelOuterCircleSpoutRadius.AutoSize = true;
            this.LabelOuterCircleSpoutRadius.Location = new System.Drawing.Point(12, 110);
            this.LabelOuterCircleSpoutRadius.Name = "LabelOuterCircleSpoutRadius";
            this.LabelOuterCircleSpoutRadius.Size = new System.Drawing.Size(206, 20);
            this.LabelOuterCircleSpoutRadius.TabIndex = 2;
            this.LabelOuterCircleSpoutRadius.Text = "The outer circle of the spout";
            // 
            // LabelSpoutLength
            // 
            this.LabelSpoutLength.AutoSize = true;
            this.LabelSpoutLength.Location = new System.Drawing.Point(12, 142);
            this.LabelSpoutLength.Name = "LabelSpoutLength";
            this.LabelSpoutLength.Size = new System.Drawing.Size(97, 20);
            this.LabelSpoutLength.TabIndex = 3;
            this.LabelSpoutLength.Text = "spout length";
            // 
            // LabelInnerCircleSpout
            // 
            this.LabelInnerCircleSpout.AutoSize = true;
            this.LabelInnerCircleSpout.Location = new System.Drawing.Point(12, 174);
            this.LabelInnerCircleSpout.Name = "LabelInnerCircleSpout";
            this.LabelInnerCircleSpout.Size = new System.Drawing.Size(204, 20);
            this.LabelInnerCircleSpout.TabIndex = 4;
            this.LabelInnerCircleSpout.Text = "The inner circle of the spout";
            // 
            // LabelHandleThickness
            // 
            this.LabelHandleThickness.AutoSize = true;
            this.LabelHandleThickness.Location = new System.Drawing.Point(12, 206);
            this.LabelHandleThickness.Name = "LabelHandleThickness";
            this.LabelHandleThickness.Size = new System.Drawing.Size(131, 20);
            this.LabelHandleThickness.TabIndex = 5;
            this.LabelHandleThickness.Text = "Handle thickness";
            // 
            // TextBoxTeapotHeight
            // 
            this.TextBoxTeapotHeight.Location = new System.Drawing.Point(293, 46);
            this.TextBoxTeapotHeight.Name = "TextBoxTeapotHeight";
            this.TextBoxTeapotHeight.Size = new System.Drawing.Size(100, 26);
            this.TextBoxTeapotHeight.TabIndex = 6;
            this.TextBoxTeapotHeight.Text = "180";
            this.TextBoxTeapotHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TextBoxTeapotHeight.TextChanged += new System.EventHandler(this.TextBoxTeapotHeight_TextChanged);
            // 
            // TextBoxBaseCircleRadius
            // 
            this.TextBoxBaseCircleRadius.Location = new System.Drawing.Point(293, 78);
            this.TextBoxBaseCircleRadius.Name = "TextBoxBaseCircleRadius";
            this.TextBoxBaseCircleRadius.Size = new System.Drawing.Size(100, 26);
            this.TextBoxBaseCircleRadius.TabIndex = 7;
            this.TextBoxBaseCircleRadius.Text = "90";
            this.TextBoxBaseCircleRadius.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TextBoxBaseCircleRadius.TextChanged += new System.EventHandler(this.TextBoxBaseCircleRadius_TextChanged);
            // 
            // TextBoxOuterCircleSpout
            // 
            this.TextBoxOuterCircleSpout.Location = new System.Drawing.Point(293, 110);
            this.TextBoxOuterCircleSpout.Name = "TextBoxOuterCircleSpout";
            this.TextBoxOuterCircleSpout.Size = new System.Drawing.Size(100, 26);
            this.TextBoxOuterCircleSpout.TabIndex = 8;
            this.TextBoxOuterCircleSpout.Text = "10";
            this.TextBoxOuterCircleSpout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TextBoxOuterCircleSpout.TextChanged += new System.EventHandler(this.TextBoxOuterCircleSpoutCircleRadius_TextChanged);
            // 
            // TextBoxLabelSpoutLength
            // 
            this.TextBoxLabelSpoutLength.Location = new System.Drawing.Point(293, 142);
            this.TextBoxLabelSpoutLength.Name = "TextBoxLabelSpoutLength";
            this.TextBoxLabelSpoutLength.Size = new System.Drawing.Size(100, 26);
            this.TextBoxLabelSpoutLength.TabIndex = 9;
            this.TextBoxLabelSpoutLength.Text = "136.4";
            this.TextBoxLabelSpoutLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TextBoxLabelSpoutLength.TextChanged += new System.EventHandler(this.TextBoxLabelSpoutLength_TextChanged);
            // 
            // TextBoxInnerCircleSpout
            // 
            this.TextBoxInnerCircleSpout.Location = new System.Drawing.Point(293, 174);
            this.TextBoxInnerCircleSpout.Name = "TextBoxInnerCircleSpout";
            this.TextBoxInnerCircleSpout.Size = new System.Drawing.Size(100, 26);
            this.TextBoxInnerCircleSpout.TabIndex = 10;
            this.TextBoxInnerCircleSpout.Text = "8";
            this.TextBoxInnerCircleSpout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TextBoxInnerCircleSpout.TextChanged += new System.EventHandler(this.TextBoxInnerCircleSpout_TextChanged);
            // 
            // TextBoxHandleThickness
            // 
            this.TextBoxHandleThickness.Location = new System.Drawing.Point(293, 206);
            this.TextBoxHandleThickness.Name = "TextBoxHandleThickness";
            this.TextBoxHandleThickness.Size = new System.Drawing.Size(100, 26);
            this.TextBoxHandleThickness.TabIndex = 11;
            this.TextBoxHandleThickness.Text = "10";
            this.TextBoxHandleThickness.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TextBoxHandleThickness.TextChanged += new System.EventHandler(this.TextHandleThickness_TextChanged);
            // 
            // LabelHeightLimits
            // 
            this.LabelHeightLimits.AutoSize = true;
            this.LabelHeightLimits.Location = new System.Drawing.Point(437, 46);
            this.LabelHeightLimits.Name = "LabelHeightLimits";
            this.LabelHeightLimits.Size = new System.Drawing.Size(151, 20);
            this.LabelHeightLimits.TabIndex = 12;
            this.LabelHeightLimits.Text = "from 100 to 200 mm";
            // 
            // LabelBaseCircleRadiusLimits
            // 
            this.LabelBaseCircleRadiusLimits.AutoSize = true;
            this.LabelBaseCircleRadiusLimits.Location = new System.Drawing.Point(437, 81);
            this.LabelBaseCircleRadiusLimits.Name = "LabelBaseCircleRadiusLimits";
            this.LabelBaseCircleRadiusLimits.Size = new System.Drawing.Size(142, 20);
            this.LabelBaseCircleRadiusLimits.TabIndex = 13;
            this.LabelBaseCircleRadiusLimits.Text = "from 80 to 100 mm";
            // 
            // LabelOuterCircleSpoutCircleRadiusLimits
            // 
            this.LabelOuterCircleSpoutCircleRadiusLimits.AutoSize = true;
            this.LabelOuterCircleSpoutCircleRadiusLimits.Location = new System.Drawing.Point(437, 113);
            this.LabelOuterCircleSpoutCircleRadiusLimits.Name = "LabelOuterCircleSpoutCircleRadiusLimits";
            this.LabelOuterCircleSpoutCircleRadiusLimits.Size = new System.Drawing.Size(133, 20);
            this.LabelOuterCircleSpoutCircleRadiusLimits.TabIndex = 14;
            this.LabelOuterCircleSpoutCircleRadiusLimits.Text = "from 10 to 20 mm";
            // 
            // LabelSpoutLengthLimits
            // 
            this.LabelSpoutLengthLimits.AutoSize = true;
            this.LabelSpoutLengthLimits.Location = new System.Drawing.Point(437, 142);
            this.LabelSpoutLengthLimits.Name = "LabelSpoutLengthLimits";
            this.LabelSpoutLengthLimits.Size = new System.Drawing.Size(142, 20);
            this.LabelSpoutLengthLimits.TabIndex = 15;
            this.LabelSpoutLengthLimits.Text = "from 50 to 150 mm";
            // 
            // LabelInnerCircleSpoutLimists
            // 
            this.LabelInnerCircleSpoutLimists.AutoSize = true;
            this.LabelInnerCircleSpoutLimists.Location = new System.Drawing.Point(437, 177);
            this.LabelInnerCircleSpoutLimists.Name = "LabelInnerCircleSpoutLimists";
            this.LabelInnerCircleSpoutLimists.Size = new System.Drawing.Size(115, 20);
            this.LabelInnerCircleSpoutLimists.TabIndex = 16;
            this.LabelInnerCircleSpoutLimists.Text = "from 5 to 9 mm";
            // 
            // LabelHandleThicknessLimits
            // 
            this.LabelHandleThicknessLimits.AutoSize = true;
            this.LabelHandleThicknessLimits.Location = new System.Drawing.Point(437, 212);
            this.LabelHandleThicknessLimits.Name = "LabelHandleThicknessLimits";
            this.LabelHandleThicknessLimits.Size = new System.Drawing.Size(150, 20);
            this.LabelHandleThicknessLimits.TabIndex = 17;
            this.LabelHandleThicknessLimits.Text = "from 5.4 to 11.7 mm";
            // 
            // ButtonBuildFigure
            // 
            this.ButtonBuildFigure.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ButtonBuildFigure.Location = new System.Drawing.Point(497, 327);
            this.ButtonBuildFigure.Name = "ButtonBuildFigure";
            this.ButtonBuildFigure.Size = new System.Drawing.Size(90, 40);
            this.ButtonBuildFigure.TabIndex = 18;
            this.ButtonBuildFigure.Text = "Build";
            this.ButtonBuildFigure.UseVisualStyleBackColor = true;
            this.ButtonBuildFigure.Click += new System.EventHandler(this.ButtonBuildFigure_Click);
            // 
            // LableHandleType
            // 
            this.LableHandleType.AutoSize = true;
            this.LableHandleType.Location = new System.Drawing.Point(12, 237);
            this.LableHandleType.Name = "LableHandleType";
            this.LableHandleType.Size = new System.Drawing.Size(181, 20);
            this.LableHandleType.TabIndex = 20;
            this.LableHandleType.Text = "The shape of the handle";
            // 
            // СomboBoxHandleType
            // 
            this.СomboBoxHandleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.СomboBoxHandleType.FormattingEnabled = true;
            this.СomboBoxHandleType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.СomboBoxHandleType.IntegralHeight = false;
            this.СomboBoxHandleType.Items.AddRange(new object[] {
            "Circle",
            "Square"});
            this.СomboBoxHandleType.Location = new System.Drawing.Point(293, 239);
            this.СomboBoxHandleType.Name = "СomboBoxHandleType";
            this.СomboBoxHandleType.Size = new System.Drawing.Size(100, 28);
            this.СomboBoxHandleType.Sorted = true;
            this.СomboBoxHandleType.TabIndex = 21;
            this.СomboBoxHandleType.SelectedIndexChanged += new System.EventHandler(this.СomboBoxHandleType_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 379);
            this.Controls.Add(this.СomboBoxHandleType);
            this.Controls.Add(this.LableHandleType);
            this.Controls.Add(this.ButtonBuildFigure);
            this.Controls.Add(this.LabelHandleThicknessLimits);
            this.Controls.Add(this.LabelInnerCircleSpoutLimists);
            this.Controls.Add(this.LabelSpoutLengthLimits);
            this.Controls.Add(this.LabelOuterCircleSpoutCircleRadiusLimits);
            this.Controls.Add(this.LabelBaseCircleRadiusLimits);
            this.Controls.Add(this.LabelHeightLimits);
            this.Controls.Add(this.TextBoxHandleThickness);
            this.Controls.Add(this.TextBoxInnerCircleSpout);
            this.Controls.Add(this.TextBoxLabelSpoutLength);
            this.Controls.Add(this.TextBoxOuterCircleSpout);
            this.Controls.Add(this.TextBoxBaseCircleRadius);
            this.Controls.Add(this.TextBoxTeapotHeight);
            this.Controls.Add(this.LabelHandleThickness);
            this.Controls.Add(this.LabelInnerCircleSpout);
            this.Controls.Add(this.LabelSpoutLength);
            this.Controls.Add(this.LabelOuterCircleSpoutRadius);
            this.Controls.Add(this.LabelBaseCircleRadius);
            this.Controls.Add(this.LabelTeapotHeight);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Teapot builder ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelTeapotHeight;
        private System.Windows.Forms.Label LabelBaseCircleRadius;
        private System.Windows.Forms.Label LabelOuterCircleSpoutRadius;
        private System.Windows.Forms.Label LabelSpoutLength;
        private System.Windows.Forms.Label LabelInnerCircleSpout;
        private System.Windows.Forms.Label LabelHandleThickness;
        private System.Windows.Forms.TextBox TextBoxTeapotHeight;
        private System.Windows.Forms.TextBox TextBoxBaseCircleRadius;
        private System.Windows.Forms.TextBox TextBoxOuterCircleSpout;
        private System.Windows.Forms.TextBox TextBoxLabelSpoutLength;
        private System.Windows.Forms.TextBox TextBoxInnerCircleSpout;
        private System.Windows.Forms.TextBox TextBoxHandleThickness;
        private System.Windows.Forms.Label LabelHeightLimits;
        private System.Windows.Forms.Label LabelBaseCircleRadiusLimits;
        private System.Windows.Forms.Label LabelOuterCircleSpoutCircleRadiusLimits;
        private System.Windows.Forms.Label LabelSpoutLengthLimits;
        private System.Windows.Forms.Label LabelInnerCircleSpoutLimists;
        private System.Windows.Forms.Label LabelHandleThicknessLimits;
        private System.Windows.Forms.Button ButtonBuildFigure;
        private System.Windows.Forms.Label LableHandleType;
        private System.Windows.Forms.ComboBox СomboBoxHandleType;
    }
}

