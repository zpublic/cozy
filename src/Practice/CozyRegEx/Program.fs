open System.Text.RegularExpressions
open FsVerbalExpressions
open FsVerbalExpressions.CommonVerbEx

let TestRegEx() =
    let r = new Regex("^.{3}$");
    printfn "%A" (r.IsMatch("123"))
    printfn "%A" (r.IsMatch("1234"))
    printfn "finish TestRegEx!\n"

open FsVerbalExpressions.FsRegEx
let TestFsRegEx() =
    "123"
    |> isMatch "^.{3}$"
    |> printfn "%A"
    "1-2-34"
    |> replace "[1-9]" "hehe"
    |> printfn "%A"
    printfn "finish TestFsRegEx!\n"

open FsVerbalExpressions.VerbalExpression
let TestVerbEx() =
    let key =  "key"
    VerbEx()
    |> add "COD"
    |> beginCaptureNamed key
    |> any "0-9"
    |> repeatPrevious 3
    |> endCapture
    |> then' "END"
    |> VerbalExpression.capture "COD123END" key
    |> printfn "%s"

    Email
    |> VerbalExpression.isMatch "1.com"
    |> printfn "%A"
    Email
    |> VerbalExpression.isMatch "1@qq.com"
    |> printfn "%A"

    VerbEx()
    |> beginCaptureNamed "upper"
    |> unicodeCategory UniCodeGeneralCategory.LetterUppercase
    |> add "+"
    |> endCapture
    |> VerbalExpression.capture "some mixed case WORDS" "upper"
    |> printfn "%s"
    printfn "finish TestVerbEx!\n"

let TestVerbExExsample() =
    let key =  "key"
    let sa = """aaaaaaaaaaaaaa<a href="/15789491-2.html">11111111"""
    let vex = VerbEx()
                |> add """<a href="/"""
                |> beginCaptureNamed key
                |> anything
                |> then' ".html"
                |> endCapture
                |> then' "\">"
    vex
    |> toString
    |> printfn "%s"
    vex
    |> VerbalExpression.capture sa key
    |> printfn "%s"

[<EntryPoint>]
let main argv = 
    //TestRegEx()
    //TestFsRegEx()
    //TestVerbEx()
    TestVerbExExsample()
    0
