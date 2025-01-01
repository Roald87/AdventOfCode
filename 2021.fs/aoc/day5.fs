namespace aoc

open System.IO

open Utilities

module Day5 =

    type Coordinate = { x: int; y: int }
    type Coordinates = { x1: int; y1: int; x2: int; y2: int }

    let toCoordinates (pairs: string list) =
        let c = pairs |> List.map (split ",") |> List.map (List.map int)

        { x1 = c.[0].[0]
          y1 = c.[0].[1]
          x2 = c.[1].[0]
          y2 = c.[1].[1] }

    let parseInput =
        File.ReadLines
        >> Seq.toList
        >> List.map (split " -> ")
        >> List.map toCoordinates

    let sweepCoords1 c =
        match c with
        | c when c.x1 = c.x2 ->
            let step = if c.y1 < c.y2 then 1 else -1

            [ c.y1 .. step .. c.y2 ] |> List.map (fun y -> { x = c.x1; y = y })
        | c when c.y1 = c.y2 ->
            let step = if c.x1 < c.x2 then 1 else -1

            [ c.x1 .. step .. c.x2 ] |> List.map (fun x -> { x = x; y = c.y1 })
        | _ -> []

    let markVents1 (coords: Coordinates list) =
        coords |> List.map sweepCoords1 |> List.concat

    let countVents (c: Coordinate list) = c |> List.countBy id

    let part1 (coords: list<Coordinates>) =
        let coorCounts = coords |> markVents1 |> countVents

        coorCounts |> List.filter (fun (_, n) -> n >= 2) |> List.length

    let sweepCoords2 c =
        match c with
        | c when
            abs (c.x1 - c.y2) = abs (c.y1 - c.x2)
            || abs (c.x1 - c.x2) = abs (c.y1 - c.y2)
            ->
            let xstep = if c.x1 < c.x2 then 1 else -1
            let x = [ c.x1 .. xstep .. c.x2 ]

            let ystep = if c.y1 < c.y2 then 1 else -1
            let y = [ c.y1 .. ystep .. c.y2 ]

            List.zip x y |> List.map (fun (x, y) -> { x = x; y = y })
        | _ -> sweepCoords1 c

    let markVents2 (coords: Coordinates list) =
        coords |> List.map sweepCoords2 |> List.concat

    let part2 (coords: list<Coordinates>) =
        let coorCounts = coords |> markVents2 |> countVents

        coorCounts |> List.filter (fun (_, n) -> n >= 2) |> List.length

    let printGrid coords =

        let grid =
            Array2D.init 10 10 (fun x y ->
                (coords |> countVents |> dict).TryGetValue({ x = y; y = x }) |> snd)

        printfn "%A" grid
