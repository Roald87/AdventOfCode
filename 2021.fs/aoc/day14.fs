namespace aoc

open System.Collections.Generic
open System.IO

open Utilities

module Day14 =
    let polymerTemplate = File.ReadLines >> Seq.head

    let pairInsertions =
        File.ReadLines
        >> Seq.skip 2
        >> Seq.map (fun x -> split "->" x)
        >> Seq.map (fun x -> x.[0].Trim(), x.[1].Trim())
        >> dict

    let rec doSteps
        remainingSteps
        (template: string)
        (inserts: IDictionary<string, string>)
        =
        let newTemplate =
            template
            |> stringToChars
            |> Array.pairwise
            |> Array.map (fun (fst, snd) -> $"{fst}{snd}")
            |> Array.map (fun x ->
                match inserts.TryGetValue x with
                | true, value -> $"{x.[0]}{value}"
                | _ -> x)
            |> String.concat ""

        let last = template.[template.Length - 1] |> string

        match remainingSteps with
        | 1 -> newTemplate + last
        | _ -> doSteps (remainingSteps - 1) (newTemplate + last) inserts

    let part1 fname =
        let template = polymerTemplate fname
        let inserts = pairInsertions fname
        let poly = doSteps 10 template inserts

        let counted =
            poly
            |> stringToChars
            |> Seq.toArray
            |> Seq.countBy id
            |> Seq.sortBy (fun (c, i) -> i)

        let _, highest = counted |> Seq.last
        let _, lowest = counted |> Seq.head
        highest - lowest
