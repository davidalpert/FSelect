
namespace FSelect.AST

open System

type Selector(identifier:string) = 
    member self.Identifier = identifier
    override self.ToString() = sprintf "%s( %s )" (self.GetType().Name) self.Identifier

type SimpleSelector(identifier:string) =
    inherit Selector(identifier)

type WildcardSelector() =
    inherit SimpleSelector("*")

type ElementSelector(identifier:string) =
    inherit SimpleSelector(identifier)
    member self.ElementName with get() = self.Identifier

type ClassSelector(identifier:string) =
    inherit SimpleSelector(identifier)
    member self.ClassName with get() = self.Identifier

type IdentitySelector(identifier:string) =
    inherit SimpleSelector(identifier)
    member self.Key with get() = self.Identifier

type SelectorSequence(?selectors:Selector list) as self =
    member self.Selectors = match selectors with
                                | Some(selectors) -> new System.Collections.Generic.List<Selector>(selectors)
                                | None -> new System.Collections.Generic.List<Selector>()

