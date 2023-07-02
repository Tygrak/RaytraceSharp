using Raylib_cs;
using System.Numerics;
using System;

namespace RaytracerSharp {
    public struct HitRecord {
        public bool Hit;
        public RayCollision RayCollision;
        public Material Material;
        public Vector2 Uv;

        public HitRecord(bool hit, RayCollision rayCollision, Material material, Vector2 uv) {
            Hit = hit;
            RayCollision = rayCollision;
            Material = material;
            Uv = uv;
        }

        public float distance {get {return RayCollision.distance;}}
        public Vector3 point {get {return RayCollision.point;}}
        public Vector3 normal {get {return RayCollision.normal;}}
    }
}