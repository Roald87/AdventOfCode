namespace aoc

open System.IO

open Utilities

module Day9 =
    let parseInput =
        File.ReadLines
        >> Seq.map (stringToChars >> Seq.map (fun x -> int (x) - int ('0')))
        >> array2D

    let selectNeighbours maxX maxY c =
        let x, y = c

        [ x - 1, y; x + 1, y; x, y - 1; x, y + 1 ]
        |> List.filter (fun (x, y) -> (x >= 0 && x <= maxX && y >= 0 && y <= maxY))

    let array2DSize arr =
        let width = (arr |> Array2D.length1) - 1
        let height = (arr |> Array2D.length2) - 1
        width, height

    let lowestPoints (heightMap: int[,]) =
        let maxX, maxY = array2DSize heightMap

        heightMap
        |> Array2D.mapi (fun i j x ->
            selectNeighbours maxX maxY (i, j)
            |> List.filter (fun (i, j) -> x >= heightMap.[i, j]))
        |> Array2D.mapi (fun i j x -> if x.Length = 0 then Some(i, j) else None)
        |> Seq.cast<option<int * int>>
        |> Seq.filter (fun x -> x.IsSome)
        |> Seq.map (fun x -> x.Value)

    let part1 input =
        input
        |> lowestPoints
        |> Seq.map (fun (x, y) -> (input.[x, y]))
        |> Seq.sumBy (fun x -> x + 1)

    let part2 input =
        let width, height = array2DSize input

        let neighborSelect = selectNeighbours width height

        let rec growPatch currentPatch neighboursToCheck =
            let neightboursPartOfPatch =
                neighboursToCheck |> Set.filter (fun (i, j) -> input.[i, j] < 9)

            let newPatch = neightboursPartOfPatch |> Set.union currentPatch

            let newNeighbours =
                neightboursPartOfPatch
                |> Set.map neighborSelect
                |> List.concat
                |> Set.ofList
                |> (fun x -> x - currentPatch)

            if Set.count newNeighbours = 0 then
                currentPatch
            else
                growPatch newPatch newNeighbours

        lowestPoints input
        |> Seq.map (fun x -> [ x ] |> Set.ofList)
        |> Seq.map (fun elem -> growPatch elem elem)
        |> Seq.map (fun x -> x.Count)
        |> Seq.sortDescending
        |> Seq.take 3
        |> Seq.reduce (fun acc x -> acc * x)
