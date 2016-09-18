namespace CozyIris
open Gma.QrCodeNet.Encoding
open ImageProcessor
open System.Drawing
open ImageProcessor.Imaging
open System.IO
open System.Drawing.Imaging

module Main =
    [<EntryPoint>]
    let main argv = 
        let image1 = @"d:\data\img\gif.gif"
        let image2 = @"d:\data\img\qr1.gif"
        let image3 = @"d:\data\img\qr2.gif"

        let qb = QrBitmap()
        qb.GetImg(@"http://www.baidu.com").Save(image2)

        let over = new ImageLayer()
        over.Image <- Image.FromFile(image2)
        over.Size <- over.Image.Size
        let ifac = new ImageFactory()
        ifac.Load(image1)
            .Resize(qb.Size)
            .Overlay(over)
            .Save(image3)
            .Dispose()
        printfn "finish!"
        0
