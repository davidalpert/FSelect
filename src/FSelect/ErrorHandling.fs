module ErrorHandling

open System
open System.Text
open Microsoft.FSharp.Text.Lexing
open Microsoft.FSharp.Text.Parsing
open FDebug

let mutable ErrorContextDescriptor : string = "<NO ERROR>"
//let mutable ErrorContext : ParseErrorContext<'a> option = 
//    match 'a with
//    | _ None;

let parse_error_rich =
    let inline (|+) (buffer:StringBuilder) (text:string) =
        buffer.Append text

    let inline (|++) (buffer:StringBuilder) (text:string) =
        buffer.AppendLine text

    Some (fun (ctxt: ParseErrorContext<_>) ->
        ErrorContextDescriptor <- "parse error ... gathering data"

        let sb = new StringBuilder()
        let nl = Environment.NewLine
        sb
        |+  nl
        |++ (sprintf "- CurrentToken: %A" ctxt.CurrentToken) 
        |++ (sprintf "- Message: %s" ctxt.Message) 
        |++ (sprintf "- ReduceTokens: %A" ctxt.ReduceTokens) 
        |++ (sprintf "- ReducibleProductions: %A" ctxt.ReducibleProductions) 
        |++ (sprintf "- ShiftTokens: %A" ctxt.ShiftTokens) 
        |> ignore

        for x in ctxt.ShiftTokens do
            sb
            |++ (sprintf "  * %d" x)
            |> ignore
            
        sb
        |++ (sprintf "- StateStack: %A" ctxt.StateStack)
        |> ignore

        (*
        sb
        |++ (let p1,p2 = ctxt.ParseState.ResultRange in
                sprintf "Result range from line %d col %d to line %d col %d" p1.Line p1.Column p2.Line p2.Column
            )
        |> ignore
        *)

        sb
        |++ "- ParserLocalStore:"
        |> ignore
        let x = typedefof<LexBuffer<char>>
        for KeyValue(k,v) in ctxt.ParseState.ParserLocalStore do
            sb |++ (sprintf "  * %s -> %s" k ( match v.GetType() with
                                        | x -> LexBuffer<char>.LexemeString (v :?> LexBuffer<char>)
                                        | _ -> v.ToString() 
            )) |> ignore

        ErrorContextDescriptor <- sb.ToString()
        //FDebug.writefn "Parse error: %s%s" nl (sb.ToString())
    )