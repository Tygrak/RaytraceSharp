using Raylib_cs;
using System.Numerics;
using System;

namespace RaytracerSharp {
    public class Sphere : Hittable {
        public Material Material = new LambertianMaterial(new Vector3(0.5f, 0.5f, 0.5f));
        public Vector3 Position;
        public float Radius;

        public Sphere(Vector3 position, float radius) {
            Position = position;
            Radius = radius;
        }

        public Sphere(Vector3 position, float radius, Material material) {
            Position = position;
            Radius = radius;
            Material = material;
        }

        public override HitRecord Hit(Ray ray, float t_min, float t_max) {
            RayCollision rayCollision = Raylib.GetRayCollisionSphere(ray, Position, Radius);
            if (rayCollision.hit && rayCollision.distance > t_min && rayCollision.distance < t_max) {
                return new HitRecord(true, rayCollision, Material, GetSphereUv(rayCollision.normal));
            }
            return new HitRecord(false, rayCollision, Material, Vector2.Zero);
        }

        public static Vector2 GetSphereUv(Vector3 p) {
            // p: a given point on the sphere of radius one, centered at the origin.
            // u: returned value [0,1] of angle around the Y axis from X=-1.
            // v: returned value [0,1] of angle from Y=-1 to Y=+1.
            //     <1 0 0> yields <0.50 0.50>       <-1  0  0> yields <0.00 0.50>
            //     <0 1 0> yields <0.50 1.00>       < 0 -1  0> yields <0.50 0.00>
            //     <0 0 1> yields <0.25 0.50>       < 0  0 -1> yields <0.75 0.50>

            float theta = MathF.Acos(-p.Y);
            float phi = MathF.Atan2(-p.Z, p.X) + MathF.PI;

            float u = phi / (2*MathF.PI);
            float v = theta / MathF.PI;
            return new Vector2(u, v);
        }
    }
}