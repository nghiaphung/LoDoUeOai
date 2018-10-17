using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Timers;

using System.IO;
using System.IO.Ports;
using System.Xml;

using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.Util;
using Emgu.CV.OCR;
using Emgu.CV.UI;


using DirectShowLib;

using Microsoft.Office.Interop.Excel;

using XnaFan.ImageComparison;

using PCA_App.Class;
using PCALib;

namespace PCA_App
{
    public partial class Form1 : Form
    {
        #region ComPort + Camera
        static SerialPort _Com;
        static String _InputData = string.Empty;

        Microsoft.Office.Interop.Excel.Application oXL;
        Microsoft.Office.Interop.Excel._Workbook oWB;
        Microsoft.Office.Interop.Excel._Worksheet oSheet;
        Microsoft.Office.Interop.Excel.Range oRng;

        private bool _CaptureInProgress;

        private Mat _frame;
        private Mat _grayFrame;
        private Mat _smallGrayFrame;
        private Mat _smoothedGrayFrame;
        private Mat _cannyFrame;

        //Procesing image
        private Mat _openImage;
        private Mat _grayImage;
        private Mat _smallGray;
        private Mat _smoothedGray;
        private Mat _cannyImage;
        private Mat _resultImage;

        private Mat modelImage, observedImage;  //Image input
        private Mat cropImage, resultImage;          //Image output
        private Mat textImage, textResult, textCrop;          //Image output
        private Mat subMat, pinoutImage, pinoutResult, pinoutCrop;          //Image output


        //Camera Variable
        private VideoCapture _Capture;
        List<KeyValuePair<int, string>> ListCamerasData = new List<KeyValuePair<int, string>>();

        clsMatrixOperation _objMatrix = new clsMatrixOperation();
        double[][] EigenFaceImage = null;
        double[][] BaseMatrix = null;
        double[][] CopyImageMatrix_forallImages = null;
        double[][] EigenVectorforImage = null;
        double[][] MeanVector = null;
        double[][] MainImageMatrix_forAllImages = null;

        public Form1()
        {
            InitializeComponent();

            //Initialize COM Port
            _Com = new SerialPort();
            _Com.ReadTimeout = 500;
            _Com.WriteTimeout = 500;
            _Com.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            string[] Ports = SerialPort.GetPortNames();
            cbxPort.Items.AddRange(Ports);

            // Create a List to store for ComboCameras
            List<KeyValuePair<int, string>> ListCamerasData = new List<KeyValuePair<int, string>>();
            // Find systems cameras with DirectShow.Net dll
            DsDevice[] _SystemCamereas = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

            int _DeviceIndex = 0;
            foreach (DirectShowLib.DsDevice _Camera in _SystemCamereas)
            {
                ListCamerasData.Add(new KeyValuePair<int, string>(_DeviceIndex, _Camera.Name));
                _DeviceIndex++;
            }
            cbxCameras.DataSource = null;   // Clear the combobox
            cbxCameras.Items.Clear();

            cbxCameras.DataSource = new BindingSource(ListCamerasData, null);   // Bind the combobox
            cbxCameras.DisplayMember = "Value";
            cbxCameras.ValueMember = "Key";

            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Item select
            if (cbxCameras.Items.Count > 0) 
                cbxCameras.SelectedIndex = 0;
            else cbxCameras.SelectedIndex = 0;
            if (cbxPort.Items.Count > 0) 
                cbxPort.SelectedIndex = 0;

            clsMatrixOperation.iDefaultImageCount = PCA_App.Properties.Settings.Default.DefaultNumberOfImages;
            clsMatrixOperation.iDimensionalReduction = PCA_App.Properties.Settings.Default.DReductionVal;
            GetImageMatrix();

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_Com.IsOpen) _Com.Close();
            _Capture.Stop();
            _Capture.Dispose();
        }

        #endregion



        private void btnOpenCOM_Click(object sender, EventArgs e)
        {
            if (!_Com.IsOpen)
            {
                _Com.PortName = cbxPort.Text.ToString();
                _Com.BaudRate = 115200;
                _Com.DataBits = 8;
                _Com.StopBits = StopBits.One;
                _Com.Parity = Parity.None;
                _Com.Handshake = Handshake.None;
                _Com.Open();

                //Change text
                btnOpenCOM.Text = "Close";
            }
            else
            {

                _Com.Close();
                btnOpenCOM.Text = "Open";
            }
        }

