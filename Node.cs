using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsEngine
{
    public class Node : Entity
    {
        public static float Size = 5f;

        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Force;
        public float Mass = 1;

        private bool collided = false;
        private Vector2 edgeVertice;

        public Node(Vector2 pos)
        {
            Position = pos;
        }

        public Node(float x, float y) : this(new Vector2(x, y))
        {

        }

        public override void BeforeUpdate()
        {
            base.BeforeUpdate();
            collided = false;
            Force = Vector2.Zero;
            Force += Mass * World.G;

        }

        public override void Update()
        {
            base.Update();
            if (MInput.LeftClick())
            {
                Force += Math.Min(Vector2.Distance(MInput.Position.ToVector2(), Position), 50) * (MInput.Position.ToVector2() - Position) * World.Step;
            }
        }

        public override void AfterUpdate()
        {
            base.AfterUpdate();
            //Euler Integration
            Velocity += Force * World.Step / Mass;
            //if point is within polygon, push it out to the closest edge and rebound velocity along the normalized pussh vector
            Vector2 delta = Vector2.Zero;
            if (collided)
            {
                delta = edgeVertice - Position;
                //Position = edgeVertice;

                if (delta.X * Velocity.X + delta.Y * Velocity.Y < 0)
                {
                    delta.Normalize();
                    //rebound
                    Velocity = Velocity - 2 * delta * (Velocity.X * delta.X + Velocity.Y * delta.Y);
                }
            }
            Position += Velocity * World.Step;
            //Velocity += delta;
        }

        public override void Draw()
        {
            base.Draw();
            if (collided)
            {
                Render.Rect(edgeVertice - Vector2.One * 5, 10, 10, Color.Red);
            }
            else
            {
                Render.Rect(Position - Vector2.One * 5, 10, 10, Color.Red);
            }
        }

        public void ProcessCollision(Polygon poly)
        {
            if (poly.Contains(Position))
            {
                collided = true;

                edgeVertice = poly.ClosestPoint(Position);
            }
        }
    }
}
