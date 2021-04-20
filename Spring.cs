using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsEngine
{
    public class Spring : Entity
    {
        public Node A;
        public Node B;
        public float RestLength;
        public float Stiffness = 1f;
        public float DampingFactor = 0.1f;

        public Spring(Node a, Node b)
        {
            A = a;
            B = b;

            RestLength = Vector2.Distance(A.Position, B.Position) + 10;

            Depth = 1;
        }

        public Spring(Node a, Node b, float desiredLength)
        {
            RestLength = desiredLength;
        }

        public override void Update()
        {
            base.Update();

            float springForce = Stiffness * (Vector2.Distance(A.Position, B.Position) - RestLength);

            Vector2 veloBA = NormalizedDifference(B.Position, A.Position);
            Vector2 deltaVelo = B.Velocity - A.Velocity;
            float dampingForce = Vector2.Dot(veloBA, deltaVelo) * DampingFactor;

            float totalSpringForce = springForce + dampingForce;

            A.Force += NormalizedDifference(B.Position, A.Position) * totalSpringForce;
            B.Force += NormalizedDifference(A.Position, B.Position) * totalSpringForce;
        }

        public override void Draw()
        {
            base.Draw();
            Render.Line(A.Position, B.Position, Color.Black);
        }

        public static Vector2 NormalizedDifference(Vector2 a, Vector2 b)
        {
            return (a - b) / Vector2.Distance(a, b);
        }
    }
}
