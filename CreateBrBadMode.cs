using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

#pragma warning disable

public class LogoGenerator
{
    private const int CanvasWidth = 1280;
    private const int CanvasHeight = 640;
    private const int LogoWidth = 800;
    private const int LogoHeight = 400;
    private const int SquareSize = 120;
    private const int SmallTextSize = 16;
    private const int LargeTextSize = 72;
    private const int SquareTextSize = 64;

    public static Bitmap CreateLogo(string topLeftText, string topRightText, string topSquareText, string topCornerText,
                                   string bottomLeftText, string bottomRightText, string bottomSquareText, string bottomCornerText,
                                   Color? textColor = null, Color? squareColor = null, Color? squareTextColor = null,
                                   Color? cornerTextColor = null, Color? backgroundColor = null)
    {
        Bitmap bitmap = new Bitmap(CanvasWidth, CanvasHeight);

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
            Font largeFont = new Font("Minion Variable Concept", LargeTextSize, FontStyle.Bold);
            Font smallFont = new Font("Arial", SmallTextSize, FontStyle.Regular);
            Font squareFont = new Font("Arial", SquareTextSize, FontStyle.Bold);

            // محاسبه مرکز کل بوم
            float canvasCenterX = CanvasWidth / 2f;
            float canvasCenterY = CanvasHeight / 2f;

            // مبدا لوگو (برای اینکه دقیقا وسط بوم قرار بگیره)
            float logoOriginX = canvasCenterX - LogoWidth / 2f;
            float logoOriginY = canvasCenterY - LogoHeight / 2f;

            // محاسبه موقعیت مربع‌ها نسبت به مبدا لوگو
            float topSquareX = logoOriginX + (LogoWidth / 2f - SquareSize / 2f);
            float topSquareY = logoOriginY + (LogoHeight / 2f - SquareSize);
            float bottomSquareX = topSquareX + SquareSize;
            float bottomSquareY = topSquareY + SquareSize;

            // --- رسم مربع بالا ---
            using (Brush brush = new SolidBrush(finalSquareColor))
                graphics.FillRectangle(brush, topSquareX, topSquareY, SquareSize, SquareSize);

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

            if (!string.IsNullOrEmpty(topCornerText))
            {
                using (Brush brush = new SolidBrush(finalCornerTextColor))
                    graphics.DrawString(topCornerText, smallFont, brush, topSquareX + SquareSize - 30, topSquareY + 5);
            }

            // --- رسم مربع پایین ---
            using (Brush brush = new SolidBrush(finalSquareColor))
                graphics.FillRectangle(brush, bottomSquareX, bottomSquareY, SquareSize, SquareSize);

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

            if (!string.IsNullOrEmpty(bottomCornerText))
            {
                using (Brush brush = new SolidBrush(finalCornerTextColor))
                    graphics.DrawString(bottomCornerText, smallFont, brush, bottomSquareX + SquareSize - 35, bottomSquareY + 5);
            }

            // --- متن‌های اطراف مربع‌ها ---
            SizeF topLeftSize = string.IsNullOrEmpty(topLeftText) ? SizeF.Empty : graphics.MeasureString(topLeftText, largeFont);
            SizeF topRightSize = string.IsNullOrEmpty(topRightText) ? SizeF.Empty : graphics.MeasureString(topRightText, largeFont);
            SizeF bottomLeftSize = string.IsNullOrEmpty(bottomLeftText) ? SizeF.Empty : graphics.MeasureString(bottomLeftText, largeFont);
            SizeF bottomRightSize = string.IsNullOrEmpty(bottomRightText) ? SizeF.Empty : graphics.MeasureString(bottomRightText, largeFont);

            if (!string.IsNullOrEmpty(topLeftText))
            {
                using (Brush brush = new SolidBrush(finalTextColor))
                {
                    float textX = topSquareX - topLeftSize.Width - 10;
                    float textY = topSquareY + (SquareSize - topLeftSize.Height) / 2f;
                    graphics.DrawString(topLeftText, largeFont, brush, textX, textY);
                }
            }

            if (!string.IsNullOrEmpty(topRightText))
            {
                using (Brush brush = new SolidBrush(finalTextColor))
                {
                    float textX = topSquareX + SquareSize + 10;
                    float textY = topSquareY + (SquareSize - topRightSize.Height) / 2f;
                    graphics.DrawString(topRightText, largeFont, brush, textX, textY);
                }
            }

            if (!string.IsNullOrEmpty(bottomLeftText))
            {
                using (Brush brush = new SolidBrush(finalTextColor))
                {
                    float textX = bottomSquareX - bottomLeftSize.Width - 10;
                    float textY = bottomSquareY + (SquareSize - bottomLeftSize.Height) / 2f;
                    graphics.DrawString(bottomLeftText, largeFont, brush, textX, textY);
                }
            }

            if (!string.IsNullOrEmpty(bottomRightText))
            {
                using (Brush brush = new SolidBrush(finalTextColor))
                {
                    float textX = bottomSquareX + SquareSize + 10;
                    float textY = bottomSquareY + (SquareSize - bottomRightSize.Height) / 2f;
                    graphics.DrawString(bottomRightText, largeFont, brush, textX, textY);
                }
            }

            // آزادسازی فونت‌ها
            largeFont.Dispose();
            smallFont.Dispose();
            squareFont.Dispose();
        }

        return bitmap;
    }
}
