using System.Drawing;

namespace Praktika2024
{
    /// <summary>
    /// Анализатор чертежей
    /// </summary>
    internal class ImageAnalyzer
    {
        public enum Placement
        {
            NotFitting,
            PotrtaitOrientation,
            LandscapeOrientation
        }

        /// <summary>
        /// Возвращает размеры изображения в точках
        /// </summary>
        /// <param name="bitmap">битмап с изображением</param>
        /// <returns>размеры изображения в точках</returns>
        public Size GetImagePixelSize(Bitmap bitmap)
        {
            Size size = new Size(bitmap.Width, bitmap.Height);
            return size;
        }

        /// <summary>
        /// Возвращает размеры изображения в мм
        /// </summary>
        /// <param name="bitmap">битмап</param>
        /// <param name="dpi">разрешение</param>
        /// <returns>размеры в миллиметрах</returns>
        public SizeF GetImageMmSize(Bitmap bitmap, int dpi)
        {
            SizeF mmSize = new SizeF(ConvertPixelsToMm(bitmap.Width, dpi), ConvertPixelsToMm(bitmap.Height, dpi));
            return mmSize;
        }

        /// <summary>
        /// Вычисляет процент заполнения изображения
        /// </summary>
        /// <param name="bitmap">битмап с изображением</param>
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
                    if (pixelColor.ToArgb() != Color.White.ToArgb())
                        nonWhitePixelCount++;
                }
            }
            return (double)nonWhitePixelCount / totalPixelCount * 100;
        }

        /// <summary>
        /// Проверяет, можно ли разместить изображение на листе
        /// </summary>
        /// <param name="bitmap">битмап с изображением</param>
        /// <param name="mmSheetSize">размеры листа принтера в мм</param>
        /// <param name="dpi">исходное разрешение</param>
        /// <returns>true - можно, false - нельзя</returns>
        public Placement IsImageFits(Bitmap bitmap, SizeF mmSheetSize, int dpi)
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
        /// <param name="pointSize">размер в пикселях</param>
        /// <param name="dpi">разрешение</param>
        /// <returns></returns>
        private float ConvertPixelsToMm(int pixelSize, int dpi)
        {
            float inch = 25.4F;
            float mmSize = pixelSize / (float)dpi * inch;
            return mmSize;
        }
    }
}
