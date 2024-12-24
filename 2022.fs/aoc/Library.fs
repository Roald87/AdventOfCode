namespace aoc

open System.IO

module utils =
    let split (sep:string) (str:string) = str.Split(sep)

    let trim (str: string) = str.Trim()

    let readAllLines fname = File.ReadAllLines(fname)

    let replace (find:string) (replace: string) (str: string) = str.Replace(find, replace)

    let transpose (arr: array<array<'a>>) = 
        let rows = arr.Length
        let cols = arr.[0].Length
        Array.init cols (fun c -> Array.init rows (fun r -> arr.[r].[c]))

    let canBeSeen (arr:list<'a>) valToSpot =
        let lowerThanHead = valToSpot < arr.Head
        let firstValue = arr |> List.length = 1
        let higherThanIntermediateValues = 
            if arr.Tail |> List.length > 0 then
                valToSpot >= (arr.Tail |> List.max)
            else 
                true

        (lowerThanHead || firstValue) || higherThanIntermediateValues

    let elementsWhichCanBeSeen (arr:array<'a>) = 
        let idxViewingDisance = 
            arr
            |> Array.skip 1
            |> Array.tryFindIndex (fun x -> x >= arr[0] )

        match idxViewingDisance with
        | None -> (arr |> Array.length) - 1
        | Some i -> i + 1

    type Position = int * int

    let updateTailPosition (head: Position) (tail:Position) = 
        let hx, hy = head
        let tx, ty = tail

        if abs(hx - tx) > 1 then
            ((tx + (hx - tx)/2, ty + (hy - ty)))
        elif abs(hy - ty) > 1 then
            // move diag
            ((tx + (hx - tx), ty + (hy - ty)/2))
        elif hx = tx then
            // move vertical
            (tx, ty + (hy - ty)/2)
        elif hy = ty then   
            // move horizontal 
            (tx + (hx - tx)/2, ty)
        else 
            (tail)
       

    let newHead (snake: Position ) direction =
        let hx, hy = snake

        match direction with
        | "U" -> (hx, hy+1)
        | "D" -> (hx, hy-1)
        | "L" -> (hx-1, hy)
        | "R" -> (hx+1, hy)
        | _ -> failwith $"Unrecognized direction {direction}"

    let newTail newHead oldTail =         
        let rec updateSnake snakeAcc oldTail =
            match oldTail, snakeAcc with
            | (head::remainder, []) -> 
                let updatedTail = [updateTailPosition head remainder.Head]
                printfn "%A" (1, updatedTail, oldTail)
                updateSnake updatedTail remainder
            | ([_], _) -> 
                printfn "%A" $"end {snakeAcc}"
                snakeAcc
            | (head::remainder, acc) ->
                let updatedTail = [updateTailPosition (acc |> List.last) head]
                printfn "%A" (3, updatedTail, acc, head, remainder, oldTail)
                updateSnake (acc@updatedTail) remainder
            | _ -> oldTail 

        updateSnake [] (newHead::oldTail)

    let move2 (snake: Position list) direction =
        let _newHead = newHead snake.Head direction
        let _newTail = newTail _newHead snake.Tail

        _newHead::_newTail

    let tailPos2 directions =
        let dirs = 
            directions
            |> Seq.map (fun (dir, n) -> [for _ in 0..n-1 -> dir]) 
            |> Seq.collect id
        
        let mutable snake = [for i in 1..10 -> (0,0)]
        let mutable ts = Set.empty

        for direction in dirs do
            snake <- move2 snake direction
            printfn "%A" snake
            ts <- Set.add (snake |> List.last) ts 

        ts |> Set.count 

