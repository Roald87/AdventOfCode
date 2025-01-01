// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System.IO

let read fname = File.ReadLines fname

let readInput = read "input.txt" |> Seq.toList

let count x = Seq.filter ((=) x) >> Seq.length

let validPassword (chr, min, max, (password: string)) =
    let counts = count chr password

    counts >= min && counts <= max

let validPassword2 (chr, first, second, (password: string)) =
    try
        (password.[first - 1] = chr) <> (password.[second - 1] = chr)
    with :? System.IndexOutOfRangeException ->
        printfn "Index out of range"
        false

[<EntryPoint>]
let main argv =

    let parseInput (input: string) =
        let parts = input.Split(" ")
        let minMax = parts.[0].Split("-") |> Array.map int
        let chr = parts.[1].[0] |> char
        let password = parts.[2]

        chr, minMax.[0], minMax.[1], password

    let validPasswords =
        readInput |> List.map parseInput |> List.filter validPassword2 |> List.length

    printfn "Valid passes: %i" validPasswords
    0 // return an integer exit code
