namespace aoc

open System
open System.IO

open Utilities

module Day4 =
    let read fname = File.ReadLines fname

    let readInput = read >> Seq.toList
    
    let draws = readInput >> List.head >> split ","
    
    let boards =
        readInput // import all lines from the input file
        >> List.skip 1 // skip the first line
        >> List.filter (not << String.IsNullOrWhiteSpace) // filter out the empty lines
        >> List.map (split " ") // split each line at a space
        >> List.chunkBySize 5 // collect 5 lines into a new list
    
    let emptyRow =
        List.filter (fun x -> x <> "")
        >> List.length
        >> (=) 0
    
    let emptyRows =
        List.map emptyRow
        >> List.exists (fun x -> x = true)
    
    let emptyColumns = List.transpose >> emptyRows
    
    let removeDrawnNumber numberDrawn =
        List.map (List.map (fun x -> if x = numberDrawn then "" else x))
    
    let rowScore =
        List.filter (fun x -> x <> "")
        >> List.map Convert.ToInt32
    
    let totalBoardScore =
        List.map rowScore >> List.map List.sum >> List.sum
    
    let firstToWin draws boards =
        let rec checkBingo (numbersToDraw: list<string>) boards =
            let newBoard =
                boards
                |> List.map (removeDrawnNumber numbersToDraw.Head)
    
            let bingo =
                newBoard
                |> List.tryFind (fun x -> emptyRows x || emptyColumns x)
    
            match bingo with
            | Some b -> totalBoardScore b * int (numbersToDraw.Head)
            | None -> checkBingo numbersToDraw.Tail newBoard
    
        checkBingo draws boards
    
    let lastToWin draws boards =
        let rec checkBingo (numbersToDraw: list<string>) boards =
            let newBoard =
                boards
                |> List.map (removeDrawnNumber numbersToDraw.Head)
                |> List.filter (fun x -> not (emptyRows x || emptyColumns x))
    
            match newBoard.Length with
            | 1 -> firstToWin numbersToDraw.Tail newBoard
            | _ -> checkBingo numbersToDraw.Tail newBoard
    
        checkBingo draws boards
    