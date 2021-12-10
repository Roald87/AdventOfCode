namespace aoc

open System.IO

open Utilities

module Day9 =
    let parseInput = 
        File.ReadLines 
        >> Seq.toList
        >> List.map (stringToChars >> Array.toList >> List.map (fun x -> int(x) - int('0')))
    
    let lowestPoints (t: int list list) = 
        let mutable lowestCoords = []
    
        for i in 0..t.Length - 1 do
            for j in 0..t.[0].Length - 1 do
                let diffNextOne = if j < t.[0].Length - 1 then (t[i][j] - t[i][j + 1]) else -1
                let diffPrevOne = if j > 0 then (t[i][j] - t[i][j - 1]) else -1
                let diffBelowOne = if i < t.Length - 1 then (t[i][j] - t[i + 1][j]) else -1
                let diffAboveOne = if i > 0 then (t[i][j] - t[i - 1][j]) else -1
                if diffNextOne < 0 && diffPrevOne < 0 && diffBelowOne < 0 && diffAboveOne < 0 then
                    lowestCoords <- List.append lowestCoords [ (i,j) ]
    
        lowestCoords
    
    let part1 t = 
        t 
        |> lowestPoints 
        |> List.map (fun (x, y) -> (t[x][y]))
        |> List.sumBy (fun x -> x + 1)    