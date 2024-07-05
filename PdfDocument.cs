using System.Drawing;
using BitMiracle.Docotic.Pdf;


namespace Praktika2024
{
    /// <summary>
    /// Класс для работы с pdf документом
    /// </summary>
    internal class PdfDocument : IDocument
    {
        /// <summary>
        /// объект библиотеки для работы с pdf файлом
        /// </summary>
        private BitMiracle.Docotic.Pdf.PdfDocument doc;

        /// <summary>
        /// имя файла
        /// </summary>
        private string fileName;
        public string FileName { get { return fileName; } }

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="fileName">имя файла</param>
        public PdfDocument(string fileName)
        {
            this.fileName = fileName;
            doc = new BitMiracle.Docotic.Pdf.PdfDocument(fileName);
        }

        /// <summary>
        /// Возвращает число страниц документа
        /// </summary>
        /// <returns>число страниц</returns>
        public int GetPageCount()
        {
            return doc.PageCount;
        }

        /// <summary>
        /// Возвращает название документа
        /// </summary>
        /// <returns>название документа</returns>
        public string GetTitle()
        {
            return doc.Info.Title;
        }
        /// <summary>
        /// Возвращает обрезанный битмап с изображением
        /// </summary>
        /// <param name="pageIndex">номер страницы</param>
        /// <returns>битмап с изображением</returns>
        public Bitmap GetDrawingBitmap(int pageIndex)
        {
            PdfPage page = doc.GetPage(pageIndex);
            PdfDrawOptions options = PdfDrawOptions.CreateFitSize(new PdfSize(page.Width, page.Height), true);
            options.BackgroundColor = new PdfRgbColor(255, 255, 255);
            Bitmap drawingBitmap = null;
            using (var memoryStream = new MemoryStream())
            {
                page.Save(memoryStream, options);
                drawingBitmap = new Bitmap(memoryStream);
            }
            return drawingBitmap;
        }

        /// <summary>
        /// Обрезает страницу документа по фактическим границам изображения на ней
        /// </summary>
        /// <param name="pageIndex">номер страницы</param>
        public void CropPdfPage(int pageIndex)
        {
            PdfPage page = doc.GetPage(pageIndex);
            PdfDrawOptions options = PdfDrawOptions.CreateFitSize(new PdfSize(page.Width, page.Height), true);
            options.BackgroundColor = new PdfRgbColor(255, 255, 255);
            Bitmap fullPageBitmap = null;
            using (var memoryStream = new MemoryStream())
            {
                page.Save(memoryStream, options);
                fullPageBitmap = new Bitmap(memoryStream);
            }
            Rectangle bounds = GetDrawingBounds(fullPageBitmap);
            var box = new PdfBox(bounds.Left, page.Height - bounds.Bottom, bounds.Left + bounds.Width, page.Height - bounds.Bottom + bounds.Height);
            page.CropBox = box;
            page.MediaBox = box;
            page.TrimBox = box;
            page.BleedBox = box;
            page.ArtBox = box;
        }

        /// <summary>
        /// Сохраняет документ
        /// </summary>
        /// <param name="fileName">имя файла</param>
        public void SaveDocument(string fileName)
        {
            doc.Save(fileName);
        }

    /// <summary>
    /// Определяет границы изображения на странице документа
    /// </summary>
    /// <param name="bitmap">битмап страницы документа</param>
    /// <returns>область, определяющая границы чертежа</returns>
    private Rectangle GetDrawingBounds(Bitmap bitmap)
        {
            int left = -1, right = -1, top = -1, bottom = -1;
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);
                    if (!pixelColor.ToArgb().Equals(Color.White.ToArgb()))
                    {
                        left = x;
                        break;
                    }
                }
                if (left >= 0)
                    break;
            }
            for (int x = bitmap.Width - 1; x >= 0; x--)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);
                    if (!pixelColor.ToArgb().Equals(Color.White.ToArgb()))
                    {
                        right = x;
                        break;
                    }
                }
                if (right >= 0)
                    break;
            }
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);
                    if (!pixelColor.ToArgb().Equals(Color.White.ToArgb()))
                    {
                        top = y;
                        break;
                    }
                }
                if (top >= 0)
                    break;
            }
            for (int y = bitmap.Height - 1; y >= 0; y--)
            {
                for (int x = bitmap.Width - 1; x >= 0; x--)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);
                    if (!pixelColor.ToArgb().Equals(Color.White.ToArgb()))
                    {
                        bottom = y;
                        break;
                    }
                }
                if (bottom >= 0)
                    break;
            }
            // Возвращаем прямоугольник с границами чертежа
            return Rectangle.FromLTRB(left, top, right, bottom);
        }
    }
}
