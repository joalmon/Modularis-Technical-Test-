# Languages Application - Support Extension (Spanish)

This project extends the "Languages Application" by adding support for the Spanish language. The primary challenge was to integrate this new functionality without modifying any existing projects, classes, interfaces, or unit tests, ensuring the integrity of the original core system.

## Architectural Approach: Plugin-Based Architecture

The solution leverages a **Plugin-Based Architecture** discovered through the analysis of the `MessageContainer` component. The system is designed to use **Reflection** to dynamically load assemblies (`.dll` files) at runtime. 

The `MessageContainer` scans the application's execution directory for any class that implements the `IMessage` interface. By identifying this pattern, the Spanish support was implemented as a completely decoupled module.

### Implementation Details:
1. **Isolated Module:** A new C# Class Library project was created specifically for the Spanish language.
2. **Contract Fulfillment:** The new module implements the `IMessage` interface, providing the required localized strings (e.g., "Hola") while maintaining consistency with the English and French implementations.
3. **Automated Deployment:** To ensure a seamless integration, a **Post-build event** was configured in the new project:
     xcopy /Y /I "$(TargetDir)$(TargetName).dll" "$(SolutionDir)bin\Debug"
   
### Execution Instructions

1. Open the solution in Visual Studio.
2. Perform a Build Solution (Build > Build Solution). This triggers the post-build event that automatically moves the Spanish library to the execution folder.
3. Run the main project. The "Spanish" option will automatically appear in the interface's language selection, demonstrating successful dynamic component scanning.