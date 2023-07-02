using Raylib_cs;
using System.Numerics;
using System;

namespace RaytracerSharp {
    public class MetalMaterial : Material {
        public Vector3 Albedo;
        public float Fuzz = 0.1f;

        public MetalMaterial(Vector3 albedo) {
            Albedo = albedo;
        }

        public MetalMaterial(Vector3 albedo, float fuzz) {
            Albedo = albedo;
            Fuzz = fuzz;
        }

        public override (bool reflect, Vector3 attenuation, Ray scattered) Scatter(Ray ray, HitRecord hitRecord) {
            Vector3 scatterDirection = Raymath.Vector3Reflect(ray.direction, hitRecord.normal)+Helper.RandomInUnitSphere(Renderer.random)*Fuzz;
            Ray scattered = new Ray(hitRecord.point, scatterDirection);
            Vector3 attenuation = Albedo;
            return (Vector3.Dot(scatterDirection, hitRecord.normal) > 0, attenuation, scattered);
        }
    }
}