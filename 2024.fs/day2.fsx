open System.IO

let diffOk (pair: int * int) =
    let diff = abs (fst pair - snd pair)
    diff >= 1 && diff <= 3

let allDiffsOk arr =
    arr |> Array.pairwise |> Array.forall diffOk

let isSorted arr : bool =
    arr = (arr |> Array.sort) || arr = (arr |> Array.sortDescending)

let isSafe arr =
    (arr |> isSorted) && (arr |> allDiffsOk)

let isSafeWithoutOne arr =
    arr
    |> Array.mapi (fun i _ -> Array.removeAt i arr)
    |> Array.map isSafe
    |> Array.contains true

let isSafe2 arr =
    arr |> isSafe || arr |> isSafeWithoutOne

let solve (part: string) rule =
    File.ReadAllLines("day2.txt")
    |> Seq.map (fun x -> x.Split(" ") |> Array.map int)
    |> Seq.filter rule
    |> Seq.length
    |> printfn "part %s: %A" part

solve "a" isSafe
solve "b" isSafe2
