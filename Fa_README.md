# PaintHub

🎨 **GitHub Repository Social Media Banner Generator**

یک ابزار کنسولی برای ساخت بنر زیبا از ریپوزیتوری‌های GitHub که می‌توانند در شبکه‌های اجتماعی استفاده شوند.

## ✨ امکانات

- **حالت خودکار**: ساخت بنر با رنگ‌های پیش‌فرض
- **حالت دستی**: انتخاب رنگ‌های دلخواه برای تمام عناصر
- **دانلود خودکار آواتار**: دریافت و ذخیره تصویر پروفایل
- **نمایش آمار**: ستاره‌ها، فورک‌ها و issues

## 🚀 نحوه استفاده

### حالت خودکار
```
>>> a
Enter the GitHub repository address : https://github.com/username/repository
```

### حالت دستی  
```
>>> m
Enter the GitHub repository address : https://github.com/username/repository
Enter the background color code (#ffff) : #ffffff
Enter the title color code (#ffff) : #2c3e50
Enter the owner name color code (#ffff) : #3498db
Enter the description color code (#ffff) : #7f8c8d
Enter the status color code (#ffff) : #e74c3c
```

## 📋 پیش‌نیازها

- .NET 6.0 یا بالاتر
- اتصال به اینترنت (برای دریافت اطلاعات از GitHub API)

## 🎯 خروجی

فایل PNG با نام `Card-[نام-ریپو].png` در پوشه پروژه ذخیره می‌شود.

## 🔧 نصب و اجرا

1. کد را کلون کنید
2. پروژه را بیلد کنید: `dotnet build`  
3. اجرا کنید: `dotnet run`

## 📝 نکات

- آدرس ریپو باید کامل باشد (شامل https://)
- پایان آدرس ریپو با .git نباشد. (خطای 404)
- توضیحات بلند خودکار کوتاه می‌شوند (حداکثر 50 کاراکتر)
- کدهای رنگ باید در فرمت هگز باشند (بدون #)

## نمونه بنر ساخته شده با PointHub
<img width="1280" height="640" alt="Card-Point_Hub" src="https://github.com/user-attachments/assets/02540710-1b93-4fcd-8cca-cf36d10917a9" />

---
*ساخته شده با ❤️ برای جامعه GitHub*
