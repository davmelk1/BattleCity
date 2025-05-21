# BattleCity

A simple .NET-based game inspired by the classic Battle City.

## Prerequisites

- Git

## Installation instructions

Follow the instructions below based on your operating system to clone and run the application.


Clone the repository
```
git clone https://github.com/davmelk1/BattleCity
cd BattleCity
```

### 🪟 Windows
Install Chocolatey (if not already installed)
```
Set-ExecutionPolicy Bypass -Scope Process -Force; `
[System.Net.ServicePointManager]::SecurityProtocol = `
[System.Net.ServicePointManager]::SecurityProtocol -bor 3072; `
iex ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1'))
```
Install .Net SDK
```
choco install dotnet-sdk -y
```
Run the application
```
dotnet run
```

### 🐧 Ubuntu

Install .Net SDK
```
sudo apt update
mkdir $HOME/dotnet_install && cd $HOME/dotnet_install
sudo apt install curl
curl -L https://aka.ms/install-dotnet-preview -o install-dotnet-preview.sh
sudo bash install-dotnet-preview.sh
sudo apt install libcsfml-dev   #5, 24, 88
```

Run the application
```
dotnet run
```
(If your dotnet version differs from 9.0, in MyGame.csproj set the TargetFramework value to your dotnet version)