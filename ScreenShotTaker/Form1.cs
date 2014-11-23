using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenShotTaker
{
    public partial class Form1 : Form
    {
        int c = 1;
        string filePath;
        string pathSeparator = @"\";
        public static int x;
        public static int y;

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SaveScreenShot();
        }

        private void SaveScreenShot()
        {
            //SendKeys.Send("{PRTSC}");
            var bmpScreenshot = new Bitmap(x,
                               y,
                               PixelFormat.Format32bppArgb);

            // Create a graphics object from the bitmap.
            var gfxScreenshot = Graphics.FromImage(bmpScreenshot);

            // Take the screenshot from the upper left corner to the right bottom corner.
            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                        Screen.PrimaryScreen.Bounds.Y,
                                        0,
                                        0,
                                        Screen.PrimaryScreen.Bounds.Size,
                                        CopyPixelOperation.SourceCopy);

            // Save the screenshot to the specified path that the user has chosen.

            bmpScreenshot.Save(filePath + pathSeparator + c + ".jpg", ImageFormat.Jpeg);
            c++;
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@".\click.wav");
            player.Play();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            int interval;
            this.Hide();
            CheckDirectory();
            SelectPortion();
            this.WindowState = FormWindowState.Minimized;
            this.Show();

            try
            {
                interval = (int.Parse(txtTime.Text)) * 1000;
            }
            catch (Exception)
            {
                MessageBox.Show("Wrong interval, setting to default 10 seconds");
                interval = 10;
            }

            timer1.Interval = interval;
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            timer1.Start();
        }

        private void SelectPortion()
        {
            
            var bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                     Screen.PrimaryScreen.Bounds.Height,
                     PixelFormat.Format32bppArgb);

            // Create a graphics object from the bitmap.
            var gfxScreenshot = Graphics.FromImage(bmpScreenshot);

            // Take the screenshot from the upper left corner to the right bottom corner.
            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                        Screen.PrimaryScreen.Bounds.Y,
                                        0,
                                        0,
                                        Screen.PrimaryScreen.Bounds.Size,
                                        CopyPixelOperation.SourceCopy);
            
            Form2 screen = new Form2(bmpScreenshot);
            screen.WindowState = FormWindowState.Maximized;
            screen.Width = Screen.PrimaryScreen.Bounds.X;
            screen.Height = Screen.PrimaryScreen.Bounds.Y;
            screen.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            screen.ShowDialog();

            

        }

        private void CheckDirectory()
        {
            filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + pathSeparator + "ScreensTaken";
           
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

  



    }
}
