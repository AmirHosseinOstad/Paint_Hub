using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

#pragma warning disable

public class LogoGenerator
{
    private const int LogoWidth = 1280;
    private const int LogoHeight = 640;
    private const int SquareSize = 120;
    private const int SmallTextSize = 24;
    private const int LargeTextSize = 72;
    private const int SquareTextSize = 64;

    // ثابت‌های لوگو کوچک
    private const int SmallSquareSize = 60;
    private const int SmallSquareTextSize = 32;
    private const int SmallLargeTextSize = 36;

    public static Bitmap CreateLogo(string topLeftText, string topRightText, string topSquareText, string topCornerText,
                                   string bottomLeftText, string bottomRightText, string bottomSquareText, string bottomCornerText,
                                   string smallLeftText, string smallRightText, string smallSquareText,
                                   Color? textColor = null, Color? squareColor = null, Color? squareTextColor = null,
                                   Color? cornerTextColor = null, Color? backgroundColor = null)
    {
        Bitmap bitmap = new Bitmap(LogoWidth, LogoHeight);

        using (Graphics graphics = Graphics.FromImage(bitmap))
        {
            // تنظیمات کیفیت
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // پس‌زمینه
            graphics.Clear(backgroundColor ?? Color.White);

            // رنگ‌های پیش‌فرض
            Color finalTextColor = textColor ?? Color.Black;
            Color finalSquareColor = squareColor ?? Color.FromArgb(34, 139, 34); // سبز
            Color finalSquareTextColor = squareTextColor ?? Color.White;
            Color finalCornerTextColor = cornerTextColor ?? Color.White;

            // فونت‌ها
            Font largeFont = new Font("Arial", LargeTextSize, FontStyle.Bold);
            Font smallFont = new Font("Arial", SmallTextSize, FontStyle.Regular);
            Font squareFont = new Font("Arial", SquareTextSize, FontStyle.Bold);

            // فونت‌های لوگو کوچک
            Font smallSquareFont = new Font("Arial", SmallSquareTextSize, FontStyle.Bold);
            Font smallLargeFont = new Font("Arial", SmallLargeTextSize, FontStyle.Regular);

            // محاسبه مرکز تصویر
            float centerX = LogoWidth / 2f;
            float centerY = LogoHeight / 2f;

            // محاسبه اندازه متن‌های بالا برای تعیین موقعیت مربع بالا
            SizeF topLeftSize = string.IsNullOrEmpty(topLeftText) ? SizeF.Empty : graphics.MeasureString(topLeftText, largeFont);
            SizeF topRightSize = string.IsNullOrEmpty(topRightText) ? SizeF.Empty : graphics.MeasureString(topRightText, largeFont);

            // محاسبه اندازه متن‌های پایین برای تعیین موقعیت مربع پایین
            SizeF bottomLeftSize = string.IsNullOrEmpty(bottomLeftText) ? SizeF.Empty : graphics.MeasureString(bottomLeftText, largeFont);
            SizeF bottomRightSize = string.IsNullOrEmpty(bottomRightText) ? SizeF.Empty : graphics.MeasureString(bottomRightText, largeFont);

            // تعیین موقعیت مربع بالا (کمی بالاتر از مرکز)
            float topSquareX = centerX - SquareSize / 2f;
            float topSquareY = centerY - SquareSize - 50;

            // تعیین موقعیت مربع پایین (مورب - گوشه چپ بالایش با گوشه راست پایین مربع بالا تماس داشته باشد)
            float bottomSquareX = topSquareX + SquareSize; // شروع از گوشه راست مربع بالا
            float bottomSquareY = topSquareY + SquareSize; // شروع از گوشه پایین مربع بالا

            // رسم مربع بالا
            using (Brush brush = new SolidBrush(finalSquareColor))
            {
                graphics.FillRectangle(brush, topSquareX, topSquareY, SquareSize, SquareSize);
            }

            // متن داخل مربع بالا
            if (!string.IsNullOrEmpty(topSquareText))
            {
                using (Brush brush = new SolidBrush(finalSquareTextColor))
                {
                    SizeF squareTextSize = graphics.MeasureString(topSquareText, squareFont);
                    float squareTextX = topSquareX + (SquareSize - squareTextSize.Width) / 2f;
                    float squareTextY = topSquareY + (SquareSize - squareTextSize.Height) / 2f;
                    graphics.DrawString(topSquareText, squareFont, brush, squareTextX, squareTextY);
                }
            }

            // متن گوشه مربع بالا
            if (!string.IsNullOrEmpty(topCornerText))
            {
                using (Brush brush = new SolidBrush(finalCornerTextColor))
                {
                    graphics.DrawString(topCornerText, smallFont, brush, topSquareX + SquareSize - 30, topSquareY + 5);
                }
            }

            // رسم مربع پایین
            using (Brush brush = new SolidBrush(finalSquareColor))
            {
                graphics.FillRectangle(brush, bottomSquareX, bottomSquareY, SquareSize, SquareSize);
            }

            // متن داخل مربع پایین
            if (!string.IsNullOrEmpty(bottomSquareText))
            {
                using (Brush brush = new SolidBrush(finalSquareTextColor))
                {
                    SizeF squareTextSize = graphics.MeasureString(bottomSquareText, squareFont);
                    float squareTextX = bottomSquareX + (SquareSize - squareTextSize.Width) / 2f;
                    float squareTextY = bottomSquareY + (SquareSize - squareTextSize.Height) / 2f;
                    graphics.DrawString(bottomSquareText, squareFont, brush, squareTextX, squareTextY);
                }
            }

            // متن گوشه مربع پایین
            if (!string.IsNullOrEmpty(bottomCornerText))
            {
                using (Brush brush = new SolidBrush(finalCornerTextColor))
                {
                    graphics.DrawString(bottomCornerText, smallFont, brush, bottomSquareX + SquareSize - 35, bottomSquareY + 5);
                }
            }

            // محاسبه موقعیت متن‌های اطراف مربع‌ها

            // متن چپ بالا (سمت چپ مربع بالا)
            if (!string.IsNullOrEmpty(topLeftText))
            {
                using (Brush brush = new SolidBrush(finalTextColor))
                {
                    float textX = topSquareX - topLeftSize.Width - 5;
                    float textY = topSquareY + (SquareSize - topLeftSize.Height) / 2f;
                    graphics.DrawString(topLeftText, largeFont, brush, textX, textY);
                }
            }

            // متن راست بالا (سمت راست مربع بالا)
            if (!string.IsNullOrEmpty(topRightText))
            {
                using (Brush brush = new SolidBrush(finalTextColor))
                {
                    float textX = topSquareX + SquareSize + 5;
                    float textY = topSquareY + (SquareSize - topRightSize.Height) / 2f;
                    graphics.DrawString(topRightText, largeFont, brush, textX, textY);
                }
            }

            // متن چپ پایین (سمت چپ مربع پایین)
            if (!string.IsNullOrEmpty(bottomLeftText))
            {
                using (Brush brush = new SolidBrush(finalTextColor))
                {
                    float textX = bottomSquareX - bottomLeftSize.Width - 5;
                    float textY = bottomSquareY + (SquareSize - bottomLeftSize.Height) / 2f;
                    graphics.DrawString(bottomLeftText, largeFont, brush, textX, textY);
                }
            }

            // متن راست پایین (سمت راست مربع پایین)
            if (!string.IsNullOrEmpty(bottomRightText))
            {
                using (Brush brush = new SolidBrush(finalTextColor))
                {
                    float textX = bottomSquareX + SquareSize + 5;
                    float textY = bottomSquareY + (SquareSize - bottomRightSize.Height) / 2f;
                    graphics.DrawString(bottomRightText, largeFont, brush, textX, textY);
                }
            }

            // === لوگو کوچک زیرین ===
            // محاسبه موقعیت مربع کوچک (مرکز افقی، زیر لوگو اصلی)
            float smallSquareX = centerX - SmallSquareSize / 2f;
            float smallSquareY = bottomSquareY + SquareSize + 30;

            // رسم مربع کوچک
            using (Brush brush = new SolidBrush(finalSquareColor))
            {
                graphics.FillRectangle(brush, smallSquareX, smallSquareY, SmallSquareSize, SmallSquareSize);
            }

            // متن داخل مربع کوچک
            if (!string.IsNullOrEmpty(smallSquareText))
            {
                using (Brush brush = new SolidBrush(finalSquareTextColor))
                {
                    SizeF squareTextSize = graphics.MeasureString(smallSquareText, smallSquareFont);
                    float squareTextX = smallSquareX + (SmallSquareSize - squareTextSize.Width) / 2f;
                    float squareTextY = smallSquareY + (SmallSquareSize - squareTextSize.Height) / 2f;
                    graphics.DrawString(smallSquareText, smallSquareFont, brush, squareTextX, squareTextY);
                }
            }

            // متن چپ لوگو کوچک
            if (!string.IsNullOrEmpty(smallLeftText))
            {
                using (Brush brush = new SolidBrush(finalTextColor))
                {
                    SizeF textSize = graphics.MeasureString(smallLeftText, smallLargeFont);
                    float textX = smallSquareX - textSize.Width - 5;
                    float textY = smallSquareY + (SmallSquareSize - textSize.Height) / 2f;
                    graphics.DrawString(smallLeftText, smallLargeFont, brush, textX, textY);
                }
            }

            // متن راست لوگو کوچک
            if (!string.IsNullOrEmpty(smallRightText))
            {
                using (Brush brush = new SolidBrush(finalTextColor))
                {
                    float textX = smallSquareX + SmallSquareSize + 5;
                    SizeF textSize = graphics.MeasureString(smallRightText, smallLargeFont);
                    float textY = smallSquareY + (SmallSquareSize - textSize.Height) / 2f;
                    graphics.DrawString(smallRightText, smallLargeFont, brush, textX, textY);
                }
            }

            // آزادسازی فونت‌ها
            largeFont.Dispose();
            smallFont.Dispose();
            squareFont.Dispose();
            smallSquareFont.Dispose();
            smallLargeFont.Dispose();
        }

        return bitmap;
    }
}