using Raylib_cs;
using System.Numerics;
using System;

namespace RaytracerSharp {
    public class Box : Hittable {
        public Material Material;
        public Vector3 Position {get;}
        public Vector3 Size {get;}
        private BoundingBox boundingBox;

        public Box(Vector3 position, Vector3 size, Material material) {
            Material = material;
            Position = position;
            Size = size;
            boundingBox = new BoundingBox(Position-Size/2, Position+Size/2);
        }

        public override HitRecord Hit(Ray ray, float t_min, float t_max) {
            RayCollision rayCollision = Raylib.GetRayCollisionBox(ray, boundingBox);
            if (rayCollision.hit && rayCollision.distance > t_min && rayCollision.distance < t_max) {
                //todo: uvs
                return new HitRecord(true, rayCollision, Material, Vector2.Zero);
            }
            return new HitRecord(false, rayCollision, Material, Vector2.Zero);
        }
    }
}