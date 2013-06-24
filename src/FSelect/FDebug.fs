module FDebug 

    open System.Diagnostics

    let write message = Debug.Write(message)

    let writen message = Debug.WriteLine(message)

    let writef fmt = Printf.ksprintf Debug.Write fmt

    let writefn fmt = Printf.ksprintf Debug.WriteLine fmt