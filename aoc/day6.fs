namespace aoc

open System.IO

open Utilities

module Day6 =

    let parseInput =
        File.ReadLines
        >> Seq.toList
        >> List.map (split ",")
        >> List.head
        >> List.map int

    let simSingleFishGrowth step initialValue =
        let pop = [ for i in 0UL .. 8UL -> 1UL ]

        let rec grow step pop =
            match step - initialValue with
            | 0UL -> pop
            | _ -> grow (step - 1UL) (pop.Tail @ [ pop.[0] + pop.[2] ])

        grow step pop

    let simFishSchool reps =
        List.map uint64
        >> List.map (simSingleFishGrowth reps)
        >> List.map (List.rev >> List.head >> uint64)
        >> List.sum
