namespace aoc

open System.IO

open Utilities

module Day8 =

    let parseInput =
        File.ReadLines
        >> Seq.toList
        >> List.map (split "|" >> List.map (split " "))

    let part1 (text: string list list list) =
        text
        |> (List.map (fun x -> x.[1])
            >> List.concat
            >> List.groupBy (fun x -> x.Length)
            >> List.filter (fun (n, x) -> n = 2 || n = 4 || n = 3 || n = 7)
            >> List.map (fun (_, x) -> x.Length)
            >> List.sum)

    let stringToCharSet = Seq.toArray >> Set.ofSeq

    let firstOfLength n (lst: string list) =
        lst
        |> List.filter (fun x -> x.Length = n)
        |> List.head

    let findWiring letters =
        let one =
            letters |> firstOfLength 2 |> stringToCharSet

        let four =
            letters |> firstOfLength 4 |> stringToCharSet

        let seven =
            letters |> firstOfLength 3 |> stringToCharSet

        let eight =
            letters |> firstOfLength 7 |> stringToCharSet

        let otherNumbers =
            letters
            |> List.groupBy (fun x -> x.Length)
            |> List.filter (fun (n, _) -> n <> 2 && n <> 4 && n <> 3 && n <> 7)
            |> dict

        let a = seven - one
        let b_and_d = four - one
        let c_and_f = one
        let e_and_g = eight - (seven + four)

        let getMappedChar charSet =
            List.map (fun x -> stringToCharSet x - charSet)
            >> List.filter (fun x -> x.Count = 1)
            >> List.head

        let g =
            getMappedChar (seven + four) otherNumbers.[6]

        let e = e_and_g - g

        let b =
            getMappedChar (one + a + e_and_g) otherNumbers.[6]

        let d = b_and_d - b

        let f =
            getMappedChar (a + b + d + e + g) otherNumbers.[6]

        let c = c_and_f - f

        [ a, 'a'
          b, 'b'
          c, 'c'
          d, 'd'
          e, 'e'
          f, 'f'
          g, 'g' ]
        |> List.map (fun (k, v) -> (k.MaximumElement, v))
        |> Map.ofList

    let translate (text: string list list) =
        let wiring = findWiring text.[0]

        text.[1]
        |> List.map (
            stringToChars
            >> (Array.map (fun c -> wiring.[c]))
            >> (Array.sort >> concatA)
        )

    let segmentLettersToInt letters =
        match letters with
        | "abcefg" -> 0
        | "cf" -> 1
        | "acdeg" -> 2
        | "acdfg" -> 3
        | "bcdf" -> 4
        | "abdfg" -> 5
        | "abdefg" -> 6
        | "acf" -> 7
        | "abcdefg" -> 8
        | "abcdfg" -> 9
        | _ -> -1

    let numberListToDecimal =
        List.rev
        >> List.mapi (fun i x -> 10.0 ** (float i) * float (x))

    let translateToNumber =
        translate
        >> List.map segmentLettersToInt
        >> numberListToDecimal
        >> List.sum

    let part2 = List.map translateToNumber >> List.sum
