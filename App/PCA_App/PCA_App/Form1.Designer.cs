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
            this.ibxCamResult = new Emgu.CV.UI.ImageBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ibxCamReview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ibxCamCapture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ibxCamResult)).BeginInit();
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
            this.ibxCamReview.Location = new System.Drawing.Point(169, 133);
            this.ibxCamReview.Name = "ibxCamReview";
            this.ibxCamReview.Size = new System.Drawing.Size(249, 239);
            this.ibxCamReview.TabIndex = 2;
            this.ibxCamReview.TabStop = false;
            // 
            // ibxCamCapture
            // 
            this.ibxCamCapture.Location = new System.Drawing.Point(445, 133);
            this.ibxCamCapture.Name = "ibxCamCapture";
            this.ibxCamCapture.Size = new System.Drawing.Size(281, 238);
            this.ibxCamCapture.TabIndex = 2;
            this.ibxCamCapture.TabStop = false;
            // 
            // ibxCamResult
            // 
            this.ibxCamResult.Location = new System.Drawing.Point(744, 133);
            this.ibxCamResult.Name = "ibxCamResult";
            this.ibxCamResult.Size = new System.Drawing.Size(255, 237);
            this.ibxCamResult.TabIndex = 2;
            this.ibxCamResult.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 392);
            this.Controls.Add(this.ibxCamResult);
            this.Controls.Add(this.ibxCamCapture);
            this.Controls.Add(this.ibxCamReview);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ibxCamReview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ibxCamCapture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ibxCamResult)).EndInit();
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
        private Emgu.CV.UI.ImageBox ibxCamResult;

    }
}

