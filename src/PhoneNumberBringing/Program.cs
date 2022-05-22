// Приведение номеров к виду +7**********, удаление повторов

using System.Text.RegularExpressions;

var inpData = File.ReadAllLines("Inp.txt")
    .Select(s => new Regex("\\d+").Match(s).Value)
    .Select(s => new Regex("^3").Replace(s, "+73"))
    .Select(s => new Regex("^4").Replace(s, "+74"))
    .Select(s => new Regex("^7").Replace(s, "+7"))
    .Select(s => new Regex("^8").Replace(s, "+7"))
    .Select(s => new Regex("^9").Replace(s, "+79"))
    .Where(s => s.Length > 7)
    .Distinct();

/*
for (var i = 0; i < arr.Count; i++)
{
    switch (arr[i].Length)
    {
        case 10:
            arr[i] = arr[i].Insert(0, "+7");
            break;
        case 11 when arr[i][0] == '7' || arr[i][0] == '8':
            arr[i] = arr[i].Remove(0, 1);
            arr[i] = arr[i].Insert(0, "+7");
            break;
    }
}
*/

File.WriteAllLines("Out.txt", inpData);