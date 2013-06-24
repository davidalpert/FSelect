
namespace FSelect.AST

open System

//type ISelector =
    //interface
        //abstract member Identifier : string
    //end

type Selector(identifier:string) = 
    //interface ISelector
    member self.Identifier = identifier
    override self.ToString() = sprintf "IDENTIFIER( %s )" self.Identifier

type SelectorSequence(?selectors:Selector list) as self =
    member self.Selectors = match selectors with
                                | Some(selectors) -> new System.Collections.Generic.List<Selector>(selectors)
                                | None -> new System.Collections.Generic.List<Selector>()
    new(selector:Selector) = SelectorSequence( [ selector ] )

