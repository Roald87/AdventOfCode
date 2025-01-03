use std::{collections::HashSet, fs::read_to_string};

fn main() {
    let input = read_lines_day01("day01.txt");
    println!("Part 1a: {:?}", day01a(input.trim()));
    println!("Part 1b: {:?}", day01b(input.trim()));
}

fn read_lines_day01(fname: &str) -> String {
    read_to_string(fname).unwrap()
}

fn day01a(input: &str) -> i32 {
    input.lines().map(|s| s.parse::<i32>().unwrap()).sum()
}

fn day01b(input: &str) -> i32 {
    let nums = input.lines().map(|s| s.parse::<i32>().unwrap());
    let mut seen: HashSet<i32> = HashSet::new();
    let mut total = 0;
    for n in nums.cycle() {
        total += n;
        if !seen.insert(total) {
            return total;
        }
    }
    panic!("No value seen")
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_day01_test_data() {
        assert_eq!(day01a("+1\n+1\n-2\n-1"), -1);
        assert_eq!(day01b("+3\n+3\n+4\n-2\n-4"), 10);
    }

    #[test]
    fn test_day01_real_data() {
        let input = read_lines_day01("day01.txt");
        assert_eq!(day01a(input.trim()), 543);
        assert_eq!(day01b(input.trim()), 621);
    }
}
