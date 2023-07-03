

using Raylib_cs;
using System.Numerics;
using System;

namespace RaytracerSharp {
    public static class DefinedScenes {
        public static Scene SpheresScene() {
            List<Hittable> hittables = new List<Hittable>(){
                new Sphere(new Vector3(0,  0, 0), 2.5f, new LambertianCheckeredMaterial(new Vector3(0.3f, 0.3f, 0.9f), new Vector3(0.7f, 0.3f, 0.7f))), 
                new Sphere(new Vector3(-4.5f, -1, 0), 2.0f, new MetalMaterial(new Vector3(0.8f, 0.8f, 0.8f), 0.25f)), 
                new Sphere(new Vector3(4.5f,  -1, 0), 2.5f, new MetalMaterial(new Vector3(0.8f, 0.6f, 0.2f), 0.6f)), 
                new Sphere(new Vector3(1.5f,  -1, -5), 2.5f, new DielectricMaterial(new Vector3(1.0f, 1.0f, 1.0f), 1.5f)), 
                new Sphere(new Vector3(-5.5f,  0.5f, -6), 2.5f, new DielectricMaterial(new Vector3(1.0f, 1.0f, 1.0f), 1.5f)),
                new Sphere(new Vector3(9.5f,  2.5f, 10), 3.5f, new DielectricMaterial(new Vector3(1.0f, 1.0f, 1.0f), 1.5f)),
                //ground
                new Sphere(new Vector3(0, -100.5f, 0), 98.0f, new LambertianMaterial(new Vector3(0.2f, 0.8f, 0.3f)))
            };
            Func<Vector3, Vector3> skyFunc = (Vector3 d) => Raymath.Vector3Lerp(new Vector3(1, 1, 1), new Vector3(0.25f, 0.25f, 0.75f), MathF.Max(Vector3.Normalize(d).Y, 0));
            Camera3D camera = new Camera3D(new Vector3(25f, 15f, -1.5f), new Vector3(0, 0, 0), new Vector3(0, 1, 0), 45, CameraProjection.CAMERA_PERSPECTIVE);
            return new Scene(hittables, skyFunc, camera);
        }

        public static Scene DarkScene() {
            List<Hittable> hittables = new List<Hittable>(){
                new Sphere(new Vector3(0,  0, 0), 2.5f, new LambertianCheckeredMaterial(new Vector3(0.3f, 0.3f, 0.9f), new Vector3(0.7f, 0.3f, 0.7f))), 
                new Sphere(new Vector3(-4.5f, -1, 0), 2.0f, new MetalMaterial(new Vector3(0.8f, 0.8f, 0.8f), 0.25f)), 
                new Sphere(new Vector3(4.5f,  -1, 0), 2.5f, new MetalMaterial(new Vector3(0.8f, 0.6f, 0.2f), 0.6f)), 
                //glass
                new Sphere(new Vector3(11.5f,  2.5f, 5), 2.5f, new DielectricMaterial(new Vector3(1.0f, 1.0f, 1.0f), 1.5f)), 
                new Sphere(new Vector3(-5.5f, 1.5f, -6), 2.5f, new DielectricMaterial(new Vector3(1.0f, 1.0f, 1.0f), 1.5f)),
                new Sphere(new Vector3(-9.5f, 3.5f, 2), 3.5f, new DielectricMaterial(new Vector3(1.0f, 1.0f, 1.0f), 1.5f)),
                //lights
                new Sphere(new Vector3(-4f,  3.0f, 10), 1.5f, new EmissiveMaterial(new Vector3(5.0f, 0.1f, 5.0f))),
                new Sphere(new Vector3(0f,  3.0f, 10), 1.5f, new EmissiveMaterial(new Vector3(0.1f, 5.0f, 0.1f))),
                new Sphere(new Vector3(4f,  3.0f, 10), 1.5f, new EmissiveMaterial(new Vector3(0.1f, 0.1f, 5.0f))),
                //ground
                new Sphere(new Vector3(0, -300.5f, 0), 300.0f, new LambertianMaterial(new Vector3(0.2f, 0.8f, 0.3f)))
            };
            Func<Vector3, Vector3> skyFunc = (Vector3 d) => new Vector3(0.0025f, 0.0025f, 0.0025f);
            Camera3D camera = new Camera3D(new Vector3(25f, 15f, -1.5f), new Vector3(0, 0, 0), new Vector3(0, 1, 0), 45, CameraProjection.CAMERA_PERSPECTIVE);
            return new Scene(hittables, skyFunc, camera);
        }

