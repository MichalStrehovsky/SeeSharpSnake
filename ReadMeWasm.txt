Wasm port of the SeeSharpSnake
(https://github.com/MichalStrehovsky/SeeSharpSnake and
https://medium.com/@MStrehovsky/building-a-self-contained-game-in-c-under-8-kilobytes-74c3cf60ea04
)

Commands to build

emcc MiniRuntime.wasm.c -c -o MiniRuntime.bc -s WASM=1

csc.exe /debug /O /noconfig /nostdlib /runtimemetadataversion:v4.0.30319 MiniRuntime.cs MiniBCL.cs Game\FrameBuffer.cs Game\Random.cs Game\Game.cs Game\Snake.cs Pal\Thread.Wasm.cs Pal\Environment.Wasm.cs Pal\Console.Wasm.cs Pal\Console.cs /out:zerosnake.ilexe /langversion:latest /unsafe

..\corert\bin\WebAssembly.wasm.Debug\tools\ilc --targetarch=wasm zerosnake.ilexe -o zerosnake.bc --systemmodule:zerosnake --Os -g

"E:\GitHub\emsdk\upstream\emscripten\emcc.bat" "zerosnake.bc" -o "zerosnake.html" -s WASM=1 --emrun MiniRuntime.bc -s ASYNCIFY=1 --js-library pal\console.js --shell-file pal\shell_minimal.html -Os --llvm-lto 3

