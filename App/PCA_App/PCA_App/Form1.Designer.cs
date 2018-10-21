namespace PCA_App
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
            this.components = new System.ComponentModel.Container();
            this.cbxPort = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnStartCam = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOpenCOM = new System.Windows.Forms.Button();
            this.cbxCameras = new System.Windows.Forms.ComboBox();
            this.ibxCamReview = new Emgu.CV.UI.ImageBox();
            this.ibxCamCapture = new Emgu.CV.UI.ImageBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBoxOK = new System.Windows.Forms.TextBox();
            this.textBoxResult = new System.Windows.Forms.TextBox();
            this.btnInspection = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ibxCamReview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ibxCamCapture)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxPort
            // 
            this.cbxPort.FormattingEnabled = true;
            this.cbxPort.Location = new System.Drawing.Point(73, 11);
            this.cbxPort.Name = "cbxPort";
            this.cbxPort.Size = new System.Drawing.Size(93, 21);
            this.cbxPort.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "COM Port";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnStartCam);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnOpenCOM);
            this.groupBox1.Controls.Add(this.cbxCameras);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbxPort);
            this.groupBox1.Location = new System.Drawing.Point(12, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(265, 79);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // btnStartCam
            // 
            this.btnStartCam.Location = new System.Drawing.Point(184, 49);
            this.btnStartCam.Name = "btnStartCam";
            this.btnStartCam.Size = new System.Drawing.Size(75, 23);
            this.btnStartCam.TabIndex = 4;
            this.btnStartCam.Text = "Start";
            this.btnStartCam.UseVisualStyleBackColor = true;
            this.btnStartCam.Click += new System.EventHandler(this.btnStartCam_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Camera";
            // 
            // btnOpenCOM
            // 
            this.btnOpenCOM.Location = new System.Drawing.Point(184, 11);
            this.btnOpenCOM.Name = "btnOpenCOM";
            this.btnOpenCOM.Size = new System.Drawing.Size(75, 23);
            this.btnOpenCOM.TabIndex = 5;
            this.btnOpenCOM.Text = "Open";
            this.btnOpenCOM.UseVisualStyleBackColor = true;
            this.btnOpenCOM.Click += new System.EventHandler(this.btnOpenCOM_Click);
            // 
            // cbxCameras
            // 
            this.cbxCameras.FormattingEnabled = true;
            this.cbxCameras.Location = new System.Drawing.Point(73, 49);
            this.cbxCameras.Name = "cbxCameras";
            this.cbxCameras.Size = new System.Drawing.Size(93, 21);
            this.cbxCameras.TabIndex = 3;
            // 
            // ibxCamReview
            // 
            this.ibxCamReview.Location = new System.Drawing.Point(32, 15);
            this.ibxCamReview.Name = "ibxCamReview";
            this.ibxCamReview.Size = new System.Drawing.Size(332, 262);
            this.ibxCamReview.TabIndex = 2;
            this.ibxCamReview.TabStop = false;
            // 
            // ibxCamCapture
            // 
            this.ibxCamCapture.Location = new System.Drawing.Point(556, 141);
            this.ibxCamCapture.Name = "ibxCamCapture";
            this.ibxCamCapture.Size = new System.Drawing.Size(313, 262);
            this.ibxCamCapture.TabIndex = 2;
            this.ibxCamCapture.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ibxCamReview);
            this.groupBox2.Location = new System.Drawing.Point(138, 126);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(394, 289);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Camera Review";
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(538, 125);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(354, 289);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Capture";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBoxOK);
            this.groupBox4.Controls.Add(this.textBoxResult);
            this.groupBox4.Location = new System.Drawing.Point(313, 22);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(188, 78);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Result";
            // 
            // textBoxOK
            // 
            this.textBoxOK.Location = new System.Drawing.Point(24, 45);
            this.textBoxOK.Name = "textBoxOK";
            this.textBoxOK.Size = new System.Drawing.Size(136, 20);
            this.textBoxOK.TabIndex = 1;
            // 
            // textBoxResult
            // 
            this.textBoxResult.Location = new System.Drawing.Point(24, 19);
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.Size = new System.Drawing.Size(136, 20);
            this.textBoxResult.TabIndex = 0;
            // 
            // btnInspection
            // 
            this.btnInspection.Location = new System.Drawing.Point(12, 141);
            this.btnInspection.Name = "btnInspection";
            this.btnInspection.Size = new System.Drawing.Size(109, 35);
            this.btnInspection.TabIndex = 8;
            this.btnInspection.Text = "START INSPECTION";
            this.btnInspection.UseVisualStyleBackColor = true;
            this.btnInspection.Click += new System.EventHandler(this.btnInspection_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Location = new System.Drawing.Point(12, 194);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(109, 23);
            this.btnExcel.TabIndex = 10;
            this.btnExcel.Text = "EXPORT";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(12, 380);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(109, 23);
            this.button5.TabIndex = 9;
            this.button5.Text = "EXIT";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 619);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnInspection);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.ibxCamCapture);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ibxCamReview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ibxCamCapture)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnOpenCOM;
        private System.Windows.Forms.Button btnStartCam;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxCameras;
        private Emgu.CV.UI.ImageBox ibxCamReview;
        private Emgu.CV.UI.ImageBox ibxCamCapture;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBoxOK;
        private System.Windows.Forms.TextBox textBoxResult;
        private System.Windows.Forms.Button btnInspection;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button button5;

    }
}

