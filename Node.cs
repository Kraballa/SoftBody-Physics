using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsEngine
{
    public class Node : Entity
    {
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
            Force = Vector2.Zero;
            Force += Mass * World.G;

            if (MInput.LeftClick())
            {
                Force += Vector2.Distance(MInput.Position.ToVector2(), Position) * (MInput.Position.ToVector2() - Position) * (1 / 60f);
            }
        }

        public override void Update()
        {
            base.Update();

        }

        public override void AfterUpdate()
        {
            base.AfterUpdate();
            //Euler Integration
            Velocity += Force * (1 / 60f) / Mass;
            Position += Velocity * (1 / 60f);
        }

        public override void Draw()
        {
            base.Draw();
            Render.Rect(Position - Vector2.One * 5, 10, 10, Color.Red);
        }
    }
}
