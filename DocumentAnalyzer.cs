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

        /// <summary>
        /// Аналмзатор чертежей
        /// </summary>
        private DrawingAnalyzer analyzer;

        /// <summary>
        /// Конфиг программы
        /// </summary>
        private IConfig config;

        /// <summary>
        /// Вывод результатов анализа
        /// </summary>
        private IResultOutput output;

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
                output.OutputDrawingInfo(i+1, size, fillPercentage, placement);
            }
        }
    }
}
