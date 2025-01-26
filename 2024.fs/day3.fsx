open System.IO
open System.Text.RegularExpressions

File.ReadAllLines("day3.txt")
|> Array.map (fun x -> Regex("mul\((\d+),(\d+)\)").Matches(x))
|> Seq.map (Seq.map (fun m -> Seq.tail [ for m in m.Groups -> m.Value ] |> Seq.map int))
|> Seq.collect id
|> Seq.sumBy (Seq.reduce (*))
|> printfn "part a:%A"

let getAllDoes arr =
    let rec getDoMul acc arr =
        match arr with
        | "don't()" :: "do()" :: tail -> getDoMul acc tail
        | [ "don't()"; _ ] -> acc
        | "don't()" :: tail -> getDoMul acc ("don't()" :: tail.Tail)
        | "do()" :: tail -> getDoMul acc tail
        | [] -> acc
        | head :: tail -> getDoMul (head :: acc) tail

    getDoMul [] arr

File.ReadAllLines("day3.txt")
|> Array.map (fun x -> Regex("mul\((\d+),(\d+)\)|do\(\)|don\'t\(\)").Matches(x))
|> Seq.collect id
|> Seq.map (fun x -> x.Value)
|> Seq.toList
|> getAllDoes
|> Seq.map (fun s -> s[4 .. s.Length - 2].Split(",") |> Seq.map int)
|> Seq.sumBy (Seq.reduce (*))
|> printfn "part b: %A"
