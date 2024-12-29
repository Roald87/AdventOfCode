use std::{collections::HashSet, fs::read_to_string};
use num::complex::Complex;

fn main() {
    // let instructions = read_lines("day01.txt");
    // println!("part 1a: {:?}", day01(&instructions, false));
    // println!("part 1b: {:?}", day01(&instructions, true));
    
    let instructions = read_lines_day02("day02.txt");
    println!("part 2a: {:?}", day02(&instructions));
    println!("part 2b: {:?}", day02b(&instructions));
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

fn read_lines_day02(fname: &str) -> Vec<String>{
    read_to_string(fname)
        .unwrap()
        .trim()
        .split_ascii_whitespace()
        .map(String::from)
        .collect()
}

fn move_keypad_a(curr_pos: (i8, i8), direction: char) -> (i8, i8){
    let new_pos = match direction {
        'U' => (curr_pos.0, curr_pos.1 + 1), 
        'D' => (curr_pos.0, curr_pos.1 - 1),
        'L' => (curr_pos.0 - 1, curr_pos.1), 
        'R' => (curr_pos.0 + 1, curr_pos.1), 
        _ => panic!("Invalid direction {}", direction),
    };
    (new_pos.0.clamp(-1, 1), new_pos.1.clamp(-1, 1))
}

fn keypad_a(pos: (i8, i8)) -> u8 {
    let n = match pos {
        (-1, 1) => 1,
        (0, 1) => 2,
        (1, 1) => 3,
        (-1, 0) => 4,
        (0, 0) => 5,
        (1, 0) => 6,
        (-1, -1) => 7,
        (0, -1) => 8,
        (1,-1) => 9,
        _ => panic!("Invalid position {:?}", pos)
    };
    n
}

fn day02(instructions: &[String]) -> String{
    let mut curr_pos = (0, 0);
    let mut code = String::new();
    for instruction in instructions{
        for direction in instruction.chars(){
            curr_pos = move_keypad_a(curr_pos, direction);
        }
        code.push_str(&keypad_a(curr_pos).to_string());
    }

    code
}

fn move_keypad_b(curr_pos: (i8, i8), direction: char) -> (i8, i8){
    let new_pos = match direction {
        'U' => (curr_pos.0, curr_pos.1 + 1), 
        'D' => (curr_pos.0, curr_pos.1 - 1),
        'L' => (curr_pos.0 - 1, curr_pos.1), 
        'R' => (curr_pos.0 + 1, curr_pos.1), 
        _ => panic!("Invalid direction {}", direction),
    };
    if keypad_b(new_pos) == "0" {
        curr_pos
    }
    else {
        new_pos
    }
}

fn keypad_b(pos: (i8, i8)) -> String {
    let n = match pos {
        (0, 2) => "1",
        (-1, 1) => "2",
        (0, 1) => "3",
        (1, 1) => "4",
        (-2, 0) => "5",
        (-1, 0) => "6",
        (0, 0) => "7",
        (1, 0) => "8",
        (2, 0) => "9",
        (-1, -1) => "A",
        (0,-1) => "B",
        (1,-1) => "C",
        (0,-2) => "D",
        _ => "0",
    };
    n.to_string()
}

fn day02b(instructions: &[String]) -> String{
    let mut curr_pos = (-2, 0);
    let mut code = String::new();
    for instruction in instructions{
        for direction in instruction.chars(){
            curr_pos = move_keypad_b(curr_pos, direction);
        }
        code.push_str(&keypad_b(curr_pos).to_string());
    }
    code
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_day01_real_data() {
        let instructions = read_lines("day01.txt");

        assert_eq!(day01(&instructions, false), 243, "Part 1a with real data is not correct.");
        assert_eq!(day01(&instructions, true), 142, "Part 1b with real data is not correct.");
    }

    #[test]
    fn test_day02_test_data() {
        let instructions = read_lines_day02("day02-ex.txt");

        assert_eq!(day02(&instructions), "1985", "Part 1a with test data is not correct.");
        assert_eq!(day02b(&instructions), "5DB3", "Part 1b with test data is not correct.");
    }

    #[test]
    fn test_day02_real_data() {
        let instructions = read_lines_day02("day02.txt");

        assert_eq!(day02(&instructions), "84452", "Part 2a with real data is not correct.");
        assert_eq!(day02b(&instructions), "D65C3", "Part 2b with real data is not correct.");
    }
}
