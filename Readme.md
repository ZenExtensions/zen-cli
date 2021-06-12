# Zen
[![Actions Status](https://github.com/WajahatAliAbid/zen-cli/workflows/.NET%20Core%20Build/badge.svg?branch=main)](https://github.com/WajahatAliAbid/zen-cli/actions) [![Actions Status](https://github.com/WajahatAliAbid/zen-cli/workflows/.NET%20Core%20Publish/badge.svg)](https://github.com/WajahatAliAbid/zen-cli/actions) [![Current Version](https://img.shields.io/badge/Version-0.0.6-brightgreen?logo=nuget&labelColor=30363D)](./CHANGELOG.md#006--2021-06-12)

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

### Get Network Interface Information
To get information about Network interfaces, use the following command
```bash
zen getinfo nic
```

This command will display output in following format
```bash
                                                      Network Interfaces                                                      
                                                                                                                              
| Id              | Name            | Supports Multicast? | Operational Status | Interface Type | DNS Enabled? | Gateway IP  |
| --------------- | --------------- | ------------------- | ------------------ | -------------- | ------------ | ----------- |
| lo              | lo              | False               | Unknown            | Loopback       | True         |             |
| enp0s10         | enp0s10         | True                | Down               | Ethernet       | True         |             |
| wlp1s0          | wlp1s0          | True                | Up                 | Ethernet       | True         | 192.168.4.1 |
| docker0         | docker0         | True                | Down               | Ethernet       | True         |             |
```
### Download gitignore file
To get the gitignore file, run the following command and follow instructions
```bash
zen misc gitignore
```
You can also specify the query separated by commas with the command
```bash
zen misc gitignore --query "visual, go"
```
By default, the gitignore file will be stored in the same folder where command is run, but this can be changed by explicitly providing destination
```bash
zen misc gitignore --destination /home/user/projects/my-app
```
# Changelog
You can read complete changelog [here](./CHANGELOG.md)