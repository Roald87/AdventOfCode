open System.IO

let to2dArray arr = arr |> Seq.map (Seq.toArray) |> array2D

let toArray (arr: 'T[,]) = arr |> Seq.cast<'T> |> Seq.toArray

let checkHorz (arr: char array2d) =
    arr
    |> Array2D.mapi (fun r c x ->
        if c < (arr |> Array2D.length1) - 3 then
            Some(new string (arr[r, c .. c + 3]))
        else
            None)
    |> toArray
    |> Array.choose id
    |> Array.filter (fun s -> s = "XMAS" || s = "SAMX")

let checkDiag (arr: char array2d) =
    arr
    |> Array2D.mapi (fun r c x ->
        if c < (arr |> Array2D.length1) - 3 && r < (arr |> Array2D.length2) - 3 then
            Some(new string ([| arr[r, c]; arr[r + 1, c + 1]; arr[r + 2, c + 2]; arr[r + 3, c + 3] |]))
        else
            None)
    |> toArray
    |> Array.choose id
    |> Array.filter (fun s -> s = "XMAS" || s = "SAMX")

let chars = File.ReadAllLines("day4.txt") |> to2dArray

let transpose arr =
    let rows = arr |> Array2D.length1
    let cols = arr |> Array2D.length2
    let transposedArray = Array2D.init cols rows (fun r c -> arr[c, cols - 1 - r])

    transposedArray

[| chars |> checkDiag
   chars |> checkHorz
   chars |> transpose |> checkDiag
   chars |> transpose |> checkHorz |]
|> Seq.collect id
|> Seq.length
|> printfn "part a: %A" // 2517

chars
|> Array2D.mapi (fun r c x ->
    match chars[r .. r + 2, c .. c + 2] |> toArray with
    | [| 'M'; _; 'M'; _; 'A'; _; 'S'; _; 'S' |] -> 1
    | [| 'S'; _; 'M'; _; 'A'; _; 'S'; _; 'M' |] -> 1
    | [| 'M'; _; 'S'; _; 'A'; _; 'M'; _; 'S' |] -> 1
    | [| 'S'; _; 'S'; _; 'A'; _; 'M'; _; 'M' |] -> 1
    | _ -> 0)
|> toArray
|> Array.sum
|> printfn "part b: %A" //1960
