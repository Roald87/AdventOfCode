namespace aoc

open System
open System.IO

module Day3 =
    let read fname = File.ReadLines fname |> Seq.toList

    let elementWiseAdd = List.map2 (fun x y -> x + y)

    let elementWiseSum (lst: seq<list<int>>) =
        let head = lst |> Seq.head

        lst
        |> Seq.fold
            (fun acc lst -> elementWiseAdd acc lst)
            [ for _ in 1 .. head.Length -> 0 ]

    let charArrayToInts = List.map (fun x -> int (x) - 48)

    let stringToInts =
        Seq.map Seq.toList >> Seq.map charArrayToInts

    let mostCommonDigit (lst: string list) =
        lst
        |> stringToInts
        |> elementWiseSum
        |> Seq.map (fun x -> x > lst.Length / 2)

    let leastCommonDigit = mostCommonDigit >> Seq.map not

    let boolsToDecimal (boolNumber: seq<bool>) =
        let binaryNumber = boolNumber |> Seq.map Convert.ToInt32
        Convert.ToInt32(String.concat "" (binaryNumber |> Seq.map string), 2)

    let mostCommonDigitsAsInt = mostCommonDigit >> boolsToDecimal

    let leastCommonDigitsAsInt =
        mostCommonDigit >> Seq.map not >> boolsToDecimal

    let countOnesAtPosition (pos: int) (lst: string list) =
        lst
        |> List.filter (fun x -> x.[pos] = '1')
        |> List.length

    let oxygen lst =
        let rec mostCommon iteration rest =
            let summedDigits =
                mostCommonDigit rest
                |> Seq.map Convert.ToInt32
                |> Seq.map string
                |> String.concat ""

            // printfn "iteration %i" iteration
            // printfn "sum: %A" summedDigits

            // if (rest.Length % 2) = 0 then
            //     printfn "rest: %A \n" rest

            match rest.Length with
            | 1 -> rest.[0]
            | l when
                l % 2 = 0
                && (countOnesAtPosition iteration rest = l / 2)
                ->
                mostCommon
                    (iteration + 1)
                    (rest |> List.filter (fun x -> x.[iteration] = '1'))
            | _ ->
                mostCommon
                    (iteration + 1)
                    (rest
                    |> List.filter (fun x -> x.[iteration] = (summedDigits.[iteration])))

        Convert.ToInt32(mostCommon 0 lst, 2)

    let co2 lst =
        let rec leastCommon iteration rest =
            let summedDigits =
                leastCommonDigit rest
                |> Seq.map Convert.ToInt32
                |> Seq.map string
                |> String.concat ""

            // printfn "iteration %i" iteration
            // printfn "sum: %A" summedDigits

            // if (rest.Length % 2) = 0 then
            //     printfn "rest: %A \n" rest

            match rest.Length with
            | 1 -> rest.[0]
            | l when
                l % 2 = 0
                && (countOnesAtPosition iteration rest = l / 2)
                ->
                leastCommon
                    (iteration + 1)
                    (rest |> List.filter (fun x -> x.[iteration] = '0'))
            | _ ->
                leastCommon
                    (iteration + 1)
                    (rest
                    |> List.filter (fun x -> x.[iteration] = (summedDigits.[iteration])))

        Convert.ToInt32(leastCommon 0 lst, 2)
