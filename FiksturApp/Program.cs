using System;
using System.Net.Http;
using HtmlAgilityPack;

class Program
{
    static void Main()
    {

        //Main fonksiyonu asenkron değildir, asenkron GetFixture fonksiyonunu kullanmak için Wait() metodu kullanırız.
        GetFixture().Wait();
    }

    static async Task GetFixture()
    {
        var url = "https://www.fanatik.com.tr/fenerbahce/fenerbahce-fikstur-fenerbahce-2023-2024-sezonu-derbi-haftalari-ve-mac-programi-2518297";

        //belirtilen URL'den HTML içeriğini çekmek istek oluşturma
        var httpClient = new HttpClient();

        //  HttpClient aracılığıyla belirtilen URL'den HTML içeriğini asenkron olarak alır.
        var html = await httpClient.GetStringAsync(url);

        // Bu nesne, HTML içeriğini ayrıştırmak için kullanır
        var htmlDocument = new HtmlDocument();

        //HTML içeriğini ayrıştırır ve HtmlDocument nesnesinin içine yükler.
        htmlDocument.LoadHtml(html);

        //belirli bir XPath ifadesine uyan HTML elementlerini seçer.
        //DocumentNode özelliği, bu HTML içeriğinin en üst düğümüne (root düğümüne) erişmemizi sağlar.
        // metni "Fenerbahçe fikstür" olan bir <strong> elementini seçer.
        //Seçilen bu <strong> elementinin kardeşlerinden bir sonraki <ul> elementini seçer.
        //SelectSingleNode metodu, belirtilen XPath ifadesine uyan ilk HTML elementini seçer. Bu element, Fenerbahçe'nin fikstürünü içeren <ul> elementi olacaktır.
        //.SelectNodes("li"): Seçilen <ul> elementinin içindeki tüm <li> elementlerini seçer. fixtures , fikstürün listesini içeren bir koleksiyon (list) olacaktır.

        var fixtures = htmlDocument.DocumentNode.SelectSingleNode("//p[strong[contains(text(), 'Fenerbahçe fikstür')]]/following-sibling::ul[1]").SelectNodes("li");

        foreach (var fixture in fixtures)
        {
            Console.WriteLine(fixture.InnerText); //element içeriği yazdırılır.
        }
    }
}