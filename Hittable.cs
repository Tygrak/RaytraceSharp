using Raylib_cs;
using System.Numerics;
using System;

namespace RaytracerSharp {
    public struct HitRecord {
        public bool Hit;
        public RayCollision RayCollision;
        public Material Material;

        public HitRecord(bool hit, RayCollision rayCollision, Material material) {
            Hit = hit;
            RayCollision = rayCollision;
            Material = material;
        }

        public float distance {get {return RayCollision.distance;}}
    }

    public abstract class Hittable {
        public abstract HitRecord Hit(Ray ray, float t_min, float t_max);
    }
}