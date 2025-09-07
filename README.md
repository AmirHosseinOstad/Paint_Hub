# PaintHub  
![C#](https://img.shields.io/badge/language-C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET Core](https://img.shields.io/badge/.NET_Core-5C2D91?style=for-the-badge&logo=dotnet&logoColor=white)
![GitHub](https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white)
![Bitmap](https://img.shields.io/badge/Bitmap-Library-blue?style=for-the-badge)

🎨 **GitHub Repository Social Media Banner Generator**  

## Use without clone
The PaintHub is online as an asp.net core mvc (Web_Version branch) on the host and can be used from https://painthub.elesoft.ir/
<br/>
On the web page, you can create a banner for your GitHub repo with a graphical interface.

## Latest Update:
- Added Breaking Bad mode (Ability to create a banner for the repo in the style of the series logo)
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
```
>>> a
Enter the GitHub repository address : https://github.com/username/repository
```

### Breaking Bad Mode
```
>>> b
Enter the GitHub repository address : https://github.com/username/repository
```


### Manual Mode
```
>>> m
Enter the GitHub repository address : https://github.com/username/repository
Enter the background color code (#ffff) : #ffffff
Enter the title color code (#ffff) : #2c3e50
Enter the owner name color code (#ffff) : #3498db
Enter the description color code (#ffff) : #7f8c8d
Enter the status color code (#ffff) : #e74c3c
```

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

## Example Banner Generated with PaintHub
<img width="1280" height="640" alt="BrBad-monaco-editor" src="https://github.com/user-attachments/assets/724fa08c-4285-40b2-9040-37469801b747" />
<img width="1280" height="640" alt="Card-monaco-editor" src="https://github.com/user-attachments/assets/e36a6646-9c1b-42c5-a908-2fcd7ee8fecb" />
<img width="1280" height="640" alt="Card-monaco-editor" src="https://github.com/user-attachments/assets/f65513d7-f126-46a9-ac38-072c9421b69a" />


---
*Built with ❤️ for the GitHub community*  
