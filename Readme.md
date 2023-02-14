# Zen
[![Actions Status](https://github.com/ZenExtensions/zen-cli/workflows/.NET%20Core%20Publish/badge.svg)](https://github.com/ZenExtensions/zen-cli/actions) [![Current Version](https://img.shields.io/badge/Version-1.3.2-brightgreen?logo=nuget&labelColor=30363D)](./CHANGELOG.md#132--2022-04-19)

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
    zen getinfo eol
    zen getinfo eol --query dotnet
    zen getinfo ip
    zen getinfo myip
    zen getinfo public-ip

OPTIONS:
    -h, --help       Prints help information   
    -v, --version    Prints version information

COMMANDS:
    getinfo     Get information about things      
    gen         Generate different things from cli
```
# Changelog
You can read complete changelog [here](./CHANGELOG.md)