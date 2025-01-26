use std::{collections::HashSet, fs::read_to_string};

fn main() {
    let input = read_lines_day01("day01.txt");
    println!("Part 1a: {:?}", day01a(&input));
    println!("Part 1b: {:?}", day01b(&input));
}

fn read_lines_day01(fname: &str) -> Vec<i32> {
    read_to_string(fname)
        .unwrap()
        .lines()
        .map(|line| line.parse::<i32>().unwrap())
        .collect()
}

fn day01a(nums: &[i32]) -> i32 {
    nums.iter().sum()
}

fn day01b(nums: &[i32]) -> i32 {
    let mut seen: HashSet<i32> = HashSet::new();
    let mut total = 0;
    for n in nums.iter().cycle() {
        total += n;
        if !seen.insert(total) {
            break;
        }
    }
    total
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_day01_test_data() {
        assert_eq!(day01a(&[1, 1, -2, -1]), -1);
        assert_eq!(day01b(&[3, 3, 4, -2, -4]), 10);
    }

    #[test]
    fn test_day01_real_data() {
        let input = read_lines_day01("day01.txt");
        assert_eq!(day01a(&input), 543);
        assert_eq!(day01b(&input), 621);
    }
}
