# logly

[![CircleCI](https://circleci.com/gh/bolorundurowb/logly.svg?style=svg)](https://circleci.com/gh/bolorundurowb/logly)  [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

This work is based on that done by _Casey MacPherson_ [here](https://www.codedad.net/2017/08/26/asp-net-core-2-response-logging-2/)

logly is a library written for ASP.NET Core. Its purpose to to make request and response logging more modular and beautiful.

Here is an example of what it looks like:

![logly screenshot](https://res.cloudinary.com/dg2dgzbt4/image/upload/v1530508697/logly_bittgm.png)

## Prerequisite 
To prevent the default logging by ASP.NET Core, you would need to make some changes to the default `BuildWebHost` method.

This:
```csharp
public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
```

becomes this:

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
To add logging, there are two ways, the simple way where you add the following line to the `Configure` method in the `Startup.cs` file.


```csharp
app.UseLogly();
```

that adds request method, response status code logging and response time logging. If you want to specify more logging options, you can use the `LoggerOptionsBuilder`, as shown below.

```csharp
app.UseLogly(opts => opts
                    .AddRequestMethod()
                    .AddStatusCode()
                    .AddResponseTime()
                    .AddUrl()
                    .AddResponseLength());
```