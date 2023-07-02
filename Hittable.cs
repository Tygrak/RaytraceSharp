using Raylib_cs;
using System.Numerics;
using System;

namespace RaytracerSharp {
    public abstract class Hittable {
        public abstract HitRecord Hit(Ray ray, float t_min, float t_max);
    }
}