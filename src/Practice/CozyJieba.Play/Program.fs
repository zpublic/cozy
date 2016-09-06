open System
open System.IO
open System.Text
open JiebaNet.Analyser
open JiebaNet.Segmenter
open Newtonsoft.Json

[<EntryPoint>]
let main argv =
    let Obj2JsonIndented obj = JsonConvert.SerializeObject(obj, Formatting.Indented)

    let fileName = @"C:\jiebanet\config\xiaoshuo1.txt"
    let text = File.ReadAllText(fileName)
    let tfidf = new TfidfExtractor();

    tfidf.ExtractTags(text, 30, Constants.NounAndVerbPos)
    |> Seq.cast<string>
    |> Seq.toList
    |> Obj2JsonIndented
    |> (fun x -> File.WriteAllText(@"c:\jiebanet\config\xiaoshuo1_.txt", x))
    0
