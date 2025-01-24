use num::complex::Complex;
use num::integer::Roots;
use regex::Regex;
use std::{
    cmp::Reverse,
    collections::{HashMap, HashSet},
    fs::read_to_string,
};

fn main() {
    let instructions = read_lines("day01.txt");
    println!("part 1a: {:?}", day01(&instructions, false));
    println!("part 1b: {:?}", day01(&instructions, true));

    let instructions = read_lines_day02("day02.txt");
    println!("part 2a: {:?}", day02(&instructions, &KEYPAD_A, (1, 1)));
    println!("part 2b: {:?}", day02(&instructions, &KEYPAD_B, (2, 0)));

    let side_lengths = read_lines_day03("day03.txt");
    println!("part 3a: {:?}", day03(&side_lengths));
    println!("part 3b: {:?}", day03b(&side_lengths));

    let codes = read_lines_day04("day04.txt");
    println!("part 4a: {:?}", day04a(&codes));
    println!("part 4b: {:?}", day04b(&codes));
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
        _ => panic!("Unexpected input {instruction}"),
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
        let (new_dir, step) = step(dir, instruction);
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

fn move_keypad(curr_pos: (usize, usize), direction: char, keypad: &[&str]) -> (usize, usize) {
    let size = keypad.len().sqrt();
    let new_pos = match direction {
        'D' => ((curr_pos.0 + 1).clamp(0, size - 1), curr_pos.1),
        'U' => (curr_pos.0.saturating_sub(1), curr_pos.1),
        'L' => (curr_pos.0, curr_pos.1.saturating_sub(1)),
        'R' => (curr_pos.0, (curr_pos.1 + 1).clamp(0, size - 1)),
        _ => panic!("Invalid direction {direction}"),
    };

    let key = keypad.get(new_pos.0 * size + new_pos.1).copied();
    if key == Some("0") || key.is_none() {
        curr_pos
    } else {
        new_pos
    }
}

fn day02(instructions: &[String], keypad: &[&str], start: (usize, usize)) -> String {
    let mut curr_pos = start;
    let size = keypad.len().sqrt();
    let mut code = String::new();
    for instruction in instructions {
        for direction in instruction.chars() {
            curr_pos = move_keypad(curr_pos, direction, keypad);
        }
        code.push_str(keypad[curr_pos.0 * size + curr_pos.1]);
    }
    code
}

fn read_lines_day03(fname: &str) -> Vec<Vec<i32>> {
    read_to_string(fname)
        .unwrap()
        .lines()
        .map(|x| x.split_whitespace().map(|n| n.parse::<i32>().unwrap()))
        .map(std::iter::Iterator::collect)
        .collect()
}

fn day03(side_lengths: &[Vec<i32>]) -> usize {
    side_lengths
        .iter()
        .map(|sides| {
            let mut sorted_sides = sides.clone();
            sorted_sides.sort_unstable();
            sorted_sides
        })
        .filter(|sides| sides[0] + sides[1] > sides[2])
        .count()
}

// https://stackoverflow.com/a/64499219/6329629
fn transpose<T>(v: Vec<Vec<T>>) -> Vec<Vec<T>> {
    assert!(!v.is_empty());
    let len = v[0].len();
    let mut iters: Vec<_> = v
        .into_iter()
        .map(std::iter::IntoIterator::into_iter)
        .collect();
    (0..len)
        .map(|_| {
            iters
                .iter_mut()
                .map(|n| n.next().unwrap())
                .collect::<Vec<T>>()
        })
        .collect()
}

fn day03b(side_lengths: &[Vec<i32>]) -> usize {
    let sides_tranposed = transpose(side_lengths.to_vec());
    sides_tranposed
        .into_iter()
        .flat_map(|row| {
            row.chunks(3)
                .map(|sides| {
                    let mut sorted_sides = sides.to_vec();
                    sorted_sides.sort_unstable();
                    sorted_sides[0] + sorted_sides[1] > sorted_sides[2]
                })
                .collect::<Vec<bool>>()
        })
        .filter(|&sides| sides)
        .count()
}

fn is_room_real(name: &str, checksum: &str) -> bool {
    let counts = name
        .chars()
        .filter(|x| *x != '-')
        .fold(HashMap::new(), |mut acc, c| {
            acc.entry(c).and_modify(|count| *count += 1).or_insert(1);
            acc
        });

    let mut chars: Vec<(&char, &i32)> = counts.iter().collect();
    chars.sort_by_key(|(k, v)| (Reverse(**v), **k));

    let computed_checksum: String = chars.iter().take(5).map(|(k, _v)| *k).collect();

    checksum == computed_checksum
}

fn read_lines_day04(fname: &str) -> Vec<(String, i32, String)> {
    let pattern = Regex::new(r"([a-z-]+)-(\d+)\[([a-z]+)\]").unwrap();
    let input = read_to_string(fname).unwrap();
    pattern
        .captures_iter(&input)
        .map(|c| {
            let (_, [code, i, h]) = c.extract();
            (code.to_string(), i.parse::<i32>().unwrap(), h.to_string())
        })
        .collect()
}

fn day04a(codes: &Vec<(String, i32, String)>) -> i32 {
    codes
        .iter()
        .filter_map(|(name, id, checksum)| is_room_real(name, checksum).then_some(*id))
        .sum()
}

fn decrypt_room(room: String, id: i32) -> String {
    let a = b'a';
    let i = (id % 26) as u8;
    room.chars()
        .map(|c| {
            if c == '-' {
                ' '
            } else {
                ((c as u8 - a + i) % 26 + a) as char
            }
        })
        .collect()
}

fn day04b(codes: &Vec<(String, i32, String)>) -> i32 {
    for (code, id, _) in codes {
        if decrypt_room(code.to_string(), *id).starts_with("north") {
            return *id;
        }
    }
    0
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

    #[test]
    fn test_day03_test_data() {
        assert_eq!(day03(&[vec![5, 10, 25]]), 0, "test 1 incorrect");
        assert_eq!(day03(&[vec![5, 15, 25]]), 0, "test 2 incorrect");
        assert_eq!(day03(&[vec![10, 16, 25]]), 1, "test 3 incorrect");
        assert_eq!(day03(&[vec![25, 15, 10]]), 0, "test 4 incorrect");
    }

    #[test]
    fn test_day03_real_data() {
        let side_lengths = read_lines_day03("day03.txt");

        assert_eq!(
            day03(&side_lengths),
            982,
            "Part 3a with real data not correct"
        );
        assert_eq!(
            day03b(&side_lengths),
            1826,
            "Part 3b with real data not correct"
        );
    }

    #[test]
    fn room_is_real() {
        assert!(is_room_real("aaaaa-bbb-z-y-x", "abxyz"));
    }

    #[test]
    fn test_day04_real_data() {
        let codes = read_lines_day04("day04.txt");
        assert_eq!(day04a(&codes), 409147, "Part 4a with real data not correct");
        assert_eq!(day04b(&codes), 991, "Part 4b with real data not correct");
    }

    #[test]
    fn test_day04_test_data() {
        assert_eq!(
            decrypt_room("qzmt-zixmtkozy-ivhz".to_string(), 343),
            "very encrypted name"
        );
    }
}
