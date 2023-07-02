using Raylib_cs;
using System.Numerics;
using System;

namespace RaytracerSharp {
    public class LambertianMaterial : Material {
        public Vector3 Albedo;

        public LambertianMaterial(Vector3 albedo) {
            Albedo = albedo;
        }

        public override (bool reflect, Vector3 attenuation, Ray scattered) Scatter(Ray ray, HitRecord hitRecord) {
            Vector3 scatterDirection = hitRecord.normal + Helper.RandomUnitVector(Renderer.random);
            if (scatterDirection.LengthSquared() < 0.001f) {
                scatterDirection = hitRecord.normal;
            }
            Ray scattered = new Ray(hitRecord.point, scatterDirection);
            Vector3 attenuation = Albedo;
            return (true, attenuation, scattered);
        }
    }
}