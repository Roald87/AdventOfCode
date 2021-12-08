namespace aoc

open System.IO

module Utilities =

    let split (sep: string) (str: string) = str.Split(sep) |> Array.toList

    let parseCsv =
        File.ReadLines
        >> Seq.toList
        >> List.map (split ",")

    let parseSingleLineCsv = parseCsv >> List.head

    let parseSingleLineIntCsv = parseSingleLineCsv >> List.map int

    let stringToChars = Seq.toArray
