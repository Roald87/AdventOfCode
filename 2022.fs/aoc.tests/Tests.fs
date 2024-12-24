module Tests

open Xunit
open FsUnit

open aoc.utils

[<Fact>]
let ``Test position (1, 1) of example up`` () =
    elementsWhichCanBeSeen [|5; 3|] |> should equal 1
 
[<Fact>]
let ``Test position (1, 1) of example left`` () =
    elementsWhichCanBeSeen [|5; 5; 2|] |> should equal 1

[<Fact>]
let ``Test position (1, 1) of example right`` () =
    elementsWhichCanBeSeen [|5; 1; 2|] |> should equal 2

[<Fact>]
let ``Test position (1, 1) of example down`` () =
    elementsWhichCanBeSeen [|5; 3; 5; 3|] |> should equal 2

[<Fact>]
let ``Test position (3, 2) of example left`` () =
    elementsWhichCanBeSeen [|5; 3; 3|] |> should equal 2    

[<Fact>]
let ``Test position (1, 1) of example top`` () =
    elementsWhichCanBeSeen [|5; 0|] |> should equal 1  

[<Fact>]
let ``Test position (3, 2) of example right`` () =
    elementsWhichCanBeSeen [|5; 4; 9|] |> should equal 2 

[<Fact>]
let ``Test position (3, 3) of example up`` () =
    elementsWhichCanBeSeen [|4; 3; 1; 7|] |> should equal 3

[<Fact>]
let ``Test position (3, 2) of example down`` () =
    elementsWhichCanBeSeen [|3; 4; 9|] |> should equal 1

// [<Fact>]
// let ``Do first three moves of example, tail covers 4 positions`` () =
//     tailPos2 [ ("R", 5); ("U", 8); ("L", 8) ] |> should equal 4

[<Fact>]
let ``wait with diagonal move`` () =
    updateTailPosition (5,1) (4,0) |> should equal (4,0)
    updateTailPosition (2,1) (1,0) |> should equal (1,0)

[<Fact>]
let ``new tail`` () =
    let actual = newTail (2,0) [(0,0); (0,0); (0,0)]
    (actual |> List.length) |> should equal 3
    actual |> should equal [(1,0); (0,0); (0,0)]

    newTail (4,1) [(3,0); (2,0); (1,0)] |> should equal [(3,0); (2,0); (1,0)]
    newTail (3,0) [(1,0); (0,0); (0,0)] |> should equal [(2,0); (1,0); (0,0)]

// [<Fact>]
// let ``move snake part b`` () =
//     let actual1 = move2 [for i in 1..10 -> (0,0)] "R"
//     actual1 |> List.length |> should equal 10
//     actual1 |> should equal ((1,0)::[for i in 1..9 -> (0,0)])

//     let actual2 = move2 actual1 "R"
//     actual2 |> List.length |> should equal 10
//     actual2 |> should equal ([(2,0);(1,0)]@[for i in 1..8 -> (0,0)])

//     let actual3 = move2 [(5, 0); (4, 0); (3, 0); (2, 0); (1, 0); (0, 0); (0, 0); (0, 0); (0, 0); (0, 0)] "U"
//     let expected = [(5, 1); (4, 0); (3, 0); (2, 0); (1, 0); (0, 0); (0, 0); (0, 0); (0, 0); (0, 0)]
//     actual3 |> should equal expected