open System.IO

// let calculations =
//     File.ReadAllLines("day7-ex.txt")
//     |> Seq.map (fun s -> s.Split(":"))
//     |> Seq.map (fun s -> (int s[0], s[1].Trim().Split() |> Seq.map int))

// let toBinary (number: int) = System.Convert.ToString(number, 2)

// let combs n =
//     let length = n |> toBinary |> Seq.toArray |> Seq.length

//     let binaryNums =
//         seq {
//             for i in 0..n ->
//                 i
//                 |> toBinary
//                 |> (fun s -> s.PadLeft(length, '0'))
//                 |> Seq.map (fun c -> int c - int '0')
//         }

//     let operations n a b =
//         match n with
//         | 0 -> a * b
//         | 1 -> a + b
//         | _ -> failwith $"Invalid number {n}"

//     binaryNums |> Seq.map (Seq.map (fun (i: int) -> operations i))


// calculations
// |> Seq.map (
//     fun (ans, nums) ->
//         let ops = combs (Seq.length nums)

//         ops
//         |> Seq.map (
//             fun op ->
//                 let head, tail =
//                     match nums |> Seq.toList with
//                     | head :: tail -> head, (tail |> List.toSeq)
//                     | _ -> failwith "List of numbers too short"

//                 (Seq.zip tail op) 
//                 |> Seq.reduce (fun (n, f) -> f head n )
//                 |> Seq.exists (fun a -> a = ans)
//         )
//     |> printfn "%A"
// )

open System.IO

type Equation =
    { testNumber: uint64
      stepNumbers: list<uint64> }

let parseContents filename =
    File.ReadAllLines filename
    |> Seq.map (fun x ->
        let parts = x.Split(": ")
        let steps = parts[1].Split(" ") |> Array.map uint64

        { testNumber = uint64 (parts[0])
          stepNumbers = Array.toList steps })

type Operator =
    | Add
    | Mul
    | Concat

let evaluateOperator acc cur operation=
    match operation with
    | Add -> acc + cur
    | Mul -> acc * cur
    | Concat -> (string acc) + (string cur) |> uint64

let hasSolution equation operators =

    let rec resolveEquation steps acc =
        match steps with
        | [] -> acc = equation.testNumber
        | head :: tail ->
            operators
            |> Seq.map (evaluateOperator acc head) 
            |> Seq.exists (resolveEquation tail)

    resolveEquation equation.stepNumbers 0UL


let contents = parseContents "day7-ex.txt"

// Part 1
Seq.filter (fun x -> hasSolution x [ Add; Mul ]) contents
|> Seq.sumBy (fun x -> x.testNumber)
|> printfn "Test value sum: %d"

//Part 2
Seq.filter (fun x -> hasSolution x [ Add; Mul; Concat ]) contents
|> Seq.sumBy (fun x -> x.testNumber)
|> printfn "Test value sum with concat: %d"