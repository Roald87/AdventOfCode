namespace aoc

open System
open System.IO

open Utilities

module Day13 =

    let dotPositions =
        File.ReadLines
        >> Seq.takeWhile (fun x -> x.Contains ",")
        >> Seq.toList
        >> List.map (split ",")
        >> List.map (fun x -> List.map int x)

    type Fold =
        | X of int
        | Y of int

    let toFold (str: string) =
        let foldLine = str |> split "=" |> List.last |> int
        if str.Contains "x" then X foldLine else Y foldLine

    let foldInstructions =
        File.ReadLines
        >> Seq.rev
        >> Seq.takeWhile (fun x -> x.Contains "fold")
        >> Seq.rev
        >> Seq.map toFold

    let readDots input =
        let dots = dotPositions input

        let width = dots |> List.map (fun x -> x.[0]) |> List.max

        let height = dots |> List.map (fun x -> x.[1]) |> List.max

        let paper = Array2D.zeroCreate (height + 1) (width + 1)

        for dot in dots do
            paper.[dot.[1], dot.[0]] <- 1

        paper

    let size arr =
        arr |> Array2D.length1, arr |> Array2D.length2

    let mirrorX arr =
        let heigth, width = size arr
        Array2D.init heigth width (fun i j -> arr.[i, Math.Abs(j - width + 1)])

    let mirrorY arr =
        let heigth, width = size arr
        Array2D.init heigth width (fun i j -> arr.[Math.Abs(i - heigth + 1), j])

    let elementWiseAdd (arr1: int[,]) (arr2: int[,]) =
        let heigth, width = size arr1
        Array2D.init heigth width (fun i j -> arr1.[i, j] + arr2.[i, j])

    let fold line (paper: int[,]) =
        let side1, side2 =
            match line with
            | X x -> paper.[*, .. x - 1], mirrorX paper.[*, x + 1 ..]
            | Y y -> paper.[.. y - 1, *], mirrorY paper.[y + 1 .., *]

        elementWiseAdd side1 side2

    let doFolding foldingPaper instructions =
        let rec doFold (foldingPaper: int[,]) (instructions: seq<Fold>) =
            match instructions |> Seq.toList with
            | [] -> foldingPaper
            | head :: tail -> doFold (fold head foldingPaper) tail

        doFold foldingPaper instructions

    let part1 input =
        fold (foldInstructions input |> Seq.head) (readDots input)
        |> Seq.cast<int>
        |> Seq.sumBy (fun x -> if x >= 1 then 1 else 0)

    let part2 input =
        let foldedPaper = doFolding (readDots input) (foldInstructions input)

        let widthArr = foldedPaper |> Array2D.length1

        foldedPaper
        |> Seq.cast<int>
        |> Seq.map (fun x -> if x >= 1 then "█" else " ")
        |> Seq.toArray
        |> Array.splitInto widthArr
        |> Array.map (String.concat "")
