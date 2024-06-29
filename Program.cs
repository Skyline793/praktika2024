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
        IResultOutput output = new ConsoleResultOutput();
        DrawingAnalyzer drawingAnalyzer = new DrawingAnalyzer();
        DocumentAnalyzer documentAnalyzer = new DocumentAnalyzer(doc, drawingAnalyzer, reader, output);
        documentAnalyzer.Analyze();
    }
}

