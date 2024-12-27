use std::{collections::HashSet, fs::read_to_string};
use num::complex::Complex;

fn main() {
    let instructions = read_lines("day01.txt");
    println!("part a: {:?}", day01(&instructions, false));
    println!("part b: {:?}", day01(&instructions, true));
}

fn read_lines(fname: &str) -> Vec<String> {
    read_to_string(fname)
        .unwrap()
        .trim()
        .split(", ")
        .map(String::from)
        .collect()
}

fn parse_step(instruction: &str) -> (Complex<i32>, i32) {
    let (turn, n) = instruction.split_at(1);
    let direction = match turn {
        "R" => Complex::new(0, 1),
        "L" => Complex::new(0, -1),
        _ => panic!("Unexpected input {}", instruction),
    };
    (direction, n.parse().unwrap())
}

fn step(direction: Complex<i32>, instruction: &str) -> (Complex<i32>, Complex<i32>) {
    let (turn, n) = parse_step(instruction);
    let new_dir = direction * turn;
    (new_dir, new_dir * n)
}

fn day01(instructions: &[String], first_double_location: bool) -> i32 {
    let mut dir = Complex::new(0, 1);
    let mut pos = Complex::new(0, 0);
    let mut visited = HashSet::new();
    visited.insert((pos.re, pos.im));
    for instruction in instructions {
        let (new_dir, step) = step(dir, &instruction);
        dir = new_dir;
        for _ in 0..(step.re.abs() + step.im.abs()) {
            pos += dir;
            if first_double_location & !visited.insert((pos.re, pos.im)) {
                return pos.re.abs() + pos.im.abs();
            }
        }
    }
    pos.re.abs() + pos.im.abs()
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_day01_real_data() {
        let instructions = read_lines("day01.txt");

        assert_eq!(day01(&instructions, false), 243, "Part a is not correct.");
        assert_eq!(day01(&instructions, true), 142, "Part b is not correct.");
    }
}
