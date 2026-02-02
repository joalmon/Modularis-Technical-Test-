# Employee Management System - Modularis Tech Test

This project is a Single Page Application (SPA) developed with TypeScript for employee management, integrated with Modularis REST services. The architecture focuses on maintainability, scalability, and configuration decoupling.

## Technical Stack

* Language: TypeScript 5.x
* Architecture Pattern: MVVM (Model-View-ViewModel)
* Styling: Custom CSS3 using Native Variables 
* Data Management: Fetch API with asynchronous flow handling

## Technical Features

1. Dynamic Configuration: Implementation of a ConfigService that loads environment parameters from an external appsettings.json file.
2. Decoupled Architecture: Data services use lazy evaluation to resolve configuration at runtime, ensuring the app settings are loaded before any API call.
3. Modular Interface: Design based on reusable CSS components and system variables.
4. Asset Optimization: Edit and Delete icons in the "Actions" column were implemented using inline SVGs. This reduces the application footprint by eliminating the need for external icon libraries.
5. Data Integrity: Use of interfaces and models for mapping API responses and ensuring consistent data structures.

## Assumptions & Design Decisions

During development, the following technical assumptions and decisions were made:

* Data Integrity Requirements: It was identified that the REST API requires EmployeeNo and EmploymentStartDate as mandatory fields. These were integrated into the "Add Employee" form to ensure successful record creation.
* Input Validation (SSN): Based on API error responses, specific formatting for the SSN is required; dedicated placeholders and front-end validations were implemented accordingly.
* Environment Portability: Configuration is externalized to allow seamless transitions between different API environments without code changes.
* Browser Support: The application targets modern browsers with native support for ES Modules and CSS Variables.

## Installation and Deployment

### 1. Prerequisites
* Node.js (Runtime environment).

### 2. Dependency Installation
npm install

### 3. Project Compilation
To compile the TypeScript source code into JavaScript:
npx tsc

*Note: This will generate the distribution files in the project structure.*

### 4. Running the Application
To serve the project locally, run:
npx serve .

### 5. Credential Configuration
Configure the appsettings.json file in the root directory:
{
  "api": {
    "baseUrl": "API_URL",
    "customerId": "YOUR_CUSTOMER_ID",
    "apiKey": "YOUR_API_KEY"
  }
}

**Quick Start Credentials:**

For evaluation purposes, you may use the following credentials:
**Base URL:** "https://gateway.modularis.com/HRDemo/RESTActivityWebService/HRDemo.Example/Employees/"
**Customer ID:** "C93F949C-41B8-4C9E-95AA-B030B31F6F3F"
**API Key:** "ISJ5YCtIddm69OtTLsYqL0SDtmTWoXxZnOKSqe"

## Troubleshooting

### Potential Network Issues (CORS/Firewall)
If you encounter network errors:
1. Ensure that local security settings (Firewall/Antivirus) are not blocking outgoing requests.
2. If CORS issues persist, a browser extension (like CORS Unblock) is recommended for local evaluation.

### Data Persistence Observation
The /Employees endpoint requires at least one existing record to return a 200 OK status. If a 404 error occurs on initialization, perform an initial POST request (Create Employee) to enable resource resolution.

## Directory Structure

* /src: TypeScript source files (Models, Services, ViewModels).
* /css: Custom stylesheet and UI definitions.
* app.ts: Application bootstrap and entry point.
* appsettings.json: Global environment configuration.
* index.html: Main application entry point.
* (Generated) /dist or root .js files: Compiled JavaScript output.

---
