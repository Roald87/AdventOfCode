namespace aoc

open System.IO

module Day1 =

    let read = File.ReadLines >> Seq.toList >> List.map int

    let count x = Seq.filter ((=) x) >> Seq.length

    let depthDiff (depth: int list) =
        List.map2 (fun x y -> x - y) depth.[1..] depth.[.. depth.Length - 2]

    let diff list1 list2 =
        List.map2 (fun x y -> x - y) list1 list2

    let depthSum (depth: int list) =
        List.map3
            (fun x y z -> x + y + z)
            depth.[2..]
            depth.[1 .. depth.Length - 2]
            depth.[.. depth.Length - 3]

    let part2 (depth: int list) =
        let depthSum = depthSum depth

        diff depthSum.[1..] depthSum.[.. depthSum.Length - 2]
        |> List.filter (fun x -> x > 0)
        |> List.length
