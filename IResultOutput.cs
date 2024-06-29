using System.Drawing;

namespace Praktika2024
{
    /// <summary>
    /// Интерфейс вывода результатов программы
    /// </summary>
    internal interface IResultOutput
    {
        /// <summary>
        /// Выводит название файла
        /// </summary>
        /// <param name="title">название файла</param>
        public abstract void OutputDocumentTitle(string title);

        /// <summary>
        /// Выводит параметры принтера
        /// </summary>
        /// <param name="sheetSize">размеры листа</param>
        /// <param name="dpi">DPI принтера</param>
        public abstract void OutputPrinterInfo(SizeF sheetSize, int dpi);

        /// <summary>
        /// Выводит информацию о чертеже
        /// </summary>
        /// <param name="pageNum">номер страницы документа, содержащей чертеж</param>
        /// <param name="drawingSize">размеры чертежа</param>
        /// <param name="fillPercentage">процент заполнения</param>
        /// <param name="fits">флаг, указывающий, помещается ли чертеж на лист</param>
        public abstract void OutputDrawingInfo(int pageNum, Size drawingSize, double fillPercentage, DrawingAnalyzer.Placement placement);
    }
}
