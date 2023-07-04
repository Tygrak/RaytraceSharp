using Raylib_cs;
using System.Numerics;
using System;
using System.Diagnostics;
using System.Text;
using static Raylib_cs.Raylib;
using static Raylib_cs.ConfigFlags;

namespace RaytracerSharp {
    public class GPURenderer {
        public int Width = 800;
        public int Height = 480;
        public int SamplesPerPixelPerPass = 64;
        public int MaxDepth = 16;
        public bool UseSceneCamera = false;

        private Camera3D Camera = new Camera3D(new Vector3(0f, 2.5f, -6.0f), new Vector3(0, 0, 0), new Vector3(0, 1, 0), 45, CameraProjection.CAMERA_PERSPECTIVE);
        public static Random random = new Random();
        private Image target;
        private string statusText = "hi";

        private Stopwatch totalWatch = Stopwatch.StartNew();
        private Stopwatch currentFrameWatch = Stopwatch.StartNew();

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
            ReloadShader();
            RenderLoop();
        }

        Shader shader;
        int cameraPositionLoc, cameraTargetLoc, cameraInverseLoc, iTimeLoc, iResolutionLoc;
        private void ReloadShader() {
            Shader newShader = LoadShader(null, $"resources/raytrace.fs");
            if (newShader.id == 0) {
                Console.WriteLine("Shader reload failed!");
                return;
            }
            UnloadShader(shader);
            shader = newShader;
            Console.WriteLine("Shader reloaded!");
            float[] resolution = {(float) Width, (float) Height};
            cameraPositionLoc = GetShaderLocation(shader, "cameraPosition");
            cameraTargetLoc = GetShaderLocation(shader, "cameraTarget");
            cameraInverseLoc = GetShaderLocation(shader, "cameraInverse");
            iTimeLoc = GetShaderLocation(shader, "iTime");
            iResolutionLoc = GetShaderLocation(shader, "iResolution");
            Raylib.SetShaderValue(shader, iResolutionLoc, resolution, ShaderUniformDataType.SHADER_UNIFORM_VEC2);
        }

        private void RenderLoop() {
            float runTime = 0.0f;
            while (!Raylib.WindowShouldClose()) {
                if (IsKeyPressed(KeyboardKey.KEY_R)) {
                    ReloadShader();
                }

                //UpdateCamera(ref Camera, CameraMode.CAMERA_FREE);
                float deltaTime = GetFrameTime();
                runTime += deltaTime;

                Matrix4x4 viewMatrix = Raymath.MatrixLookAt(Camera.position, Camera.target, Camera.up);
                Matrix4x4 projectionMatrix = Raymath.MatrixPerspective( Camera.fovy*0.017453292, (double) Width/Height, 0.01, 10000); 
                Matrix4x4 cameraMatrix = viewMatrix*projectionMatrix;
                Matrix4x4 inverseCameraMatrix = cameraMatrix;
                Matrix4x4.Invert(cameraMatrix, out inverseCameraMatrix);
                Raylib.SetShaderValueMatrix(shader, cameraInverseLoc, inverseCameraMatrix);
                Raylib.SetShaderValue(shader, iTimeLoc, runTime, ShaderUniformDataType.SHADER_UNIFORM_FLOAT);
                Raylib.SetShaderValue(shader, cameraTargetLoc, Camera.target, ShaderUniformDataType.SHADER_UNIFORM_VEC3);
                Raylib.SetShaderValue(shader, cameraPositionLoc, Camera.position, ShaderUniformDataType.SHADER_UNIFORM_VEC3);

                BeginDrawing();
                ClearBackground(Color.RAYWHITE);

                BeginShaderMode(shader);
                DrawRectangle(0, 0, Width, Height, Color.WHITE);
                EndShaderMode();

                DrawText(statusText, 10, 10, 10, Color.PINK);

                EndDrawing();
            }
            UnloadShader(shader);
            Raylib.CloseWindow();
        }
    }
}