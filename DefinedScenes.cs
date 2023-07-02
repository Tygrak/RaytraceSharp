

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
            Func<Vector3, Vector3> SkyFunc = (Vector3 d) => Raymath.Vector3Lerp(new Vector3(1, 1, 1), new Vector3(0.25f, 0.25f, 0.75f), MathF.Max(Vector3.Normalize(d).Y, 0));
            return new Scene(hittables, SkyFunc);
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
            Func<Vector3, Vector3> SkyFunc = (Vector3 d) => new Vector3(0.0025f, 0.0025f, 0.0025f);
            return new Scene(hittables, SkyFunc);
        }
    }
}