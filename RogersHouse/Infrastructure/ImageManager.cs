using System.Drawing;
using System.IO;

namespace RogersHouse.WebUI.Infrastructure
{
    public class ImageManager
    {
        private const int ThumbWidth = 115;
        private const int ThumbHeigth = 77;
        private const int ImageWidth = 496;
        private const int ImageHeigth = 296;

        public Image CreateThumbs(Stream image)
        {
            return ResizeCore(image, ThumbWidth, ThumbHeigth);
        }

        public Image ResizeImage(Stream image)
        {
            return ResizeCore(image, ImageWidth, ImageHeigth);
        }

        private static Image ResizeCore(Stream image, int width, int height)
        {
            using (Image originalImage = Image.FromStream(image))
            {
                int newWidth = originalImage.Width;
                int newHeight = originalImage.Height;

                if (originalImage.Width > width)
                {
                    newWidth = width;
                    newHeight = originalImage.Height*newWidth/originalImage.Width;
                }

                if (newHeight > height)
                {
                    newHeight = height;
                    newWidth = originalImage.Width*newHeight/originalImage.Height;
                }

                Image result = new Bitmap(newWidth, newHeight);

                using (Graphics g = Graphics.FromImage(result))
                {
                    g.PageUnit = GraphicsUnit.Pixel;
                    g.DrawImage(originalImage, new Rectangle(0, 0, width, height));
                }
                return result;
            }
        }
    }
}