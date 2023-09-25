using HtmlAgilityPack;
using DotNetEnv;

class Program
{
    static async Task Main(string[] args)
    {

        DotNetEnv.Env.Load();

        var articlesLequipe = await FonctionsScrap.ScrapLequipe();
        var articlesOneFootball = await FonctionsScrap.ScrapOneFootball();
        foreach (var article in articlesLequipe)
        {
            Console.WriteLine(article.url);
            Console.WriteLine(article.text);
        }
        foreach (var article in articlesOneFootball)
        {
            Console.WriteLine(article.url);
            Console.WriteLine(article.text);
        }
    }

}

