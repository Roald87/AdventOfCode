namespace aoc

open System.IO

module Day2 =

    let read = File.ReadLines >> Seq.toList

    let separate (str: string) = str.Split(" ") |> Array.toList

    let parseInput = read >> List.map separate

    let count x = Seq.filter ((=) x) >> Seq.length

    type Movement =
        { Horizontal: int64
          Vertical: int64
          Aim: int64 }

    let move (movementSoFar: Movement) newMove =

        match newMove with
        | [ "forward"; _ ] ->
            { Horizontal = movementSoFar.Horizontal + int64 (newMove.[1])
              Vertical =
                movementSoFar.Vertical + movementSoFar.Aim * int64 (newMove.[1])
              Aim = movementSoFar.Aim }
        | [ "down"; _ ] ->
            { Horizontal = movementSoFar.Horizontal
              Vertical = movementSoFar.Vertical //+ int64 (newMove.[1])
              Aim = movementSoFar.Aim + int64 (newMove.[1]) }
        | [ "up"; _ ] ->
            { Horizontal = movementSoFar.Horizontal
              Vertical = movementSoFar.Vertical //- int64 (newMove.[1])
              Aim = movementSoFar.Aim - int64 (newMove.[1]) }
        | _ -> movementSoFar

    let part2 fname =
        parseInput fname
        |> List.fold
            move
            { Horizontal = 0L
              Vertical = 0L
              Aim = 0L }
