
namespace FSelect.AST

open System

// base classes

[<AbstractClass>]
[<AllowNullLiteral>]
type Selector() as self = 
    abstract member Identifier:string
    abstract member ContextSelector:Selector
    override self.ToString() = sprintf "%s( %s )" (self.GetType().Name) self.Identifier

type SelectorSequence(?selectors:Selector list) as self =
    member self.Selectors = match selectors with
                                | Some(selectors) -> new System.Collections.Generic.List<Selector>(selectors)
                                | None -> new System.Collections.Generic.List<Selector>()

// SimpleSelectors ---------------------------------------------

[<AllowNullLiteral>]
type SimpleSelector(identifier:string) =
    inherit Selector() 
    override self.Identifier = identifier
    override self.ContextSelector = null

type WildcardSelector() =
    inherit SimpleSelector("*")

type ElementSelector(identifier:string) =
    inherit SimpleSelector(identifier)
    member self.ElementName with get() = self.Identifier

type ClassSelector(identifier:string) =
    inherit WildcardSelector()
    member self.ClassName with get() = identifier

type IdentitySelector(identifier:string) =
    inherit WildcardSelector()
    member self.Key with get() = identifier

// SimpleSelectors ---------------------------------------------

type CompoundSelector(identifier:string, context:Selector) = 
    inherit Selector() 
    override self.Identifier = identifier
    override self.ContextSelector = context


