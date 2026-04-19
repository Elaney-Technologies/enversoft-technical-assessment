# EnverSoft Exercise 2

## Overview
This solution reads a CSV file of people records and produces two output text files:

1. `name_frequencies.txt`  
   A combined frequency count of first names and last names, sorted by frequency in descending order and then alphabetically in ascending order.

2. `sorted_addresses.txt`  
   A list of addresses sorted alphabetically by street name, ignoring the house number at the beginning of the address.

## Project Structure
I split the solution into a small console application and a separate test project.

- `EnverSoft.Exercise2` – main application
- `EnverSoft.Exercise2.Tests` – unit tests

Within the main project, I separated the logic into focused services so that each part of the problem is easier to read, test, and maintain.

## Main Components
- `CsvReaderService`  
  Reads the CSV file and maps each row into a `PersonRecord`.

- `FrequencyService`  
  Combines first names and last names, groups them, counts them, and applies the required sorting.

- `AddressService`  
  Extracts the street-name portion of each address and sorts the full address list using that value.

## Approach
My goal was to keep the solution simple, but still structure it in a clean and testable way.

The application flow is:

1. Read the CSV input file.
2. Parse each row into a strongly typed model.
3. Generate the name frequency output.
4. Generate the sorted address output.
5. Write both results to text files.


## Error Handling
I added basic validation to make the solution safer to run and easier to debug.

The application checks for:
- missing or invalid file paths
- missing input files
- malformed CSV rows with an incorrect number of columns
- invalid or null input passed into processing methods

The console output also shows the full output file paths after processing so the generated files are easy to find.

## Testing
I used xUnit for the test project.

The tests cover:
- reading a valid CSV file
- handling invalid CSV rows
- generating the correct name frequencies
- sorting addresses by street name
- a few edge cases such as empty input or null values

## How to Run
From the root folder, run:

```bash
dotnet build
dotnet run --project EnverSoft.Exercise2
````

## How to Test

From the root folder, run:

```bash
dotnet test
```

## Output Files

Running the application creates:

* `name_frequencies.txt`
* `sorted_addresses.txt`

These are written to the current working directory unless the output paths are changed in code.

## Notes

I kept the implementation intentionally lightweight because the problem is small, but I still separated the logic into services so the solution remains easy to test and extend.
