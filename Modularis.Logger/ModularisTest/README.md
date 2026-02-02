# Modularis Technical Test - Messaging Application

This project is a refactored version of a legacy logging system, redesigned to be scalable, maintainable, and compliant with modern software architecture standards using C# and .NET.

## Key Improvements and Architecture

The original code was refactored from a procedural, tightly-coupled script into a robust system by applying SOLID principles and Design Patterns:

* **Singleton Pattern**: Implemented in the JobLogger class to ensure a single, thread-safe access point to the logging system throughout the application lifecycle.
* **Strategy Pattern**: Decoupled the logging destinations (Console, File, Database) into interchangeable strategies. This allows adding new destinations without modifying the core logic.
* **Dependency Injection (DI)**: The system supports injecting specific strategies at runtime. This allows for better modularity and makes the code 100% testable.
* **Open/Closed Principle (OCP)**: The system is open for extension (adding new strategies) but closed for modification (core JobLogger logic remains untouched).

## Project Structure

* **JobLogger.cs**: The core orchestrator using the Singleton pattern. It manages global configuration and delegates the logging task to registered strategies.
* **Interfaces/ILogStrategy.cs**: The contract that defines the standard behavior for any logging destination.
* **Strategies/**:
    * **ConsoleLogStrategy**: Handles colored output based on message severity.
    * **FileLogStrategy**: Manages file persistence with auto-generated names by date and directory creation using relative paths.
    * **DatabaseLogStrategy**: Simulates database integration (Mocked for demonstration purposes).
* **Enums/MessageType.cs**: Strongly typed definitions for Message, Warning, and Error levels.

## Execution Guide

Follow these steps to set up and run the project on any development environment:

### 1. Prerequisites
* Visual Studio 2019 or later.
* .NET Framework 4.6.1+ 
* NuGet package: System.Configuration.ConfigurationManager

### 2. Initial Setup
1. Clone or extract the project files into a local directory.
2. Open the solution file (.sln) using Visual Studio.
3. **Library Update**: The versions of MSTest.TestAdapter and MSTest.TestFramework were updated to the latest stable and recent version to ensure compatibility with modern test runners.
4. Build the solution

### 3. Running Unit Tests
The project includes a comprehensive suite of tests to verify the logging logic:
1. Open the Test Explorer (Menu: Test > Test Explorer).
2. Click on "Run All" to execute the test suite.
3. Verification:
   * **Console and DB Output**: Select a finished test and click the "Output" link in the summary pane to view simulated database inserts and console writes.
   * **File Output**: Navigate to the test project output folder (typically bin\Debug\Logs). A daily log file will be generated automatically.
   
---