        public static Scene DarkSceneShrimple() {
            List<Hittable> hittables = new List<Hittable>(){
                new Sphere(new Vector3(0,  0, -11), 2.5f, new LambertianCheckeredMaterial(new Vector3(0.95f, 0.95f, 0.95f), new Vector3(0.6f, 0.6f, 0.6f), 16)), 
                new Sphere(new Vector3(-4.5f, -0, 16), 2.0f, new MetalMaterial(new Vector3(0.8f, 0.8f, 0.8f), 0.25f)), 
                new Sphere(new Vector3(7.5f, -0, 13), 2.0f, new MetalMaterial(new Vector3(0.3f, 0.8f, 0.8f), 0.25f)), 
                new Sphere(new Vector3(4.5f,  -1, 0), 2.5f, new MetalMaterial(new Vector3(0.8f, 0.6f, 0.2f), 0.6f)), 
                //glass
                new Sphere(new Vector3(11.5f,  2.5f, 5), 2.5f, new DielectricMaterial(new Vector3(1.0f, 1.0f, 1.0f), 1.45f)), 
                new Sphere(new Vector3(-5.5f, 1.5f, -6), 2.5f, new DielectricMaterial(new Vector3(1.0f, 1.0f, 1.0f), 1.55f)),
                new Sphere(new Vector3(-9.5f, 3.5f, 2), 3.5f, new DielectricMaterial(new Vector3(1.0f, 1.0f, 1.0f), 1.5f)),
                //box
                new RotatedHittable(new Box(Vector3.Zero, new Vector3(5.5f, 5.0f, 2.5f), new DielectricMaterial(new Vector3(1.0f, 1.0f, 1.0f), 1.5f)), new Vector3(0, -0.5f, 0), new Vector3(-10.5f, 0.5f, 25.25f)),
                //lights
                new Sphere(new Vector3(-4f,  3.0f, 10), 2.5f, new EmissiveMaterial(new Vector3(1.5f, 1.5f, 1.5f))),
                //ground
                new Sphere(new Vector3(0, -300.5f, 0), 300.0f, new LambertianMaterial(new Vector3(0.2f, 0.8f, 0.3f)))
            };
            Func<Vector3, Vector3> skyFunc = (Vector3 d) => new Vector3(0.0025f, 0.0025f, 0.0025f);
            Camera3D camera = new Camera3D(new Vector3(55f, 15f, -10.5f), new Vector3(0, 0, 0), new Vector3(0, 1, 0), 30, CameraProjection.CAMERA_PERSPECTIVE);
            return new Scene(hittables, skyFunc, camera);
        }

