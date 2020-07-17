#!/bin/sh
dotnet restore -s ./pkgbin
dotnet build -c release --no-restore
