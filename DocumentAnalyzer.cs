using System.Drawing;

namespace Praktika2024
{
    /// <summary>
    /// класс-фасад для анализа всего документа
    /// </summary>
    internal class DocumentAnalyzer
    {
        /// <summary>
        /// Документ
        /// </summary>
        private IDocument document;
        public IDocument Document { get { return document; } set { document = value; } }

        /// <summary>
        /// Аналмзатор чертежей
        /// </summary>
        private ImageAnalyzer analyzer;
        public ImageAnalyzer Analyzer { get { return analyzer; } set { analyzer = value; } }

        /// <summary>
        /// Конфиг программы
        /// </summary>
        private IConfig config;
        public IConfig Config { get { return config; } set { config = value; } }

        /// <summary>
        /// Список объектов для вывода результатов
        /// </summary>
        private List<IResultOutput> outputers;

        /// <summary>
        /// имя оптимизированного документа для печати
        /// </summary>
        private string printedFileName;
        public string PrintedFileName { get { return printedFileName; } set { printedFileName = value; } }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="document">Документ</param>
        /// <param name="analyzer">Анализатор чертежей</param>
        /// <param name="config">Конфиг программы</param>
        public DocumentAnalyzer(IDocument document, ImageAnalyzer analyzer, IConfig config, string printedFileName)
        {
            outputers = new List<IResultOutput>();
            this.document = document;
            this.analyzer = analyzer;
            this.config = config;
            this.printedFileName = printedFileName;
        }

        /// <summary>
        /// Добавить новый вывод
        /// </summary>
        /// <param name="output">вывод результатов</param>
        public void AddOutput(IResultOutput output)
        {
            outputers.Add(output);
        }

        /// <summary>
        /// Удалить вывод результатов из списка
        /// </summary>
        /// <param name="output">вывод результатов</param>
        public void RemoveOutput(IResultOutput output)
        {
            outputers.Remove(output);
        }

        /// <summary>
        /// Анализирует документ постранично и выводит результаты
        /// </summary>
        /// <returns>true - если весь документ можно распечатать, иначе false</returns>
        public bool Analyze()
        {
            bool printable = true;
            foreach (var output in outputers)
            {
                output.OutputDocumentTitle(document.GetTitle());
            }
            int dpi = config.GetDpi();
            foreach (var output in outputers)
            {
                output.OutputSourceInfo(config.GetSheetSize(), dpi, config.GetPrinterName());
            }
            for (int i = 0; i < document.GetPageCount(); i++)
            {
                document.CropPdfPage(i);
                Bitmap drawing = document.GetDrawingBitmap(i);
                SizeF drawingSize = analyzer.GetImageMmSize(drawing, dpi);
                double fillPercentage = analyzer.CalculateFillPercentage(drawing);
                ImageAnalyzer.Placement placement = analyzer.IsImageFits(drawing, config.GetSheetSize(), config.GetDpi());
                if (placement == ImageAnalyzer.Placement.NotFitting)
                    printable = false;
                foreach (var output in outputers)
                {
                    output.OutputDrawingInfo(i + 1, drawingSize, fillPercentage, placement);
                }
            }
            foreach (var output in outputers)
            {
                output.OutputPrintable(printable, printedFileName);
            }
            if (printable)
            {
                document.SaveDocument(printedFileName);

            }
            return printable;
        }
    }
}
