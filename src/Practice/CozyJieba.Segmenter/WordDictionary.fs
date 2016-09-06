module WordDictionary
open System
open System.IO

let LoadDict dictfile =
    File.ReadAllLines dictfile
    |> Array.map (fun x -> x.Split(' '))