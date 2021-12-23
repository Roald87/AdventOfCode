namespace aoc

open System
open System.IO

open Utilities

module Day12 =

    let parseInput =
        File.ReadLines
        >> Seq.toList
        >> List.map (split "-")

    let trajectories input : Map<string, list<string>> =
        input @ (input |> List.map List.rev)
        |> List.groupBy (fun from -> from.[0])
        |> List.map (fun (from, toos) -> from, List.concat toos)
        |> List.map (fun (from, toos) ->
            (from, (List.filter (fun towards -> from <> towards) toos)))
        |> Map

    let allLower (str: string) =
        let lowerCases =
            str
            |> stringToChars
            |> Array.filter (fun x -> x |> Char.IsLower)
            |> Array.length

        lowerCases = str.Length

    let countAllPaths countSmallOnesTwice input =
        let caveMap = trajectories input
        let mutable nPaths = 0

        let rec countNextPaths origin seen countTwice =
            let newSeen =
                if origin |> allLower then
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
