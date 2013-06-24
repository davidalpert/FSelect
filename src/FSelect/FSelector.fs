﻿module FSelector

open System
open Microsoft.FSharp.Text.Lexing

open Lexer
open Parser

type ParseException (line:int, column:int, message:string, ?innerException:exn) =
    inherit ApplicationException (sprintf "Parse error: %s" message, match innerException with | Some(ex) -> ex | _ -> null)
    member self.LineNumber = line
    member self.Column = line

let rec Parse input =

        // 1. lex the input into a stream of tokens
        //    printfn "Lexing [%s]" input
        let lexbuff = LexBuffer<char>.FromString(input)

        try
            // 2. parse that token stream with the yacc-based parser and the lexer's tokenizer
            let model = Parser.Parse Lexer.tokenize lexbuff
            
            // 3. return the resulting objectmodel
            model

        with ex ->

            // 4. in the event of an exception while parsing, 
            //    extract the end position of the lexing buffer
            //    which is where the parse failed.
            let endPos = lexbuff.EndPos

            // 5. raise a ParseException with information about the parse failure.
            //raise (new ParseException(lexbuff.StartPos.Column.ToString(), endPos.Line, endPos.Column, ex))
            //    use this line instead to show more detail about the error:
            let innerEx = new System.Exception(ErrorHandling.ErrorContextDescriptor, ex)
            raise (new ParseException(endPos.Line, endPos.Column, ex.Message, innerEx))