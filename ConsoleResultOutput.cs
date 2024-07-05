using System.Drawing;

namespace Praktika2024
{
    /// <summary>
    /// Класс, выводящий результаты программы в консоль
    /// </summary>
    internal class ConsoleResultOutput : IResultOutput
    {
        /// <summary>
        /// Выводит название документа
        /// </summary>
        /// <param name="title">название документа</param>
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
        public void OutputDrawingInfo(int pageNum, SizeF drawingSize, double fillPercentage, ImageAnalyzer.Placement placement)
        {
            Console.WriteLine("Чертеж на странице {0}\nРазмеры чертежа: {1:f1} x {2:f1} мм\nПроцент заполнения: {3:f2}%",
                    pageNum, drawingSize.Width, drawingSize.Height, fillPercentage);
            if (placement == ImageAnalyzer.Placement.PotrtaitOrientation)
                Console.WriteLine("Чертеж может быть распечатан на выбранном листе в книжной ориентации\n");
            else if (placement == ImageAnalyzer.Placement.LandscapeOrientation)
                Console.WriteLine("Чертеж может быть распечатан на выбранном листе в альбомной ориентации\n");
            else
                Console.WriteLine("Чертеж невозможно распечатать на выбранном листе\n");
        }

        /// <summary>
        /// Выводит информацию о том, возможно ли распечатать документ
        /// </summary>
        /// <param name="printable">флаг возможности печати</param>
        /// <param name="printedFileName">имя оптимизированного файла для печати</param>
        public void OutputPrintable(bool printable, string printedFileName)
        {
            string info;
            if (printable)
                info = string.Format("Документ может быть распечатан целиком. Оптимизированный документ, который будет напечатан, сохранен под именем: {0}\n", printedFileName);
            else
                info = "Документ нельзя распечатать целиком, поскольку некоторые страницы невозможно разместить на выбранном формате листа\n";
            Console.WriteLine(info);
        }

        /// <summary>
        /// Выводит входные параметры
        /// </summary>
        /// <param name="sheetSize">размеры листа</param>
        /// <param name="dpi">исходный DPI</param>
        /// <param name="printerName"></param>
        public void OutputSourceInfo(SizeF sheetSize, int dpi, string printerName)
        {
            string sheetSizeInfo;
            if (sheetSize.Height == int.MaxValue)
                sheetSizeInfo = string.Format("Размер листа принтера: ширина - {0} мм, высота - не ограничена\n", sheetSize.Width);
            else
                sheetSizeInfo = string.Format("Размер листа принтера: ширина - {0} мм, высота - {1} мм\n", sheetSize.Width, sheetSize.Height);
            Console.WriteLine(sheetSizeInfo + "Исходное разрешение: {2} DPI\nИмя принтера для печати: {3}\n", sheetSize.Width, sheetSize.Height, dpi, printerName);
        }
    }
}
