# Zen
[![Actions Status](https://github.com/ZenExtensions/zen-cli/workflows/.NET%20Core%20Publish/badge.svg)](https://github.com/ZenExtensions/zen-cli/actions) [![Current Version](https://img.shields.io/badge/Version-1.4.0-brightgreen?logo=nuget&labelColor=30363D)](./CHANGELOG.md#140--2023-02-16)

This command helps make things several day to day things easier.

# Overview
## Installation
Installing is as simple as running this command 🤟
```bash
dotnet tool install --global zen-cli
```
If you need to update the cli, run the following command
```bash
dotnet tool update --global zen-cli
```
You need to have .net 6 runtime installed on your system for this.

## Usage
You can invoke the command in bash (or powershell on windows) as follows.
```bash
zen --help
```

```
USAGE:
    zen [OPTIONS] <COMMAND>

EXAMPLES:
    zen gen uuid
    zen gen password
    zen gen gitignore
    zen getinfo ip

OPTIONS:
    -h, --help       Prints help information   
    -v, --version    Prints version information

COMMANDS:
    gen        Generate different things from cli
    getinfo    Get information from cl
```
# Changelog
You can read complete changelog [here](./CHANGELOG.md)