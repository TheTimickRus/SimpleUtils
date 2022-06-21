using FuzzySharp;
using Spectre.Console;

const string fileFirst = "First.txt";
const string fileSecond = "Second.txt";

Console.Title = "DiffTextUtils (0.1) by TheTimickRus";

AnsiConsole.WriteLine("\n\n");
var mainRatio = AnsiConsole.Ask("> Enter percentage match", 80);
Console.Clear();

while (true)
{
    var outList = new List<(int, string)>();

    AnsiConsole.Write(
        new FigletText("DiffTextFiles")
            .Color(Color.CadetBlue));
    AnsiConsole.Write(
        new Rule($"DiffTextUtils (0.1) by TheTimickRus | Ratio: {mainRatio}")
            .RightAligned()
            .RuleStyle(new Style(Color.SlateBlue3)));
    AnsiConsole.WriteLine();
    
    var fileFirstText = File.ReadAllLines(fileFirst);
    var fileSecondText = File.ReadAllLines(fileSecond);

    foreach (var fStr in fileFirstText)
    {
        if (string.IsNullOrEmpty(fStr))
            continue;
        
        for (var j = 0; j < fileSecondText.Length; j++)
        {
            var ratio = Fuzz.Ratio(fStr.Trim(), fileSecondText[j].Trim());
            if (ratio > mainRatio)
                outList.Add((j + 1, $"> [royalblue1]{j + 1}, [royalblue1]Ratio: {ratio} %[/] | {fStr} - {fileSecondText[j]}[/]"));
        }
    }

    outList
        .OrderBy(tuple => tuple.Item1)
        .ToList()
        .ForEach(tuple => AnsiConsole.MarkupLine(tuple.Item2));

    AnsiConsole.WriteLine();
    AnsiConsole.MarkupLine("All Done!");

    if (AnsiConsole.Ask<string>("Restart? (y/n): ") is not "y")
        break;
    
    Console.Clear();
}
