# PaintHub  

🎨 **GitHub Repository Social Media Banner Generator**  

## Latest Update:
- Improved banner design  
- Added programming language label  
- Added icon  

A console tool for generating beautiful banners from GitHub repositories that can be used on social media.  

## ✨ Features

- **Automatic Mode**: Generate banners with default colors  
- **Manual Mode**: Choose custom colors for all elements  
- **Auto Avatar Download**: Fetch and store the profile picture  
- **Stats Display**: Stars, forks, and issues  

## 🚀 Usage

### Automatic Mode
a
Enter the GitHub repository address : https://github.com/username/repository

shell
Copy code

### Manual Mode
m
Enter the GitHub repository address : https://github.com/username/repository
Enter the background color code (#ffff) : #ffffff
Enter the title color code (#ffff) : #2c3e50
Enter the owner name color code (#ffff) : #3498db
Enter the description color code (#ffff) : #7f8c8d
Enter the status color code (#ffff) : #e74c3c

markdown
Copy code

## 📋 Prerequisites

- .NET 6.0 or higher  
- Internet connection (to fetch data from GitHub API)  

## 🎯 Output

A PNG file named `Card-[repo-name].png` will be saved in the project folder.  

## 🔧 Installation & Run

1. Clone the code  
2. Build the project: `dotnet build`  
3. Run the project: `dotnet run`  

## 📝 Notes

- The repository address must be complete (including https://)  
- Do not end the repository URL with `.git` (will cause 404 error)  
- Long descriptions are automatically shortened (maximum 50 characters)  
- Color codes must be in HEX format (without #)  

## Example Banner Generated with PointHub
<img width="1280" height="640" alt="Card-Point_Hub" src="https://github.com/user-attachments/assets/02540710-1b93-4fcd-8cca-cf36d10917a9" />  

---
*Built with ❤️ for the GitHub community*  
