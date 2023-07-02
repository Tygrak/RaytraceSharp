using Raylib_cs;
using System.Numerics;

namespace RaytracerSharp {
    public static class Helper {
        public static float RandomFloat(Random random, float min, float max) {
            return Raymath.Lerp(min, max, random.NextSingle());
        }

        public static float RandomFloat(Random random) {
            return random.NextSingle();
        }

        public static Color ColorFromVector(Vector3 color) {
            color = Raymath.Vector3ClampValue(color, 0, 1);
            return new Color((int) MathF.Floor(color.X*255f), (int) MathF.Floor(color.Y*255f), (int) MathF.Floor(color.Z*255f), 255);
        }

        public static Vector3 RandomInUnitSphere(Random random) {
            while (true) {
                Vector3 p = new Vector3(RandomFloat(random, -1, 1), RandomFloat(random, -1, 1), RandomFloat(random, -1, 1));
                if (p.LengthSquared() >= 1) {
                    continue;
                }
                return p;
            }
        }

        public static Vector3 RandomUnitVector(Random random) {
            return Vector3.Normalize(RandomInUnitSphere(random));
        }

        public static Vector3 RandomInHemisphere(Random random, Vector3 normal) {
            Vector3 inUnitSphere = RandomInUnitSphere(random);
            if (Vector3.Dot(inUnitSphere, normal) > 0.0) {
                return inUnitSphere;
            } else {
                return -inUnitSphere;
            }
        }
    }
}