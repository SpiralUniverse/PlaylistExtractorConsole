using System;
using System.IO;
using System.Text.RegularExpressions;


string m3uFilePath;
string outputFilePath = "song_info.txt";

if(args.Length != 1)
{
    System.Console.WriteLine("Usage: M3uParser <m3u_file>");
    if(args.Length == 0)
    {
        System.Console.WriteLine("Please Enter a proper m3u file path (abslute)");

    }
    else return;
}

m3uFilePath = Console.ReadLine();


if(!File.Exists(m3uFilePath)) 
{
    System.Console.WriteLine("M3U file not found");
    return;
}

try
{
    using (StreamReader reader = new StreamReader(m3uFilePath))
    using (StreamWriter writer = new StreamWriter(outputFilePath))
    {
        string line;
        int linenb = 0;
        while ((line = reader.ReadLine()) != null)
        {
            linenb++;
            if(line.StartsWith("#EXTINF"))
            {
                Match match = Regex.Match(line, @"EXTINF:\d+, - (.+?)\s*_(.+?)$");
                if(match.Success)
                {
                    string songName = match.Groups[1].Value.Trim();
                    string artistName = match.Groups[2].Value.Trim();
                    writer.WriteLine($"{songName} - {artistName}");
                }
            }
        }
    }
    System.Console.WriteLine("Songs data extracted and saved to " + outputFilePath);
}
catch (System.Exception ex)
{
    
    System.Console.WriteLine("Error: " + ex.Message);
}