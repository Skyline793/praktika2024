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
        private DrawingAnalyzer analyzer;
        public DrawingAnalyzer Analyzer { get { return analyzer; } set { analyzer = value; } }

        /// <summary>
        /// Конфиг программы
        /// </summary>
        private IConfig config;
        public IConfig Config { get { return config; } set { config = value; } }

        /// <summary>
        /// Вывод результатов анализа
        /// </summary>
        private IResultOutput output;
        public IResultOutput Output { get { return output; } set { output = value; } }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="document">Документ</param>
        /// <param name="analyzer">Аналмзатор чертежей</param>
        /// <param name="config">Конфиг программы</param>
        /// <param name="output">Вывод результатов анализа</param>
        public DocumentAnalyzer(IDocument document, DrawingAnalyzer analyzer, IConfig config, IResultOutput output)
        {
            this.document = document;
            this.analyzer = analyzer;
            this.config = config;
            this.output = output;
        }

        /// <summary>
        /// Анализирует документ постранично и выводит результаты
        /// </summary>
        public void Analyze()
        {
            output.OutputDocumentTitle(document.GetTitle());
            output.OutputPrinterInfo(config.GetSheetSize(), config.GetPrinterDpi());
            for (int i = 0; i < document.GetPageCount(); i++)
            {
                Bitmap drawing = document.GetDrawingBitmap(i);
                Size size = analyzer.GetDrawingSize(drawing);
                double fillPercentage = analyzer.CalculateFillPercentage(drawing);
                DrawingAnalyzer.Placement placement = analyzer.IsDrawingFits(drawing, config.GetSheetSize(), config.GetPrinterDpi());
                output.OutputDrawingInfo(i + 1, size, fillPercentage, placement);
            }
        }

    }
}
