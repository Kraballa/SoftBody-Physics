using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsEngine
{
    public class Polygon : Entity
    {
        public RectangleF Bounds;

        public List<Vector2> Vertices = new List<Vector2>();

        public Polygon(params Vector2[] vertices)
        {
            foreach (Vector2 v in vertices)
                Vertices.Add(v);


            float leftmost = Vertices[0].X;
            float upmost = Vertices[0].Y;
            float downmost = Vertices[0].X;
            float rightmost = Vertices[0].X;

            foreach (Vector2 vec in Vertices)
            {
                if (vec.X > rightmost)
                    rightmost = vec.X;
                if (vec.X < leftmost)
                    leftmost = vec.X;
                if (vec.Y > downmost)
                    downmost = vec.Y;
                if (vec.Y < upmost)
                    upmost = vec.Y;

            }

            Bounds = new RectangleF(leftmost, upmost, rightmost - leftmost, downmost - upmost);
        }

        public override void Draw()
        {
            base.Draw();

            Render.HollowRect(Bounds.Position, Bounds.Width, Bounds.Height, Color.Red);

            for (int i = 0; i < Vertices.Count - 1; i++)
            {
                Render.Line(Vertices[i], Vertices[i + 1], Color.Black, 3f);
            }
            Render.Line(Vertices[0], Vertices[Vertices.Count - 1], Color.Black, 3f);
        }
    }
}
