namespace aoc

open System.IO
open System.Collections.Generic
open FSharpx.Collections

open Utilities

// Stolen from https://github.com/bainewedlock/aoc-2021-fsharp/blob/master/aoc-2021-15/Program.fs
module Day15 =
    let parseInput =
        File.ReadLines
        >> Seq.map (
            stringToChars
            >> Seq.map (fun x -> int (x) - int ('0'))
        )
        >> array2D

    type Point = int * int
    type Cells = IDictionary<Point, int>
    
    type Input = {
        cells : Cells
        goal  : Point
        width : int
        height : int }
       
    // let parse (fname:string) =
    //     let cells = parseInput fname
    //     let mx, my = size cells
    //     {
    //         cells  = Map (cells |> Array2D.mapi (fun i j x -> Point(i, j), x) |> Seq.cast<Point * int>)
    //         goal   = mx,my
    //         width  = mx+1
    //         height = my+1
    //     }
    
    let parseLine (y:int,line:string) =
        let parseChar (x, c) = (x,y), c|>string|>int
        Seq.indexed line |> Seq.map parseChar
    
    let parse (input:string) =
        let cells = input.Split "\r\n" |> Seq.indexed |> Seq.collect parseLine
        let mx = cells |> Seq.map (fst >> fst) |> Seq.max
        let my = cells |> Seq.map (fst >> snd) |> Seq.max
        {
            cells  = Map cells
            goal   = mx,my
            width  = mx+1
            height = my+1
        }

    type WeightedPoint = { risk : int; point : Point }
    type Seen = Point Set
    type Queue = Heap<WeightedPoint>
    
    let neighbours (x,y) (cells:Cells) =
        [1,0; 0,1; -1,0; 0,-1]
        |> List.map (fun (dx,dy) -> (dx + x, dy + y))
        |> List.filter cells.ContainsKey
    
    let calcVisit wp cells (seen:Point Set) =
        neighbours wp.point cells
        |> List.filter (seen.Contains >> not)
        |> List.map (fun p -> { risk=wp.risk + cells.Item p; point = p })
    
    let addToSeen (s:Seen) p = s.Add p.point
    let addToQueue (h:Queue) p = h.Insert p
    
    let dijkstra input =
        let seen = Set [(0,0)]
        let queue = Heap(false, 0, HeapData.E).Insert { risk=0; point=0,0 }
        let rec loop (seen:Point Set) (queue:Heap<WeightedPoint>) =
            let u = queue.Head
            if u.point = input.goal then u.risk else
            let visit = calcVisit u input.cells seen
            loop
                (visit |> List.fold addToSeen seen)
                (visit |> List.fold addToQueue (queue.Tail()))
        loop seen queue

    let rec wrap x = if x > 9 then wrap (x-9) else x

    let multiplyGrid grid =
        let cells = seq [
            for fx in 0..4 do
            for fy in 0..4 do
            for c in grid.cells do
                let x0, y0 = c.Key
                let p = x0 + fx*grid.width, y0+fy*grid.height
                let v = c.Value + fx + fy |> wrap
                yield p, v ]
        let u = grid.width * 5 - 1
        { grid with cells = dict cells; goal = (u,u) }
    
    let part1 input =
        input
        |> parse
        |> dijkstra
    
    let part2 input =
        input
        |> parse
        |> multiplyGrid
        |> dijkstra