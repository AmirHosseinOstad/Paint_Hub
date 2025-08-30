using System.Drawing;


namespace Paint_Hub
{
    internal class PaintText
    {
        public void AddText(string Text, string Font, Brush Brush, Color Color, string DirectoryOutput)
        {
            Bitmap bmp = new Bitmap(800, 500);

            // ابزاری برای نقاشی روی تصویر
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color);
                Font font = new Font("B Nazanin", 24, FontStyle.Bold);
                g.DrawString("سلام دنیا!", font, Brush, new PointF(400, 250));
            }

            // ذخیره کردن تصویر به صورت PNG
            bmp.Save("DirectoryOutput.png");
        }
    }
}
