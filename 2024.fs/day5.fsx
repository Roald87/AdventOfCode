open System.IO

// Custom sorting order
let sort order (a: string) (b: string) =
    let rec sortWithOrder (order: Set<string * string>) a b =
        if order.Contains(a, b) then -1
        elif order.Contains(b, a) then 1
        else failwith "Unknown case"

    sortWithOrder order a b

let (pageOrder, updates) =
    let (ord, up) =
        File.ReadAllLines("day5.txt")
        |> Array.partition (fun line -> line.Contains("|"))

    (ord
     |> Array.map (fun s ->
         let nums = s.Split("|")
         (nums[0], nums[1]))
     |> Set,
     up |> Array.filter (fun s -> s.Length > 0) |> Array.map (fun s -> s.Split(",")))


let (sortedUpdates, unsortedUpdates) =
    updates
    |> Array.partition (fun arr -> arr = (arr |> Array.sortWith (sort pageOrder)))

sortedUpdates
|> Seq.map (fun arr -> arr[arr.Length / 2] |> int)
|> Seq.sum
|> printfn "part a: %A"

unsortedUpdates
|> Seq.map (fun x -> Array.sortWith (sort pageOrder) x)
|> Seq.map (fun arr -> arr[arr.Length / 2] |> int)
|> Seq.sum
|> printfn "part b: %A"
