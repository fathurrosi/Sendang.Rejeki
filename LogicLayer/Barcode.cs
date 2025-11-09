//using System.Drawing;
//using Spire.Barcode;

using ZXing;
using ZXing.Common;
using System.Drawing;
namespace LogicLayer
{
    public class Barcode
    {

        public Image GenerateBarcode(string url)
        {
            // Create a barcode writer
            BarcodeWriter writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new EncodingOptions
                {
                    Width = 300,
                    Height = 300,
                    Margin = 1
                }
            };

            // Write the barcode
            Bitmap barcodeBitmap = writer.Write(url);

            // Return the barcode image
            return barcodeBitmap;
        }
        //public Image GenerateBarcode()
        //{
        //    IBarcodeSettings setting = 

        //    // Create a BarcodeGenerator instance
        //    Spire.Barcode.BarCodeGenerator generator = new BarCodeGenerator( setting, "https://example.com");

        //    // Set the barcode parameters
        //    generator.BarcodeType = BarcodeType.QRCode;
        //    generator.QRCodeVersion = QRCodeVersion.Auto;
        //    generator.X = 2;

        //    // Get the barcode image
        //    Image barcodeImage = generator.GenerateImage();

        //    // Return the barcode image
        //    return barcodeImage;
        //}

        //public Image GenerateBarcode(string url)
        //{
        //    // Create barcode settings
        //    BarcodeSettings setting = new BarcodeSettings
        //    {
        //        Type = BarCodeType.QRCode,
        //        QRCodeDataMode = Spire.Barcode.QRCodeDataMode.Auto,
        //        X = 2,
        //        Data = url // Set the URL here
        //    };

        //    // Create a BarcodeGenerator instance
        //    BarCodeGenerator generator = new BarCodeGenerator(setting);

        //    // Generate and return the barcode image
        //    return generator.GenerateImage();
        //}
    }
}
