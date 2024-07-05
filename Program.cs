using System.Drawing.Printing;

namespace Praktika2024;

class Program
{
    static void Main(string[] args)
    {
        BitMiracle.Docotic.LicenseManager.AddLicenseData("6C0LE-Q3IX5-X9F6B-IINMD-N01ZD");
        IConfig config;
        ///проверяем задание конфигурации программы через аргументы командной строки
        if (args.Length == 0)
        {
            try
            {
                const string configPath = "config.ini";
                config = new FileConfig(configPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Нажмите любую клавишу для выхода.");
                Console.Read();
                return;
            }
        }
        else if (args.Length == 4)
        {
            try
            {
                config = new CommandLineConfig(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Нажмите любую клавишу для выхода.");
                Console.Read();
                return;
            }
        }
        else
        {
            Console.WriteLine("Некорректное число аргументов командной строки");
            return;
        }
        string pdfFilePath = config.GetDocumentName();
        PdfDocument doc;
        try
        {
            doc = new PdfDocument(pdfFilePath);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return;
        }
        IResultOutput consoleOutput = new ConsoleResultOutput();
        ImageAnalyzer drawingAnalyzer = new ImageAnalyzer();
        IResultOutput fileOutput = new FileResultOutput();
        string printedDocName = "printed.pdf";
        DocumentAnalyzer documentAnalyzer = new DocumentAnalyzer(doc, drawingAnalyzer, config, printedDocName);
        documentAnalyzer.AddOutput(consoleOutput);
        documentAnalyzer.AddOutput(fileOutput);
        if (documentAnalyzer.Analyze())
        {
            IPrinter printer = new PdfPrinter();
            try
            {
                printer.OpenDocument(printedDocName);
                PrinterSettings settings = new PrinterSettings();
                settings.PrinterName = config.GetPrinterName();
                settings.DefaultPageSettings.PaperSize = new PaperSize("Custom",
                    (int)(config.GetSheetSize().Width / 25.4 * 100), (int)(config.GetSheetSize().Height / 25.4 * 100));
                settings.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
                printer.Print(settings);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        Console.WriteLine("Работа программы завершена. Нажмите любую клавишу для выхода.");
        Console.Read();
    }
}

