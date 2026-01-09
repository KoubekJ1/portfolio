using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicplayer.dataobjects
{
    /// <summary>
    /// Data class representing an image in the database
    /// </summary>
    public class IconImage : IDataObject
    {
        private int? _id;
        private Bitmap _image;

        /// <summary>
        /// Constructs a new image
        /// </summary>
        /// <param name="image">image data</param>
        public IconImage(Bitmap image)
        {
            _image = image;
        }

        /// <summary>
        /// Constructs a new IconImage with the given ID
        /// </summary>
        /// <param name="image">image data</param>
        /// <param name="id">ID</param>
        public IconImage(Bitmap image, int id)
        {
			_id = id;
            Image = image;
        }

        public Bitmap Image { get { return _image; }
            set {
                

                int size = 256;

                if (value.Width == size && value.Height == size)
                {
                    _image = value;
                    return;
                }

                _image = ResizeImage(value, size, size);
            }
        }

        public int? Id { get => _id; set => _id = value; }

        public int? GetID()
        {
            return _id;
        }

        /// <summary>
        /// Resizes the image to the given size
        /// </summary>
        /// <param name="image">image</param>
        /// <param name="width">width</param>
        /// <param name="height">height</param>
        /// <returns></returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
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

            return destImage;
        }
    }
}
