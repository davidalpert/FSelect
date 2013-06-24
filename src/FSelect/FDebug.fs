namespace FSelect

open System.Diagnostics

type FDebug() =

    static member val Trace = false with get, set

    static member write message = Debug.Write(message)

    static member writen message = Debug.WriteLine(message)

    static member writef fmt = Printf.ksprintf Debug.Write fmt

    static member writefn fmt = Printf.ksprintf Debug.WriteLine fmt