        public static Scene CornellBox() {
            float height = 4.5f;
            float width = 5f;
            float depth = 4f;
            Vector3 ldb = new Vector3(-width, -height, depth);
            Vector3 rdb = new Vector3(width, -height, depth);
            Vector3 lub = new Vector3(-width, height, depth);
            Vector3 rub = new Vector3(width, height, depth);
            Vector3 ldf = new Vector3(-width, -height, -depth);
            Vector3 rdf = new Vector3(width, -height, -depth);
            Vector3 luf = new Vector3(-width, height, -depth);
            Vector3 ruf = new Vector3(width, height, -depth);

            Vector3 light1 = new Vector3(-width/4, height-0.001f, -depth/4);
            Vector3 light2 = new Vector3(width/4, height-0.001f, -depth/4);
            Vector3 light3 = new Vector3(width/4, height-0.001f, depth/4);
            Vector3 light4 = new Vector3(-width/4, height-0.001f, depth/4);
            List<Hittable> hittables = new List<Hittable>(){
                //walls
                new Quad(ldb, ldf, luf, lub, new LambertianMaterial(new Vector3(0.95f, 0.1f, 0.1f))), //right
                new Quad(rub, ruf, rdf, rdb, new LambertianMaterial(new Vector3(0.1f, 0.95f, 0.1f))), //left
                new Quad(lub, rub, rdb, ldb, new LambertianMaterial(new Vector3(0.7f, 0.7f, 0.7f))), //back
                new Quad(luf, ruf, rub, lub, new LambertianMaterial(new Vector3(0.7f, 0.7f, 0.7f))), //up
                new Quad(ldb, rdb, rdf, ldf, new LambertianMaterial(new Vector3(0.7f, 0.7f, 0.7f))), //down
                //stuff
                new RotatedHittable(new Box(Vector3.Zero, new Vector3(2.5f, 5.0f, 2.5f), new LambertianMaterial(new Vector3(0.7f, 0.7f, 0.7f))), new Vector3(0, -0.5f, 0), new Vector3(2.5f, -2.5f, 2.25f)),
                new RotatedHittable(new Box(Vector3.Zero, new Vector3(2.5f, 2.5f, 2.5f), new LambertianMaterial(new Vector3(0.7f, 0.7f, 0.7f))), new Vector3(0, 2, 0), new Vector3(-2.5f, -3.5f, -1.5f)),
                //light
                new Quad(light1, light2, light3, light4, new EmissiveMaterial(Vector3.One*10))
            };
            Func<Vector3, Vector3> skyFunc = (Vector3 d) => new Vector3(0.025f, 0.025f, 0.025f);
            Camera3D camera = new Camera3D(new Vector3(0f, 0f, -20.0f), new Vector3(0, 0, 0), new Vector3(0, 1, 0), 30, CameraProjection.CAMERA_PERSPECTIVE);
            return new Scene(hittables, skyFunc, camera);
        }
        
        public static Scene CornellBoxPlus() {
            float height = 4.5f;
            float width = 5f;
            float depth = 4f;
            Vector3 ldb = new Vector3(-width, -height, depth);
            Vector3 rdb = new Vector3(width, -height, depth);
            Vector3 lub = new Vector3(-width, height, depth);
            Vector3 rub = new Vector3(width, height, depth);
            Vector3 ldf = new Vector3(-width, -height, -depth);
            Vector3 rdf = new Vector3(width, -height, -depth);
            Vector3 luf = new Vector3(-width, height, -depth);
            Vector3 ruf = new Vector3(width, height, -depth);

            Vector3 light1 = new Vector3(-width/4, height-0.001f, -depth/4);
            Vector3 light2 = new Vector3(width/4, height-0.001f, -depth/4);
            Vector3 light3 = new Vector3(width/4, height-0.001f, depth/4);
            Vector3 light4 = new Vector3(-width/4, height-0.001f, depth/4);
            List<Hittable> hittables = new List<Hittable>(){
                //walls
                new Quad(ldb, ldf, luf, lub, new LambertianMaterial(new Vector3(0.95f, 0.1f, 0.1f))), //right
                new Quad(rub, ruf, rdf, rdb, new LambertianMaterial(new Vector3(0.1f, 0.95f, 0.1f))), //left
                new Quad(lub, rub, rdb, ldb, new LambertianMaterial(new Vector3(0.7f, 0.7f, 0.7f))), //back
                new Quad(luf, ruf, rub, lub, new LambertianMaterial(new Vector3(0.7f, 0.7f, 0.7f))), //up
                new Quad(ldb, rdb, rdf, ldf, new LambertianMaterial(new Vector3(0.7f, 0.7f, 0.7f))), //down
                //stuff
                new RotatedHittable(new Box(Vector3.Zero, new Vector3(2.5f, 5.0f, 2.5f), new MetalMaterial(new Vector3(0.95f, 0.5f, 0.2f), 0.35f)), new Vector3(0, -0.5f, 0), new Vector3(2.5f, -2.5f, 2.25f)),
                new RotatedHittable(new Box(Vector3.Zero, new Vector3(2.5f, 2.5f, 2.5f), new LambertianMaterial(new Vector3(0.7f, 0.7f, 0.7f))), new Vector3(0, 2, 0), new Vector3(-2.5f, -3.5f, -1.5f)),
                new Sphere(new Vector3(2.5f, -3.5f, -1.25f), 1.0f, new MetalMaterial(new Vector3(0.25f, 0.45f, 0.95f), 0.15f)),
                //light
                new Quad(light1, light2, light3, light4, new EmissiveMaterial(Vector3.One*10))
            };
            Func<Vector3, Vector3> skyFunc = (Vector3 d) => new Vector3(0.025f, 0.025f, 0.025f);
            Camera3D camera = new Camera3D(new Vector3(0f, 0f, -20.0f), new Vector3(0, 0, 0), new Vector3(0, 1, 0), 30, CameraProjection.CAMERA_PERSPECTIVE);
            return new Scene(hittables, skyFunc, camera);
        }

