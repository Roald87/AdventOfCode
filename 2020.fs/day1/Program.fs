// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System.IO

let read fname = File.ReadLines fname

let readInput = read "input.txt" |> Seq.toList

[<EntryPoint>]
let main argv =
    let numbers = readInput |> List.map int

    for number1 in numbers do
        for number2 in numbers do
            for number3 in numbers do
                if number1 + number2 + number3 = 2020 then
                    printfn "%i" (number1 * number2 * number3)

    0 // return an integer exit code
