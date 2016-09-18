namespace CozyIris
open System.Drawing
open Gma.QrCodeNet.Encoding

type QrBitmap() = class
    let _gridWidth              = 16
    let _tranWidth              = 6
    let mutable _size           = new Size()

    member x.GridWidth          = _gridWidth
    member x.TransparentWidth   = _tranWidth
    member x.Size               = _size

    member x.GetImg(str:string)    =
        let qr = new QrEncoder()
        let qrc = qr.Encode(str)
        let w = qrc.Matrix.Width
        
        let bm = new Bitmap(w * _gridWidth, w * _gridWidth)

        let KeyGrid x y = (x<7 && y<7) || (x<7&&y>w-7-1) || (y<7&&x>w-7-1)
        let FixPix x y c =
            if KeyGrid x y then
                for a in x*_gridWidth..x*_gridWidth+_gridWidth-1 do
                for b in y*_gridWidth..y*_gridWidth+_gridWidth-1 do
                    bm.SetPixel(a, b, c)
            else
                for a in x*_gridWidth+_tranWidth..x*_gridWidth+_gridWidth-_tranWidth-1 do
                for b in y*_gridWidth+_tranWidth..y*_gridWidth+_gridWidth-_tranWidth-1 do
                    bm.SetPixel(a, b, c)
        let SetPix x y =
            match qrc.Matrix.Item(x, y) with
            | true  -> FixPix x y Color.Black
            | flase -> FixPix x y Color.White

        for a in 0 .. w-1 do
            for b in 0 .. w-1 do
                SetPix a b |> ignore
        _size.Height <- _gridWidth*w
        _size.Width <- _gridWidth*w
        bm
end