        public static Scene CornellBoxDeep() {
            float height = 4.5f;
            float width = 5f;
            float depth = 25f;
            Vector3 ldb = new Vector3(-width, -height, depth);
            Vector3 rdb = new Vector3(width, -height, depth);
            Vector3 lub = new Vector3(-width, height, depth);
            Vector3 rub = new Vector3(width, height, depth);
            Vector3 ldf = new Vector3(-width, -height, -depth);
            Vector3 rdf = new Vector3(width, -height, -depth);
            Vector3 luf = new Vector3(-width, height, -depth);
            Vector3 ruf = new Vector3(width, height, -depth);

            Vector3 light1 = new Vector3(-width/4, height-0.001f, -depth/4);
            Vector3 light2 = new Vector3(width/4, height-0.001f, -depth/4);
            Vector3 light3 = new Vector3(width/4, height-0.001f, depth/4);
            Vector3 light4 = new Vector3(-width/4, height-0.001f, depth/4);
            List<Hittable> hittables = new List<Hittable>(){
                //walls
                new Quad(ldb, ldf, luf, lub, new LambertianMaterial(new Vector3(0.95f, 0.1f, 0.1f))), //right
                new Quad(rub, ruf, rdf, rdb, new LambertianMaterial(new Vector3(0.1f, 0.95f, 0.1f))), //left
                new Quad(lub, rub, rdb, ldb, new LambertianMaterial(new Vector3(0.7f, 0.7f, 0.7f))), //back
                new Quad(luf, ruf, rub, lub, new LambertianMaterial(new Vector3(0.7f, 0.7f, 0.7f))), //up
                new Quad(ldb, rdb, rdf, ldf, new LambertianMaterial(new Vector3(0.7f, 0.7f, 0.7f))), //down
                new Quad(ldf, rdf, ruf, luf, new LambertianMaterial(new Vector3(0.7f, 0.7f, 0.7f))), //front
                //stuff
                new RotatedHittable(new Box(Vector3.Zero, new Vector3(2.5f, 5.0f, 2.5f), new LambertianMaterial(new Vector3(0.7f, 0.7f, 0.7f))), new Vector3(0, -0.5f, 0), new Vector3(2.5f, -2.5f, 2.25f)),
                new RotatedHittable(new Box(Vector3.Zero, new Vector3(2.5f, 2.5f, 2.5f), new LambertianMaterial(new Vector3(0.7f, 0.7f, 0.7f))), new Vector3(0, 2, 0), new Vector3(-2.5f, -3.5f, -1.5f)),
                //light
                new Quad(light1, light2, light3, light4, new EmissiveMaterial(Vector3.One*10))
            };
            Func<Vector3, Vector3> skyFunc = (Vector3 d) => new Vector3(0.025f, 0.025f, 0.025f);
            Camera3D camera = new Camera3D(new Vector3(0f, 0f, -20.0f), new Vector3(0, 0, 0), new Vector3(0, 1, 0), 30, CameraProjection.CAMERA_PERSPECTIVE);
            return new Scene(hittables, skyFunc, camera);
        }
    }
}