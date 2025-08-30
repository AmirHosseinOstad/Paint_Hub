using System.Text.Json;
using System.Xml.Linq;
using Paint_Hub;

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

PaintText paintText = new PaintText();

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

        string RepiAddress = Console.ReadLine().ToString();

        var RepoInfo = GetRepoInfo(RepiAddress);

        if (RepoInfo[0] == "0" && RepoInfo[1] == "0")
        {
            Console.ForegroundColor= ConsoleColor.Red;
            Console.WriteLine("Error : ");
            Console.WriteLine(RepoInfo[2]);
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
                int stars = root.GetProperty("stargazers_count").GetInt32();

                return (new string[] { name, description, stars.ToString() });
            }
        }
        catch (Exception ex)
        {
            return (new string[] { "0", "0", ex.Message });
        }
    }
}