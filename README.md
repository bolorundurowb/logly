# Logly

[![Build, Test & Coverage](https://github.com/bolorundurowb/shortid/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/bolorundurowb/shortid/actions/workflows/build-and-test.yml) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE) ![NuGet Version](https://img.shields.io/nuget/v/logly)

Logly is an ASP.NET Core middleware library for structured HTTP request and response logging. It is inspired by [morgan](https://github.com/expressjs/morgan) for Node.js and gives you control over exactly what gets logged, with minimal setup.

## Example Output

![logly screenshot](https://res.cloudinary.com/dg2dgzbt4/image/upload/v1530508697/logly_bittgm.png)

## Installation

Install via the NuGet package manager:

```
dotnet add package Logly
```

Or via the Package Manager Console:

```
Install-Package Logly
```

## Supported Frameworks

- .NET Core 3.1
- .NET 5, 6, 7, 8, 9, 10

## Prerequisites

To avoid duplicate log output, clear the default ASP.NET Core logging providers when building your host:

```csharp
public static IWebHost BuildWebHost(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
        .ConfigureLogging((context, logging) =>
        {
            logging.ClearProviders();
        })
        .UseStartup<Startup>()
        .Build();
```

## Setup

### Simple Setup

Add a single line to your middleware pipeline in `Program.cs` or `Startup.cs`:

```csharp
app.UseLogly();
```

This logs the following by default:

- HTTP method (GET, POST, etc.)
- Request URL
- Response status code

### Custom Setup

Use `LoggerOptionsBuilder` to choose exactly what gets logged:

```csharp
app.UseLogly(opts => opts
    .AddRequestMethod()   // HTTP method (GET, POST, etc.)
    .AddUrl()             // Request URL
    .AddStatusCode()      // Response status code
    .AddResponseTime()    // Time taken to process the request (ms)
    .AddResponseLength()  // Response body size (bytes)
);
```

### Ignoring URLs

Use `IgnoreUrls` to exclude specific paths from logging. This is useful for health checks, metrics endpoints, or any route you do not want to appear in logs.

```csharp
app.UseLogly(opts => opts
    .AddRequestMethod()
    .AddStatusCode()
    .IgnoreUrls("/health", "/metrics", "/swagger/*")
);
```

Glob patterns are supported, so `/swagger/*` will match all routes under `/swagger/`.

## Credits

This library was originally based on work by [Casey MacPherson](https://www.codedad.net/2017/08/26/asp-net-core-2-response-logging-2/).

