namespace aoc

open System.IO

open Utilities

module Day10 =

    let parseInput = File.ReadLines >> Seq.toList

    let replacePossibleMatchingBraces (line: string) =
        line
            .Replace("()", "")
            .Replace("[]", "")
            .Replace("<>", "")
            .Replace("{}", "")

    let removeAllMatchingBraces (line: string) =
        let rec removeBraces (line: string) =
            let filteredLine = replacePossibleMatchingBraces line

            if filteredLine.Length = line.Length then
                line
            else
                removeBraces filteredLine

        removeBraces line

    let firstInvalidClosingBrace (line: string) =
        let i =
            line.IndexOfAny([| '}'; '>'; ']'; ')' |])

        match i with
        | -1 -> None
        | _ -> Some(line.[i])

    let valueClosingBrackets (bracket: char option) =
        match bracket with
        | Some (')') -> 3
        | Some (']') -> 57
        | Some ('}') -> 1197
        | Some ('>') -> 25137
        | _ -> 0

    let part1 =
        List.map (
            removeAllMatchingBraces
            >> firstInvalidClosingBrace
            >> valueClosingBrackets
        )
        >> List.sum

    let charValues (c: char) =
        match c with
        | ')' -> 1
        | ']' -> 2
        | '}' -> 3
        | '>' -> 4
        | _ -> 0

    let opposingBracket (c: char) =
        match c with
        | '(' -> ')'
        | '[' -> ']'
        | '{' -> '}'
        | '<' -> '>'
        | _ -> ' '

    let scores input =
        input
        |> List.map (removeAllMatchingBraces)
        |> List.filter (fun x -> (firstInvalidClosingBrace x).IsNone)
        |> List.map (
            stringToChars
            >> Array.rev
            >> Array.map opposingBracket
            >> Array.map (fun x -> x |> charValues |> int64)
            >> Array.reduce (fun (a: int64) (b: int64) -> a * (int64 5) + b)
        )
        |> List.sort

    let part2 input =
        let allScores = scores input

        allScores.[allScores.Length / 2]
