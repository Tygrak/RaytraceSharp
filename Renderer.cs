using Raylib_cs;
using System.Numerics;
using System;
using System.Diagnostics;

namespace RaytracerSharp {
    public class Renderer {
        public int Width = 800;
        public int Height = 480;
        public int SamplesPerPixelPerPass = 64;
        public int MaxDepth = 16;
        public bool UseSceneCamera = true;

        private Camera3D Camera = new Camera3D(new Vector3(25f, 15f, -1.5f), new Vector3(0, 0, 0), new Vector3(0, 1, 0), 45, CameraProjection.CAMERA_PERSPECTIVE);
        public static Random random = new Random();
        private Image target;
        private Texture2D texture;
        private string statusText = "";

        private int currentRenderedLine = 0;
        private int currentRenderPass = 0;
        private Stopwatch totalWatch = Stopwatch.StartNew();
        private Stopwatch currentFrameWatch = Stopwatch.StartNew();
        private bool tookScreenshot = false;

        private Scene currentScene = DefinedScenes.CornellBox();

        private (Vector3 color, int samples)[,] accumulatedColor = new (Vector3 color, int samples)[1, 1];


        public void Start() {
            Width = 800;
            Height = 480;
            Init();
        }

        public void Start(int width, int height) {
            Width = width;
            Height = height;
            Init();
        }

        private void Init() {
            accumulatedColor = new (Vector3 color, int samples)[Width, Height];
            Raylib.InitWindow(Width, Height, "RaytracerSharp");
            target = Raylib.GenImageChecked(Width, Height, 10, 10, Color.DARKGRAY, Color.GRAY);
            if (UseSceneCamera) {
                Camera = currentScene.DefaultCamera;
            }
            totalWatch = Stopwatch.StartNew();
            RenderLoop();
        }
        
        private unsafe void RenderLoop() {
            texture = Raylib.LoadTextureFromImage(target);
            RenderFromTexture(texture);
            RenderSceneToImage();
            Raylib.UpdateTextureRec(texture, new Rectangle(0, 0, Width, Height), target.data);
            while (!Raylib.WindowShouldClose()) {
                RenderSceneToImage();
            }
            Raylib.CloseWindow();
        }

        private unsafe void RenderSceneToImage() {
            if (currentRenderedLine == 0) {
                Console.WriteLine($"Start (previous samples: {accumulatedColor[0, 0].samples})");
                currentFrameWatch = Stopwatch.StartNew();
            }
            int targetLine = Math.Min(currentRenderedLine+16, Height);
            int samples = SamplesPerPixelPerPass;
            if (currentRenderPass == 0) {
                samples = 4;
            } else if (currentRenderPass == 1) {
                samples = SamplesPerPixelPerPass-4;
            }
            Parallel.ForEach(Enumerable.Range(currentRenderedLine, targetLine-currentRenderedLine).ToList(), y => {
                for (int x = 0; x < Width; x++) {
                    RenderPixel(x, y, samples);
                }
            });
            /*for (int y = currentRenderedLine; y < targetLine; y++) {
                if (y % 50 == 0) {
                    Console.WriteLine($"Line {y}");
                }
                for (int x = 0; x < Width; x++) {
                    RenderPixel(x, y, samples);
                }
            }*/
            currentRenderedLine = targetLine;
            if (accumulatedColor[0, 0].samples < 100 || currentRenderedLine >= Height) {
                Raylib.UpdateTextureRec(texture, new Rectangle(0, 0, Width, Height), target.data);
                statusText = $"(samples: {accumulatedColor[0, 0].samples})";
                RenderFromTexture(texture);
            }
            if (currentRenderedLine >= Height) {
                currentRenderedLine = 0;
                currentRenderPass++;
                currentFrameWatch.Stop();
                long elapsedMs = currentFrameWatch.ElapsedMilliseconds;
                Console.WriteLine($"Finished ({elapsedMs/1000.0f}s)");
                if (accumulatedColor[0, 0].samples > 1000 && !tookScreenshot) {
                    tookScreenshot = true;
                    long totalMs = totalWatch.ElapsedMilliseconds;
                    Console.WriteLine($"Saving screenshot ({totalMs/1000.0f})");
                    Raylib.TakeScreenshot($"data/{totalMs/1000.0f}s{System.DateTime.Now.Hour}h{System.DateTime.Now.Minute}m{System.DateTime.Now.Day}-{System.DateTime.Now.Month}-{System.DateTime.Now.Year}.png");
                }
            }
        }

        private void RenderPixel(int x, int y, int numberOfSamples) {
            for (int samples = 0; samples < 4; samples++) {
                Ray ray = Raylib.GetMouseRay(new Vector2(x+random.NextSingle()-0.5f, y+random.NextSingle()-0.5f), Camera);
                accumulatedColor[x, y] = (accumulatedColor[x, y].color+RenderRay(ray, MaxDepth, numberOfSamples/4), accumulatedColor[x, y].samples+numberOfSamples/4);
            }
            Vector3 colorVector = accumulatedColor[x, y].color/accumulatedColor[x, y].samples;
            colorVector = new Vector3(MathF.Sqrt(colorVector.X), MathF.Sqrt(colorVector.Y), MathF.Sqrt(colorVector.Z));
            Raylib.ImageDrawPixel(ref target, x, y, Helper.ColorFromVector(colorVector));
        }

        private Vector3 RenderRay(Ray ray, int depth, int samples) {
            if (depth <= 0) {
                return Vector3.Zero;
            }
            HitRecord? result = RayHitWorld(ray);
            if (result is HitRecord hit) {
                Vector3 resultColor = new Vector3();
                for (int i = 0; i < samples; i++) {
                    var scattered = hit.Material.Scatter(ray, hit);
                    Vector3 emitted = hit.Material.Emit(hit.Uv);
                    if (scattered.reflect) {
                        Vector3 col = scattered.attenuation*RenderRay(scattered.scattered, depth-1, 1);
                        //return hit.normal*0.5f+Vector3.One*0.5f;
                        resultColor += col+emitted;
                    }
                    resultColor += emitted;
                }
                return resultColor;
            } else {
                return currentScene.SkyFunction.Invoke(ray.direction)*samples;
            }
        }

        private HitRecord? RayHitWorld(Ray ray) {
            HitRecord? closest = null;
            for (int i = 0; i < currentScene.Objects.Count; i++) {
                var hit = currentScene.Objects[i].Hit(ray, 0.001f, float.PositiveInfinity);
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

            Raylib.DrawText(statusText, 8, 8, 8, Color.PINK);

            Raylib.EndDrawing();
        }
    }
}