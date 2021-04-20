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
        public float Stiffness = 100f;
        public float DampingFactor = 5f;

        public Spring(Node a, Node b) : this(a, b, Vector2.Distance(a.Position, b.Position))
        {

        }

        public Spring(Node a, Node b, float desiredLength)
        {
            A = a;
            B = b;
            RestLength = desiredLength;
            Depth = 1;
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
