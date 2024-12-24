#!/bin/bash

# Check if exactly one argument is provided
if [ "$#" -ne 1 ]; then
  echo "Usage: $0 <day_number>"
  exit 1
fi

# Extract the day number from the argument
day_number=$1

# Create the files
touch "day${day_number}.fsx" "day${day_number}.txt" "day${day_number}-ex.txt"

echo "Files day${day_number}.fsx, day${day_number}.txt, and day${day_number}-ex.txt created."
