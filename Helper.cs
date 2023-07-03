using Raylib_cs;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace RaytracerSharp {
    public static class Helper {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float RandomFloat(Random random, float min, float max) {
            return Raymath.Lerp(min, max, random.NextSingle());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float RandomFloat(Random random) {
            return random.NextSingle();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ColorFromVector(Vector3 color) {
            color = Raymath.Vector3Clamp(color, Vector3.Zero, Vector3.One);
            return new Color((int) MathF.Floor(color.X*255f), (int) MathF.Floor(color.Y*255f), (int) MathF.Floor(color.Z*255f), 255);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 RandomInUnitSphere(Random random) {
            /*while (true) {
                Vector3 p = new Vector3(RandomFloat(random, -1, 1), RandomFloat(random, -1, 1), RandomFloat(random, -1, 1));
                if (p.LengthSquared() >= 1) {
                    continue;
                }
                return p;
            }*/
            float u = random.NextSingle();
            float v = random.NextSingle();
            float theta = u * 2.0f * MathF.PI;
            float phi = MathF.Acos(2.0f * v - 1.0f);
            float r = MathF.Pow(random.NextSingle(), 1.0f/3.0f);
            float sinTheta = MathF.Sin(theta);
            float cosTheta = MathF.Cos(theta);
            float sinPhi = MathF.Sin(phi);
            float cosPhi = MathF.Cos(phi);
            float x = r * sinPhi * cosTheta;
            float y = r * sinPhi * sinTheta;
            float z = r * cosPhi;
            return new Vector3(x, y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 RandomUnitVector(Random random) {
            float z = 2f * random.NextSingle() - 1f;
            float theta = 2f * MathF.PI * random.NextSingle();
            float radius = MathF.Sqrt(1f - z * z);
            float x = radius * MathF.Cos(theta);
            float y = radius * MathF.Sin(theta);
            return new Vector3(x, y, z);
            /*
            float z = 2f * random.NextSingle() - 1f;
            float theta = 2f * MathF.PI * random.NextSingle();
            float radius = MathF.Sqrt(1f - z * z);
            float x = radius * MathF.Cos(theta);
            float y = radius * MathF.Sin(theta);
            return new Vector3(x, y, z);
            */
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 RandomInHemisphere(Random random, Vector3 normal) {
            Vector3 inUnitSphere = RandomUnitVector(random);
            if (Vector3.Dot(inUnitSphere, normal) > 0.0) {
                return inUnitSphere;
            } else {
                return -inUnitSphere;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Refract(Vector3 uv, Vector3 n, float etaiOverEtat) {
            float cosTheta = MathF.Min(Vector3.Dot(-uv, n), 1.0f);
            Vector3 rOutPerp =  etaiOverEtat * (uv + cosTheta*n);
            Vector3 rOutParallel = -MathF.Sqrt(MathF.Abs(1.0f - rOutPerp.LengthSquared())) * n;
            return rOutPerp + rOutParallel;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsFrontFace(Vector3 direction, Vector3 normal) {
            return Vector3.Dot(direction, normal) < 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SchlickReflectance(float cosine, float refIdx) {
            // Use Schlick's approximation for reflectance.
            float r0 = (1-refIdx) / (1+refIdx);
            r0 = r0*r0;
            return r0 + (1-r0)*MathF.Pow((1 - cosine), 5.0f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 FromVector4(Vector4 vector4) {
            return new Vector3(vector4.X, vector4.Y, vector4.Z);
        }
    }
}