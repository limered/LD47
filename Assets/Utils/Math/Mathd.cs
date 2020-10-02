// Type: UnityEngine.Mathd
// Assembly: UnityEngine, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Assembly location: C:\Program Files (x86)\Unity\Editor\Data\Managed\UnityEngine.dll

using UnityEngine;

namespace Utils.Math { 
    public struct Mathd {
        // ReSharper disable once InconsistentNaming
        public const double PI = 3.14159265358979;
        public const double Infinity = double.PositiveInfinity;
        public const double NegativeInfinity = double.NegativeInfinity;
        public const double Deg2Rad = PI/180.0;
        public const double Rad2Deg = 180.0/PI;
        public const double Epsilon = double.Epsilon;

        public static double Sin(double d) {
            return System.Math.Sin(d);
        }

        public static double Cos(double d) {
            return System.Math.Cos(d);
        }

        public static double Tan(double d) {
            return System.Math.Tan(d);
        }

        public static double Asin(double d) {
            return System.Math.Asin(d);
        }

        public static double Acos(double d) {
            return System.Math.Acos(d);
        }

        public static double Atan(double d) {
            return System.Math.Atan(d);
        }

        public static double Atan2(double y, double x) {
            return System.Math.Atan2(y, x);
        }

        public static double Sqrt(double d) {
            return System.Math.Sqrt(d);
        }

        public static double Abs(double d) {
            return System.Math.Abs(d);
        }

        public static int Abs(int value) {
            return System.Math.Abs(value);
        }

        public static double Min(double a, double b)
        {
            return a < b ? a : b;
        }

        public static double Min(params double[] values) {
            var length = values.Length;
            if (length == 0)
                return 0.0d;
            var num = values[0];
            for (var index = 1; index < length; ++index) {
                if (values[index] < num)
                    num = values[index];
            }
            return num;
        }

        public static int Min(int a, int b)
        {
            return a < b ? a : b;
        }

        public static int Min(params int[] values) {
            var length = values.Length;
            if (length == 0)
                return 0;
            var num = values[0];
            for (var index = 1; index < length; ++index) {
                if (values[index] < num)
                    num = values[index];
            }
            return num;
        }

        public static double Max(double a, double b)
        {
            return a > b ? a : b;
        }

        public static double Max(params double[] values) {
            var length = values.Length;
            if (length == 0)
                return 0d;
            var num = values[0];
            for (var index = 1; index < length; ++index) {
                if (values[index] > num)
                    num = values[index];
            }
            return num;
        }

        public static int Max(int a, int b)
        {
            return a > b ? a : b;
        }

        public static int Max(params int[] values) {
            var length = values.Length;
            if (length == 0)
                return 0;
            var num = values[0];
            for (var index = 1; index < length; ++index) {
                if (values[index] > num)
                    num = values[index];
            }
            return num;
        }

        public static double Pow(double d, double p) {
            return System.Math.Pow(d, p);
        }

        public static double Exp(double power) {
            return System.Math.Exp(power);
        }

        public static double Log(double d, double p) {
            return System.Math.Log(d, p);
        }

        public static double Log(double d) {
            return System.Math.Log(d);
        }

        public static double Log10(double d) {
            return System.Math.Log10(d);
        }

        public static double Ceil(double d) {
            return System.Math.Ceiling(d);
        }

        public static double Floor(double d) {
            return System.Math.Floor(d);
        }

        public static double Round(double d) {
            return System.Math.Round(d);
        }

        public static int CeilToInt(double d) {
            return (int)System.Math.Ceiling(d);
        }

        public static int FloorToInt(double d) {
            return (int)System.Math.Floor(d);
        }

        public static int RoundToInt(double d) {
            return (int)System.Math.Round(d);
        }

        public static double Sign(double d) {
            return d >= 0.0 ? 1d : -1d;
        }

        public static double Clamp(double value, double min, double max) {
            if (value < min)
                value = min;
            else if (value > max)
                value = max;
            return value;
        }

        public static int Clamp(int value, int min, int max) {
            if (value < min)
                value = min;
            else if (value > max)
                value = max;
            return value;
        }

        public static double Clamp01(double value) {
            if (value < 0.0)
                return 0.0d;
            return value > 1.0 ? 1d : value;
        }

        public static double Lerp(double from, double to, double t) {
            return from + (to - from) * Clamp01(t);
        }

        public static double LerpAngle(double a, double b, double t) {
            var num = Repeat(b - a, 360d);
            if (num > 180.0d)
                num -= 360d;
            return a + num * Clamp01(t);
        }

        public static double MoveTowards(double current, double target, double maxDelta)
        {
            if (Abs(target - current) <= maxDelta)
                return target;
            return current + Sign(target - current) * maxDelta;
        }

        public static double MoveTowardsAngle(double current, double target, double maxDelta) {
            target = current + DeltaAngle(current, target);
            return MoveTowards(current, target, maxDelta);
        }

        public static double SmoothStep(double from, double to, double t) {
            t = Clamp01(t);
            t = (-2.0 * t * t * t + 3.0 * t * t);
            return to * t + from * (1.0 - t);
        }

