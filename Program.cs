namespace Praktika2024;

class Program
{
    static void Main(string[] args)
    {
        BitMiracle.Docotic.LicenseManager.AddLicenseData("6C0LE-Q3IX5-X9F6B-IINMD-N01ZD");
        IConfig reader;
        ///проверяем задание конфигурации программы через аргументы командной строки
        if (args.Length == 0)
        {
            try
            {
                const string configPath = "config.ini";
                reader = new FileConfig(configPath);
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
                reader = new CommandLineConfig(args);
            }
            catch(Exception e)
            {
                Console.WriteLine (e.Message);
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
        string pdfFilePath = reader.GetDocumentName();
        PdfDocument doc;
        try
        {
            doc = new PdfDocument(pdfFilePath);
        } catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return;
        }
        IResultOutput consoleOutput = new ConsoleResultOutput();
        DrawingAnalyzer drawingAnalyzer = new DrawingAnalyzer();
        DocumentAnalyzer documentAnalyzer = new DocumentAnalyzer(doc, drawingAnalyzer, reader, consoleOutput);
        documentAnalyzer.Analyze();
        IResultOutput fileOutput = new FileResultOutput();
        documentAnalyzer.Output = fileOutput;
        documentAnalyzer.Analyze();
        Console.WriteLine("Работа программы завершена. Нажмите любую клавишу для выхода.");
        Console.Read();
    }
}

