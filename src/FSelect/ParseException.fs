
namespace FSelect

open System

type LexingException (lineNumber:int, column:int, unrecognizedInput:string, ?innerException:exn) =
    inherit ApplicationException (sprintf "unrecognized input '%s' at line %d column %d" unrecognizedInput lineNumber column, match innerException with | Some(ex) -> ex | _ -> null)
    member self.LineNumber = lineNumber
    member self.Column = column

type ParseException (lineNumber:int, column:int, unexpectedToken:string, ?innerException:exn) =
    inherit ApplicationException (sprintf "unexpected token '%s' at %d column %d." unexpectedToken lineNumber column, match innerException with | Some(ex) -> ex | _ -> null)
    member self.LineNumber = lineNumber
    member self.Column = column
