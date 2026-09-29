using HidSharp;
using Raylib_cs;
using Calibrator.Devices;
using System.Numerics;

namespace Calibrator.Screens;

public interface Screen
{
    void Update();
    void Draw();
    Screen? nextScreen { get; }
}