        //Data Recieve Function
        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            //if (_InputData == "f") reading = true;

        }

        private void btnStartCam_Click(object sender, EventArgs e)
        {
            try
            {
                _Capture = new VideoCapture(cbxCameras.SelectedIndex);
                _Capture.SetCaptureProperty(CapProp.FrameWidth, 1080);
                _Capture.SetCaptureProperty(CapProp.FrameHeight, 1080);
                _Capture.ImageGrabbed += ProcessFrame;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return;
            }

            if (_Capture != null)
            {
                if (_CaptureInProgress)
                {  //stop the capture
                    btnStartCam.Text = "Start";
                    _Capture.Stop();
                    _Capture.Dispose();
                    _CaptureInProgress = false; //Flag the state of the camera
                    //sttCamera.Text = "Camera OFF";
                }
                else
                {
                    //start the capture
                    btnStartCam.Text = "Stop";
                    _Capture.Start();
                    _CaptureInProgress = true; //Flag the state of the camera
                    //sttCamera.Text = "Camera ON";
                }
            }

        }

        private void ProcessFrame(object sender, EventArgs arg)
        {
            if (_Capture != null && _Capture.Ptr != IntPtr.Zero)
            {
                _Capture.Retrieve(_frame, 0);
                //Show video capture
                ibxCamReview.Image = _frame;
            }
        }

        void GetImageMatrix()
        {
            //Code for Intializing Matrix
            MainImageMatrix_forAllImages = new double[1][];
            double[][] MainImageMatrix_Covariance = new double[1][];
            CopyImageMatrix_forallImages = new double[1][];
            //InitializeMatrix_1(ref MainImageMatrix);
            // Call function to fetch all image in a matrix format
            _objMatrix.InitializeMatrix(ref MainImageMatrix_forAllImages);
            _objMatrix.CopyMatrix(MainImageMatrix_forAllImages, ref CopyImageMatrix_forallImages);
            //Code for finding means vector from the MainImage Matrix
            MeanVector = new double[1][];
            MeanVector = _objMatrix.GetMeanVector(ref MainImageMatrix_forAllImages);

            //Performing Step 2 of Algorithm

            _objMatrix.getMeanAdjustedMatrix(ref MainImageMatrix_forAllImages, MeanVector);

            //Compute Covariance matrix

            MainImageMatrix_Covariance = _objMatrix.getCovarianceMatrix(MainImageMatrix_forAllImages);

            //Computing Eigen values and Eigen vecotors by suding MaPack library
            PCALib.Matrix obj = new Matrix(MainImageMatrix_Covariance);
            IEigenvalueDecomposition EigenVal;
            EigenVal = obj.GetEigenvalueDecomposition();

            // Take only top 10 eigen values
            double[] EigenValforImage = new double[PCA_App.Properties.Settings.Default.DReductionVal];
            double[][] TransposevalImage = new double[PCA_App.Properties.Settings.Default.DReductionVal][];
            EigenFaceImage = new double[PCA_App.Properties.Settings.Default.DReductionVal][];
            EigenVectorforImage = new double[PCA_App.Properties.Settings.Default.DefaultNumberOfImages][];
            // double[][] PixelmatrixforImage = new double[FaceRecognition.Properties.Settings.Default.DefaultNumberOfImages][];
            _objMatrix.GetTopEigenValues(EigenVal.RealEigenvalues, ref EigenValforImage);

            // Taking top 10 Eigen vectors
            PCALib.IMatrix EigenVector = EigenVal.EigenvectorMatrix;

            _objMatrix.GetTopEigenVectors(EigenVector, ref EigenVectorforImage);
            // _objMatrix.Transpose(CopyImageMatrix, ref TransposevalImage);
            _objMatrix.Multiply(MainImageMatrix_forAllImages, EigenVectorforImage, ref EigenFaceImage);

            // Transpose the Eigen Face images so that we can use inbuilt max and min function in array.
            _objMatrix.Transpose(EigenFaceImage, ref TransposevalImage, PCA_App.Properties.Settings.Default.DReductionVal);
            double[][] PixelmatrixforImage = new double[PCA_App.Properties.Settings.Default.DReductionVal][];
            _objMatrix.ConvertinPixelScale(TransposevalImage, ref PixelmatrixforImage, EigenFaceImage.Length);
            //  _objMatrix.Transpose(PixelmatrixforImage, ref TransposevalImage,EigenFaceImage.Length);

            int iControl = 0;
            /* foreach (Control cobj in gbxImages.Controls)
             {
                 if (cobj is PictureBox)
                 {
                     cobj.BackgroundImage = _objMatrix.DrawFaceValue(PixelmatrixforImage, iControl);
                     iControl++;
                 }

             }*/

            BaseMatrix = new double[clsMatrixOperation.iDefaultImageCount][];
            double[][] ProductMatrix = null;
            double[][] Transponse_EachImage = new double[PCA_App.Properties.Settings.Default.DReductionVal][];
            for (int irow = 0; irow < clsMatrixOperation.iDefaultImageCount; irow++)
            {
                Transponse_EachImage = new double[EigenFaceImage.Length][];
                _objMatrix.Transpose(MainImageMatrix_forAllImages, ref Transponse_EachImage, 1, irow + 1);
                _objMatrix.Multiply(Transponse_EachImage, EigenFaceImage, ref ProductMatrix);
                for (int iColumn = 0; iColumn < clsMatrixOperation.iDimensionalReduction; iColumn++)
                {
                    if (iColumn == 0)
                        BaseMatrix[irow] = new double[clsMatrixOperation.iDimensionalReduction];
                    BaseMatrix[irow][iColumn] = new double();
                    BaseMatrix[irow][iColumn] = ProductMatrix[0][iColumn];
                }
            }
            //  _objMatrix.Multiply(CopyImageMatrix, EigenFaceImage, ref BaseMatrix);
    }



    }


}
