using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace imgprocessor
{
    internal class Gradientpanel : Panel
    {
        public Color CollorTop { get; set; }
        public Color ColorBottom { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            LinearGradientBrush lgb;
            if (ClientRectangle.Height != 0 && ClientRectangle.Width != 0) lgb = new LinearGradientBrush(ClientRectangle, ColorBottom, CollorTop, 60F);
            else lgb = new LinearGradientBrush(new PointF(1, 1), new PointF(2, 2), ColorBottom, CollorTop);
            Graphics g = e.Graphics;
            g.FillRectangle(lgb, ClientRectangle);
            base.OnPaint(e);
        }
    }
}