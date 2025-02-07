# logly 🚀

[![Build Logly Project](https://github.com/bolorundurowb/logly/actions/workflows/build.yml/badge.svg)](https://github.com/bolorundurowb/logly/actions/workflows/build.yml)  [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE) ![NuGet Version](https://img.shields.io/nuget/v/logly)


This work is based on that done by _Casey MacPherson_ [here](https://www.codedad.net/2017/08/26/asp-net-core-2-response-logging-2/).

**logly** is a powerful library for ASP.NET Core that makes request and response logging more modular and visually appealing. 📝✨

## 📸 Example Output
Here's an example of what logly logging looks like:

![logly screenshot](https://res.cloudinary.com/dg2dgzbt4/image/upload/v1530508697/logly_bittgm.png)

## 🔧 Prerequisites
To prevent default logging by ASP.NET Core, you need to modify the `BuildWebHost` method.

### Before:
```csharp
public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
```

### After:
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

## 🚀 Setup
To enable logging, you have two options:

### ✅ Simple Setup
Just add the following line to the `Configure` method in the `Startup.cs` file:
```csharp
app.UseLogly();
```
This enables basic logging for:
- Request Method 📝
- Response Status Code ✅
- Response Time ⏱️

### ⚙️ Advanced Setup
For more control, use `LoggerOptionsBuilder` to specify logging options:
```csharp
app.UseLogly(opts => opts
                    .AddRequestMethod()  // Logs HTTP method (GET, POST, etc.)
                    .AddStatusCode()     // Logs response status codes (200, 404, etc.)
                    .AddResponseTime()   // Logs response time in milliseconds
                    .AddUrl()            // Logs request URL
                    .AddResponseLength() // Logs response size in bytes
                    .AddIgnoreUrls("/health", "/metrics")); // Ignore specific URLs
```

## 🛠️ New Feature: Ignore Specific URLs
You can now specify a list of URL patterns to ignore using `AddIgnoreUrls`. This is useful for skipping logging on health checks, monitoring endpoints, or other unnecessary logs.
```csharp
app.UseLogly(opts => opts.AddIgnoreUrls("/health", "/metrics", "/swagger/*"));
```
- Supports **glob patterns** for flexibility (e.g., `/swagger/*` ignores all Swagger routes).

---
🚀 **logly** makes logging clean, structured, and easy to use. Give it a try and streamline your ASP.NET Core logging today! 🚀

