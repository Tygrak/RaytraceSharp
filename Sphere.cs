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
                return new HitRecord(true, rayCollision, Material);
            }
            return new HitRecord(false, rayCollision, Material);
        }
    }
}