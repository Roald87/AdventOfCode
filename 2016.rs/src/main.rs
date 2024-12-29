use num::complex::Complex;
use num::integer::Roots;
use std::{collections::HashSet, fs::read_to_string};

fn main() {
    let instructions = read_lines("day01.txt");
    println!("part 1a: {:?}", day01(&instructions, false));
    println!("part 1b: {:?}", day01(&instructions, true));

    let instructions = read_lines_day02("day02.txt");
    println!("part 2a: {:?}", day02(&instructions, &KEYPAD_A, (1, 1)));
    println!("part 2b: {:?}", day02(&instructions, &KEYPAD_B, (2, 0)));
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

fn read_lines_day02(fname: &str) -> Vec<String> {
    read_to_string(fname)
        .unwrap()
        .trim()
        .split_ascii_whitespace()
        .map(String::from)
        .collect()
}

#[rustfmt::skip]
const KEYPAD_A: [&str; 9] = [
    "1", "2", "3",
    "4", "5", "6",
    "7", "8", "9",
];

#[rustfmt::skip]
const KEYPAD_B: [&str; 25] = [
    "0", "0", "1", "0", "0",
    "0", "2", "3", "4", "0",
    "5", "6", "7", "8", "9",
    "0", "A", "B", "C", "0",
    "0", "0", "D", "0", "0",
];

fn move_keypad<const N: usize>(
    curr_pos: (usize, usize),
    direction: char,
    keypad: &[&str; N],
) -> (usize, usize) {
    let size = N.sqrt();
    let new_pos = match direction {
        'D' => ((curr_pos.0 + 1).clamp(0, size - 1), curr_pos.1),
        'U' => (curr_pos.0.saturating_sub(1), curr_pos.1),
        'L' => (curr_pos.0, curr_pos.1.saturating_sub(1)),
        'R' => (curr_pos.0, (curr_pos.1 + 1).clamp(0, size - 1)),
        _ => panic!("Invalid direction {}", direction),
    };

    let key = keypad.get(new_pos.0 * size + new_pos.1).copied();
    if key == Some("0") || key == None {
        curr_pos
    } else {
        new_pos
    }
}

fn day02<const N: usize>(
    instructions: &[String],
    keypad: &[&str; N],
    start: (usize, usize),
) -> String {
    let mut curr_pos = start;
    let mut code = String::new();
    for instruction in instructions {
        for direction in instruction.chars() {
            curr_pos = move_keypad(curr_pos, direction, keypad);
        }
        code.push_str(
            keypad
                .get(curr_pos.0 * (N.sqrt()) + curr_pos.1)
                .unwrap_or(&""),
        );
    }

    code
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_day01_real_data() {
        let instructions = read_lines("day01.txt");

        assert_eq!(
            day01(&instructions, false),
            243,
            "Part 1a with real data is not correct."
        );
        assert_eq!(
            day01(&instructions, true),
            142,
            "Part 1b with real data is not correct."
        );
    }

    #[test]
    fn test_day02_test_data() {
        let instructions = read_lines_day02("day02-ex.txt");

        assert_eq!(
            day02(&instructions, &KEYPAD_A, (1, 1)),
            "1985",
            "Part 2a with test data is not correct."
        );
        assert_eq!(
            day02(&instructions, &KEYPAD_B, (2, 0)),
            "5DB3",
            "Part 2b with test data is not correct."
        );
    }

    #[test]
    fn test_day02_real_data() {
        let instructions = read_lines_day02("day02.txt");

        assert_eq!(
            day02(&instructions, &KEYPAD_A, (1, 1)),
            "84452",
            "Part 2a with real data is not correct."
        );
        assert_eq!(
            day02(&instructions, &KEYPAD_B, (2, 0)),
            "D65C3",
            "Part 2b with real data is not correct."
        );
    }
}
