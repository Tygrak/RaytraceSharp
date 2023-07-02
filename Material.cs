using Raylib_cs;
using System.Numerics;
using System;

namespace RaytracerSharp {
    public class Material {
        public virtual (bool reflect, Vector3 attenuation, Ray scattered) Scatter(Ray ray, RayCollision rayCollision) {
            Vector3 scatterDirection = rayCollision.normal + Helper.RandomUnitVector(Renderer.random);
            Ray scattered = new Ray(rayCollision.point, scatterDirection);
            Vector3 attenuation = Vector3.One;
            return (true, attenuation, scattered);
        }
    }
}