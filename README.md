# A self-contained C# game in 8 kB

This repo is a complement to Michal's [article on building an 8 kB self-contained game in C#](https://medium.com/@MStrehovsky/building-a-self-contained-game-in-c-under-8-kilobytes-74c3cf60ea04?sk=334b06f72dad47f15d0ba0cc6a502487). By self-contained I mean _this 8 kB C# game binary doesn't need a .NET runtime to work_. See the article on how that's done.

The project files and scripts in this repo build the same game (Snake clone) in several different configurations, each with a different size of the output.

😱 Scroll all the way down for instructions on how to run this on DOS.

![Snake game](SeeSharpSnake.gif)

## Building

### To build the W4 version of the game

1. Open "x64 Native Tools Command Prompt for VS 2019" (it's in your Start menu)
2. CD into the repo root directory

```
csc.exe /O /noconfig /nostdlib /runtimemetadataversion:v4.0.30319 MiniRuntime.cs MiniBCL.cs Game\Random.cs Game\Game.cs Game\Snake.cs W4.cs Game\Music.cs /out:zerosnake.ilexe /langversion:latest /unsafe /target:library
```

Find ilc.exe (the [CoreRT](http://github.com/dotnet/corert) ahead of time compiler) on your machine. You can add it as a package to a project and do a publish and it will download to your nuget package location, e.g. https://stackoverflow.com/questions/70474778/compiling-c-sharp-project-to-webassembly

```
[PATH_TO_ILC_EXE]\ilc.exe zerosnake.ilexe -o zerosnake.bc --systemmodule:zerosnake --preinitstatics -g --targetarch=wasm  --nativelib --codegenopt:Target=wasm32-unknown-unknown --targetos:wasm

e.g
..\wl2\.packages\runtime.win-x64.microsoft.dotnet.ilcompiler.llvm\7.0.0-alpha.1.22063.2\tools\ilc.exe zerosnake.ilexe -o zerosnake.bc --systemmodule:zerosnake --preinitstatics -g --targetarch=wasm  --nativelib --codegenopt:Target=wasm32-unknown-unknown --targetos:wasm

```

```
%EMSDK%\upstream\bin\wasm-ld -o zerosnake.wasm zerosnake.o MiniRuntime.bc zerosnakeclrjit.o -mllvm -combiner-global-alias-analysis=false -mllvm -disable-lsr --import-undefined --export-if-defined=__start_em_asm --export-if-defined=__stop_em_asm --export-if-defined=fflush  --export-table -z stack-size=1024 --import-memory --initial-memory=65536 --max-memory=65536 --global-base=6560 --export=update --export=start --no-entry
```

