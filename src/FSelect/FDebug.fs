namespace FSelect

open System.Diagnostics

type FDebug() =

    static member writef fmt = Printf.ksprintf Debug.Write fmt 

    static member writefn fmt = Printf.ksprintf Debug.WriteLine fmt 