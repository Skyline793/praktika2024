using System.Drawing;

namespace Praktika2024
{
    /// <summary>
    /// Интерфейс документа
    /// </summary>
    internal interface IDocument
    {
        /// <summary>
        /// Возвращает число страниц документа
        /// </summary>
        /// <returns>число страниц</returns>
        public abstract int GetPageCount();

        /// <summary>
        /// Возвращает обрезанный битмап с изображением
        /// </summary>
        /// <param name="pageIndex">номер страницы</param>
        /// <returns>битмап с изображением</returns>
        public abstract Bitmap GetDrawingBitmap(int pageIndex);

        /// <summary>
        /// Возвращает название документа
        /// </summary>
        /// <returns>название документа</returns>
        public abstract string GetTitle();
    }
}
