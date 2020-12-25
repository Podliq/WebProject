namespace ProjectApi.Infrastructure
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;

    public class ImageHelper : IImageSizeReducer
    {
        public string GetReducedSizeImage(string imageString, int width, int height)
        {
            var splittedImageString = imageString
                .Split(',')
                .ToArray();

            var imageBaseType = splittedImageString[0];
            imageString = string.Join("", splittedImageString.Skip(1).ToArray());

            byte[] bytes = Convert.FromBase64String(imageString);

            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            using (var ms = new MemoryStream())
            {
                destImage.Save(ms, ImageFormat.Jpeg);
                var reducedImageString = imageBaseType + ',' + Convert.ToBase64String(ms.ToArray());
                return reducedImageString;
            }
        }
    }
}
