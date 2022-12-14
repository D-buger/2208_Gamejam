using System.Linq;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SOO
{
    public static class Util
    {
        public static Vector3[] ToVector3(this Vector2[] vectors)
            => System.Array.ConvertAll<Vector2, Vector3>(vectors, v => v);

        public static Vector2 Centroid(this ICollection<Vector2> vectors)
            => vectors.Aggregate((agg, next) => agg + next) / vectors.Count();

        public static void Set(this Vector2 vector, Vector2 newVector) => vector = newVector;

        public static Vector2 Abs(this Vector2 vector)
            => new Vector2(Mathf.Abs(vector.x), Mathf.Abs(vector.y));

        public static float LengthSq(this Vector2 vector)
            => vector.x * vector.x + vector.y * vector.y;

        public static Vector2 neg(this Vector2 vector)
            => -vector;

        public static Vector2 Cross(Vector2 vector, float a, Vector2 output)
        {
            output.x = vector.y * a;
            output.y = vector.x * -a;
            return output;
        }

        public static Vector2 Cross(float a, Vector2 vector, Vector2 output)
        {
            output.x = vector.y * -a;
            output.y = vector.x * a;
            return output;
        }

        public static float Cross(Vector2 a, Vector2 b)
            => a.x * b.y - a.y * b.x;

        public static float Cross(Vector2 a, Vector2 b, Vector2 c)
            => (c.y - a.y) * (b.x - a.x) - (c.x - a.x) * (b.y - a.y);

        public static float Dot(Vector2 a, Vector2 b)
            => a.x * b.x + a.y * b.y;

        public static float Dot(Vector2 a, Vector2 b, Vector2 c)
            => (c.x - a.x) * (b.x - a.x) + (c.y - a.y) * (b.y - a.y);


        public static float Distance(Vector2 a, Vector2 b)
            => (a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y);

        public static float IsBetween(Vector2 a, Vector2 b, Vector2 c)
        {
            float result = Distance(a, b) + Distance(b, c) - Distance(a, c);
            return result;
            //return -Mathf.Epsilon < result && result < Mathf.Epsilon;
        }

        public static float distanceSq(Vector2 a, Vector2 b)
        {
            float dx = a.x - b.x;
            float dy = a.y - b.y;

            return dx * dx + dy * dy;
        }

        //???? ???????? ?????? ???? ?????? (?????? ????????)?? ?????? ?? ????.

        //string?? ?????????? ???????? ?????????? ???? ???????? ?????? ????????.
        // + ?????? ?????????? ???? ?????? ?????????? ?????? ?????? ?????? string?????? ?????? ??????
        public static string StringBuilder(params string[] str)
        {
            StringBuilder strBuilder = new StringBuilder(str[0]);

            if (str.Length <= 1)
                return strBuilder.ToString();

            for (int i = 1; i < str.Length; i++)
                strBuilder.Append(str[i]);

            return strBuilder.ToString();
        }

        public static string StringBuilder(string str, int i)
        {
            StringBuilder strBuilder = new StringBuilder(str);
            return strBuilder.Append(i).ToString();
        }

        public static T[] ListToArray<T>(this List<T> list)
        {
            T[] array = new T[list.Count];

            for (int i = 0; i < list.Count; i++)
            {
                array[i] = list[i];
            }

            return array;
        }

        public static List<T> ArrayToList<T>(this T[] array)
        {
            List<T> list = new List<T>(array.Length);

            for (int i = 0; i < array.Length; i++)
            {
                list.Add(array[i]);
            }

            return list;
        }

        public static Vector2 AngleToVector(float angle)
        {
            angle *= Mathf.Deg2Rad;

            Vector2 vec =
                new Vector2(
                    -Mathf.Sin(angle),
                    Mathf.Cos(angle)
                    );

            return vec;
        }

        /// <summary>
        /// ?????? ????
        /// </summary>
        /// <param name="pointCount"></param>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static Vector2[] CurvePointsOfVectors(int pointCount, params Vector2[] vec)
        {
            Vector2[] points = new Vector2[pointCount + 1];
            float unit = 1.0f / pointCount;

            int n = vec.Length - 1;
            float[] t = new float[n];
            float[] u = new float[n];
            t[0] = 0f;
            for(int i = 0; i < pointCount + 1; i++, t[0] += unit)
            {
                u[0] = (1 - t[0]);
                for(int j = 1; j < n; j++)
                {
                    t[j] = t[0] * t[j - 1];
                    u[j] = u[0] * u[j - 1];
                }

                points[i] =
                    vec[0] * u[n - 1] + vec[n] * t[n - 1];
                for(int j =1; j < n; j++)
                {
                    points[i] += vec[j] * (t[j - 1] * u[n - 1 - j] * n);
                }
            }

            return points;
        }

        public static Sprite TextureToSprite(this Texture2D texture)
           => Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        public static Sprite TextureToSprite(this Texture2D texture, Rect rect)
           => Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));

    }
}