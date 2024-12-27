use std::{collections::HashSet, fs::read_to_string};
use num::complex::Complex;

fn main() {
    let fname = "day01.txt";
    println!("part a: {:?}", day01a(read_lines(fname))); // part a: 243
    println!("part b: {:?}", day01b(read_lines(fname))); // part b: 142
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

fn day01a(instructions: Vec<String>) -> i32 {
    let mut dir = Complex::new(0, 1);
    let mut pos = Complex::new(0, 0);
    for instruction in instructions {
        let (new_dir, step) = step(dir, &instruction);
        dir = new_dir;
        pos += step;
    }
    pos.re.abs() + pos.im.abs()
}

fn day01b(instructions: Vec<String>) -> i32 {
    let mut dir = Complex::new(0, 1);
    let mut pos = Complex::new(0, 0);
    let mut visited = HashSet::new();
    visited.insert((pos.re, pos.im));
    for instruction in instructions {
        let (new_dir, step) = step(dir, &instruction);
        dir = new_dir;
        for _ in 0..(step.re.abs() + step.im.abs()) {
            pos += dir;
            if !visited.insert((pos.re, pos.im)) {
                return pos.re.abs() + pos.im.abs();
            }
        }
    }
    pos.re.abs() + pos.im.abs()
}
