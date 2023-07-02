using Raylib_cs;
using System.Numerics;
using System;

namespace RaytracerSharp {
    public class DielectricMaterial : Material {
        public Vector3 Albedo;
        public float IndexOfRefraction;

        public DielectricMaterial(Vector3 albedo, float indexOfRefraction) {
            Albedo = albedo;
            IndexOfRefraction = indexOfRefraction;
        }

        public override (bool reflect, Vector3 attenuation, Ray scattered) Scatter(Ray ray, HitRecord hitRecord) {
            float refractionRatio = Helper.IsFrontFace(ray.direction, hitRecord.normal) ? (1.0f/IndexOfRefraction) : IndexOfRefraction;

            Vector3 unitDirection = Vector3.Normalize(ray.direction);
            float cosTheta = MathF.Min(Vector3.Dot(-unitDirection, hitRecord.normal), 1.0f);
            float sinTheta = MathF.Sqrt(1.0f - cosTheta*cosTheta);

            bool cannot_refract = refractionRatio * sinTheta > 1.0;
            Vector3 scatterDirection;

            if (cannot_refract || Helper.SchlickReflectance(cosTheta, refractionRatio) > Renderer.random.NextSingle()) {
                scatterDirection = Raymath.Vector3Reflect(unitDirection, hitRecord.normal);
            } else {
                scatterDirection = Helper.Refract(unitDirection, hitRecord.normal, refractionRatio);
            }

            Ray scattered = new Ray(hitRecord.point, scatterDirection);
            Vector3 attenuation = Albedo;
            return (true, attenuation, scattered);
        }
    }
}