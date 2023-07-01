using Raylib_cs;
using System.Numerics;

namespace RaytracerSharp {
    static class Program {

        public static void Main() {
            Raylib.InitWindow(800, 480, "Hello World");

            Renderer renderer = new Renderer();
            renderer.Start();
        }
    }
}