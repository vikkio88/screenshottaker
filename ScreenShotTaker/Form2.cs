using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenShotTaker
{
    public partial class Form2 : Form
    {
        private Bitmap bmpScreenshot;

        public Form2()
        {
           
        }

        public Form2(Bitmap bmpScreenshot)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.bmpScreenshot = bmpScreenshot;
            pctbxScreen.Width = bmpScreenshot.Width;
            pctbxScreen.Height = bmpScreenshot.Height;
            pctbxScreen.Image = bmpScreenshot;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private Point RectStartPoint;
        private Rectangle Rect = new Rectangle();
        private Brush selectionBrush = new SolidBrush(Color.FromArgb(128, 72, 145, 220));

        // Start Rectangle
        //
        private void pictureBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // Determine the initial rectangle coordinates...
            RectStartPoint = e.Location;
            Invalidate();
        }

        // Draw Rectangle
        //
        private void pictureBox1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            Point tempEndPoint = e.Location;
            Rect.Location = new Point(
                Math.Min(RectStartPoint.X, tempEndPoint.X),
                Math.Min(RectStartPoint.Y, tempEndPoint.Y));
            Rect.Size = new Size(
                Math.Abs(RectStartPoint.X - tempEndPoint.X),
                Math.Abs(RectStartPoint.Y - tempEndPoint.Y));
            pctbxScreen.Invalidate();
        }

        // Draw Area
        //
        private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // Draw the rectangle...
            if (pctbxScreen.Image != null)
            {
                if (Rect != null && Rect.Width > 0 && Rect.Height > 0)
                {
                    e.Graphics.FillRectangle(selectionBrush, Rect);
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (Rect.Contains(e.Location))
                {
                    Console.WriteLine("Right click");
                    Console.WriteLine(Rect.Height);
                    Console.WriteLine(Rect.Width);
                    Form1.x = Rect.Width;
                    Form1.y = Rect.Height;
                    this.Close();
                   // Console.WriteLine(Rect.X+Rect.Y);
                }
            }
        }
    }
}
