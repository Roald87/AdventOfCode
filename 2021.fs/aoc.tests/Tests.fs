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

    movement.Horizontal * movement.Vertical |> should equal 1971095320L

[<Fact>]
let ``day 3 part 1 real data`` () =
    let input = Day3.read "assets/input3.txt"

    Day3.mostCommonDigitsAsInt input * Day3.leastCommonDigitsAsInt input
    |> should equal 738234

[<Fact>]
let ``day 3 part 2 real data`` () =
    let input = Day3.read "assets/input3.txt"

    Day3.oxygen input * Day3.co2 input |> should equal 3969126

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

    Day4.firstToWin myDraws myBoards |> should equal 49860

[<Fact>]
let ``day 4 part 2 real data`` () =
    let myDraws = Day4.draws "assets/input4.txt"
    let myBoards = Day4.boards "assets/input4.txt"

    Day4.lastToWin myDraws myBoards |> should equal 24628

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

[<Fact>]
let ``Day 6 part 1 large numbers `` () =
    let input = Day6.parseInput "assets/test6.txt"

    Day6.simFishSchool 18UL input |> should equal 26UL

    Day6.simFishSchool 80UL input |> should equal 5934UL

[<Fact>]
let ``Day 8 part 1`` () =
    let input = Day8.parseInput "assets/input8.txt"

    Day8.part1 input |> should equal 367

[<Fact>]
let ``Day 8 part 2`` () =
    let input = Day8.parseInput "assets/input8.txt"

    Day8.part2 input |> should equal 974512.0

[<Fact>]
let ``Day 9 part 1`` () =
    let input = Day9.parseInput "assets/input9.txt"

    Day9.part1 input |> should equal 526

[<Fact>]
let ``Day 9 part 2 test data`` () =
    let input = Day9.parseInput "assets/test9.txt"

    Day9.part2 input |> should equal 1134

[<Fact>]
let ``Day 9 part 2 real data`` () =
    let input = Day9.parseInput "assets/input9.txt"

    Day9.part2 input |> should equal 1123524

[<Fact>]
let ``Day 10 part 1 real data`` () =
    let input = Day10.parseInput "assets/input10.txt"

    Day10.part1 input |> should equal 345441

[<Fact>]
let ``Day 10 part 2 real data`` () =
    let input = Day10.parseInput "assets/input10.txt"

    Day10.part2 input |> should equal 3235371166L

[<Fact>]
let ``Day 11 part 1 real data`` () =
    let input = Day11.parseInput "assets/input11.txt"

    Day11.part1 input |> should equal 1749

[<Fact>]
let ``Day 11 part 2 real data`` () =
    let input = Day11.parseInput "assets/input11.txt"

    Day11.part2 input |> should equal 285

[<Fact>]
let ``Day 12 part 1 real data`` () =
    let input = Day12.parseInput "assets/input12.txt"

    Day12.part1 input |> should equal 3576

[<Fact>]
let ``Day 12 part 2 test data`` () =
    let input = Day12.parseInput "assets/test12.txt"

    Day12.part2 input |> should equal 36

[<Fact>]
let ``Day 12 part 2 real data`` () =
    let input = Day12.parseInput "assets/input12.txt"

    Day12.part2 input |> should equal 84271

[<Fact>]
let ``Day 13 part 1 real data`` () =
    Day13.part1 "assets/input13.txt" |> should equal 745

[<Fact>]
let ``Day 13 part 2 real data`` () =
    Day13.part2 "assets/input13.txt"
    |> should
        equal
        [| " ██  ███  █  █   ██ ████ ███   ██   ██  "
           "█  █ █  █ █ █     █ █    █  █ █  █ █  █ "
           "█  █ ███  ██      █ ███  ███  █    █    "
           "████ █  █ █ █     █ █    █  █ █ ██ █    "
           "█  █ █  █ █ █  █  █ █    █  █ █  █ █  █ "
           "█  █ ███  █  █  ██  █    ███   ███  ██  " |]

[<Fact>]
let ``Day 14 part 1 real data`` () =
    Day14.part1 "assets/input14.txt" |> should equal 3555

[<Fact>]
let ``Day 15 part 1 real data`` () =
    let input = System.IO.File.ReadAllText "assets/input15.txt"

    Day15.part2 input |> should equal 540

[<Fact>]
let ``Day 15 part 2 test data`` () =
    let input = System.IO.File.ReadAllText "assets/test15.txt"

    Day15.part2 input |> should equal 315

[<Fact>]
let ``Day 25 part 1 real data`` () =
    Day25.part1 "assets/input25.txt" |> should equal 360
