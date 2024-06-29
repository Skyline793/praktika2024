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
        /// dpi принтера
        /// </summary>
        private int dpi;

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
            dpi = int.Parse(data["Settings"]["dpi"]);
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
