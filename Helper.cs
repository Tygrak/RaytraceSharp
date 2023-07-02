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
            color = Raymath.Vector3Clamp(color, Vector3.Zero, Vector3.One);
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

        public static Vector3 Refract(Vector3 uv, Vector3 n, float etaiOverEtat) {
            float cosTheta = MathF.Min(Vector3.Dot(-uv, n), 1.0f);
            Vector3 rOutPerp =  etaiOverEtat * (uv + cosTheta*n);
            Vector3 rOutParallel = -MathF.Sqrt(MathF.Abs(1.0f - rOutPerp.LengthSquared())) * n;
            return rOutPerp + rOutParallel;
        }

        public static bool IsFrontFace(Vector3 direction, Vector3 normal) {
            return Vector3.Dot(direction, normal) < 0;
        }

        public static float SchlickReflectance(float cosine, float refIdx) {
            // Use Schlick's approximation for reflectance.
            float r0 = (1-refIdx) / (1+refIdx);
            r0 = r0*r0;
            return r0 + (1-r0)*MathF.Pow((1 - cosine), 5.0f);
        }

        public static Vector3 FromVector4(Vector4 vector4) {
            return new Vector3(vector4.X, vector4.Y, vector4.Z);
        }
    }
}