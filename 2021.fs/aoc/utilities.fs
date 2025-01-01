namespace aoc

open System
open System.IO

module Utilities =

    let split (sep: string) (str: string) =
        str.Split(sep, StringSplitOptions.RemoveEmptyEntries) |> Array.toList

    let parseCsv = File.ReadLines >> Seq.toList >> List.map (split ",")

    let parseSingleLineCsv = parseCsv >> List.head

    let parseSingleLineIntCsv = parseSingleLineCsv >> List.map int

    let stringToChars (str: string) = str |> Seq.toArray

    let concat (chars: char list) =
        chars |> List.map string |> String.concat ""

    let concatA (chars: char[]) = chars |> Array.toList |> concat

    let size arr =
        arr |> Array2D.length1, arr |> Array2D.length2
