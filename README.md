# PaintHub - GitHub Banner Generator 🎨

A web application that generates beautiful banners for GitHub repositories with automatic repository information fetching and customizable styling options, including a unique Breaking Bad themed banner generator.

## Overview

PaintHub is an ASP.NET Core MVC application that creates visually appealing banner cards for GitHub repositories. Originally developed as a console application, it has been migrated to a web-based solution to provide better user experience and accessibility. The application now features multiple generation modes including an innovative Breaking Bad themed logo generator.

## Features

✅ **Automatic Avatar Download** - Fetches repository owner's avatar from GitHub API  
✅ **Repository Statistics Display** - Shows stars, forks, and open issues with custom icons  
✅ **Custom Color Selection** - Choose your preferred colors for different elements  
✅ **Minimal & Attractive Design** - Clean, professional banner design  
✅ **Three Generation Modes** - Automatic, Manual, and Breaking Bad themed options  
✅ **Breaking Bad Logo Generator** - Creates periodic table-style logos inspired by the TV series  
✅ **Persian/Farsi UI Support** - RTL interface for Persian users  
✅ **Atomic Data Processing** - Intelligent text parsing for chemical element representation  

## Technologies Used

- **ASP.NET Core 9.0** - Web framework
- **System.Drawing.Common 9.0.8** - Image processing and generation
- **GitHub API** - Repository data fetching
- **Custom Font Support** - Butler Regular and Arial fonts
- **HTML/CSS/JavaScript** - Frontend interface with Persian RTL support

## Installation & Setup

### Prerequisites
- .NET 9.0 SDK
- Visual Studio 2022 or VS Code
- Required fonts in `wwwroot/Fonts/` directory

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

3. Create required directories:
```bash
mkdir wwwroot/Images
mkdir wwwroot/ImagesResult
mkdir wwwroot/Fonts
mkdir wwwroot/Exception
```

4. Build the project:
```bash
dotnet build
```

5. Run the application:
```bash
dotnet run
```

6. Navigate to `https://localhost:7279` or `http://localhost:5238`

## Project Structure

### Controllers

#### `HomeController.cs`
The main controller handling all banner generation logic:

**Actions:**
- `Index()` - Main landing page with three mode selection options
- `Auto()` - GET/POST for automatic banner generation with default colors
- `Breaking_Bad()` - GET/POST for Breaking Bad themed logo generation
- `Manual()` - GET/POST for custom banner generation with user-selected colors
- `Result()` - Displays the generated banner with download options

**Key Methods:**
- `GetRepoInfo(string url)` - Fetches repository data from GitHub API
- `SaveImage(string imageUrl, string fileName)` - Downloads and saves repository owner's avatar
- Exception handling with logging to `wwwroot/Exception/File.txt`

### Models

#### `CreateCard.cs` - `CardGenerator` Class
Contains the core banner generation logic:
- **Card Dimensions**: 1280x640 pixels
- **Advanced Image Processing**: Rounded corners, custom fonts, gradient effects
- **Custom Icon System**: Hand-drawn star, fork, and issue icons
- **Text Rendering**: Multi-layered text with different colors and fonts
- **Badge System**: Language badges with rounded corners

**Main Method:**
```csharp
public static Bitmap CreateCard(string imagePath, string title, string subject, 
    string information, int stars, int forks, int issues, string badgeText,
    Color? titleColor = null, Color? subjectColor = null, 
    Color? informationColor = null, Color? backgColor = null, 
    Color? statColor = null)
```

#### `CreateBrBadMode.cs` - Breaking Bad Logo System
Two main classes for Breaking Bad themed generation:

**`LogoGenerator` Class:**
- Creates complex Breaking Bad style logos with dual-square design
- Supports main logo with atomic elements and creator attribution
- Custom positioning and font rendering
- Support for Butler Regular font

**`CreateBrBadMode` Class:**
- Alternative Breaking Bad logo generator
- Simplified dual-square layout
- Center-aligned design system

#### `GetAtomicData.cs` - Atomic Data Processing
Intelligent text processing system for chemical element extraction:
- **Chemical Element Database**: Complete periodic table with 99 elements
- **Smart Text Parsing**: Splits repository names intelligently
- **CamelCase Detection**: Handles modern naming conventions
- **Atomic Number Mapping**: Assigns correct atomic numbers
- **Conflict Resolution**: Prevents duplicate symbol usage

**Key Methods:**
- `GetData(string text, bool isRepoName)` - Main processing method
- `SplitRepoName(string repoName)` - Intelligent name splitting
- `FindSymbol(string input, string? preSymbol)` - Element detection

#### `ErrorViewModel.cs`
Standard ASP.NET Core error handling model.

#### `Image.cs`
Alternative image saving implementation with direct bitmap conversion.

### Views

#### Layout & Shared
- `_Layout.cshtml` - Main layout with Persian RTL support, Google Fonts (Vazirmatn), and animations
- `Error.cshtml` - Error page template
- `_ValidationScriptsPartial.cshtml` - Client-side validation scripts

#### Home Views
- **`Index.cshtml`** - Landing page with three mode selection buttons
- **`Auto.cshtml`** - Simple form for automatic banner generation
- **`Breaking_Bad.cshtml`** - Form for Breaking Bad themed logo generation
- **`Manual.cshtml`** - Advanced form with color customization and real-time color picker synchronization
- **`Result.cshtml`** - Display generated banner with download functionality

## Usage Guide

### Automatic Mode
1. Go to the main page
2. Click "حالت خودکار" (Automatic Mode)
3. Enter GitHub repository URL (e.g., `https://github.com/microsoft/vscode`)
4. Click "ایجاد بنر" (Generate Banner)
5. Download your generated banner

