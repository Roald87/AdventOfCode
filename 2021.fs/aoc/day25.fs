namespace aoc

open System.IO

open Utilities

module Day25 =
    let parseInput = File.ReadLines >> Seq.map stringToChars >> array2D

    let moveEast input =
        let _, w = size input

        input
        |> Array2D.mapi (fun i j x ->
            if input.[i, (j + w - 1) % w] = '>' && x = '.' then '>'
            elif input.[i, (j + 1) % w] = '.' && x = '>' then '.'
            else x)

    let moveSouth input =
        let h, _ = size input

        input
        |> Array2D.mapi (fun i j x ->
            if input.[(i + h - 1) % h, j] = 'v' && x = '.' then 'v'
            elif input.[(i + 1) % h, j] = '.' && x = 'v' then '.'
            else x)

    let doStep = moveEast >> moveSouth

    let moveUntilJammed input =
        let rec moveUntilJammed currentState n =
            let newState = currentState |> doStep
            if newState = currentState then n + 1 else moveUntilJammed newState (n + 1)

        moveUntilJammed input 0

    let part1 = parseInput >> moveUntilJammed
