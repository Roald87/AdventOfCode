// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System.IO

let read fname = File.ReadLines fname

let readInput = read "input.txt" |> Seq.toList

let count x = Seq.filter ((=) x) >> Seq.length

let isTree (chr: char) = chr = '#'

let trees dx (rows: string list) =
    rows
    |> List.mapi (fun i x -> (i, x))
    |> List.filter (fun (i, x) ->
        (let index = ((i + 1) * dx) % x.Length
         isTree x.[index]))
    |> List.length

[<EntryPoint>]
let main argv =

    let treeMap1 = readInput.[1..]

    let treeMap2 =
        treeMap1
        |> List.mapi (fun i x -> i, x)
        |> List.filter (fun (i, _) -> i % 2 = 1)
        |> List.map (fun (_, x) -> x)

    let ans =
        int64 (trees 1 treeMap1)
        * int64 (trees 3 treeMap1)
        * int64 (trees 5 treeMap1)
        * int64 (trees 7 treeMap1)
        * int64 (trees 1 treeMap2)

    printfn "Trees: %i" ans
    0 // return an integer exit code
