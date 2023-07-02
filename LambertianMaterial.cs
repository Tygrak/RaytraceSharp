using Raylib_cs;
using System.Numerics;
using System;

namespace RaytracerSharp {
    public class LambertianMaterial : Material {
        public Vector3 Albedo;

        public LambertianMaterial(Vector3 albedo) {
            Albedo = albedo;
        }

        public override (bool reflect, Vector3 attenuation, Ray scattered) Scatter(Ray ray, RayCollision rayCollision) {
            Vector3 scatterDirection = rayCollision.normal + Helper.RandomUnitVector(Renderer.random);
            if (scatterDirection.LengthSquared() < 0.001f) {
                scatterDirection = rayCollision.normal;
            }
            Ray scattered = new Ray(rayCollision.point, scatterDirection);
            Vector3 attenuation = Albedo;
            return (true, attenuation, scattered);
        }
    }
}