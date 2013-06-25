
namespace FSelect

open System
open Microsoft.FSharp.Text.Lexing

open Lexer
open Parser

// FSelector is a class, visible from C#, in the FSelect namespace (as defined above)
type FSelector = 

    // Parse(input) is a static method, visible on the FSelector type
    static member Parse(input:System.String) =

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
            let startPos = lexbuff.StartPos
            let endPos = lexbuff.EndPos
    
            // 5. raise a ParseException with information about the parse failure.
            //raise (new ParseException(lexbuff.StartPos.Column.ToString(), endPos.Line, endPos.Column, ex))
            //    use this line instead to show more detail about the error:
            match ex.Message with 
                    | "unrecognized input" -> (
                                    let nextCharOffset = endPos.AbsoluteOffset + 1
                                    let nextChar = match nextCharOffset < input.Length with
                                                     | true -> (input.Chars endPos.AbsoluteOffset).ToString()
                                                     | false -> "EOF"
                                    raise (new LexingException(endPos.Line, endPos.Column, nextChar, ex))
                                )
                    | "parse error" -> (
                                    let innerEx = new System.Exception(ErrorHandling.ErrorContextDescriptor, ex)
                                    raise (new ParseException(startPos.Line, startPos.Column, (lexeme lexbuff), innerEx))
                                )
                    | _ -> raise ex