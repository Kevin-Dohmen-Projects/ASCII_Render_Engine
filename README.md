# C#_ASCII_Render_Engine
[![Build and Release](https://github.com/Kevin-Dohmen/CSharp_ASCII_Render_Engine/actions/workflows/build-and-release.yml/badge.svg)](https://github.com/Kevin-Dohmen/CSharp_ASCII_Render_Engine/actions/workflows/build-and-release.yml)  
A C# ASCII render engine

## Functionality:
- Shader renderer (OpenGL inspired)
- 2D Primitive Shape Renderer (with assignable shader)
- 2D Line Renderer
- Vectors (i know C# has built-in shaders but i don't care)
  - `Vec2`
  - `Vec3`
  - `Vec4`
- Basic keyboard input (at the moment only windows)

## CI/CD:
The Pipeline is set up to build and release the project if a commit message contains `[run-ci]`.  
Example: `git commit -m "[run-ci] Fixed a bug"`