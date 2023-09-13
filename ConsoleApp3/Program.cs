using HtmlAgilityPack;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine(args[0]);
        if (args.Length == 0)
        {
            Console.WriteLine("Aucun argument n'a été passé.");
        }else
        {
            Console.WriteLine("argument passé.");
        }
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

