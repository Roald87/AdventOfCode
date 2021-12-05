module Tests

open aoc

open Xunit
open FsUnit.Xunit

[<Fact>]
let ``day 1 part 2 real data`` () =
    let input = Day1.read "assets/input1.txt"
    Day1.part2 input |> should equal 1571

[<Fact>]
let ``day 2 part 2 real data`` () =
    let movement = Day2.part2 "assets/input2.txt"

    movement.Horizontal * movement.Vertical
    |> should equal 1971095320L

[<Fact>]
let ``day 3 part 1 real data`` () =
    let input = Day3.read "assets/input3.txt"

    Day3.mostCommonDigitsAsInt input
    * Day3.leastCommonDigitsAsInt input
    |> should equal 738234

[<Fact>]
let ``day 3 part 2 real data`` () =
    let input = Day3.read "assets/input3.txt"

    Day3.oxygen input * Day3.co2 input
    |> should equal 3969126

[<Fact>]
let ``day 3 part test data oxygen`` () =
    let input = Day3.read "assets/test3.txt"

    Day3.oxygen input |> should equal 23

[<Fact>]
let ``day 3 part test data co2`` () =
    let input = Day3.read "assets/test3.txt"

    Day3.co2 input |> should equal 10

[<Fact>]
let ``day 4 part 1 real data`` () =
    let myDraws = Day4.draws "assets/input4.txt"
    let myBoards = Day4.boards "assets/input4.txt"

    Day4.firstToWin myDraws myBoards
    |> should equal 49860

[<Fact>]
let ``day 4 part 2 real data`` () =
    let myDraws = Day4.draws "assets/input4.txt"
    let myBoards = Day4.boards "assets/input4.txt"

    Day4.lastToWin myDraws myBoards
    |> should equal 24628

[<Fact>]
let ``day 5 part 1 test data`` () =
    let input = Day5.parseInput "assets/test5.txt"

    Day5.part1 input |> should equal 5

[<Fact>]
let ``day 5 part 2 test data`` () =
    let input = Day5.parseInput "assets/test5.txt"

    Day5.part2 input |> should equal 12

[<Fact>]
let ``day 5 part 1 real data`` () =
    let input = Day5.parseInput "assets/input5.txt"

    Day5.part1 input |> should equal 7297

[<Fact>]
let ``day 5 part 2 real data`` () =
    let input = Day5.parseInput "assets/input5.txt"

    Day5.part2 input |> should equal 21038

// open Day5

// [<Fact>]
// let ``day 5 diagonal check`` () =
//     Day5.sweepCoords2 { x1 = 6; y1 = 4; x2 = 2; y2 = 0 }
//     |> should
//         equal
//         [ { x = 6; y = 4 }
//           { x = 5; y = 3 }
//           { x = 4; y = 2 }
//           { x = 3; y = 1 }
//           { x = 2; y = 0 } ]
