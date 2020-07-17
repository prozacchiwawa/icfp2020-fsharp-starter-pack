#!/bin/sh

mkdir -p ./pkgbin
dotnet nuget add source ./pkgbin
(dotnet add testfs.fsproj package -s ./pkgbin FSharp.Core) || /bin/true
(dotnet add package FSharp.Core --version 4.7.2 --package-directory ./pkgbin) || /bin/true
