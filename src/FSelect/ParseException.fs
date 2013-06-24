
namespace FSelect

open System

type LexingException (lineNumber:int, column:int, message:string, ?innerException:exn) =
    inherit ApplicationException (sprintf "Lexing error: %s" message, match innerException with | Some(ex) -> ex | _ -> null)
    member self.LineNumber = lineNumber
    member self.Column = column

type ParseException (lineNumber:int, column:int, message:string, ?innerException:exn) =
    inherit ApplicationException (sprintf "Parse error: %s" message, match innerException with | Some(ex) -> ex | _ -> null)
    member self.LineNumber = lineNumber
    member self.Column = column
