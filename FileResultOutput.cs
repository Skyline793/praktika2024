using System.Drawing;

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
        /// Выводит параметры принтера
        /// </summary>
        /// <param name="sheetSize">размеры листа</param>
        /// <param name="dpi">DPI принтера</param>
        public void OutputDrawingInfo(int pageNum, Size drawingSize, double fillPercentage, DrawingAnalyzer.Placement placement)
        {
            using (StreamWriter output = new StreamWriter(fileName, true))
            {
                output.WriteLine("Чертеж на странице {0}:\nРазмеры чертежа: {1} на {2} точек\nПроцент заполнения: {3:f2}%",
                    pageNum, drawingSize.Width, drawingSize.Height,fillPercentage);
                if (placement == DrawingAnalyzer.Placement.PotrtaitOrientation)
                    output.WriteLine("Чертеж может быть распечатан на выбранном листе с указанным DPI в книжной ориентации\n");
                else if (placement == DrawingAnalyzer.Placement.LandscapeOrientation)
                    output.WriteLine("Чертеж может быть распечатан на выбранном листе с указанным DPI в альбомной ориентации\n");
                else
                    output.WriteLine("Чертеж невозможно распечатать на выбранном листе с указанным DPI\n");
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
        public void OutputPrinterInfo(SizeF sheetSize, int dpi)
        {
            using (StreamWriter output = new StreamWriter(fileName, true))
            {
                output.WriteLine("Размер листа принтера: {0}x{1} мм.\nDPI = {2}\n", sheetSize.Width, sheetSize.Height, dpi);
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
