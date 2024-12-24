namespace aoc

open System
open System.IO

open Utilities

module Day12 =

    let parseInput =
        File.ReadLines
        >> Seq.toList
        >> List.map (split "-")

    let caveMapper input : Map<string, list<string>> =
        input @ (input |> List.map List.rev)
        |> List.groupBy (fun from -> from.[0])
        |> List.map (fun (from, toos) ->
            from, (List.concat toos |> List.except [ from ]))
        |> Map

    let isLower (str: string) = str.[0] |> Char.IsLower

    let countAllPaths countSmallOnesTwice input =
        let caveMap = caveMapper input

        let rec countNextPaths origin seen countTwice =
            let newSeen =
                if origin |> isLower then
                    Set.union seen (Set.empty.Add origin)
                else
                    seen

            List.sumBy
                (fun target ->
                    if target = "end" then
                        1
                    elif seen |> Set.contains target |> not then
                        countNextPaths target newSeen countTwice
                    elif target <> "start" && countTwice then
                        countNextPaths target newSeen false
                    else
                        0)
                caveMap.[origin]

        countNextPaths "start" Set.empty countSmallOnesTwice

    let part1 = countAllPaths false

    let part2 = countAllPaths true
