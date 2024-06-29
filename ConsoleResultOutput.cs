using System.Drawing;
using static Praktika2024.DrawingAnalyzer;

namespace Praktika2024
{
    /// <summary>
    /// Класс, выводящий результаты программы в консоль
    /// </summary>
    internal class ConsoleResultOutput : IResultOutput
    {
        /// <summary>
        /// Выводит название файла
        /// </summary>
        /// <param name="title">название файла</param>
        public void OutputDocumentTitle(string title)
        {
            Console.WriteLine("Документ '{0}'\n", title);
        }

        /// <summary>
        /// Выводит информацию о чертеже
        /// </summary>
        /// <param name="pageNum">номер страницы документа, содержащей чертеж</param>
        /// <param name="drawingSize">размеры чертежа</param>
        /// <param name="fillPercentage">процент заполнения</param>
        /// <param name="fits">флаг, указывающий, помещается ли чертеж на лист</param>
        public void OutputDrawingInfo(int pageNum, Size drawingSize, double fillPercentage, DrawingAnalyzer.Placement placement)
        {
            Console.WriteLine("Чертеж на странице {0}\nРазмеры чертежа: {1} на {2} точек\nПроцент заполнения: {3:f2}%",
                    pageNum, drawingSize.Width, drawingSize.Height, fillPercentage);
            if (placement == DrawingAnalyzer.Placement.PotrtaitOrientation)
                Console.WriteLine("Чертеж может быть распечатан на выбранном листе с указанным DPI в книжной ориентации\n");
            else if (placement == DrawingAnalyzer.Placement.LandscapeOrientation)
                Console.WriteLine("Чертеж может быть распечатан на выбранном листе с указанным DPI в альбомной ориентации\n");
            else
                Console.WriteLine("Чертеж невозможно распечатать на выбранном листе с указанным DPI\n");
        }

        /// <summary>
        /// Выводит параметры принтера
        /// </summary>
        /// <param name="sheetSize">размеры листа</param>
        /// <param name="dpi">DPI принтера</param>
        public void OutputPrinterInfo(SizeF sheetSize, int dpi)
        {
            Console.WriteLine("Размер листа принтера: {0}x{1} мм.\nDPI = {2}\n", sheetSize.Width, sheetSize.Height, dpi);
        }
    }
}
