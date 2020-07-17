#!/bin/sh

dotnet run --no-restore --no-build -c release -- contest "$@" || echo "run error code: $?"
