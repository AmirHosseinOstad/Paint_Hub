using System.Drawing;
using System.Drawing.Imaging;

namespace Paint_Hub_web.Models
{
    public class Image
    {
        public void Save(Bitmap card ,string Name)
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ImagesResult", $"Card-{Name}.png");

                var directory = Path.GetDirectoryName(path);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                // تبدیل مستقیم Bitmap به byte array بدون استفاده از Save
                byte[] imageBytes;
                using (var memoryStream = new MemoryStream())
                {
                    // استفاده از کپی کننده داخلی
                    var rect = new Rectangle(0, 0, card.Width, card.Height);
                    var bitmapData = card.LockBits(rect, ImageLockMode.ReadOnly, card.PixelFormat);

                    // کپی مستقیم پیکسل‌ها
                    int bytes = Math.Abs(bitmapData.Stride) * card.Height;
                    byte[] rgbValues = new byte[bytes];
                    System.Runtime.InteropServices.Marshal.Copy(bitmapData.Scan0, rgbValues, 0, bytes);
                    card.UnlockBits(bitmapData);

                    // ساخت PNG به صورت دستی (پیچیده‌س)
                    // بهتره از روش دیگه استفاده کنیم...
                }
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("wwwroot/text.txt", $"\nDirect conversion Error: {ex}");
            }
        }
    }
}
