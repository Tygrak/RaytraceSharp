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
            return new Color((int) MathF.Floor(color.X*255.999f), (int) MathF.Floor(color.Y*255.999f), (int) MathF.Floor(color.Z*255.999f), 255);
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
    }
}