        public static double Gamma(double value, double absmax, double gamma) {
            var flag = value < 0.0;
            var num1 = Abs(value);
            if (num1 > absmax)
            {
                if (flag)
                    return -num1;
                return num1;
            }
            var num2 = Pow(num1 / absmax, gamma) * absmax;
            if (flag)
                return -num2;
            return num2;
        }

        public static bool Approximately(double a, double b) {
            return Abs(b - a) < Max(1E-06d * Max(Abs(a), Abs(b)), 1.121039E-44d);
        }

        public static double SmoothDamp(double current, double target, ref double currentVelocity, double smoothTime, double maxSpeed) {
            double deltaTime = Time.deltaTime;
            return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
        }

        public static double SmoothDamp(double current, double target, ref double currentVelocity, double smoothTime) {
            double deltaTime = Time.deltaTime;
            const double maxSpeed = double.PositiveInfinity;
            return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
        }

        public static double SmoothDamp(double current, double target, ref double currentVelocity, double smoothTime, double maxSpeed, double deltaTime) {
            smoothTime = Max(0.0001d, smoothTime);
            var num1 = 2d / smoothTime;
            var num2 = num1 * deltaTime;
            var num3 = (1.0d / (1.0d + num2 + 0.479999989271164d * num2 * num2 + 0.234999999403954d * num2 * num2 * num2));
            var num4 = current - target;
            var num5 = target;
            var max = maxSpeed * smoothTime;
            var num6 = Clamp(num4, -max, max);
            target = current - num6;
            var num7 = (currentVelocity + num1 * num6) * deltaTime;
            currentVelocity = (currentVelocity - num1 * num7) * num3;
            var num8 = target + (num6 + num7) * num3;
            if (num5 - current > 0.0 != num8 > num5)
            {
                return num8;
            }
            num8 = num5;
            currentVelocity = (num8 - num5) / deltaTime;
            return num8;
        }

        public static double SmoothDampAngle(double current, double target, ref double currentVelocity, double smoothTime, double maxSpeed) {
            double deltaTime = Time.deltaTime;
            return SmoothDampAngle(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
        }

        public static double SmoothDampAngle(double current, double target, ref double currentVelocity, double smoothTime) {
            double deltaTime = Time.deltaTime;
            var maxSpeed = double.PositiveInfinity;
            return SmoothDampAngle(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
        }

        public static double SmoothDampAngle(double current, double target, ref double currentVelocity, double smoothTime, double maxSpeed, double deltaTime) {
            target = current + DeltaAngle(current, target);
            return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
        }

        public static double Repeat(double t, double length) {
            return t - Floor(t / length) * length;
        }

        public static double PingPong(double t, double length) {
            t = Repeat(t, length * 2d);
            return length - Abs(t - length);
        }

        public static double InverseLerp(double from, double to, double value) {
            if (from < to) {
                if (value < from)
                    return 0d;
                if (value > to)
                    return 1d;
                value -= from;
                value /= to - from;
                return value;
            }
            if (from <= to)
                return 0d;
            if (value < to)
                return 1d;
            if (value > from)
                return 0d;
            return (1.0d - (value - to) / (from - to));
        }

        public static double DeltaAngle(double current, double target) {
            var num = Repeat(target - current, 360d);
            if (num > 180.0d)
                num -= 360d;
            return num;
        }

        internal static bool LineIntersection(Vector2d p1, Vector2d p2, Vector2d p3, Vector2d p4, ref Vector2d result) {
            var num1 = p2.x - p1.x;
            var num2 = p2.y - p1.y;
            var num3 = p4.x - p3.x;
            var num4 = p4.y - p3.y;
            var num5 = num1 * num4 - num2 * num3;
            if (System.Math.Abs(num5) < Epsilon)
                return false;
            var num6 = p3.x - p1.x;
            var num7 = p3.y - p1.y;
            var num8 = (num6 * num4 - num7 * num3) / num5;
            result = new Vector2d(p1.x + num8 * num1, p1.y + num8 * num2);
            return true;
        }

        internal static bool LineSegmentIntersection(Vector2d p1, Vector2d p2, Vector2d p3, Vector2d p4, ref Vector2d result) {
            var num1 = p2.x - p1.x;
            var num2 = p2.y - p1.y;
            var num3 = p4.x - p3.x;
            var num4 = p4.y - p3.y;
            var num5 = (num1 * num4 - num2 * num3);
            if (System.Math.Abs(num5) < Epsilon)
                return false;
            var num6 = p3.x - p1.x;
            var num7 = p3.y - p1.y;
            var num8 = (num6 * num4 - num7 * num3) / num5;
            if (num8 < 0.0d || num8 > 1.0d)
                return false;
            var num9 = (num6 * num2 - num7 * num1) / num5;
            if (num9 < 0.0d || num9 > 1.0d)
                return false;
            result = new Vector2d(p1.x + num8 * num1, p1.y + num8 * num2);
            return true;
        }
    }
}
