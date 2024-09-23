using System;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace GrafikProjekt1
{
    class Program
    {

        static void Main(string[] args)
        {

            GameWindowSettings settings = new GameWindowSettings()
            {
                UpdateFrequency = 60.0
            };

            NativeWindowSettings windowSettings = new NativeWindowSettings()
            {
                Title = "cool window",
                ClientSize = new Vector2i(800, 600)

            };

            Game game = new Game(settings, windowSettings);
            game.Run();
        }

    }
}