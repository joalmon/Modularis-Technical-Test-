# Modularis Technical Test - Formula Validation

This project provides a professional-grade solution for validating the structural integrity of mathematical or logical formulas, focusing on the correct nesting and balancing of delimiters such as `()`, `[]`, and `{}`.

## Architecture and Design Decisions

The solution has been engineered with scalability and clean code principles in mind:

* **Strategy-Ready Design**: By implementing the `IFormulaValidator` interface, the logic is decoupled from its consumption, allowing for future extensions or different validation rules without affecting existing code.
* **Optimized Algorithm**: The core logic utilizes a **Stack** data structure, ensuring an optimal time complexity of **O(n)** and space complexity of **O(n)**.
* **Efficient Lookups**: The validator uses `Dictionary` and `HashSet` structures to achieve **O(1)** lookup times for matching symbols, which is significantly more efficient than nested loops or multiple conditional checks.

## Assumptions

* **Empty Formulas**: The system assumes that an empty string or a null input is structurally valid (well-formed), as there are no mismatched or unclosed delimiters.
* **Non-Delimiter Characters**: Any characters within the string that are not defined as delimiters (e.g., alphanumeric characters, mathematical operators) are ignored during the structural validation process.

## Execution Guide

### 1. Prerequisites
* Visual Studio 2019 or later.
* .NET Framework 4.6.1
* XUnit for unit testing. Make sure you have these dependencies installed: Microsoft.NET.Test.Sdk, xunit, xunit.runner.visualstudio

### 2. Initial Setup
1. Clone or extract the project files.
2. Open the solution file (.sln) in Visual Studio.
3. Install XUnit 

### 3. Running Unit Tests
1. Open the **Test Explorer** (Menu: Test > Test Explorer).
2. Click **Run All**.
3. The test suite automatically processes the following files provided in the requirements:
   * `Well formed formulas.txt`: Verified as structurally correct.
   * `Bad formed formulas.txt`: Verified as structurally incorrect.
   
### Additional Notes
The sample files `Well formed formulas.txt` and `Bad formed formulas.txt` have been configured in the project properties with **Copy to Output Directory: Copy Always**.
   * **Why?**: This ensures that when the project is compiled, the text files are automatically moved to the execution folder (`/bin`). This allows the unit tests to access them using relative paths via `Directory.GetCurrentDirectory()`, making the solution portable and "ready-to-run" on any machine without manual file handling.
