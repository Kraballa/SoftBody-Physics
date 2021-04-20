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

            foreach (Entity ent in World.Entities)
            {
                if (ent is Node)
                {
                    Node other = ent as Node;
                    float dist = Vector2.Distance(other.Position, Position);

                    if (dist < Size)
                    {
                        //Position += (other.Position - Position) * (5 - dist);
                    }
                }
            }

            Force = Vector2.Zero;
            Force += Mass * World.G;


        }

        public override void Update()
        {
            base.Update();
            if (MInput.LeftClick())
            {
                Force += Math.Max(Vector2.Distance(MInput.Position.ToVector2(), Position), 0.1f) * (MInput.Position.ToVector2() - Position) * World.Step;
            }
        }

        public override void AfterUpdate()
        {
            base.AfterUpdate();
            //Euler Integration
            Velocity += Force * World.Step / Mass;
            Position += Velocity * World.Step;
        }

        public override void Draw()
        {
            base.Draw();
            Render.Rect(Position - Vector2.One * 5, 10, 10, Color.Red);
        }

        public bool Collide()
        {

            return false;
        }
    }
}
