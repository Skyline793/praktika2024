using BitMiracle.Docotic.Pdf;
using BitMiracle.Docotic.Pdf.Gdi;
using System.Drawing;
using System.Drawing.Printing;

namespace Praktika2024
{
    /// <summary>
    /// Принтер для PDF документов
    /// </summary>
    internal class PdfPrinter : IPrinter
    {
        /// <summary>
        /// Объект для работы с печатью
        /// </summary>
        private PrintDocument printDocument;

        /// <summary>
        /// Объект для работы с pdf документом
        /// </summary>
        private BitMiracle.Docotic.Pdf.PdfDocument pdfDoc;

        /// <summary>
        /// индекс текущей страницы
        /// </summary>
        private int currPageIndex;

        /// <summary>
        /// Число страниц документа
        /// </summary>
        private int pageCount;

        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public PdfPrinter()
        {
            printDocument = new PrintDocument();
            printDocument.QueryPageSettings += PrintDocument_QueryPageSettings;
            printDocument.PrintPage += PrintDocument_PrintPage;
        }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="fileName">имя pdf файла</param>
        public PdfPrinter(string fileName)
        {
            pdfDoc = new BitMiracle.Docotic.Pdf.PdfDocument(fileName);
            printDocument = new PrintDocument();
            printDocument.QueryPageSettings += PrintDocument_QueryPageSettings;
            printDocument.PrintPage += PrintDocument_PrintPage;
        }

        /// <summary>
        /// Открыть документ для работы
        /// </summary>
        /// <param name="fileName">имя файла</param>
        public void OpenDocument(string fileName)
        {
            if(pdfDoc != null) {
                pdfDoc.Dispose();
            }
            pdfDoc = new BitMiracle.Docotic.Pdf.PdfDocument(fileName);
            pageCount = pdfDoc.PageCount;
        }

        /// <summary>
        /// Печать документа
        /// </summary>
        /// <param name="settings">параметры печати</param>
        public void Print(PrinterSettings printerSettings)
        {
            if (printDocument is null)
                return;
            printDocument.PrinterSettings = printerSettings;
            printDocument.Print();
        }

        /// <summary>
        /// Задает ориентацию печати страницы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintDocument_QueryPageSettings(object sender, QueryPageSettingsEventArgs e)
        {
            PdfPage page = pdfDoc.Pages[currPageIndex];
            if (page.Width / 72.0 < printDocument.DefaultPageSettings.PaperSize.Width / 100.0 && page.Height / 72.0 < printDocument.DefaultPageSettings.PaperSize.Height / 100.0)
                e.PageSettings.Landscape = false;
            else if(page.Width / 72.0 < printDocument.DefaultPageSettings.PaperSize.Height / 100.0 && page.Height / 72.0 < printDocument.DefaultPageSettings.PaperSize.Width / 100.0)
                e.PageSettings.Landscape = true;
        }

        /// <summary>
        /// Выполняет печать страницы в левом верхнем углу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            var gr = e.Graphics;
            if (gr is null)
                return;
            gr.PageUnit = GraphicsUnit.Point;
            PdfPage page = pdfDoc.Pages[currPageIndex];
            page.Draw(gr);
            ++currPageIndex;
            e.HasMorePages = (currPageIndex < pageCount);
        }
    }
}
