using System.Drawing.Printing;

namespace Praktika2024
{
    /// <summary>
    /// Интерфейс принтеров для печати документов
    /// </summary>
    internal interface IPrinter
    {
        /// <summary>
        /// Открыть документ
        /// </summary>
        /// <param name="fileName">имя документа</param>
        public abstract void OpenDocument(string fileName);

        /// <summary>
        /// Печать документа
        /// </summary>
        /// <param name="printerSettings">Параметры печати</param>
        public abstract void Print(PrinterSettings printerSettings);
    }
}
