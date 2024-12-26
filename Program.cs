using System.Globalization;
using System.Text;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;
var culture = CultureInfo.CreateSpecificCulture("tr-TR");

CheckpointPS:
Console.WriteLine("Prefix or Suffix? P(p)/S(s)");
var ps = Console.ReadLine();
if (!string.IsNullOrEmpty(ps))
{
    if (ps.Length > 1)
    { Console.WriteLine("Please input valid option - 1 digit allowed!"); goto CheckpointPS; }

    bool isPrefix = ps[0] is 'p' or 'P';

    Console.WriteLine("Please write value to replace");
    var valueToReplace = Console.ReadLine();

    Console.WriteLine($"Please write value to put on {(ps[0] is 'p' or 'P' ? "'Prefix'" : "'Suffix'")}");
    var valueForReplace = Console.ReadLine();

    var filesToReplace = Directory.GetFiles(Environment.CurrentDirectory).Where(fn => isPrefix ? Path.GetFileName(fn).StartsWith(valueToReplace!, true, culture) : Path.GetFileName(fn).EndsWith(valueToReplace!, true, culture));

    foreach (string item in filesToReplace)
    {
        var fileName = Path.GetFileName(item).Replace(valueToReplace!, valueForReplace!, StringComparison.OrdinalIgnoreCase);
        var folderName = Path.GetDirectoryName(item);
        var combined = Path.Combine(folderName!, fileName);

        File.Move(item, combined);
    }

    Console.WriteLine("Done!");
    Console.WriteLine("Please press any key to exit");
    Console.ReadLine();
}
else
{
    Console.WriteLine("Please input valid option - null not allowed!"); goto CheckpointPS;
}
