class Article
{
    public string url { get; set; }
    public string text { get; set; }

    public Article(string url, string text)
    {
        this.url = url;
        this.text = text;
    }

}