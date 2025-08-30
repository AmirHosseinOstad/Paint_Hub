using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Security.AccessControl;
using System.Text.Json;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

#pragma warning disable

Console.Write("welcome to ");
Console.ForegroundColor = ConsoleColor.DarkYellow;
Console.Write("Paint");
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("Hub ...");

Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine(">> Press 'a' to Automatic mode,");
Console.WriteLine(">> Press 'm' to manual mode,");
Console.WriteLine(">> Press 'h' to get help.");
Console.WriteLine();

for (; ; )
{
    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.Write(">>> ");

    ConsoleKeyInfo Key = Console.ReadKey();
    char KeyChar = Key.KeyChar;

    Console.WriteLine();
    if (KeyChar == 'a')
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Automatically mode");
        Console.Write("Enter the GitHub repository address : ");
        Console.ForegroundColor = ConsoleColor.DarkYellow;

        string RepoAddress = Console.ReadLine().ToString();

        Console.WriteLine("Get repo information...");

        var RepoInfo = GetRepoInfo(RepoAddress);

        if (RepoInfo[0] == "0" && RepoInfo[1] == "0")
        {
            Console.ForegroundColor= ConsoleColor.Red;
            Console.WriteLine("Error : ");
            Console.WriteLine(RepoInfo[2]);
        }
        else
        {
            Console.WriteLine("Data processing and card making...");

            string ImageAvatar = SaveImage(RepoInfo[3], "Avatar.png");
            using (Bitmap card = CardGenerator.CreateCard(
                ImageAvatar,
                RepoInfo[0],
                RepoInfo[2],
                RepoInfo[1],
                Convert.ToInt32(RepoInfo[4]),
                Convert.ToInt32(RepoInfo[5]),
                Convert.ToInt32(RepoInfo[6]),
                RepoInfo[7]))
            {
                Console.WriteLine("Start image creation...");
                card.Save($"Card-{RepoInfo[0]}.png", ImageFormat.Png);

                Console.WriteLine("The card image was saved at the following address:");
                Console.WriteLine(ImageAvatar.Replace("Avatar.png", $"Card-{RepoInfo[0]}.png"));
            }
        }
    }
    else if (KeyChar == 'm')
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Manual mode");
        Console.Write("Enter the GitHub repository address : ");
        string RepoAddress = Console.ReadLine().ToString();

        Console.WriteLine("Get repo information...");

        var RepoInfo = GetRepoInfo(RepoAddress);

        if (RepoInfo[0] == "0" && RepoInfo[1] == "0")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error : ");
            Console.WriteLine(RepoInfo[2]);
        }
        else
        {
            Console.Write("Enter the background color code (#ffff) : #");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            string BackgColor = Console.ReadLine().ToString();
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Enter the title color code (#ffff) : #");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            string TitleColor = Console.ReadLine().ToString();
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Enter the owner name color code (#ffff) : #");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            string OwnerColor = Console.ReadLine().ToString();
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Enter the description color code (#ffff) : #");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            string DescriptionColor = Console.ReadLine().ToString();
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Enter the status color code (#ffff) : #");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            string StatusColor = Console.ReadLine().ToString();

            Console.WriteLine("Data processing and card making...");

            string ImageAvatar = SaveImage(RepoInfo[3], "Avatar.png");
            using (Bitmap card = CardGenerator.CreateCard(
                ImageAvatar,
                RepoInfo[0],
                RepoInfo[2],
                RepoInfo[1],
                Convert.ToInt32(RepoInfo[4]),
                Convert.ToInt32(RepoInfo[5]),
                Convert.ToInt32(RepoInfo[6]),
                RepoInfo[7],
                ColorTranslator.FromHtml("#" + TitleColor),
                ColorTranslator.FromHtml("#" + OwnerColor),
                ColorTranslator.FromHtml("#" + DescriptionColor),
                ColorTranslator.FromHtml("#" + BackgColor),
                ColorTranslator.FromHtml("#" + ImageAvatar)))
            {
                Console.WriteLine("Start image creation...");
                card.Save($"Card-{RepoInfo[0]}.png", ImageFormat.Png);

                Console.WriteLine("The card image was saved at the following address:");
                Console.WriteLine(ImageAvatar.Replace("Avatar.png", $"Card-{RepoInfo[0]}.png"));
            }
        }
    }

}

string[] GetRepoInfo(string url)
{
    using (HttpClient client = new HttpClient())
    {
        try
        {
            url = url.ToLower().Replace("github.com", "api.github.com/repos");

            client.DefaultRequestHeaders.Add("User-Agent", "CSharpApp");
            HttpResponseMessage response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();

            string json = response.Content.ReadAsStringAsync().Result;

            using (JsonDocument doc = JsonDocument.Parse(json))
            {
                var root = doc.RootElement;

                string name = root.GetProperty("name").GetString();
                string description = root.GetProperty("description").GetString();
                string language = root.GetProperty("language").GetString();
                int stars = root.GetProperty("stargazers_count").GetInt32();
                int forks = root.GetProperty("forks_count").GetInt32();
                int issues = root.GetProperty("open_issues_count").GetInt32();

                // اینجا باید دو مرحله‌ای بری
                string ownerName = root.GetProperty("owner").GetProperty("login").GetString();
                string avatarUrl = root.GetProperty("owner").GetProperty("avatar_url").GetString();

                if (description.Length > 50)
                {
                    description = description.Substring(0, 50) + " ...";
                }

                return new string[] { name, description, ownerName, avatarUrl, stars.ToString(), forks.ToString(), issues.ToString(), language };
            }
        }
        catch (Exception ex)
        {
            return (new string[] { "0", "0", ex.Message });
        }
    }
}

string SaveImage(string imageUrl, string fileName)
{
    // مسیر پوشه bin پروژه
    string binPath = AppContext.BaseDirectory;
    string savePath = Path.Combine(binPath, fileName);

    using (WebClient client = new WebClient())
    {
        client.DownloadFile(imageUrl, savePath);

        return savePath;
    }
}