namespace aoc

open System.IO

open Utilities

module Day9 =
    let parseInput =
        File.ReadLines
        >> Seq.toList
        >> List.map (
            stringToChars
            >> Array.toList
            >> List.map (fun x -> int (x) - int ('0'))
        )
        >> array2D

    let lowestPoints (t: int [,]) =
        let mutable lowestCoords = []

        let maxX = (t |> Array2D.length1) - 1
        let maxY = (t |> Array2D.length2) - 1

        for i in 0 .. maxX do
            for j in 0 .. maxY do
                let diffNextOne =
                    if j < maxY then
                        (t.[i, j] - t.[i, j + 1])
                    else
                        -1

                let diffPrevOne =
                    if j > 0 then
                        (t.[i, j] - t.[i, j - 1])
                    else
                        -1

                let diffBelowOne =
                    if i < maxX then
                        (t.[i, j] - t.[i + 1, j])
                    else
                        -1

                let diffAboveOne =
                    if i > 0 then
                        (t.[i, j] - t.[i - 1, j])
                    else
                        -1

                if diffNextOne < 0
                   && diffPrevOne < 0
                   && diffBelowOne < 0
                   && diffAboveOne < 0 then
                    lowestCoords <- List.append lowestCoords [ (i, j) ]

        lowestCoords

    let part1 input =
        input
        |> lowestPoints
        |> List.map (fun (x, y) -> (input.[x, y]))
        |> List.sumBy (fun x -> x + 1)

    let flooded =
        List.map (List.map (fun x -> if x < 9 then 1 else 0))
