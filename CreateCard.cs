using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

#pragma warning disable

public class CardGenerator
{
    private const int CardWidth = 1280;
    private const int CardHeight = 640;
    private const int ImageSize = 200;
    private const int ImageMarg = 80;
    private const int TextX = ImageMarg + ImageSize + 50;

    public static Bitmap CreateCard(string imagePath, string title, string subject, string information,
                                  int stars, int forks, int issues,
                                  Color? titleColor = null, Color? subjectColor = null, Color? informationColor = null,
                                  Color? backgColor = null, Color? statColor = null)
    {
        Bitmap bitmap = new Bitmap(CardWidth, CardHeight);

        using (Graphics graphics = Graphics.FromImage(bitmap))
        {
            // تنظیمات کیفیت
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // پس‌زمینه سفید
            graphics.Clear(backgColor ?? Color.White);

            // رسم حاشیه با ضخامت متناسب با اندازه تصویر
            using (Pen borderPen = new Pen(Color.LightGray, 3))
            {
                graphics.DrawRectangle(borderPen, 1, 1, CardWidth - 2, CardHeight - 2);
            }

            // رسم تصویر با گوشه‌های گرد - شعاع بیشتر برای تصویر بزرگ‌تر
            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
            {
                DrawRoundedImage(graphics, imagePath, ImageMarg, ImageMarg, ImageSize, ImageSize, 25);
            }
            else
            {
                // اگر تصویر نباشد، یک مربع خاکستری با گوشه‌های گرد بکش
                DrawRoundedRectangle(graphics, new SolidBrush(Color.DarkOrange), ImageMarg, ImageMarg, ImageSize, ImageSize, 25);

                // متن "image" در وسط
                using (Font font = new Font("Arial", 24, FontStyle.Bold))
                using (Brush brush = new SolidBrush(Color.White))
                {
                    string text = "Point_Hup";
                    SizeF textSize = graphics.MeasureString(text, font);
                    float x = ImageMarg + (ImageSize - textSize.Width) / 2;
                    float y = ImageMarg + (ImageSize - textSize.Height) / 2;
                    graphics.DrawString(text, font, brush, x, y);
                }
            }

            // فونت‌های پیش‌فرض اگر تعیین نشده باشند - اندازه‌های بزرگ‌تر برای تصویر 1280x640
            Font defaultTitleFont = new Font("Arial", 48, FontStyle.Bold);
            Font defaultSubjectFont = new Font("Arial", 36, FontStyle.Regular);
            Font defaultInformationFont = new Font("Arial", 28, FontStyle.Regular);
            Font statsFont = new Font("Arial", 18, FontStyle.Regular);

            // رنگ‌های پیش‌فرض
            Color finalTitleColor = titleColor ?? Color.Black;
            Color finalSubjectColor = subjectColor ?? Color.FromArgb(88, 88, 88);
            Color finalInformationColor = informationColor ?? Color.FromArgb(128, 128, 128);

            // رسم متن‌ها با فاصله‌گذاری مناسب برای تصویر بزرگ‌تر
            float currentY = 80;

            // Title (TEXT1)
            using (Brush brush = new SolidBrush(finalTitleColor))
            {
                graphics.DrawString(title ?? "TEXT1", defaultTitleFont, brush, TextX, currentY);
                currentY += defaultTitleFont.Height + 25;
            }

            // Subject (TEXT2)
            using (Brush brush = new SolidBrush(finalSubjectColor))
            {
                graphics.DrawString(subject ?? "Text2", defaultSubjectFont, brush, TextX, currentY);
                currentY += defaultSubjectFont.Height + 30;
            }

            // Information (TEXT3)
            using (Brush brush = new SolidBrush(finalInformationColor))
            {
                graphics.DrawString(information ?? "Text3", defaultInformationFont, brush, TextX, currentY);
            }

            // آمارها در پایین با فاصله‌گذاری مناسب
            float statsY = CardHeight - 120;
            float statsSpacing = 250;
            float statsStartX = TextX;

            using (Brush brush = new SolidBrush(statColor ?? Color.Black))
            {
                graphics.DrawString($"{stars} Stars", statsFont, brush, statsStartX, statsY);
                graphics.DrawString($"{forks} Forks", statsFont, brush, statsStartX + statsSpacing, statsY);
                graphics.DrawString($"{issues} Issues", statsFont, brush, statsStartX + statsSpacing * 2, statsY);
            }

            // آزادسازی فونت‌هایی که خودمان ساختیم
            defaultTitleFont.Dispose();
            defaultSubjectFont.Dispose();
            defaultInformationFont.Dispose();
            statsFont.Dispose();
        }

        return bitmap;
    }

    private static void DrawRoundedImage(Graphics graphics, string imagePath, int x, int y, int width, int height, int radius)
    {
        using (Image image = Image.FromFile(imagePath))
        using (GraphicsPath path = GetRoundedRectanglePath(x, y, width, height, radius))
        {
            graphics.SetClip(path);
            graphics.DrawImage(image, new Rectangle(x, y, width, height));
            graphics.ResetClip();
        }
    }

    private static void DrawRoundedRectangle(Graphics graphics, Brush brush, int x, int y, int width, int height, int radius)
    {
        using (GraphicsPath path = GetRoundedRectanglePath(x, y, width, height, radius))
        {
            graphics.FillPath(brush, path);
        }
    }

    private static GraphicsPath GetRoundedRectanglePath(int x, int y, int width, int height, int radius)
    {
        GraphicsPath path = new GraphicsPath();
        path.AddArc(x, y, radius * 2, radius * 2, 180, 90);
        path.AddArc(x + width - radius * 2, y, radius * 2, radius * 2, 270, 90);
        path.AddArc(x + width - radius * 2, y + height - radius * 2, radius * 2, radius * 2, 0, 90);
        path.AddArc(x, y + height - radius * 2, radius * 2, radius * 2, 90, 90);
        path.CloseAllFigures();
        return path;
    }
}