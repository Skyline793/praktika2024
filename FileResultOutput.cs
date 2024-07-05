using System.Drawing;
using System.Runtime.Intrinsics.Arm;

namespace Praktika2024
{
    /// <summary>
    /// Класс, выводящий результаты программы в текстовый файл
    /// </summary>
    internal class FileResultOutput : IResultOutput
    {
        /// <summary>
        /// имя файла
        /// </summary>
        private string fileName;

        /// <summary>
        /// Конструктор
        /// </summary>
        public FileResultOutput()
        {
            fileName = GenerateFileName();
        }

        /// <summary>
        /// Выводит название файла
        /// </summary>
        /// <param name="title">название файла</param>
        public void OutputDocumentTitle(string title)
        {
            using (StreamWriter output = new StreamWriter(fileName, true))
            {
                output.WriteLine("Документ '{0}'\n", title);
                output.Close();
            }
        }

        /// <summary>
        /// Выводит входные параметры
        /// </summary>
        /// <param name="sheetSize">размеры листа</param>
        /// <param name="dpi">исходный DPI</param>
        public void OutputDrawingInfo(int pageNum, SizeF drawingSize, double fillPercentage, ImageAnalyzer.Placement placement)
        {
            using (StreamWriter output = new StreamWriter(fileName, true))
            {
                output.WriteLine("Чертеж на странице {0}:\nРазмеры чертежа: {1:f1} x {2:f1} мм\nПроцент заполнения: {3:f2}%",
                    pageNum, drawingSize.Width, drawingSize.Height,fillPercentage);
                if (placement == ImageAnalyzer.Placement.PotrtaitOrientation)
                    output.WriteLine("Чертеж может быть распечатан на выбранном листе в книжной ориентации\n");
                else if (placement == ImageAnalyzer.Placement.LandscapeOrientation)
                    output.WriteLine("Чертеж может быть распечатан на выбранном листе в альбомной ориентации\n");
                else
                    output.WriteLine("Чертеж невозможно распечатать на выбранном листе\n");
                output.Close();
            }
        }

        /// <summary>
        /// Выводит информацию о чертеже
        /// </summary>
        /// <param name="pageNum">номер страницы документа, содержащей чертеж</param>
        /// <param name="drawingSize">размеры чертежа</param>
        /// <param name="fillPercentage">процент заполнения</param>
        /// <param name="fits">флаг, указывающий, помещается ли чертеж на лист</param>
        public void OutputSourceInfo(SizeF sheetSize, int dpi, string printerName)
        {
            using (StreamWriter output = new StreamWriter(fileName, true))
            {
                string sheetSizeInfo;
                if (sheetSize.Height == int.MaxValue)
                    sheetSizeInfo = string.Format("Размер листа принтера: ширина - {0} мм, высота - не ограничена\n", sheetSize.Width);
                else
                    sheetSizeInfo = string.Format("Размер листа принтера: ширина - {0} мм, высота - {1} мм\n", sheetSize.Width, sheetSize.Height);
                output.WriteLine(sheetSizeInfo + "Исходное разрешение: {2} DPI\nИмя принтера для печати: {3}\n", sheetSize.Width, sheetSize.Height, dpi, printerName);
                output.Close();
            }
        }

        /// <summary>
        /// Выводит информацию о том, возможно ли распечатать документ
        /// </summary>
        /// <param name="printable">флаг возможности печати</param>
        /// <param name="printedFileName"></param>
        public void OutputPrintable(bool printable, string printedFileName)
        {
            using (StreamWriter output = new StreamWriter(fileName, true))
            {
                string info;
                if (printable)
                    info = string.Format("Документ может быть распечатан целиком. Оптимизированный документ, который будет напечатан, сохранен под именем: {0}\n", printedFileName);
                else
                    info = "Документ нельзя распечатать целиком, поскольку некоторые страницы невозможно разместить на выбранном формате листа";
                output.WriteLine(info);
                output.Close();
            }
            
        }

        /// <summary>
        /// генерирует уникальное имя файла
        /// </summary>
        /// <returns>имя выходного файла</returns>
        private string GenerateFileName()
        {
            int i;
            string path;
            for(i=1; ; i++)
            {
                path = String.Format("AnalysisResult{0}.txt", i);
                if (!File.Exists(path))
                    break;
            }
            return path;
        }
    }
}
