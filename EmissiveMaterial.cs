using Raylib_cs;
using System.Numerics;
using System;

namespace RaytracerSharp {
    public class EmissiveMaterial : Material {
        public Vector3 Emissive;

        public EmissiveMaterial(Vector3 emissive) {
            Emissive = emissive;
        }

        public override (bool reflect, Vector3 attenuation, Ray scattered) Scatter(Ray ray, HitRecord hitRecord) {
            return (false, Vector3.Zero, ray);
        }

        public override Vector3 Emit(Vector2 uv) {
            return Emissive;
        }
    }
}