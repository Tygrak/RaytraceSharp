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
            float u = Renderer.random.NextSingle(); 
            float v= Renderer.random.NextSingle();
            float phi = 2*MathF.PI*u; 
            float costheta = 1-2*v; 
            float sintheta = MathF.Sqrt(1-costheta*costheta);
            Vector3 p = new Vector3(MathF.Cos(phi)*sintheta, MathF.Sin(phi)*sintheta, costheta);
            Vector3 w_i = Vector3.Normalize(p+hitRecord.normal);
            /*throughput *= brdf(w_o, x, w_i) * PI;
            //Vector3 scatterDirection = Vector3.Normalize(hitRecord.normal + Helper.RandomUnitVector(Renderer.random));
            Vector3 scatterDirection = Vector3.Normalize(hitRecord.normal + Helper.RandomUnitVector(Renderer.random));
            if (scatterDirection.LengthSquared() < 0.001f) {
                scatterDirection = hitRecord.normal;
            }*/
            Ray scattered = new Ray(hitRecord.point, w_i);
            Vector3 attenuation = Albedo;
            return (true, attenuation, scattered);
        }
    }
}