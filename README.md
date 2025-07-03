# JMeter Test Plan Generator

JMeter Test Plan Generator is a Visual Studio extension that allows you to automatically generate [Apache JMeter](https://jmeter.apache.org/) test plans from ASP.NET Core Web API projects. This tool streamlines the process of creating performance and load tests for your APIs by leveraging your project's Swagger/OpenAPI specification.

## Features

- **Generate JMeter Test Plans** directly from your ASP.NET Core Web API projects.
- Integrates into the Visual Studio Tools menu for easy access.
- Automatically launches your API, retrieves the Swagger/OpenAPI spec, and generates a ready-to-use JMeter test plan.
- Output is placed in a `JMeter` folder within your project directory.
- Supports .NET Core and .NET Standard projects.

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
   ![](/images/tools-menu.png)

2. **JMeter Folder Created:**
   ![](/images/jmeter-in-project.png)

3. **Generated JMeter Test Plan:**
   ![](/images/jmeter-test-plan.png)

## How It Works (Technical Details)

- The extension uses a combination of process automation and HTTP requests to:
  - Launch your API project using `dotnet run` on a random port.
  - Poll the Swagger endpoint until the OpenAPI spec is available.
  - Save the spec to a temporary file.
  - Run `rapicgen jmeter` to generate the JMeter test plan.
  - Clean up temporary files after generation.

## Building from Source

1. Clone this repository.
2. Open `src/All.sln` in Visual Studio 2022.
3. Build the solution.
4. Deploy or debug the extension as needed.

## Project Structure

- `JMeterCodeGen.Core/` - Core logic for launching projects, retrieving Swagger, and generating JMeter scripts.
- `JMeterCodeGen.Extension/` - Visual Studio extension implementation (Tools menu integration, command handling).
- `JMeterCodeGen.VSIX/` - VSIX packaging for deployment.
- `JMeterCodeGen.Core.Tests/` - Unit tests for core logic.

## Contributing

Contributions, issues, and feature requests are welcome! Please open an issue or submit a pull request.

## License

This project is licensed under the MIT License. See [LICENSE](LICENSE) for details.
