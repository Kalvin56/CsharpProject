using HtmlAgilityPack;

class FonctionsScrap
{
    public static async Task<List<Article>> ScrapLequipe()
    {
        List<Article> listeArticles = new List<Article>();
        using (var httpClient = new HttpClient())
        {
            var url = "https://www.lequipe.fr/Football/Ligue-1/";
            var html = await httpClient.GetStringAsync(url);

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var linksArticle = new HashSet<string>();

            var articles = doc.DocumentNode.SelectNodes("//article");


            if (articles != null)
            {
                foreach (var article in articles)
                {
                    var links = article.SelectNodes(".//a");

                    if (links != null)
                    {
                        foreach (var link in links)
                        {
                            var href = link.GetAttributeValue("href", "");
                            linksArticle.Add(href);
                        }
                    }
                }
            }


            foreach (var linkArticle in linksArticle)
            {
                var urlArticle = $"https://www.lequipe.fr{linkArticle}";
                var htmlArticle = await httpClient.GetStringAsync(urlArticle);

                var docArticle = new HtmlDocument();
                docArticle.LoadHtml(htmlArticle);

                var articleText = "";
                var paragraphs = docArticle.DocumentNode.SelectNodes("//*[@class='Paragraph__content']");

                if (paragraphs != null)
                {
                    foreach (var paragraph in paragraphs)
                    {
                        articleText += paragraph.InnerText;
                    }
                }

                listeArticles.Add(new Article(urlArticle, articleText));
            }

        }

        return listeArticles;
    }

    public static async Task<List<Article>> ScrapOneFootball()
    {
        List<Article> listeArticles = new List<Article>();
        using (var httpClient = new HttpClient())
        {
            var url = "https://onefootball.com/fr/competition/ligue-1-uber-eats-23";
            var html = await httpClient.GetStringAsync(url);

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var links = new HashSet<string>();

            var linkNodes = doc.DocumentNode
                .Descendants("a")
                .Where(a => a.GetAttributeValue("href", "").StartsWith("/fr/news"));

            foreach (var linkNode in linkNodes)
            {
                var link = linkNode.GetAttributeValue("href", "");

                if (!links.Contains(link))
                {
                    links.Add(link);
                }
            }

            foreach (var linkNews in links)
            {
                var news = $"https://onefootball.com{linkNews}";
                var htmlNews = await httpClient.GetStringAsync(news);

                var docNews = new HtmlDocument();
                docNews.LoadHtml(htmlNews);

                var articleText = "";

                var divs = docNews.DocumentNode.SelectNodes("//*[@class='ArticleParagraph_articleParagraph__MrxYL']");

                if (divs != null)
                {
                    foreach (var div in divs)
                    {
                        var paragraph = div.SelectSingleNode(".//p");

                        if (paragraph != null)
                        {
                            articleText += paragraph.InnerText;
                        }
                    }
                }

                listeArticles.Add(new Article(news, articleText));
            }
        }

        return listeArticles;
    }
}