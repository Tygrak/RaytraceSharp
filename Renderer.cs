using Raylib_cs;
using System.Numerics;
using System;

namespace RaytracerSharp {
    public class Renderer {
        public int Width = 800;
        public int Height = 480;
        public int SamplesPerPixel = 8;
        private Camera3D Camera = new Camera3D(new Vector3(0, 0, -10), new Vector3(0, 0, 0), new Vector3(0, 1, 0), 45, CameraProjection.CAMERA_PERSPECTIVE);
        private List<(Vector3 pos, float radius)> spheres = new List<(Vector3 pos, float radius)>(){(Vector3.Zero, 2), (new Vector3(0, -100.5f, 0), 97)};
        private Random random = new Random();

        public void Start() {
            Width = 800;
            Height = 480;
            Raylib.InitWindow(Width, Height, "RaytracerSharp");
            RenderLoop();
        }

        public void Start(int width, int height) {
            Width = width;
            Height = height;
            Raylib.InitWindow(Width, Height, "RaytracerSharp");
            RenderLoop();
        }
        
        private void RenderLoop() {
            RenderScene();
            //RenderScene();
            while (!Raylib.WindowShouldClose()) {
                //RenderEndFrame();
            }
            Raylib.CloseWindow();
        }

        private void RenderScene() {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);
            Console.WriteLine("Start");

            for (int y = 0; y < Height; y++) {
                if (y % 10 == 0) {
                    Console.WriteLine($"Line {y}");
                }
                for (int x = 0; x < Width; x++) {
                    Vector3 colorVector = new Vector3();
                    for (int samples = 0; samples < SamplesPerPixel; samples++) {
                        Ray ray = Raylib.GetMouseRay(new Vector2(x+(float) (random.NextDouble())-0.5f, y+(float) (random.NextDouble())-0.5f), Camera);
                        colorVector += RenderRay(ray);
                    }
                    colorVector /= SamplesPerPixel;
                    Raylib.DrawPixel(x, y, Helper.ColorFromVector(colorVector));
                }
            }
            Console.WriteLine("Finished");
            Raylib.EndDrawing();
        }

        private Vector3 RenderRay(Ray ray) {
            for (int i = 0; i < spheres.Count; i++) {
                RayCollision rayCollision = Raylib.GetRayCollisionSphere(ray, spheres[i].pos, spheres[i].radius);
                if (rayCollision.hit) {
                    return rayCollision.normal*0.5f+Vector3.One*0.5f;
                }
            }
            Vector3 colorVector = Raymath.Vector3Lerp(new Vector3(1, 1, 1), new Vector3(0.25f, 0.25f, 0.75f), MathF.Max(Vector3.Normalize(ray.direction).Y, 0));
            return colorVector;
        }

        private void RenderEmptyFrame() {
            //System.Threading.Thread.Sleep(5);
            Raylib.BeginDrawing();
            //Raylib.ClearBackground(Color.WHITE);

            Raylib.DrawText("Finished!", 12, 12, 16, Color.BLACK);

            Raylib.EndDrawing();
        }
    }
}