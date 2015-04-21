using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace StoreCozy.Storage
{
    public class MenuCardImageStorage
    {
        public async Task WriteImageAsync(IRandomAccessStream sourceStream, string filename)
        {
            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(sourceStream);
            uint scaledWidth = 0;
            uint scaledHeight = 0;
            if (decoder.PixelWidth > decoder.PixelHeight)
            {
                scaledWidth = 600;
                double relation = (double)decoder.PixelHeight / decoder.PixelWidth;
                scaledHeight = Convert.ToUInt32(relation * scaledWidth);
            }
            else
            {
                scaledHeight = 600;
                double relation = decoder.PixelWidth / decoder.PixelHeight;
                scaledWidth = Convert.ToUInt32(relation * scaledHeight);
            }
            var transform = new BitmapTransform() { ScaledWidth = scaledWidth, ScaledHeight = scaledHeight };
            PixelDataProvider pixelData = await decoder.GetPixelDataAsync(
                BitmapPixelFormat.Rgba8,
                BitmapAlphaMode.Straight,
                transform,
                ExifOrientationMode.RespectExifOrientation,
                ColorManagementMode.DoNotColorManage);
            var folder = ApplicationData.Current.RoamingFolder;
            StorageFile destinationFile = await folder.CreateFileAsync(filename);
            using (var destinationStream = await destinationFile.OpenAsync(
              FileAccessMode.ReadWrite))
            {
                BitmapEncoder encoder = await BitmapEncoder.CreateAsync(
                  BitmapEncoder.PngEncoderId, destinationStream);
                encoder.SetPixelData(BitmapPixelFormat.Rgba8,
                  BitmapAlphaMode.Premultiplied,
                  scaledWidth, scaledHeight, 96, 96, pixelData.DetachPixelData());
                await encoder.FlushAsync();
            }
        }

        public async Task<ImageSource> ReadImageAsync(string filename)
        {
            StorageFolder folder = ApplicationData.Current.RoamingFolder;
            StorageFile file = await folder.CreateFileAsync(filename,
              CreationCollisionOption.OpenIfExists);
            var image = new BitmapImage();
            image.SetSource(await file.OpenReadAsync());
            image.ImageOpened += (sender1, e1) =>
            {
            };
            image.ImageFailed += (sender1, e1) =>
            {
            };
            return image;
        }
    }
}
