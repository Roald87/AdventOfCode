namespace aoc

open System.IO

open Utilities

module Day11 =

    let parseInput =
        File.ReadLines
        >> Seq.toList
        >> List.map (
            stringToChars
            >> Array.toList
            >> List.map (fun x -> int (x) - int ('0'))
        )
        >> array2D

    let addOne = Array2D.map (fun x -> x + 1)

    let elementWiseAdd arr1 arr2 =
        assert (Array2D.length1 arr1 = Array2D.length1 arr2)
        assert (Array2D.length2 arr1 = Array2D.length2 arr2)

        arr1
        |> Array2D.mapi (fun i j x -> x + arr2.[i, j])

    let addOneToAdjacents arr =
        let w = Array2D.length1 arr
        let h = Array2D.length2 arr

        let paddedArray =
            Array2D.init (w + 2) (h + 2) (fun i j ->
                if i > 0 && j > 0 && i <= w && j <= h then
                    arr.[i - 1, j - 1]
                else
                    0)

        let kernel =
            Array2D.init 3 3 (fun i j -> if i = 1 && j = 1 then 0 else 1)

        let rec updateFlashes (flashedOctopusses: Set<int * int>) =
            let mutable newlyFlashedOctopusses = Set.empty

            for i in 1 .. w + 1 do
                for j in 1 .. h + 1 do
                    if flashedOctopusses.Contains(i, j) |> not
                       && paddedArray.[i, j] > 9 then
                        newlyFlashedOctopusses <- newlyFlashedOctopusses.Add(i, j)

                        paddedArray.[i - 1..i + 1, j - 1..j + 1] <-
                            (paddedArray.[i - 1..i + 1, j - 1..j + 1]
                             |> Array2D.mapi (fun i j x -> x + kernel.[i, j]))

            // printfn "%A" newlyFlashedOctopusses
            let noNewFlashes =
                (newlyFlashedOctopusses - flashedOctopusses)
                |> Set.count = 0

            if noNewFlashes then
                (paddedArray.[1..w, 1..h], flashedOctopusses.Count)
            else
                updateFlashes (flashedOctopusses + newlyFlashedOctopusses)

        updateFlashes Set.empty


    let capToNine =
        Array2D.map (fun x -> if x > 9 then 0 else x)

    let part1 input =

        let rec simulateOneRound round energyLevels flashed =
            let newEnergyLevels, newFlashes =
                energyLevels |> addOne |> addOneToAdjacents

            match round with
            | 0 -> flashed
            | _ ->
                simulateOneRound
                    (round - 1)
                    (newEnergyLevels |> capToNine)
                    (flashed + newFlashes)

        simulateOneRound 100 input 0

    let part2 input =
        let numberOfOctopuses = input |> Seq.cast<int> |> Seq.length

        let rec simulateOneRound round energyLevels flashed =
            let newEnergyLevels, newFlashes =
                energyLevels |> addOne |> addOneToAdjacents

            match newFlashes = numberOfOctopuses with
            | true -> round + 1
            | _ ->
                simulateOneRound
                    (round + 1)
                    (newEnergyLevels |> capToNine)
                    (flashed + newFlashes)

        simulateOneRound 0 input 0
