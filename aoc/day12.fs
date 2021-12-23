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
        let mutable nPaths = 0

        let rec countNextPaths origin seen countTwice =
            let newSeen =
                if origin |> isLower then
                    Set.union seen (Set.empty.Add origin)
                else
                    seen

            for target in caveMap.[origin] do
                if target = "end" then
                    nPaths <- nPaths + 1
                elif seen |> Set.contains target |> not then
                    nPaths <- countNextPaths target newSeen countTwice
                elif target <> "start" && countTwice then
                    nPaths <- countNextPaths target newSeen false
                else
                    ()

            nPaths

        countNextPaths "start" Set.empty countSmallOnesTwice

    let part1 = countAllPaths false

    let part2 = countAllPaths true
