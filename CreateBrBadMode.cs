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
    private const int SmallTextSize = 26;
    private const int LargeTextSize = 72;
    private const int SquareTextSize = 68;
    // ثابت‌های لوگو کوچک
    private const int SmallSquareSize = 60;
    private const int SmallSquareTextSize = 32;
    private const int SmallLargeTextSize = 36;
    private const int CreatedByTextSize = 26;
    public static Bitmap CreateLogo(string topLeftText, string topRightText, string topSquareText, string topCornerText,
                                   string bottomLeftText, string bottomRightText, string bottomSquareText, string bottomCornerText,
                                   string smallLeftText, string smallRightText, string smallSquareText,
                                   Color? textColor = null, Color? squareColor = null, Color? squareTextColor = null,
                                   Color? cornerTextColor = null, Color? backgroundColor = null)
    {
        Bitmap bitmap = new Bitmap(LogoWidth, LogoHeight);
        using (Graphics graphics = Graphics.FromImage(bitmap))
        {
            string customFontPath = "Butler_Regular.otf";
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
            // بارگذاری فونت سفارشی
            PrivateFontCollection privateFontCollection = null;
            FontFamily customFontFamily = null;
            if (!string.IsNullOrEmpty(customFontPath))
            {
                privateFontCollection = new PrivateFontCollection();
                privateFontCollection.AddFontFile(customFontPath);
                customFontFamily = privateFontCollection.Families[0];
            }
            // فونت‌ها - استفاده از فونت سفارشی برای متن‌های مشکی
            Font largeFont = customFontFamily != null ?
                new Font(customFontFamily, LargeTextSize, FontStyle.Bold) :
                new Font("Arial", LargeTextSize, FontStyle.Bold);
            Font smallFont = new Font("Arial", SmallTextSize, FontStyle.Regular);
            Font squareFont = new Font("Arial", SquareTextSize, FontStyle.Bold);
            // فونت‌های لوگو کوچک
            Font smallSquareFont = new Font("Arial", SmallSquareTextSize, FontStyle.Bold);
            Font smallLargeFont = customFontFamily != null ?
                new Font(customFontFamily, SmallLargeTextSize, FontStyle.Regular) :
                new Font("Arial", SmallLargeTextSize, FontStyle.Regular);
            Font createdByFont = customFontFamily != null ?
                new Font(customFontFamily, CreatedByTextSize, FontStyle.Regular) :
                new Font("Arial", CreatedByTextSize, FontStyle.Regular);

            // محاسبه مرکز تصویر
            float centerX = LogoWidth / 2f;
            float centerY = LogoHeight / 2f;

            // محاسبه اندازه متن‌ها برای تعیین عرض کل لوگو
            SizeF topLeftSize = string.IsNullOrEmpty(topLeftText) ? SizeF.Empty : graphics.MeasureString(topLeftText, largeFont);
            SizeF topRightSize = string.IsNullOrEmpty(topRightText) ? SizeF.Empty : graphics.MeasureString(topRightText, largeFont);
            SizeF bottomLeftSize = string.IsNullOrEmpty(bottomLeftText) ? SizeF.Empty : graphics.MeasureString(bottomLeftText, largeFont);
            SizeF bottomRightSize = string.IsNullOrEmpty(bottomRightText) ? SizeF.Empty : graphics.MeasureString(bottomRightText, largeFont);

            // محاسبه عرض کل لوگو اصلی (متن چپ + فاصله + مربع + مربع + فاصله + متن راست)
            float topRowWidth = (topLeftSize.Width > 0 ? topLeftSize.Width + 5 : 0) +
                               SquareSize +
                               (topRightSize.Width > 0 ? 5 + topRightSize.Width : 0);

            float bottomRowWidth = (bottomLeftSize.Width > 0 ? bottomLeftSize.Width + 5 : 0) +
                                  SquareSize +
                                  (bottomRightSize.Width > 0 ? 5 + bottomRightSize.Width : 0);

            float maxLogoWidth = Math.Max(topRowWidth, bottomRowWidth);

            // تعیین موقعیت شروع لوگو اصلی - فقط X وسط چین
            float logoStartX = centerX - maxLogoWidth / 2f;
            float logoStartY = 150; // موقعیت ثابت Y

            // تعیین موقعیت مربع بالا
            float topSquareX = logoStartX + (topLeftSize.Width > 0 ? topLeftSize.Width + 5 : 0);
            float topSquareY = logoStartY;

            // تعیین موقعیت مربع پایین
            float bottomSquareX = topSquareX + SquareSize;
            float bottomSquareY = topSquareY + SquareSize;

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
            // متن چپ بالا
            if (!string.IsNullOrEmpty(topLeftText))
            {
                using (Brush brush = new SolidBrush(finalTextColor))
                {
                    float textX = topSquareX - topLeftSize.Width - 5;
                    float textY = topSquareY + (SquareSize - topLeftSize.Height) / 2f;
                    graphics.DrawString(topLeftText, largeFont, brush, textX, textY);
                }
            }
            // متن راست بالا
            if (!string.IsNullOrEmpty(topRightText))
            {
                using (Brush brush = new SolidBrush(finalTextColor))
                {
                    float textX = topSquareX + SquareSize + 5;
                    float textY = topSquareY + (SquareSize - topRightSize.Height) / 2f;
                    graphics.DrawString(topRightText, largeFont, brush, textX, textY);
                }
            }
            // متن چپ پایین
            if (!string.IsNullOrEmpty(bottomLeftText))
            {
                using (Brush brush = new SolidBrush(finalTextColor))
                {
                    float textX = bottomSquareX - bottomLeftSize.Width - 5;
                    float textY = bottomSquareY + (SquareSize - bottomLeftSize.Height) / 2f;
                    graphics.DrawString(bottomLeftText, largeFont, brush, textX, textY);
                }
            }
            // متن راست پایین
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
            // محاسبه اندازه متن‌های لوگو کوچک
            SizeF smallLeftSize = string.IsNullOrEmpty(smallLeftText) ? SizeF.Empty : graphics.MeasureString(smallLeftText, smallLargeFont);
            SizeF smallRightSize = string.IsNullOrEmpty(smallRightText) ? SizeF.Empty : graphics.MeasureString(smallRightText, smallLargeFont);

            // محاسبه عرض کل لوگو کوچک
            float smallRowWidth = (smallLeftSize.Width > 0 ? smallLeftSize.Width + 5 : 0) +
                                 SmallSquareSize +
                                 (smallRightSize.Width > 0 ? 5 + smallRightSize.Width : 0);

            // موقعیت لوگو کوچک - کل مجموعه وسط چین
            float smallLogoStartX = centerX - smallRowWidth / 2f;
            float smallSquareX = smallLogoStartX + (smallLeftSize.Width > 0 ? smallLeftSize.Width + 5 : 0);
            float smallSquareY = bottomSquareY + SquareSize + 130;

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
                    float textX = smallSquareX - smallLeftSize.Width - 5;
                    float textY = smallSquareY + (SmallSquareSize - smallLeftSize.Height) / 2f;
                    graphics.DrawString(smallLeftText, smallLargeFont, brush, textX, textY);
                }
            }
            // متن راست لوگو کوچک
            if (!string.IsNullOrEmpty(smallRightText))
            {
                using (Brush brush = new SolidBrush(finalTextColor))
                {
                    float textX = smallSquareX + SmallSquareSize + 5;
                    float textY = smallSquareY + (SmallSquareSize - smallRightSize.Height) / 2f;
                    graphics.DrawString(smallRightText, smallLargeFont, brush, textX, textY);
                }
            }
            // متن "Created by:" بالای لوگو کوچک - وسط چین نسبت به کل لوگو کوچک
            string createdByText = "Created by:";
            using (Brush brush = new SolidBrush(finalTextColor))
            {
                SizeF createdBySize = graphics.MeasureString(createdByText, createdByFont);
                float createdByX = centerX - createdBySize.Width / 2f;
                float createdByY = smallSquareY - createdBySize.Height - 5;
                graphics.DrawString(createdByText, createdByFont, brush, createdByX, createdByY);
            }
            // آزادسازی فونت‌ها
            largeFont.Dispose();
            smallFont.Dispose();
            squareFont.Dispose();
            smallSquareFont.Dispose();
            smallLargeFont.Dispose();
            createdByFont.Dispose();
            // آزادسازی فونت سفارشی
            privateFontCollection?.Dispose();
        }
        return bitmap;
    }
}