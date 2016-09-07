namespace Cozy
open System
open System.IO
open System.Net
open Newtonsoft.Json

module Utils =
    let Throttle n fs =
            seq { let n = new Threading.Semaphore(n, n)
                  for f in fs ->
                      async { let! ok = Async.AwaitWaitHandle(n)
                              let! result = Async.Catch f
                              n.Release() |> ignore
                              return match result with
                                     | Choice1Of2 rslt -> rslt
                                     | Choice2Of2 exn  -> raise exn
                            }
                }
    let ParallelDo func a b =
        [a..b]
        |> List.map (fun x -> func (x.ToString()))
        |> Async.Parallel
        |> Async.RunSynchronously
        |> ignore
    let SyncDo func a b =
        [a..b]
        |> List.map (fun x -> func (x.ToString()))
        |> ignore

    let MkDir d = Directory.CreateDirectory d |> ignore
    let SaveFile (data:string) (path:string) =
        File.WriteAllText(path, data)
    let LoadFile (path:string) = File.ReadAllText path
    let FilesNum p = Directory.GetFiles(p).Length

    let SaveWebFile url path =
        try
            if File.Exists(path) then
                0 |> ignore
                //printfn "file exist"
            else
                let html =
                    WebRequest.Create(Uri(url))
                    |> (fun x -> x.GetResponse())
                    |> (fun x -> x.GetResponseStream())
                    |> (fun x -> new StreamReader(x))
                    |> (fun x -> x.ReadToEnd())
                SaveFile html path |> ignore
        with
            | :? System.Net.WebException -> printfn "WebException"
            | :? System.IO.IOException-> printfn "IOException"
    let SaveWebFileAsync url path = async { SaveWebFile url path }
    let SaveImage url path =
        try
            if File.Exists(path) then
                0 |> ignore
                //printfn "file exist"
            else
                let webclient = new WebClient();
                webclient.DownloadFile(string url, string path);
        with
            | :? System.Net.WebException -> printfn "WebException"
            | :? System.IO.IOException-> printfn "IOException"
    let SaveImageAsync url path = async { SaveImage url path }

    let Obj2Json obj = JsonConvert.SerializeObject obj
    let Obj2JsonIndented obj = JsonConvert.SerializeObject(obj, Formatting.Indented)
    let Json2Obj<'T> json = JsonConvert.DeserializeObject<'T> json
