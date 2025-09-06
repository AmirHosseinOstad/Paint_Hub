using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Runtime;
using System.Security.AccessControl;
using System.Text.Json;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

#pragma warning disable

;

using (Bitmap card = LogoGenerator.CreateLogo("", "aint", "P", "17",
                                   "", "ub", "H", "1",
                                   "am", "r", "I"))
{
    Console.WriteLine("Start image creation...");
    card.Save($"Card-BreakingBad.png", ImageFormat.Png);

    Console.WriteLine("The card image was saved at the following address:");
}

Console.Write("welcome to ");
Console.ForegroundColor = ConsoleColor.DarkYellow;
Console.Write("Paint");
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("Hub ...");

Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine(">> Press 'a' to Automatic mode,");
Console.WriteLine(">> Press 'b' to Breaking Bad mode,");
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

        var listInfo = GetRepoInfo(RepoAddress);

        if (!String.IsNullOrEmpty(listInfo.ErrorMessage))
        {
            Console.ForegroundColor= ConsoleColor.Red;
            Console.WriteLine("Error : ");
            Console.WriteLine(listInfo.ErrorMessage);
        }
        else
        {
            Console.WriteLine("Data processing and card making...");

            string ImageAvatar = SaveImage(listInfo.AvatarUrl, "Avatar.png");
            using (Bitmap card = CardGenerator.CreateCard(
                ImageAvatar,
                listInfo.Name,
                listInfo.OwnerName,
                listInfo.Description,
                listInfo.Stars,
                listInfo.Forks,
                listInfo.Issues,
                listInfo.Language))
            {
                Console.WriteLine("Start image creation...");
                card.Save($"Card-{listInfo.Name}.png", ImageFormat.Png);

                Console.WriteLine("The card image was saved at the following address:");
                Console.WriteLine(ImageAvatar.Replace("Avatar.png", $"Card-{listInfo.Name}.png"));
            }
        }
    }
    else if (KeyChar == 'b')
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Breaking Bad mode");
        Console.Write("Enter the GitHub repository address : ");
        Console.ForegroundColor = ConsoleColor.DarkYellow;

        string RepoAddress = Console.ReadLine().ToString();

        Console.WriteLine("Get repo information...");

        var listInfo = GetRepoInfo(RepoAddress);

        if (!String.IsNullOrEmpty(listInfo.ErrorMessage))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error : ");
            Console.WriteLine(listInfo.ErrorMessage);
        }
        else
        {
            Console.WriteLine("Data processing and card making...");

            string ImageAvatar = SaveImage(listInfo.AvatarUrl, "Avatar.png");
            using (Bitmap card = CardGenerator.CreateCard(
                ImageAvatar,
                listInfo.Name,
                listInfo.OwnerName,
                listInfo.Description,
                listInfo.Stars,
                listInfo.Forks,
                listInfo.Issues,
                listInfo.Language))
            {
                Console.WriteLine("Start image creation...");
                card.Save($"Card-{listInfo.Name}.png", ImageFormat.Png);

                Console.WriteLine("The card image was saved at the following address:");
                Console.WriteLine(ImageAvatar.Replace("Avatar.png", $"Card-{listInfo.Name}.png"));
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

        var listInfo = GetRepoInfo(RepoAddress);

        if (!String.IsNullOrEmpty(listInfo.ErrorMessage))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error : ");
            Console.WriteLine(listInfo.ErrorMessage);
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

            string ImageAvatar = SaveImage(listInfo.AvatarUrl, "Avatar.png");
            using (Bitmap card = CardGenerator.CreateCard(
            ImageAvatar,
            listInfo.Name,
                listInfo.OwnerName,
                listInfo.Description,
                listInfo.Stars,
                listInfo.Forks,
                listInfo.Issues,
                listInfo.Language,
                ColorTranslator.FromHtml("#" + TitleColor),
                ColorTranslator.FromHtml("#" + OwnerColor),
                ColorTranslator.FromHtml("#" + DescriptionColor),
                ColorTranslator.FromHtml("#" + BackgColor),
                ColorTranslator.FromHtml("#" + StatusColor)))
            {
                Console.WriteLine("Start image creation...");
                card.Save($"Card-{listInfo.Name}.png", ImageFormat.Png);

                Console.WriteLine("The card image was saved at the following address:");
                Console.WriteLine(ImageAvatar.Replace("Avatar.png", $"Card-{listInfo.Name}.png"));
            }
        }
    }
    else if (KeyChar == 'h')
    {
        Console.ForegroundColor = ConsoleColor.White;

        string TextHelp = File.ReadAllText("Help.txt");
        Console.WriteLine(TextHelp);
    }

}

ListCard GetRepoInfo(string url)
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


                ListCard listCard = new ListCard()
                {
                    Name = name,
                    Description = description,
                    Language = language,
                    Stars = stars,
                    Forks = forks,
                    Issues = issues,
                    OwnerName = ownerName,
                    AvatarUrl = avatarUrl
                };

                return listCard;
            }
        }
        catch (Exception ex)
        {
            ListCard listCard = new ListCard()
            {
                ErrorMessage = ex.Message,
            };

            return listCard;
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

class ListCard
{
    public string Name {  get; set; }
    public string Description { get; set; }
    public string Language { get; set; }
    public int Stars { get; set; }
    public int Forks { get; set; }
    public int Issues { get; set; }
    public string OwnerName { get; set; }
    public string AvatarUrl { get; set; }
    public string? ErrorMessage { get; set; }
}
