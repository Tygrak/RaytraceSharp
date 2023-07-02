using Raylib_cs;
using System.Numerics;
using System;

namespace RaytracerSharp {
    public class Scene {
        public List<Hittable> Objects = new List<Hittable>();
        public Func<Vector3, Vector3> SkyFunction = (Vector3 x) => Vector3.Zero;

        public Scene(List<Hittable> objects, Func<Vector3, Vector3> skyFunction) {
            Objects = objects;
            SkyFunction = skyFunction;
        }
    }
}