using Raylib_cs;
using System.Numerics;
using System;

namespace RaytracerSharp {
    public class Scene {
        public Camera3D DefaultCamera = new Camera3D(new Vector3(25f, 15f, -1.5f), new Vector3(0, 0, 0), new Vector3(0, 1, 0), 45, CameraProjection.CAMERA_PERSPECTIVE);
        public List<Hittable> Objects = new List<Hittable>();
        public Func<Vector3, Vector3> SkyFunction = (Vector3 x) => Vector3.Zero;

        public Scene(List<Hittable> objects, Func<Vector3, Vector3> skyFunction, Camera3D camera) {
            Objects = objects;
            SkyFunction = skyFunction;
            DefaultCamera = camera;
        }

        public Scene(List<Hittable> objects, Func<Vector3, Vector3> skyFunction) {
            Objects = objects;
            SkyFunction = skyFunction;
        }
    }
}