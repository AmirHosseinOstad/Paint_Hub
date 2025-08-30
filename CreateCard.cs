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
    private const int IconSize = 48; // سایز آیکون

    public static Bitmap CreateCard(string imagePath, string title, string subject, string information,
                                  int stars, int forks, int issues,
                                  string badgeText, Color? badgeTextColor = null,
                                  Color? titleColor = null, Color? subjectColor = null, Color? informationColor = null,
                                  Color? backgColor = null, Color? statColor = null)
    {
        badgeTextColor = backgColor;
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
                DrawRoundedImage(graphics, imagePath, ImageMarg, ImageMarg + 35, ImageSize, ImageSize, 25);
            }
            else
            {
                // اگر تصویر نباشد، یک مربع خاکستری با گوشه‌های گرد بکش
                DrawRoundedRectangle(graphics, new SolidBrush(Color.DarkOrange), ImageMarg, ImageMarg + 35, ImageSize, ImageSize, 25);

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
            Font badgeFont = new Font("Arial", 36, FontStyle.Regular); // فونت برای متن badge

            // رنگ‌های پیش‌فرض
            Color finalTitleColor = titleColor ?? Color.Black;
            Color finalSubjectColor = subjectColor ?? Color.FromArgb(88, 88, 88);
            Color finalInformationColor = informationColor ?? Color.FromArgb(128, 128, 128);
            Color finalStatColor = statColor ?? Color.Black;
            Color finalBadgeTextColor = badgeTextColor ?? Color.White;

            // رسم متن‌ها با فاصله‌گذاری مناسب برای تصویر بزرگ‌تر
            float currentY = 110;

            // Title (TEXT1)
            float titleEndX = 0;
            using (Brush brush = new SolidBrush(finalTitleColor))
            {
                graphics.DrawString(title ?? "TEXT1", defaultTitleFont, brush, TextX, currentY);
                // محاسبه انتهای title برای تعیین موقعیت badge
                SizeF titleSize = graphics.MeasureString(title ?? "TEXT1", defaultTitleFont);
                titleEndX = TextX + titleSize.Width;
            }

            // رسم badge در صورت وجود متن
            if (!string.IsNullOrEmpty(badgeText))
            {
                DrawBadge(graphics, badgeText, titleEndX + 20, currentY, badgeFont, Color.OrangeRed, finalBadgeTextColor, defaultTitleFont.Height);
            }

            currentY += defaultTitleFont.Height + 25;

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

            // رسم آیکون‌ها بالای آمارها - وسط چین شده با متن
            float iconY = statsY - IconSize - 15; // 15 پیکسل فاصله از آمارها

            // محاسبه عرض متن آمارها برای وسط چین کردن آیکون‌ها
            string starsText = $"{stars} Stars";
            string forksText = $"{forks} Forks";
            string issuesText = $"{issues} Issues";

            SizeF starsSize = graphics.MeasureString(starsText, statsFont);
            SizeF forksSize = graphics.MeasureString(forksText, statsFont);
            SizeF issuesSize = graphics.MeasureString(issuesText, statsFont);

            // آیکون ستاره برای Stars
            float starIconX = statsStartX + (starsSize.Width - IconSize) / 2;
            DrawStarIcon(graphics, starIconX, iconY, IconSize, finalStatColor);

            // آیکون شاخه برای Forks  
            float forkIconX = statsStartX + statsSpacing + (forksSize.Width - IconSize) / 2;
            DrawForkIcon(graphics, forkIconX, iconY, IconSize, finalStatColor);

            // آیکون مشکل برای Issues
            float issueIconX = statsStartX + statsSpacing * 2 + (issuesSize.Width - IconSize) / 2;
            DrawIssueIcon(graphics, issueIconX, iconY, IconSize, finalStatColor);

            using (Brush brush = new SolidBrush(finalStatColor))
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
            badgeFont.Dispose();
        }

        return bitmap;
    }

    private static void DrawBadge(Graphics graphics, string badgeText, float x, float y, Font font, Color backColor, Color textColor, float titleHeight)
    {
        // محاسبه اندازه متن
        SizeF textSize = graphics.MeasureString(badgeText, font);

        // تعیین padding برای badge
        int paddingX = 12;
        int paddingY = 6;

        // محاسبه اندازه badge
        float badgeWidth = textSize.Width + (paddingX * 2);
        float badgeHeight = textSize.Height + (paddingY * 2);

        // محاسبه Y برای وسط چین کردن badge نسبت به title
        float badgeY = y + (titleHeight - badgeHeight) / 2;

        // رسم مستطیل گرد badge
        DrawRoundedRectangle(graphics, new SolidBrush(backColor), (int)x, (int)badgeY, (int)badgeWidth, (int)badgeHeight, 8);

        // رسم متن در وسط badge
        float textX = x + paddingX;
        float textY = badgeY + paddingY;

        using (Brush textBrush = new SolidBrush(textColor))
        {
            graphics.DrawString(badgeText, font, textBrush, textX, textY);
        }
    }

    private static void DrawStarIcon(Graphics graphics, float x, float y, int size, Color color)
    {
        // رسم آیکون ستاره
        using (Brush brush = new SolidBrush(color))
        {
            PointF[] starPoints = new PointF[10];
            float centerX = x + size / 2;
            float centerY = y + size / 2;
            float outerRadius = size * 0.4f;
            float innerRadius = size * 0.2f;

            for (int i = 0; i < 10; i++)
            {
                double angle = (i * Math.PI / 5) - (Math.PI / 2);
                float radius = (i % 2 == 0) ? outerRadius : innerRadius;
                starPoints[i] = new PointF(
                    centerX + (float)(radius * Math.Cos(angle)),
                    centerY + (float)(radius * Math.Sin(angle))
                );
            }

            graphics.FillPolygon(brush, starPoints);
        }
    }

    private static void DrawForkIcon(Graphics graphics, float x, float y, int size, Color color)
    {
        // رسم آیکون فورک (شاخه‌های git)
        using (Pen pen = new Pen(color, 2))
        using (Brush brush = new SolidBrush(color))
        {
            float centerX = x + size / 2;
            float centerY = y + size / 2;
            float radius = 4;

            // دایره بالا (parent)
            graphics.FillEllipse(brush, centerX - radius, y + 4, radius * 2, radius * 2);

            // خط عمودی وسط
            graphics.DrawLine(pen, centerX, y + 8 + radius, centerX, y + size - 8 - radius);

            // دایره پایین (main branch)
            graphics.FillEllipse(brush, centerX - radius, y + size - 8 - radius, radius * 2, radius * 2);

            // شاخه سمت راست
            float branchY = centerY;
            graphics.DrawLine(pen, centerX, branchY, centerX + size * 0.3f, branchY - size * 0.2f);
            graphics.FillEllipse(brush, centerX + size * 0.3f - radius / 2, branchY - size * 0.2f - radius / 2, radius, radius);
        }
    }

    private static void DrawIssueIcon(Graphics graphics, float x, float y, int size, Color color)
    {
        // رسم آیکون مسئله (دایره با علامت تعجب)
        using (Pen pen = new Pen(color, 2))
        using (Brush brush = new SolidBrush(color))
        {
            float centerX = x + size / 2;
            float centerY = y + size / 2;
            float circleSize = size * 0.8f;

            // دایره خارجی
            graphics.DrawEllipse(pen, x + (size - circleSize) / 2, y + (size - circleSize) / 2, circleSize, circleSize);

            // علامت تعجب
            using (Font font = new Font("Arial", size * 0.5f, FontStyle.Bold))
            {
                string exclamation = "!";
                SizeF textSize = graphics.MeasureString(exclamation, font);
                float textX = centerX - textSize.Width / 2;
                float textY = centerY - textSize.Height / 2;
                graphics.DrawString(exclamation, font, brush, textX, textY);
            }
        }
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
        path.CloseAllFigures();
        return path;
    }
}