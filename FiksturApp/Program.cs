using System;
using System.Net.Http;
using HtmlAgilityPack;

class Program
{
    static void Main()
    {
        GetFixture().Wait();
    }

    static async Task GetFixture()
    {
        var url = "https://www.fanatik.com.tr/fenerbahce/fenerbahce-fikstur-fenerbahce-2023-2024-sezonu-derbi-haftalari-ve-mac-programi-2518297";
        var httpClient = new HttpClient(); 
        var html = await httpClient.GetStringAsync(url);

        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(html);

        var fixtures = htmlDocument.DocumentNode.SelectSingleNode("//p[strong[contains(text(), 'Fenerbahçe fikstür')]]/following-sibling::ul[1]").SelectNodes("li");//belirli bir XPath ifadesine uyan HTML elementlerini seçer.

        foreach (var fixture in fixtures)
        {
            Console.WriteLine(fixture.InnerText);
        }
    }
}
