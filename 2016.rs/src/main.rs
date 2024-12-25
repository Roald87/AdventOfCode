use std::{f64::consts::{E, PI}, fs::read_to_string};
use num::complex::{Complex, ComplexFloat};

fn main() {
    let fname = "day01.txt";
    println!("{:?}", day01(read_line(fname)));
}

fn read_line(fname: &str) -> Vec<String>{
    read_to_string(fname)
        .unwrap()
        .split(", ")
        .map(str::trim)
        .map(String::from)
        .collect()
}

fn step(direction : Complex<f64>, step: String) -> (Complex<f64>, Complex<f64>) {
    let (new_dir, step_size) = 
        match step.split_at(1) {
            | ("R", n) => (direction * E.powc(Complex::new(0.0, -0.5 * PI)), Complex::new(n.parse().unwrap(), 0.0)),
            | ("L", n) => (direction * E.powc(Complex::new(0.0, 0.5 * PI)), Complex::new(n.parse().unwrap(), 0.0)),
            | _ => panic!("Unexpected input {}", step),
        };
    (new_dir, new_dir * step_size)
}

fn day01(inp : Vec<String>) -> i32 {
    let mut dir = Complex::new(0.0, 1.0);
    let mut pos = Complex::new(0.0, 0.0);
    let mut one_step = Complex::new(0.0, 0.0);
    for instruction in inp {
        (dir, one_step) = step(dir, instruction);
        pos += one_step;
    }
    (pos.re.abs() + pos.im.abs()).round() as i32
}