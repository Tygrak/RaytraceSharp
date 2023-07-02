using Raylib_cs;
using System.Numerics;
using System;

namespace RaytracerSharp {
    public class Renderer {
        public int Width = 800;
        public int Height = 480;
        public int SamplesPerPixel = 8;
        public int MaxDepth = 16;
        private Camera3D Camera = new Camera3D(new Vector3(0, 0, -10), new Vector3(0, 0, 0), new Vector3(0, 1, 0), 45, CameraProjection.CAMERA_PERSPECTIVE);
        public static Random random = new Random();
        private Image target;
        private Texture2D texture;

        private List<Hittable> hittables = new List<Hittable>(){
            new Sphere(new Vector3(0,  0, 0), 2.5f, new LambertianMaterial(new Vector3(0.7f, 0.3f, 0.3f))), 
            new Sphere(new Vector3(-4.5f, -1, 0), 2.0f, new MetalMaterial(new Vector3(0.8f, 0.8f, 0.8f))), 
            new Sphere(new Vector3(4.5f,  -1, 0), 2.5f, new MetalMaterial(new Vector3(0.8f, 0.6f, 0.2f))), 
            new Sphere(new Vector3(0, -100.5f, 0), 98.0f, new LambertianMaterial(new Vector3(0.8f, 0.8f, 0.0f)))
            };

        public void Start() {
            Width = 800;
            Height = 480;
            Raylib.InitWindow(Width, Height, "RaytracerSharp");
            target = Raylib.GenImageChecked(Width, Height, 10, 10, Color.DARKGRAY, Color.GRAY);
            RenderLoop();
        }

        public void Start(int width, int height) {
            Width = width;
            Height = height;
            Raylib.InitWindow(Width, Height, "RaytracerSharp");
            target = Raylib.GenImageChecked(Width, Height, 10, 10, Color.DARKGRAY, Color.GRAY);
            RenderLoop();
        }
        
        private unsafe void RenderLoop() {
            texture = Raylib.LoadTextureFromImage(target);
            RenderFromTexture(texture);
            RenderSceneToImage();
            Raylib.UpdateTextureRec(texture, new Rectangle(0, 0, Width, Height), target.data);
            while (!Raylib.WindowShouldClose()) {
                RenderFromTexture(texture);
            }
            Raylib.CloseWindow();
        }

        private void RenderSceneToImage() {
            Console.WriteLine("Start");
            for (int y = 0; y < Height; y++) {
                if (y % 10 == 0) {
                    Console.WriteLine($"Line {y}");
                }
                for (int x = 0; x < Width; x++) {
                    Vector3 colorVector = new Vector3();
                    for (int samples = 0; samples < SamplesPerPixel; samples++) {
                        Ray ray = Raylib.GetMouseRay(new Vector2(x+(float) (random.NextDouble())-0.5f, y+(float) (random.NextDouble())-0.5f), Camera);
                        colorVector += RenderRay(ray, MaxDepth);
                    }
                    colorVector /= SamplesPerPixel;
                    colorVector = new Vector3(MathF.Sqrt(colorVector.X), MathF.Sqrt(colorVector.Y), MathF.Sqrt(colorVector.Z));
                    Raylib.ImageDrawPixel(ref target, x, y, Helper.ColorFromVector(colorVector));
                }
            }
            Console.WriteLine("Finished");
        }

        private Vector3 RenderRay(Ray ray, int depth) {
            if (depth <= 0) {
                return Vector3.Zero;
            }
            HitRecord? result = RayHitWorld(ray);
            if (result is HitRecord hit) {
                var scattered = hit.Material.Scatter(ray, hit.RayCollision);
                if (scattered.reflect) {
                    Vector3 col = scattered.attenuation*RenderRay(scattered.scattered, depth-1);
                    //return h.normal*0.5f+Vector3.One*0.5f;
                    return col;
                }
                return Vector3.Zero;
            } else {
                Vector3 colorVector = Raymath.Vector3Lerp(new Vector3(1, 1, 1), new Vector3(0.25f, 0.25f, 0.75f), MathF.Max(Vector3.Normalize(ray.direction).Y, 0));
                return colorVector;
            }
        }

        private HitRecord? RayHitWorld(Ray ray) {
            HitRecord? closest = null;
            for (int i = 0; i < hittables.Count; i++) {
                var hit = hittables[i].Hit(ray, 0.001f, float.PositiveInfinity);
                if (hit.Hit && (closest == null || hit.distance < closest.Value.distance)) {
                    closest = hit;
                }
            }
            return closest;
        }

        private void RenderFromTexture(Texture2D texture2D) {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);
            Raylib.DrawTexture(texture2D, 0, 0, Color.WHITE);

            //Raylib.DrawText("Finished!", 12, 12, 16, Color.BLACK);

            Raylib.EndDrawing();
        }
    }
}