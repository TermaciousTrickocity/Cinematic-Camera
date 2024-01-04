using System;
using System.Numerics;

namespace Camera
{
    internal class CameraEasing
    {
        public class Easing
        {
            // Catmull-Rom interpolation for 3D vectors (X, Y, Z)
            public static Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
            {
                // Calculate powers of t for interpolation
                float t2 = t * t;
                float t3 = t2 * t;

                // Calculate control point derivatives
                Vector3 v0 = (p2 - p0) * 0.5f;
                Vector3 v1 = (p3 - p1) * 0.5f;

                // Use Catmull-Rom formula to interpolate
                return (2 * p1 - 2 * p2 + v0 + v1) * t3 + (-3 * p1 + 3 * p2 - 2 * v0 - v1) * t2 + v0 * t + p1;
            }

            // Catmull-Rom interpolation for single floats (e.g., FOV)
            public static float CatmullRomFOV(float p0, float p1, float p2, float p3, float t)
            {
                // Catmull-Rom interpolation for FOV
                float t2 = t * t;
                float t3 = t2 * t;

                // Calculate control point derivatives
                float v0 = (p2 - p0) * 0.5f;
                float v1 = (p3 - p1) * 0.5f;

                // Use Catmull-Rom formula to interpolate
                return (2 * p1 - 2 * p2 + v0 + v1) * t3 + (-3 * p1 + 3 * p2 - 2 * v0 - v1) * t2 + v0 * t + p1;
            }

            // Catmull-Rom interpolation for angles (e.g., yaw, pitch, roll)
            public static float CatmullRomAngle(float p0, float p1, float p2, float p3, float t)
            {
                // Compute the tangents at p1 and p2
                float m1 = (p2 - p0) / 2.0f;
                float m2 = (p3 - p1) / 2.0f;

                // Calculate the Catmull-Rom coefficients
                float a = 2 * p1 - 2 * p2 + m1 + m2;
                float b = -3 * p1 + 3 * p2 - 2 * m1 - m2;
                float c = m1;
                float d = p1;

                // Interpolate using the Catmull-Rom formula
                return a * t * t * t + b * t * t + c * t + d;
            }

            private float LerpAngle(float a, float b, float t)
            {
                float delta = Math.Abs(b - a);
                if (delta > Math.PI)
                {
                    if (b > a)
                        a += 2 * (float)Math.PI;
                    else
                        b += 2 * (float)Math.PI;
                }
                return a + (b - a) * t;
            }

            // Generic interpolation function for floats
            public static float Interpolate(float a, float b, float t, Func<float, float> EaseInOut)
            {
                return a + (b - a) * EaseInOut(t);
            }

            // Interpolate between two 3D vectors
            public static (float, float, float) Interpolate3D((float, float, float) start, (float, float, float) end, float t, Func<float, float> EaseInOut)
            {
                float x = Interpolate(start.Item1, end.Item1, t, EaseInOut);
                float y = Interpolate(start.Item2, end.Item2, t, EaseInOut);
                float z = Interpolate(start.Item3, end.Item3, t, EaseInOut);
                return (x, y, z);
            }
        }

    }
}
