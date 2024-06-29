using System.Drawing;

namespace Praktika2024
{
    /// <summary>
    /// Интерфейс классов, считывающих конфигурацию программы
    /// </summary>
    internal interface IConfig
    {
        /// <summary>
        /// Возвращает имя файла с документом
        /// </summary>
        /// <returns>имя файла</returns>
        public abstract string GetDocumentName();

        /// <summary>
        /// Возвращает размеры листа принтера
        /// </summary>
        /// <returns>размеры листа</returns>
        public abstract SizeF GetSheetSize();

        /// <summary>
        /// Возвращает dpi принтера
        /// </summary>
        /// <returns>dpi принтера</returns>
        public abstract int GetPrinterDpi();
    }
}
