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

    let flood =
        Array2D.map (fun x -> if x < 9 then 1 else 0)

    let selectNeighbours maxX maxY c =
        let (x, y) = c

        [ (x - 1, y)
          (x + 1, y)
          (x, y - 1)
          (x, y + 1) ]
        |> List.filter (fun (x, y) -> (x >= 0 && x <= maxX && y >= 0 && y <= maxY))

    let part2 input =
        let floodedCave = flood input

        let width = floodedCave |> Array2D.length1
        let height = floodedCave |> Array2D.length2

        let neighborSelect =
            selectNeighbours (width - 1) (height - 1)

        let rec growPatch
            (currentPatch: Set<int * int>)
            (neighboursToCheck: list<int * int>) =
            
            let neightboursPartOfPatch =
                neighboursToCheck
                |> List.filter (fun (i, j) -> floodedCave.[i, j] = 1)
        
            let newPatch =
                neightboursPartOfPatch
                |> Set.ofList
                |> Set.union currentPatch
        
            let newNeighbours =
                neightboursPartOfPatch
                |> List.map neighborSelect
                |> List.concat
                |> Set.ofList
                |> (fun x -> x - currentPatch)
                |> Set.toList
        
            if newNeighbours.Length = 0 then
                currentPatch
            else
                growPatch newPatch newNeighbours
        
        lowestPoints input
        |> List.map (fun elem -> growPatch ([ elem ] |> Set.ofList) [ elem ])
        |> List.map (fun x -> x.Count)
        |> List.sortDescending
        |> List.take 3
        |> List.reduce (fun acc x -> acc * x)