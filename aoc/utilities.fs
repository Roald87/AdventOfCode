namespace aoc

module Utilities =

    let split (sep: string) (str: string) = str.Split(sep) |> Array.toList
