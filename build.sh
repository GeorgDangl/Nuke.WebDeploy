#!/usr/bin/env bash
set -euo pipefail
SCRIPT_DIR=$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)
cd "$SCRIPT_DIR"
BUILD_PROJECT="build/.build.csproj"
SDK_VERSION=10.0.100
CACHE_DIRECTORY=".fallout/temp"
export DOTNET_CLI_TELEMETRY_OPTOUT=1
export DOTNET_ROLL_FORWARD=Major

if ! command -v dotnet >/dev/null || ! dotnet --version >/dev/null 2>&1; then
    mkdir -p "$CACHE_DIRECTORY"
    curl -fsSL https://dot.net/v1/dotnet-install.sh -o "$CACHE_DIRECTORY/dotnet-install.sh"
    bash "$CACHE_DIRECTORY/dotnet-install.sh" --version "$SDK_VERSION" --install-dir "$CACHE_DIRECTORY/dotnet" --no-path
    export PATH="$SCRIPT_DIR/$CACHE_DIRECTORY/dotnet:$PATH"
fi

dotnet run --project "$BUILD_PROJECT" -- "$@"
