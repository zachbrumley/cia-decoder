using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.IO;

namespace CIA_DECLASSIFIED
{
    class Decoder
    {
        string[] fileHeader = new string[4];
        string fileType;
        public int picW;
        public int picH;
        int pixelMax;

        public BitmapMaker ConvertFromPPM(string filepath)
        {
            //string array to hold file header info
            string[] fileHeader = new string[4];

            //new streamreader to read file
            StreamReader reader = new StreamReader(filepath);

            //read first 4 rows and store to header data array
            for (int i = 0; i < fileHeader.Length; i++)
            {
                fileHeader[i] = reader.ReadLine();
            }//end for

            //string arr to hold w/h
            string[] wh = fileHeader[2].Split(' ');

            //set picw and pich
            picW = int.Parse(wh[0]);
            picH = int.Parse(wh[1]);
            pixelMax = byte.Parse(fileHeader[3]);
            //get filetype

            fileType = fileHeader[0];

            BitmapMaker bmp = new BitmapMaker(picW, picH);

            if (fileType == "P3")
            {   //loop through height
                for (int y = 0; y < picH; y++)
                {   //loop through width
                    for (int x = 0; x < picW; x++)
                    {
                        int red = int.Parse(reader.ReadLine()); //readline, get pixel rgb data
                        int green = int.Parse(reader.ReadLine());
                        int blue = int.Parse(reader.ReadLine());
                        Color clrCurrent = new Color();         //color to hold rgb data
                        clrCurrent = Color.FromArgb((byte)pixelMax, (byte)red, (byte)green, (byte)blue);
                        //set pixel on new bitmap
                        bmp.SetPixel(x, y, clrCurrent);
                    }//end x loop
                }//end y loop
                reader.Close();
            }//end if

            //return new bitmap
            return bmp;
        }
        public string DECODE(BitmapMaker bmp)
        {
            StringBuilder sb = new();

            //Loop through image 
            for (int y = 0; y < picH; y++)
            {
                for (int x = 0; x < picW; x++)
                {
                    Color clrCurrent = bmp.GetPixelColor(x, y);

                    //checking for encoded red values and adding them to message
                    if (clrCurrent.R == 32)
                    {
                        sb.Append((char)clrCurrent.R);
                    }
                    else if (clrCurrent.R >= 48 && clrCurrent.R <= 57)
                    {
                        sb.Append((char)clrCurrent.R);
                    }
                    else if (clrCurrent.R >= 65 && clrCurrent.R <= 90)
                    {
                        sb.Append((char)clrCurrent.R);
                    }

                }
            }
            return sb.ToString(); ;
        }

        private string[] GetHeader(string filepath)
        {
            StreamReader read = new StreamReader(filepath);
            string[] fileHeader = new string[4];

            for (int i = 0; i < 4; i++)
            {
                fileHeader[i] = read.ReadLine();
            }

            string[] imageWH = fileHeader[2].Split(' ');

            picW = int.Parse(imageWH[0]);
            picH = int.Parse(imageWH[1]);

            read.Close();
            return fileHeader;
        }




    }
}
