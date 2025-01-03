use std::fs::read_to_string;

fn main() {
    let input = read_lines_day01("day01.txt");
    println!("Part 1a: {:?}", day01a(input.trim()));
}

fn read_lines_day01(fname: &str) -> String {
    read_to_string(fname).unwrap()
}

fn day01a(input: &str) -> i32 {
    input.lines().map(|s| s.parse::<i32>().unwrap()).sum()
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_day01_test_data() {
        assert_eq!(day01a("+1\n+1\n-2\n-1"), -1);
    }

    #[test]
    fn test_day01_real_data() {
        let input = read_lines_day01("day01.txt");
        assert_eq!(day01a(input.trim()), 0);
    }
}
