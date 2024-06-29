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
        /// dpi принтера
        /// </summary>
        private int dpi;

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="args">аргументы командной строки</param>
        public CommandLineConfig(string[] args)
        {
            documentName = args[0];
            sheetSize = new SizeF { Width = (float)Convert.ToDouble(args[1]), Height = (float)Convert.ToDouble(args[2]) };
            dpi = Convert.ToInt32(args[3]);
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
        /// Возвращает dpi принтера
        /// </summary>
        /// <returns>dpi принтера</returns>
        public int GetPrinterDpi()
        {
            return dpi;
        }
    }
}