### Breaking Bad Mode
1. Go to the main page
2. Click "حالت بریکینگ بد" (Breaking Bad Mode)
3. Enter GitHub repository URL
4. The system automatically:
   - Extracts repository and owner names
   - Maps text to chemical elements
   - Creates periodic table-style squares
   - Generates atomic numbers and symbols
5. Download your Breaking Bad themed logo

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
5. Use color pickers or enter hex codes manually
6. Click "ایجاد بنر سفارشی" (Generate Custom Banner)
7. Download your personalized banner

## API Integration

The application integrates with GitHub's REST API:
- **Endpoint**: `https://api.github.com/repos/{owner}/{repo}`
- **Headers**: `User-Agent: CSharpApp` (required by GitHub API)
- **Data Retrieved**: Repository name, description, language, stars, forks, issues, owner info
- **Avatar Download**: Automatically fetches and saves owner's avatar image
- **Error Handling**: Graceful handling of API failures and rate limits

## Breaking Bad Logo System

### Atomic Data Processing
The Breaking Bad mode uses an intelligent system to convert text into chemical elements:

1. **Text Splitting**: Handles various naming conventions (kebab-case, snake_case, CamelCase)
2. **Element Matching**: Finds chemical elements within repository names
3. **Symbol Extraction**: Prioritizes two-character elements, falls back to single characters
4. **Conflict Prevention**: Ensures no duplicate symbols in the same logo
5. **Fallback System**: Uses default elements when no matches are found

### Visual Design
- **Dual Square Layout**: Upper and lower squares in Breaking Bad style
- **Color Scheme**: Classic green squares with white text
- **Typography**: Butler Regular font for authentic feel
- **Atomic Numbers**: Displayed in corner subscripts
- **Creator Attribution**: "Created by:" section for owner information

## File Structure

```
Paint_Hub_web/
├── Controllers/
│   └── HomeController.cs
├── Models/
│   ├── CreateCard.cs (Banner generation)
│   ├── CreateBrBadMode.cs (Breaking Bad logos)
│   ├── GetAtomicData.cs (Chemical element processing)
│   ├── ErrorViewModel.cs
│   └── Image.cs (Alternative image handling)
├── Views/
│   ├── Home/
│   │   ├── Index.cshtml (Main page with 3 modes)
│   │   ├── Auto.cshtml (Automatic generation)
│   │   ├── Breaking_Bad.cshtml (Breaking Bad mode)
│   │   ├── Manual.cshtml (Color customization)
│   │   └── Result.cshtml (Download page)
│   └── Shared/
│       ├── _Layout.cshtml (Persian RTL layout)
│       ├── Error.cshtml
│       └── _ValidationScriptsPartial.cshtml
├── wwwroot/
│   ├── css/
│   │   └── site.css (Persian RTL styling)
│   ├── Fonts/
│   │   ├── Butler_Regular.otf
│   │   └── ARIAL.TTF
│   ├── Images/ (Temporary avatar storage)
│   ├── ImagesResult/ (Generated banners)
│   └── Exception/ (Error logs)
├── .config/
│   └── dotnet-tools.json (EF Tools)
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

### Font Requirements
- **Butler Regular** (`Butler_Regular.otf`) - For Breaking Bad logos
- **Arial** (`ARIAL.TTF`) - For standard banners and UI elements

## Evolution Notes

This project has evolved significantly:

**Version 1.0**: Console application for basic GitHub banners
**Version 2.0**: Web application with automatic and manual modes
**Version 3.0**: Added Breaking Bad themed generator with:
- Atomic data processing system
- Chemical element database
- Intelligent text parsing
- Dual-logo layout system
- Persian UI with RTL support

## Advanced Features

### Atomic Data System
- **99 Chemical Elements**: Complete periodic table coverage
- **Smart Text Processing**: Handles modern repository naming conventions
- **Regex Patterns**: Advanced text splitting with CamelCase detection
- **Symbol Prioritization**: Prefers longer element symbols for accuracy
- **Conflict Resolution**: Prevents symbol duplication within single logos

### Image Generation
- **High Quality**: Anti-aliasing and high-quality interpolation
- **Custom Icons**: Hand-drawn vector-style icons for GitHub statistics
- **Font Management**: Private font collection handling
- **Memory Efficient**: Proper disposal of graphics resources
- **Error Resilience**: Graceful handling of missing fonts or images

### User Experience
- **Real-time Color Preview**: Live color picker synchronization
- **Form Validation**: Client and server-side validation
- **Responsive Design**: Works on desktop and mobile devices
- **Persian Localization**: Complete RTL interface
- **Progressive Enhancement**: Works without JavaScript

## Known Issues & Limitations

- Temporary avatar images stored in `wwwroot/Images/` (cleaned after processing)
- Generated banners accumulate in `wwwroot/ImagesResult/`
- Breaking Bad mode is experimental and may produce unexpected results
- Requires manual font installation in `wwwroot/Fonts/`
- GitHub API rate limiting may affect performance

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Make your changes
4. Add tests if applicable
5. Commit your changes (`git commit -m 'Add amazing feature'`)
6. Push to the branch (`git push origin feature/amazing-feature`)
7. Open a Pull Request

## Future Enhancements

- [ ] Database storage for generated banners
- [ ] User accounts and banner history
- [ ] Additional theme templates
- [ ] Batch processing for multiple repositories
- [ ] API endpoint for programmatic access
- [ ] Docker containerization
- [ ] Cloud deployment configurations

## License

This project is open source. Please check the repository for license information.

---

**Note**: This application demonstrates the evolution from a simple console tool to a sophisticated web application with advanced text processing, chemical element mapping, and multiple generation modes. The Breaking Bad themed generator showcases creative use of data transformation and visual design principles.
