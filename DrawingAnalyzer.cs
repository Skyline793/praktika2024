using System.Drawing;

namespace Praktika2024
{
    /// <summary>
    /// Анализатор чертежей
    /// </summary>
    internal class DrawingAnalyzer
    {
        public enum Placement
        {
            NotFitting,
            PotrtaitOrientation,
            LandscapeOrientation
        }

        /// <summary>
        /// Возвращает размеры чертежа
        /// </summary>
        /// <param name="bitmap">битмап с чертежом</param>
        /// <returns>размеры чертежа в точках</returns>
        public Size GetDrawingSize(Bitmap bitmap)
        {
            Size size = new Size(bitmap.Width, bitmap.Height);
            return size;
        }

        /// <summary>
        /// Вычисляет процент заполнения чертежа
        /// </summary>
        /// <param name="bitmap">битмап с чертежом</param>
        /// <returns>процент заполнения</returns>
        public double CalculateFillPercentage(Bitmap bitmap)
        {
            int nonWhitePixelCount = 0;
            int totalPixelCount = bitmap.Width * bitmap.Height;

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);
                    // Если пиксель не белый
                    if (pixelColor.ToArgb() != System.Drawing.Color.White.ToArgb())
                        nonWhitePixelCount++;
                }
            }
            return (double)nonWhitePixelCount / totalPixelCount * 100;
        }

        /// <summary>
        /// Проверяет, можно ли разместить чертеж на листе
        /// </summary>
        /// <param name="bitmap">битмап с чертежом</param>
        /// <param name="mmSheetSize">размеры листа принтера в мм</param>
        /// <param name="dpi">dpi принтера</param>
        /// <returns>true - можно, false - нельзя</returns>
        public Placement IsDrawingFits(Bitmap bitmap, SizeF mmSheetSize, int dpi)
        {
            SizeF mmDrawingSize = new SizeF();
            mmDrawingSize.Width = ConvertPixelsToMm(bitmap.Width, dpi);
            mmDrawingSize.Height = ConvertPixelsToMm(bitmap.Height, dpi);
            if (mmDrawingSize.Width < mmSheetSize.Width && mmDrawingSize.Height < mmSheetSize.Height)
                return Placement.PotrtaitOrientation;
            else if (mmDrawingSize.Width < mmSheetSize.Height && mmDrawingSize.Height < mmSheetSize.Width)
                return Placement.LandscapeOrientation;
            else return Placement.NotFitting;
        }

        /// <summary>
        /// Конвертирует размер в пикселях в размер в мм
        /// </summary>
        /// <param name="pixelSize">размер в пикселях</param>
        /// <param name="dpi">dpi принтера</param>
        /// <returns></returns>
        private float ConvertPixelsToMm(int pixelSize, int dpi)
        {
            float inch = 25.4F;
            float mmSize = pixelSize / (float)dpi * inch;
            return mmSize;
        }
    }
}
