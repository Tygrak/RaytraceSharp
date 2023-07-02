using Raylib_cs;
using System.Numerics;
using System;

namespace RaytracerSharp {
    public class MetalMaterial : Material {
        public Vector3 Albedo;

        public MetalMaterial(Vector3 albedo) {
            Albedo = albedo;
        }

        public override (bool reflect, Vector3 attenuation, Ray scattered) Scatter(Ray ray, RayCollision rayCollision) {
            Vector3 scatterDirection = Raymath.Vector3Reflect(ray.direction, rayCollision.normal);
            if (scatterDirection.LengthSquared() < 0.001f) {
                scatterDirection = rayCollision.normal;
            }
            Ray scattered = new Ray(rayCollision.point, scatterDirection);
            Vector3 attenuation = Albedo;
            return (Vector3.Dot(scatterDirection, rayCollision.normal) > 0, attenuation, scattered);
        }
    }
}