using System.Drawing;

namespace Praktika2024
{
    /// <summary>
    /// Класс, считывающий конфиг программы из аргументов командной строки
    /// </summary>
    internal class CommandLineConfig : IConfig
    {
        /// <summary>
        /// имя документа
        /// </summary>
        private string documentName;

        /// <summary>
        /// размеры листа принтера
        /// </summary>
        private SizeF sheetSize;

        /// <summary>
        /// исходный dpi
        /// </summary>
        private int dpi;

        /// <summary>
        /// Имя принтера для печати
        /// </summary>
        private string printerName;

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="args">аргументы командной строки</param>
        public CommandLineConfig(string[] args)
        {
            documentName = args[0];
            sheetSize = new SizeF { Width = float.Parse(args[1]), Height = float.Parse(args[2]) };
            if (sheetSize.Height == 0)
                sheetSize.Height = int.MaxValue;
            dpi = Convert.ToInt32(args[3]);
            printerName = args[4];
        }

        /// <summary>
        /// Возвращает имя файла с документом
        /// </summary>
        /// <returns>имя файла</returns>
        public string GetDocumentName()
        {
            return documentName;
        }

        /// <summary>
        /// Возвращает размеры листа принтера
        /// </summary>
        /// <returns>размеры листа</returns>
        public SizeF GetSheetSize()
        {
            return sheetSize;
        }

        /// <summary>
        /// Возвращает исходный dpi
        /// </summary>
        /// <returns>исходный dpi</returns>
        public int GetDpi()
        {
            return dpi;
        }

        /// <summary>
        /// Возвращает имя принтера для печати
        /// </summary>
        /// <returns>имя принтера</returns>
        public string GetPrinterName()
        {
            return printerName;
        }
    }
}
