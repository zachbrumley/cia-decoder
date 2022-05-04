using System;
using System.IO;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;

namespace CIA_DECLASSIFIED
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Decoder decoder = new Decoder();
        string[] fileHeader = new string[4];
        string[] picWH = new string[2];
        string selectedFilePath;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void mitOpenFile_Click(object sender, RoutedEventArgs e)
        {
            //new openfiledialog
            OpenFileDialog openFile = new OpenFileDialog();

            bool fileSelected = openFile.ShowDialog() == true;

            //load image into imagebox
            if (fileSelected)
            {
                //filepath of selected file
                selectedFilePath = openFile.FileName;

                //convert file from ppm to bitmap
                BitmapMaker bmp = decoder.ConvertFromPPM(selectedFilePath);

                //set writeablebitmap to users image
                WriteableBitmap wbm = bmp.MakeBitmap();

                //set imagebox source as writeablebitmap
                imgMain.Source = wbm;
            }//end if
        }

        private void buttonDecode_Click(object sender, RoutedEventArgs e)
        {
            if (imgMain.Source == null)
            {
                txtMessage.Text = "NO IMAGE SELECTED";
            }
            else
            {
                BitmapMaker bmp = decoder.ConvertFromPPM(selectedFilePath);
                txtMessage.Text = decoder.DECODE(bmp);
            }        
        }

        private void mitExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
