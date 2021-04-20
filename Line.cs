using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsEngine
{
    /// <summary>
    /// Describes a Line
    /// 
    /// Line intersection as per "https://martin-thoma.com/how-to-check-if-two-line-segments-intersect/"
    /// </summary>
    public class Line
    {
        public static float EPSILON = 0.000001f;

        public RectangleF Bounds;

        public Vector2 A0;
        public Vector2 B0;

        public Line(Vector2 a, Vector2 b)
        {
            A0 = a;
            B0 = b;

            float x = Math.Min(A0.X, B0.X);
            float y = Math.Min(A0.Y, B0.Y);
            float width = Math.Max(A0.X, B0.X) - x;
            float height = Math.Max(A0.Y, B0.Y) - y;

            Bounds = new RectangleF(x, y, width, height);
        }

        public static float CrossProduct(Vector2 A, Vector2 B)
        {
            return A.X * B.Y - B.X * A.Y;
        }

        public Line Translate(Vector2 vec)
        {
            return new Line(A0 + vec, B0 + vec);
        }

        public bool IsPointOnLine(Vector2 P)
        {
            Line transLine = Translate(-A0);
            Vector2 transP = P - A0;
            double r = CrossProduct(transLine.B0, transP);
            return Math.Abs(r) < EPSILON;
        }

        public bool IsPointRightOfLine(Vector2 P)
        {
            Line transLine = Translate(-A0);
            Vector2 transP = P - A0;
            return CrossProduct(transLine.B0, transP) < 0;
        }

        public bool TouchOrCross(Line other)
        {
            return IsPointOnLine(other.A0) || IsPointOnLine(other.B0) || (IsPointRightOfLine(other.A0) ^ IsPointRightOfLine(other.B0));
        }

        public bool Intersects(Line other)
        {
            return Bounds.Intersects(other.Bounds) && TouchOrCross(other) && other.TouchOrCross(this);
        }

        public bool Intersects(Vector2 A1, Vector2 B1)
        {
            Line other = new Line(A1, B1);
            return Intersects(other);
        }
    }
}
