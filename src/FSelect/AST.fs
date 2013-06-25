
namespace FSelect.AST

open System

// base classes

[<AbstractClass>]
[<AllowNullLiteral>]
type Selector() as self = 
    abstract member Identifier:string
    abstract member ContextSelector:Selector
    abstract member Text:string
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
    override self.Text = identifier
    override self.ContextSelector = null

type WildcardSelector() =
    inherit SimpleSelector("*")

type ElementSelector(identifier:string) =
    inherit SimpleSelector(identifier)
    member self.ElementName with get() = self.Identifier

type ClassSelector(identifier:string) =
    inherit WildcardSelector()
    override self.Text = sprintf ".%s" identifier
    member self.ClassName with get() = identifier

type IdentitySelector(identifier:string) =
    inherit SimpleSelector(identifier)
    override self.Text = sprintf "#%s" identifier
    member self.Key with get() = self.Identifier

// CompoundSelectors ---------------------------------------------

type CompoundSelector(context:Selector, infixOperator:string, rightPart:Selector) = 
    inherit Selector() 
    override self.ContextSelector = context
    member self.InfixOperator = infixOperator
    member self.RightPart = rightPart
    override self.Identifier = rightPart.Identifier
    override self.Text = sprintf "%s%s%s" context.Text self.InfixOperator rightPart.Text
    new(context:Selector, rightPart:Selector) = CompoundSelector(context, " ", rightPart)

type ImmediateChildSelector(context:Selector, right:Selector) = 
    inherit CompoundSelector(context, " > ", right) 

type AdjacentSiblingSelector(context:Selector, right:Selector) = 
    inherit CompoundSelector(context, " + ", right) 
