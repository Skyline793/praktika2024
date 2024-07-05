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
        /// Выводит входные параметры
        /// </summary>
        /// <param name="sheetSize">размеры листа</param>
        /// <param name="dpi">исходный DPI</param>
        /// <param name="printerName">имя принтера</param>
        public abstract void OutputSourceInfo(SizeF sheetSize, int dpi, string printerName);

        /// <summary>
        /// Выводит информацию о чертеже
        /// </summary>
        /// <param name="pageNum">номер страницы документа, содержащей чертеж</param>
        /// <param name="drawingSize">размеры чертежа</param>
        /// <param name="fillPercentage">процент заполнения</param>
        /// <param name="fits">флаг, указывающий, помещается ли чертеж на лист</param>
        public abstract void OutputDrawingInfo(int pageNum, SizeF drawingSize, double fillPercentage, ImageAnalyzer.Placement placement);

        /// <summary>
        /// Выводит информацию о том, возможно ли распечатать документ
        /// </summary>
        /// <param name="printable">флаг возможности печати</param>
        /// <param name="printedFileName">имя оптимизированного файла для печати</param>
        public abstract void OutputPrintable(bool printable, string printedFileName);
    }
}
