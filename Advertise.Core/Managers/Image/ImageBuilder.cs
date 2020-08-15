using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Web;
using Advertise.Core.Managers.File;
using ImageProcessor;
using ImageProcessor.Imaging;

namespace Advertise.Core.Managers.Image
{
    public class ImageBuilder : IImageBuilder
    {
        public async Task<bool> ProductImageProcessAsync(string filePath)
        {
            var imageFactory = new ImageFactory(true);
            imageFactory.Load(filePath);
            float height = imageFactory.Image.Height.ToInt32();
            float width = imageFactory.Image.Width.ToInt32();
            float mod = height > width ? height / width : width / height;
            int x1 = 0, y1 = 0;
            //if (height > width)
            //{
            //    y1 = 405;
            //    x1 = (int)(420 / mod) - 15;
            //}
            //else
            //{
            //   x1 = 405;
            //    y1 = (int)(420 / mod) - 15;
            //}
            string pathWatermark = HttpContext.Current.Server.MapPath(FileConst.WatermarkIcon);
           
           
           // imageFactory.Load(filePath);

            if (imageFactory.Image.HorizontalResolution.InRange(95, Single.MaxValue) || imageFactory.Image.VerticalResolution.InRange(95, Single.MaxValue))
                imageFactory.Resolution(96, 96);
          
            if (imageFactory.Image.Height.InRange(79, Int32.MaxValue) || imageFactory.Image.Width.InRange(79, Int32.MaxValue))
                if (height > width)
                {
                    imageFactory.Resize(new Size
                    {
                        Height = 900,
                        Width = (int)(900 / mod)
                    });
                    y1 = (int)imageFactory.Image.Height - 88;
                    x1 = (int)imageFactory.Image.Width - 223;
                }
                else
                {
                    imageFactory.Resize(new Size
                    {
                        Width = 900,
                        Height = (int)(900 / mod)
                    });
                    x1 = (int)imageFactory.Image.Width - 223;
                    y1 = (int)imageFactory.Image.Height - 88;
                }
            else
                imageFactory.Resize(new Size
                {
                    Height = 900,
                    Width = 900
                });
            var imageLayer = new ImageLayer
            {
                Image = System.Drawing.Image.FromFile(pathWatermark, true),
                Position = new Point
                {
                    X = x1,
                    Y = y1
                }
            };
            imageFactory.Overlay(imageLayer).Save(filePath);
            return true;
        }

        public async Task<bool> CompanyImagesFileProcessAsync(string filePath)
        {
            string pathWatermark = HttpContext.Current.Server.MapPath(FileConst.WatermarkIcon);
            var imageLayer = new ImageLayer
            {
                Image = System.Drawing.Image.FromFile(pathWatermark, true),
                Position = new Point
                {
                    X = 15,
                    Y = 15
                }
            };

            var imageFactory = new ImageFactory(true);
            imageFactory.Load(filePath);
            if (imageLayer.Image.HorizontalResolution.InRange(95, Single.MaxValue) || imageLayer.Image.VerticalResolution.InRange(95, Single.MaxValue))
                imageFactory.Resolution(96, 96);

            if (imageLayer.Image.Height.InRange(419, Int32.MaxValue) || imageLayer.Image.Width.InRange(419, Int32.MaxValue))
                imageFactory.Resize(new Size
                {
                    Height = 420,
                    Width = 420
                });
           ;
            imageFactory.BackgroundColor(Color.White).Save(filePath);
            return true;
        }

        public async Task<bool> CompanyCoverFileProcessAsync(string filePath)
        {
            string pathWatermark = HttpContext.Current.Server.MapPath(FileConst.WatermarkIcon);
            var imageLayer = new ImageLayer
            {
                Image = System.Drawing.Image.FromFile(pathWatermark, true),
                Position = new Point
                {
                    X = 15,
                    Y = 15
                }
            };

            var imageFactory = new ImageFactory(true);
            imageFactory.Load(filePath);
            if (imageLayer.Image.HorizontalResolution.InRange(95, Single.MaxValue) || imageLayer.Image.VerticalResolution.InRange(95, Single.MaxValue))
                imageFactory.Resolution(120, 120);

            if (imageLayer.Image.Height.InRange(1419, Int32.MaxValue) || imageLayer.Image.Width.InRange(299, Int32.MaxValue))
                imageFactory.Resize(new Size
                {
                    Height = 1500,
                    Width = 300
                });
            imageFactory.BackgroundColor(Color.Empty).Save(filePath);
            return true;
        }

        public async Task<bool> LogoProcessAsync(string filePath)
        {
            string pathWatermark = HttpContext.Current.Server.MapPath(FileConst.WatermarkIcon);
            var imageLayer = new ImageLayer
            {
                Image = System.Drawing.Image.FromFile(pathWatermark, true),
                Position = new Point
                {
                    X = 15,
                    Y = 15
                }
            };

            var imageFactory = new ImageFactory(true);
            imageFactory.Load(filePath);
            if (imageLayer.Image.HorizontalResolution.InRange(95, Single.MaxValue) || imageLayer.Image.VerticalResolution.InRange(95, Single.MaxValue))
                imageFactory.Resolution(96, 96);

            if (imageLayer.Image.Height.InRange(69, Int32.MaxValue) || imageLayer.Image.Width.InRange(69, Int32.MaxValue))
                imageFactory.Resize(new Size
                {
                    Height = 70,
                    Width = 70
                });
            imageFactory.BackgroundColor(Color.Empty).Save(filePath);
            return true;
        }

        public async Task<bool> ProductImagsProcessAsync(string filePath, string path)
        {
            string pathWatermark = HttpContext.Current.Server.MapPath(FileConst.WatermarkIcon);
            var imageLayer = new ImageLayer
            {
                Image = System.Drawing.Image.FromFile(pathWatermark, true),
                Position = new Point
                {
                    X = 15,
                    Y = 15
                }
            };

            var imageFactory = new ImageFactory(true);
            imageFactory.Load(filePath);
            if (imageLayer.Image.HorizontalResolution.InRange(95, Single.MaxValue) || imageLayer.Image.VerticalResolution.InRange(95, Single.MaxValue))
                imageFactory.Resolution(96, 96);

            if (imageLayer.Image.Height.InRange(314, Int32.MaxValue) || imageLayer.Image.Width.InRange(419, Int32.MaxValue))
                imageFactory.Resize(new Size
                {
                    Height = 315,
                    Width = 420
                });
            else
                imageFactory.Resize(new Size
                {
                    Height = 315,
                    Width = 420
                });

            imageFactory.BackgroundColor(Color.Transparent).Overlay(imageLayer).Save(filePath);
            return true;
        }

        public async Task<bool> CreateThumbFileAsync(string filePath)
        {
            var imageFactory = new ImageFactory();
            imageFactory.Load(filePath).Resolution(30, 30).Resize(new Size
            {
                Height = 100,
                Width = 100
            }).Save(filePath);
            return true;
        }

        public async Task<bool> TranparentWatermarkAsync(string filePath)
        {
            var textLayer = new TextLayer
            {
                Text = "Novinak",
                FontColor = Color.Transparent
            };

            var imageFactory = new ImageFactory(true);
            imageFactory.Load(filePath).Watermark(textLayer).Save(filePath);

            return true;
        }

        public async Task<bool> TransparentWatermarkWithCustomTextAsync(string filePath, string text)
        {
            var textLayer = new TextLayer
            {
                Text = text,
                FontColor = Color.Transparent
            };

            var imageFactory = new ImageFactory(true);
            imageFactory.Load(filePath).Watermark(textLayer).Save(filePath);

            return true;
        }
    }
}