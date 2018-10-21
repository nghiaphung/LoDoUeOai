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
        string workbookPath = "E:\\ImageInspection\\Data.xlsx";

        byte[] startInspection = {0x01};
        byte[] stopInspection  = {0x02};

        string dateTime = "";
        string folderName = "";


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
            //GetImageMatrix();

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
                Mat _frame = new Mat();
                _Capture.Retrieve(_frame);
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
            //Get data image from "Data" folder to an array (first dimension is pixel value, second dimension is image index)
            _objMatrix.InitializeMatrix(ref MainImageMatrix_forAllImages);
            // just copy from "MainImageMatrix_forAllImages" to "CopyImageMatrix_forallImages"
            _objMatrix.CopyMatrix(MainImageMatrix_forAllImages, ref CopyImageMatrix_forallImages);
            //Code for finding means vector from the MainImage Matrix
            MeanVector = new double[1][];
            // calculate the mean value of pixel value for each image
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

            //int iControl = 0;
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


        private void ExportData()
        {

            //Start Excel and get Application object.
            oXL = new Microsoft.Office.Interop.Excel.Application();
            //Get a new workbook.
            //oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(""));
            oWB = oXL.Workbooks.Open(workbookPath, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            oXL.Visible = true;


            //oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;
            //oSheet.Activate();
            oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oWB.Worksheets.Add();
            oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;
            oSheet.Activate();
            oSheet.Name = DateTime.Now.ToString("yyyyMMdd-HHmm");

            //Add table headers going cell by cell.
            oSheet.Cells[1, 1] = "First Name";
            oSheet.Cells[1, 2] = "Last Name";
            oSheet.Cells[1, 3] = "Full Name";
            oSheet.Cells[1, 4] = "Salary";

            //Format A1:D1 as bold, vertical alignment = center.
            oSheet.get_Range("A1", "D1").Font.Bold = true;
            oSheet.get_Range("A1", "D1").VerticalAlignment =
                Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

            // Create an array to multiple values at once.
            string[,] saNames = new string[5, 2];

            saNames[0, 0] = "John";
            saNames[0, 1] = "Smith";
            saNames[1, 0] = "Tom";

            saNames[4, 1] = "Johnson";

            //Fill A2:B6 with an array of values (First and Last Names).
            oSheet.get_Range("A2", "B6").Value2 = saNames;

            //Fill C2:C6 with a relative formula (=A2 & " " & B2).
            oRng = oSheet.get_Range("C2", "C6");
            oRng.Formula = "=A2 & \" \" & B2";

            //Fill D2:D6 with a formula(=RAND()*100000) and apply format.
            oRng = oSheet.get_Range("D2", "D6");
            //AutoFit columns A:D.
            oRng.Formula = "=RAND()*100000";
            oRng.NumberFormat = "$0.00";

            oRng = oSheet.get_Range("A1", "D1");
            oRng.EntireColumn.AutoFit();

            oXL.Visible = false;
            oXL.UserControl = false;
            //Save file
            //oWB.Save();
            //Save as
            oWB.SaveAs("C:\\file.xls", XlFileFormat.xlWorkbookNormal, null, null, false, false, XlSaveAsAccessMode.xlExclusive, false, false, false, false, false);
            oWB.Close();

        }

        private void btnInspection_Click(object sender, EventArgs e)
        {
            // send command to MCU
            _Com.Write(startInspection, 0, 1);
            //Make new Path folder
            dateTime = DateTime.Now.ToString("yyyyMMdd-HHmm");
            folderName = "D:\\ImageInspection\\" + dateTime;

            bool exist = System.IO.Directory.Exists(folderName);
            if (!exist) System.IO.Directory.CreateDirectory(folderName);

            //OpenExcelFile();

            _Com.Close();
            //_Com.DataReceived += Proxy;
            _Com.Open();

        }

        private void btnExcel_Click(object sender, EventArgs e)
        {

        }


    }


}

