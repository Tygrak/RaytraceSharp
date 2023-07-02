using Raylib_cs;
using System.Numerics;
using System;

namespace RaytracerSharp {
    public class Triangle : Hittable {
        public Material Material;
        public Vector3 Vertex1;
        public Vector3 Vertex2;
        public Vector3 Vertex3;

        public Triangle(Vector3 vertex1, Vector3 vertex2, Vector3 vertex3, Material material) {
            Material = material;
            Vertex1 = vertex1;
            Vertex2 = vertex2;
            Vertex3 = vertex3;
        }

        public override HitRecord Hit(Ray ray, float t_min, float t_max) {
            RayCollision rayCollision = Raylib.GetRayCollisionTriangle(ray, Vertex1, Vertex2, Vertex3);
            if (rayCollision.hit && rayCollision.distance > t_min && rayCollision.distance < t_max) {
                //todo: uvs
                return new HitRecord(true, rayCollision, Material, Vector2.Zero);
            }
            return new HitRecord(false, rayCollision, Material, Vector2.Zero);
        }
    }
}