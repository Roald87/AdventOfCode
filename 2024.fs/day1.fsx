open System.IO

// part a
File.ReadAllLines("day1.txt")
|> Seq.map (fun x -> x.Split("  ") |> Seq.map (fun x -> int x))
|> Seq.transpose
|> Seq.map (fun x -> x |> Seq.sort)
|> Seq.transpose
|> Seq.map Seq.toArray
|> Seq.map (fun x -> x[0] - x[1] |> abs)
|> Seq.sum
|> printfn "part a: %i"

// part b
let nums =
    File.ReadAllLines("day1.txt")
    |> Seq.map (fun x -> x.Split("  ") |> Seq.map (fun x -> int x))
    |> Seq.transpose
    |> Seq.toArray

let left = nums[0] |> Seq.sort |> Seq.toArray
let counts = nums[1] |> Seq.countBy id |> dict

left
|> Seq.map (fun x -> (if counts.TryGetValue(x) |> fst then counts[x] * x else 0))
|> Seq.sum
|> printfn "part b: %i"
