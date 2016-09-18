open ImageProcessor
open ImageProcessor.Processors
open ImageProcessor.Imaging.Filters.Photo
open System.Drawing

[<EntryPoint>]
let main argv = 
    let image = @"d:\data\img\lulu.jpg"
    let image2 = @"d:\data\img\lulu2.jpg"
    let image3 = @"d:\data\img\lulu3.jpg"
    let image4 = @"d:\data\img\lulu4.jpg"
    let fac = new ImageFactory()

    fac.Load(image)
       .Resize(new Size(100, 100))
       .Save(image2)
       .Dispose()

    fac.Load(image).Filter(MatrixFilters.BlackWhite).Save(image3).Dispose()

    fac
    |> fun x -> x.Load(image)
    |> fun x -> x.Filter(MatrixFilters.Lomograph)
    |> fun x -> x.Save(image4)
    |> ignore

    printfn "finish"
    0
