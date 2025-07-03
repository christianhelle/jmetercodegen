# JMeter Test Plan Generator (PREVIEW)

Generate JMeter Test Plans automatically from your ASP.NET Core Web API projects in Visual Studio.

## Overview

JMeter Test Plan Generator is a Visual Studio extension that streamlines the process of creating [Apache JMeter](https://jmeter.apache.org/) test plans for ASP.NET Core Web APIs. It leverages your project's Swagger/OpenAPI specification to generate ready-to-use JMeter test plans, making performance and load testing simple and efficient.

## Features

- **One-click JMeter Test Plan Generation**: Generate JMeter test plans directly from your ASP.NET Core Web API projects.
- **Visual Studio Integration**: Accessible from the Tools menu for a seamless workflow.
- **Automatic API Launch**: Launches your API project, retrieves the Swagger/OpenAPI spec, and generates the test plan automatically.
- **Output Location**: Generated test plans are placed in a `JMeter` folder within your project directory.
- **Supports .NET Core and .NET Standard**: Works with modern .NET projects.

## How It Works

1. **Select your ASP.NET Core Web API project (.csproj) in Solution Explorer.**
2. **Use the Tools Menu:**
   - Go to `Tools` > `Generate JMeter Test Plan`.
3. The extension will:
   - Launch your API project on a random port.
   - Retrieve the Swagger/OpenAPI specification from `/swagger/v1/swagger.json`.
   - Use the [rapicgen](https://github.com/ChristianHelle/rapicgen) tool to generate a JMeter test plan.
   - Place the generated test plan in a `JMeter` folder in your project.

## Prerequisites

- Visual Studio 2022 (17.0+) on Windows
- .NET SDK installed (required to build and run your API project)
- Your project must expose a Swagger/OpenAPI endpoint (e.g., using Swashbuckle)

## Installation

1. Download and install the extension from the Visual Studio Marketplace or build from source.
2. Restart Visual Studio after installation.

## Usage

1. Open your ASP.NET Core Web API solution in Visual Studio.
2. Right-click the `.csproj` file or select it in Solution Explorer.
3. Go to `Tools` > `Generate JMeter Test Plan`.
4. The generated JMeter test plan will appear in a new `JMeter` folder in your project directory.

## Example Workflow

1. **Select Project:**
   ![](https://raw.githubusercontent.com/christianhelle/jmetercodegen/main/images/tools-menu.png)

2. **JMeter Folder Created:**
   ![](https://raw.githubusercontent.com/christianhelle/jmetercodegen/main/images/jmeter-in-project.png)

3. **Generated JMeter Test Plan:**
   ![](https://raw.githubusercontent.com/christianhelle/jmetercodegen/main/images/jmeter-test-plan.png)

## Technical Details

- The extension automates the following steps:
  - Launches your API project using `dotnet run` on a random port.
  - Polls the Swagger endpoint until the OpenAPI spec is available.
  - Saves the spec to a temporary file.
  - Runs `rapicgen jmeter` to generate the JMeter test plan.
  - Cleans up temporary files after generation.

## Project Structure

- `JMeterCodeGen.Core/` - Core logic for launching projects, retrieving Swagger, and generating JMeter scripts.
- `JMeterCodeGen.Extension/` - Visual Studio extension implementation (Tools menu integration, command handling).
- `JMeterCodeGen.VSIX/` - VSIX packaging for deployment.
- `JMeterCodeGen.Core.Tests/` - Unit tests for core logic.

## Contributing

Contributions, issues, and feature requests are welcome! Please open an issue or submit a pull request.

## License

This project is licensed under the GNU GENERAL PUBLIC LICENSE Version 3. See [LICENSE](../LICENSE) for details.

---

### Notice
This project is in its very early stages and its marketplace visibility is supposed to be marked as PREVIEW.