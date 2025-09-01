# PaintHub - GitHub Banner Generator 🎨

A web application that generates beautiful banners for GitHub repositories with automatic repository information fetching and customizable styling options.

## Overview

PaintHub is an ASP.NET Core MVC application that creates visually appealing banner cards for GitHub repositories. Originally developed as a console application, it has been migrated to a web-based solution to provide better user experience and accessibility.

## Features

✅ **Automatic Avatar Download** - Fetches repository owner's avatar from GitHub API  
✅ **Repository Statistics Display** - Shows stars, forks, and open issues  
✅ **Custom Color Selection** - Choose your preferred colors for different elements  
✅ **Minimal & Attractive Design** - Clean, professional banner design  
✅ **Two Generation Modes** - Automatic (quick) and Manual (customizable) options  
✅ **Persian/Farsi UI Support** - RTL interface for Persian users  

## Technologies Used

- **ASP.NET Core 9.0** - Web framework
- **System.Drawing.Common** - Image processing and generation
- **GitHub API** - Repository data fetching
- **HTML/CSS/JavaScript** - Frontend interface

## Installation & Setup

### Prerequisites
- .NET 9.0 SDK
- Visual Studio 2022 or VS Code

### Steps
1. Clone the repository:
```bash
git clone <your-repo-url>
cd Paint_Hub_web
```

2. Restore NuGet packages:
```bash
dotnet restore
```

3. Build the project:
```bash
dotnet build
```

4. Run the application:
```bash
dotnet run
```

5. Navigate to `https://localhost:7279` or `http://localhost:5238`

## Project Structure

### Controllers

#### `HomeController.cs`
The main controller handling all banner generation logic:

**Actions:**
- `Index()` - Main landing page with mode selection
- `Auto()` - GET/POST for automatic banner generation with default colors
- `Manual()` - GET/POST for custom banner generation with user-selected colors
- `Result()` - Displays the generated banner with download options

**Key Methods:**
- `GetRepoInfo(string url)` - Fetches repository data from GitHub API
- `SaveImage(string imageUrl, string fileName)` - Downloads and saves repository owner's avatar

### Models

#### `CreateCard.cs` - `CardGenerator` Class
Contains the core banner generation logic:
- **Card Dimensions**: 1280x640 pixels
- **Image Processing**: Rounded corners, custom fonts, icon rendering
- **Text Rendering**: Multi-layered text with different colors and fonts
- **Statistics Icons**: Custom-drawn star, fork, and issue icons

**Main Method:**
```csharp
public static Bitmap CreateCard(string imagePath, string title, string subject, 
    string information, int stars, int forks, int issues, string badgeText,
    Color? titleColor = null, Color? subjectColor = null, 
    Color? informationColor = null, Color? backgColor = null, 
    Color? statColor = null)
```

#### `ErrorViewModel.cs`
Standard ASP.NET Core error handling model.

#### `Image.cs`
Alternative image saving implementation (currently unused).

### Views

#### Layout & Shared
- `_Layout.cshtml` - Main layout with Persian/RTL support and Google Fonts integration
- `Error.cshtml` - Error page template

#### Home Views
- **`Index.cshtml`** - Landing page with two mode selection buttons
- **`Auto.cshtml`** - Simple form for automatic banner generation
- **`Manual.cshtml`** - Advanced form with color customization options
- **`Result.cshtml`** - Display generated banner with download functionality

## Usage Guide

### Automatic Mode
1. Go to the main page
2. Click "حالت خودکار" (Automatic Mode)
3. Enter GitHub repository URL (e.g., `https://github.com/microsoft/vscode`)
4. Click "ایجاد بنر" (Generate Banner)
5. Download your generated banner

### Manual Mode
1. Go to the main page
2. Click "حالت دستی" (Manual Mode)
3. Enter GitHub repository URL
4. Customize colors for:
   - Background
   - Title
   - Owner name
   - Description
   - Statistics
5. Click "ایجاد بنر سفارشی" (Generate Custom Banner)
6. Download your personalized banner

## API Integration

The application integrates with GitHub's REST API:
- **Endpoint**: `https://api.github.com/repos/{owner}/{repo}`
- **Data Retrieved**: Repository name, description, language, stars, forks, issues, owner info
- **Avatar Download**: Automatically fetches owner's avatar image

## File Structure

```
Paint_Hub_web/
├── Controllers/
│   └── HomeController.cs
├── Models/
│   ├── CreateCard.cs
│   ├── ErrorViewModel.cs
│   └── Image.cs
├── Views/
│   ├── Home/
│   │   ├── Index.cshtml
│   │   ├── Auto.cshtml
│   │   ├── Manual.cshtml
│   │   └── Result.cshtml
│   └── Shared/
│       ├── _Layout.cshtml
│       └── Error.cshtml
├── wwwroot/
│   ├── css/
│   ├── Images/ (temporary avatar storage)
│   ├── ImagesResult/ (generated banners)
│   └── Fonts/ (Arial font files)
└── Program.cs
```

## Configuration

### Launch Settings
- **HTTPS**: `https://localhost:7279`
- **HTTP**: `http://localhost:5238`
- **Environment**: Development

### Dependencies
```xml
<PackageReference Include="System.Drawing.Common" Version="9.0.8" />
```

## Evolution Notes

This project started as a console application for generating GitHub repository banners. It has since evolved into a full-featured web application with:
- Interactive user interface
- Real-time color customization
- Better error handling
- Web-based file management
- Enhanced user experience

## Known Issues

- Temporary avatar images are stored in `wwwroot/Images/` (cleaned after processing)
- Generated banners are stored in `wwwroot/ImagesResult/`
- Requires `ImagesResult` directory to exist for proper functionality

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Submit a pull request

## License

This project is open source. Please check the repository for license information.

---

**Note**: This application was originally developed as a console application and later migrated to ASP.NET Core MVC for better accessibility and user experience.
