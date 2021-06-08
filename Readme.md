# Zen
[![Actions Status](https://github.com/WajahatAliAbid/zen-cli/workflows/.NET%20Core%20Build/badge.svg?branch=main)](https://github.com/WajahatAliAbid/zen-cli/actions) [![Actions Status](https://github.com/WajahatAliAbid/zen-cli/workflows/.NET%20Core%20Publish/badge.svg)](https://github.com/WajahatAliAbid/zen-cli/actions) [![Current Version](https://img.shields.io/badge/Version-0.0.0-brightgreen?logo=nuget&labelColor=30363D)](./CHANGELOG.md#Unreleased)

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
You need to have .net core 3.1 runtime installed on your system for this.

## Usage
You can invoke the command in bash (or powershell on windows) as follows.
```bash
zen
```
### Get public IP
To get public ip, run the following command
```bash
zen getinfo ip
```
This command will get the public ip and copy it to the clipboard.
*Note*: On linux based environment, [xsel](https://linux.die.net/man/1/xsel) needs to be installed for clipboard to work.

# Changelog
You can read complete changelog [here](./CHANGELOG.md)