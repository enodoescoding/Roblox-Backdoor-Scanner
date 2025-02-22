// See https://aka.ms/new-console-template for more information
using RobloxFiles;
using RobloxFiles.DataTypes;
using RobloxModelScanner;
using System.Net.Http;

int page = 0;

while (true)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("Enter a search term (leave empty for front page)");

    Console.Write("> ");
    Console.ResetColor();

    string? search = Console.ReadLine();

    if (search == null)
    {
        search = "";
    }

    Scanner scanner = new Scanner();

    while (true)
    {
        scanner.ScanPage(page, search).Wait();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Current Page: {page}  Press enter to scan the next page. Type E to exit. Enter a number to jump to a page.");
        Console.Write("> ");
        Console.ResetColor();

        string? input = Console.ReadLine();

        if (input == "E" || input == "e")
        {
            break;
        }

        if (int.TryParse(input, out int newPage))
        {
            page = newPage;
        }
        else
        {
            page++; // Increment to next page
        }
    }
}