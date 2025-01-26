open System.IO

let indicesOf char arr =
    arr
    |> Array.mapi (fun i s -> s |> Seq.mapi (fun j c -> if c = char then Some(i, j) else None))
    |> Array.toSeq
    |> Seq.collect id
    |> Seq.choose id

let maze = File.ReadAllLines("day6.txt")

let obstacles = maze |> indicesOf '#' |> Set
let startingPosition = maze |> indicesOf '^' |> Seq.head

let addTuples a b = (fst a + fst b, snd a + snd b)
let subTuples a b = (fst a - fst b, snd a - snd b)

type Direction =
    | Up
    | Down
    | Left
    | Right

let getDirection direction =
    match direction with
    | Up -> (-1, 0)
    | Down -> (1, 0)
    | Left -> (0, -1)
    | Right -> (0, 1)

let toDirection (x, y) =
    match (x, y) with
    | (-1, 0) -> Up
    | (1, 0) -> Down
    | (0, -1) -> Left
    | (0, 1) -> Right
    | _ -> failwith "Invalid direction tuple"

let rotate90 direction =
    match direction with
    | Up -> Right
    | Right -> Down
    | Down -> Left
    | Left -> Up

let traversePartA obstacles start startingDirection (size: int * int) =
    let rec takeStep visited currentPos direction =
        let newPosition: int * int = addTuples currentPos (getDirection direction)
        let rs, cs = size
        let newVisited = Seq.append visited [ currentPos ]

        match newPosition with
        | (-1, _)
        | (_, -1) -> newVisited
        | p when fst p = rs -> newVisited
        | p when snd p = cs -> newVisited
        | p when obstacles |> Set.contains p -> takeStep visited currentPos (rotate90 direction)
        | _ -> takeStep newVisited newPosition direction

    // let startingPos = [ start ] |> List.toSeq
    takeStep Seq.empty start startingDirection

let size = (maze.Length, maze[0].Length)

let visitedLocations = traversePartA obstacles startingPosition Up size

visitedLocations |> set |> Set.count |> printfn "part a: %A"

let traversePartB obstacles start startingDirection (size: int * int) =
    let rec takeStep (visited: Set<(int * int) * Direction>) currentPos direction =
        let newPosition = addTuples currentPos (getDirection direction)
        let rs, cs = size

        if Set.contains (currentPos, direction) visited then
            // going in circles, exiting
            None
        else
            let newVisited = Set.add (currentPos, direction) visited |> Some

            match newPosition with
            | (-1, _)
            | (_, -1) -> newVisited
            | p when fst p = rs -> newVisited
            | p when snd p = cs -> newVisited
            | p when obstacles |> Set.contains p -> takeStep visited currentPos (rotate90 direction)
            | _ -> takeStep newVisited.Value newPosition direction

    takeStep Set.empty start startingDirection

let total = visitedLocations |> Seq.length
let mutable checkedLocations = Set.empty

// Takes 30 seconds
visitedLocations
|> Seq.mapi (fun i x ->
    if i % 50 = 0 then
        let progress = float i / float total * 100.0
        printf $"\rProgress {progress:F1}%%"

    x)
|> Seq.pairwise
|> Seq.map (fun (prev, next) ->
    let direction = subTuples next prev |> toDirection

    if checkedLocations |> Set.contains next |> not then
        let path = traversePartB (obstacles |> Set.add next) prev direction size
        checkedLocations <- checkedLocations |> Set.add next
        if path.IsNone then Some next else None
    else
        None)
|> Seq.choose id
|> set
|> Set.count
|> printfn "\npart b: %A"
