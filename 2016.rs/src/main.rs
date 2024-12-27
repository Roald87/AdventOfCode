use std::{collections::HashSet, f64::consts::{E, PI}, fs::read_to_string};
use num::complex::{Complex, ComplexFloat};

fn main() {
    let fname = "day01.txt";
    println!("part a:{:?}", day01a(read_line(fname))); // part a: 243
    println!("part b:{:?}", day01b(read_line(fname))); // part b: 142
}

fn read_line(fname: &str) -> Vec<String>{
    read_to_string(fname)
        .unwrap()
        .split(", ")
        .map(str::trim)
        .map(String::from)
        .collect()
}

fn parse_step(instruction: &String) -> (Complex<f64>, i32) {
    match instruction.split_at(1) {
        ("R", n) => (E.powc(Complex::new(0.0, -0.5 * PI)), n.parse().unwrap()),
        ("L", n) => (E.powc(Complex::new(0.0, 0.5 * PI)), n.parse().unwrap()),
        _ => panic!("Unexpected input {}", instruction),
    }
}

fn step(direction : Complex<f64>, instruction: String) -> (Complex<f64>, Complex<i32>) {
    let (turn, n) = parse_step(&instruction);
    let new_dir = direction * turn;
    let new_int_dir  = Complex::new(new_dir.re.round() as i32, new_dir.im.round() as i32);
    let step_size = Complex::new(n, 0);
    (new_dir, new_int_dir * step_size)
}

fn day01a(instructions : Vec<String>) -> i32 {
    let mut dir = Complex::new(0.0, 1.0);
    let mut pos = Complex::new(0, 0);
    let mut one_step = Complex::new(0, 0);
    for instruction in instructions {
        (dir, one_step) = step(dir, instruction);
        pos += one_step;
    }
    pos.re.abs() + pos.im.abs()
}

fn day01b(instructions : Vec<String>) -> i32 {
    let mut dir = Complex::new(0.0, 1.0);
    let mut pos = Complex::new(0, 0);
    let mut one_step = Complex::new(0, 0);
    let mut visited = HashSet::new();
    visited.insert((pos.re, pos.im));
    for instruction in instructions {
        (dir, one_step) = step(dir, instruction);
        let from = pos;
        pos += one_step;
        let to = pos;
        let mut xs = [from.re, to.re+1];
        xs.sort();
        let mut ys = [from.im, to.im+1];
        ys.sort();
        for x in xs[0] .. xs[1] {
            for y in ys[0] .. ys[1] {
                let previous_end_position = (x, y) == (from.re, from.im);
                if !previous_end_position && visited.contains(&(x, y)) {
                    return x.abs() + y.abs();
                }
                visited.insert((x, y));
            }
        }
    }
    pos.re.abs() + pos.im.abs()
}
