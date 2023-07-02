using Raylib_cs;
using System.Numerics;
using System;

namespace RaytracerSharp {
    public class RotatedHittable : Hittable {
        public Hittable Hittable;
        private Matrix4x4 untransform;
        private Matrix4x4 transform;
        private Vector3 Translation;

        public RotatedHittable(Hittable hittable, Vector3 eulerRotation, Vector3 translation) {
            Hittable = hittable;
            untransform = Raymath.MatrixRotateXYZ(-eulerRotation);
            transform = Raymath.MatrixRotateXYZ(eulerRotation);
            Translation = translation;
        }

        public override HitRecord Hit(Ray ray, float t_min, float t_max) {
            Vector4 position = Vector4.Transform(ray.position-Translation, untransform);
            Vector4 direction = Vector4.Transform(ray.direction, untransform);
            Ray transformedRay = new Ray(Helper.FromVector4(position), Helper.FromVector4(direction));
            HitRecord result = Hittable.Hit(transformedRay, t_min, t_max);
            result.RayCollision.point = Helper.FromVector4(Vector4.Transform(result.point, transform));
            result.RayCollision.normal = Helper.FromVector4(Vector4.Transform(result.normal, transform));
            return result;
        }
    }
}