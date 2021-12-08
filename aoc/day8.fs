namespace aoc

open System.IO

open Utilities

module Day8 =

    let parseInput =
        File.ReadLines
        >> Seq.toList
        >> List.map (split "|")
        >> List.map (List.map (split " "))
        >> List.map (List.map (List.filter (fun x -> x.Length <> 0)))

    let part1 (text: string list list list) =
        text
        |> (List.map (fun x -> x.[1])
            >> List.concat
            >> List.groupBy (fun x -> x.Length)
            >> List.filter (fun (n, x) -> n = 2 || n = 4 || n = 3 || n = 7)
            >> List.map (fun (_, x) -> x.Length)
            >> List.sum)

    let normalMappingToInt letters =
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

    let stringSet = List.head >> Seq.toArray >> Set.ofSeq

    let filterLength n (lst: string list) =
        lst |> List.filter (fun x -> x.Length = n)

    let findWiring letters =
        let one = letters |> filterLength 2 |> stringSet

        let four = letters |> filterLength 4 |> stringSet

        let seven = letters |> filterLength 3 |> stringSet

        let eight = letters |> filterLength 7 |> stringSet

        let otherNumbers =
            letters
            |> List.groupBy (fun x -> x.Length)
            |> List.filter (fun (n, x) -> n <> 2 && n <> 4 && n <> 3 && n <> 7)
            |> dict

        let a = seven - one
        let b_and_d = four - one
        let c_and_f = one
        let e_and_g = eight - (seven + four)

        let g =
            otherNumbers.[6]
            |> List.map (fun x -> stringSet ([ x ]) - (seven + four))
            |> List.filter (fun x -> x.Count = 1)
            |> List.head

        let e = e_and_g - g

        let b =
            otherNumbers.[6]
            |> List.map (fun x -> stringSet ([ x ]) - (one + a + e_and_g))
            |> List.filter (fun x -> x.Count = 1)
            |> List.head

        let d = b_and_d - b

        let f =
            otherNumbers.[6]
            |> List.map (fun x -> stringSet ([ x ]) - (a + b + d + e + g))
            |> List.filter (fun x -> x.Count = 1)
            |> List.head

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

    let wiring = findWiring

    let concat (chars: char list) =
        chars |> List.map string |> String.concat ""

    let concatA (chars: char []) = chars |> Array.toList |> concat

    let translate (text: string list list) =
        let wiring = findWiring text.[0]

        text.[1]
        |> List.map stringToChars
        |> List.map (Array.map (fun c -> wiring.[c]))
        |> List.map (Array.sort >> concatA)

    let translateToNumber =
        translate
        >> List.map normalMappingToInt
        >> List.rev
        >> List.mapi (fun i x -> 10.0 ** (float i) * float (x))
        >> List.sum

    let part2 = List.map translateToNumber >> List.sum
