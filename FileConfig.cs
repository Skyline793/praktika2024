using IniFileParser.Model;
using System.Drawing;

namespace Praktika2024
{
    /// <summary>
    /// класс, считывающий конфиг программы из файла .ini
    /// </summary>
    internal class FileConfig : IConfig
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
        /// имя принтера
        /// </summary>
        private string printerName;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="configFileName">имя конфигурационного файла</param>
        public FileConfig(string configFileName)
        {
            var parser = new IniFileParser.IniFileParser();
            IniData data = parser.ReadFile(configFileName);

            documentName = data["Settings"]["docPath"];
            sheetSize = new SizeF
            {
                Width = float.Parse(data["Settings"]["sheetWidth"]),
                Height = float.Parse(data["Settings"]["sheetHeight"])
            };
            if (sheetSize.Height == 0)
                sheetSize.Height = int.MaxValue;
            dpi = int.Parse(data["Settings"]["dpi"]);
            printerName = data["Settings"]["printerName"];
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
