namespace aoc

open System.IO

open Utilities

module Day7 =
    let parseInput =
        File.ReadLines
        >> Seq.toList
        >> List.map (split ",")
        >> List.head
        >> List.map int

    let moveCost intialPositions move =
        intialPositions |> List.sumBy (fun x -> abs (move - x))

    let part1 input =
        [ -1000 .. 1000 ] |> List.map (fun x -> (x, moveCost input x)) |> List.minBy snd

    let moveCost2 intialPositions move =
        intialPositions
        |> List.map (fun x -> abs (move - x))
        |> List.map (fun x -> Array.sum [| 0..x |])
        |> List.sum

    let part2 input =
        [ 300..500 ] |> List.map (fun x -> (x, moveCost2 input x)) |> List.minBy snd
