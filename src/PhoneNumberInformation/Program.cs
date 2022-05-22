// Получение данных о номере с сервисов zvonili.com и numbers.ba-za.net

using HtmlAgilityPack;

var numbers = File.ReadAllLines("Inp.txt");
var results = new List<string>();

var web = new HtmlWeb();
foreach (var number in numbers)
{
    try
    {
        var response = web.Load($"https://zvonili.com/phone/{number}");
        var node = response.DocumentNode.SelectSingleNode("/html/body/div[2]/div[1]/div[1]/div[4]/span");

        if (node != null && node.InnerHtml != "")
        {
            results.Add($"{number}, {node.InnerHtml}");
            Console.WriteLine($"{number}, {node.InnerHtml}");
            continue;
        }
        
        response = web.Load($"https://numbers.ba-za.net/number/russia/{number}/");
        node = response.DocumentNode.SelectSingleNode("//*[@id=\"info_number\"]/div[3]/strong");
        
        if (node != null && node.InnerHtml != "▓▓▓▓▓▓▓▓▓▓▓▓")
        {
            results.Add($"{number}, {node.InnerHtml}");
            Console.WriteLine($"{number}, {node.InnerHtml}");
            continue;
        }
        
        results.Add($"{number}, НЕТ ДАННЫХ!");
        Console.WriteLine($"{number}, НЕТ ДАННЫХ!");
    }
    catch (Exception ex)
    {
        results.Add($"{number}, {ex.Message}");
        Console.WriteLine($"{number}, {ex.Message}");
    }
}

File.WriteAllLines("Out.txt", results);

return 0;