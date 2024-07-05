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
        /// Возвращает исходный dpi
        /// </summary>
        /// <returns>исходный dpi</returns>
        public abstract int GetDpi();

        /// <summary>
        /// Возвращает имя принтера для печати
        /// </summary>
        /// <returns>имя принтера</returns>
        public abstract string GetPrinterName();
    }
}
