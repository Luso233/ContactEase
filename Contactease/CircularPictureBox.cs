using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ContactEase
{
    public class CircularPictureBox : PictureBox
    {
        public CircularPictureBox()
        {
            this.SizeMode = PictureBoxSizeMode.Zoom;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, this.Width, this.Height);
            this.Region = new Region(path);
            base.OnPaint(pe);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Invalidate(); // Redraw the PictureBox with new dimensions
        }
    }
}
