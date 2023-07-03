using Raylib_cs;
using System.Numerics;
using System;

namespace RaytracerSharp {
    public class LambertianCheckeredMaterial : Material {
        public Vector3 Albedo1;
        public Vector3 Albedo2;
        public int Repetitions = 32;

        public LambertianCheckeredMaterial(Vector3 albedo1, Vector3 albedo2, int repetitions = 32) {
            Albedo1 = albedo1;
            Albedo2 = albedo2;
            Repetitions = repetitions;
        }

        public override (bool reflect, Vector3 attenuation, Ray scattered) Scatter(Ray ray, HitRecord hitRecord) {
            Vector3 scatterDirection = Vector3.Normalize(hitRecord.normal + Helper.RandomUnitVector(Renderer.random));
            if (scatterDirection.LengthSquared() < 0.001f) {
                scatterDirection = hitRecord.normal;
            }
            Ray scattered = new Ray(hitRecord.point, scatterDirection);
            Vector3 attenuation;
            int xSquare = (int) (hitRecord.Uv.X*Repetitions) % 2;
            int ySquare = (int) (hitRecord.Uv.Y*Repetitions) % 2;
            if (xSquare == ySquare) {
                attenuation = Albedo1;
            } else {
                attenuation = Albedo2;
            }
            return (true, attenuation, scattered);
        }
    }
}