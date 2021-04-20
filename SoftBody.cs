using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsEngine
{
    public class SoftBody : Entity
    {
        public RectangleF Bounds;
        private int NumX;
        private int NumY;

        public SoftBody(int numX, int numY, RectangleF rect)
        {
            NumX = numX;
            NumY = numY;
            Bounds = rect;
        }

        public override void Added(World world)
        {
            base.Added(world);

            Node[,] nodes = new Node[NumX, NumY];

            Vector2 delta = new Vector2((float)Bounds.Width / (NumX + 1), (float)Bounds.Height / (NumY + 1));

            for (int x = 0; x < NumX; x++)
            {
                for (int y = 0; y < NumY; y++)
                {
                    Vector2 pos = Bounds.Position + new Vector2(delta.X * x, delta.Y * y);
                    nodes[x, y] = new Node(pos);
                    World.Add(nodes[x, y]);
                }
            }

            for (int x = 0; x < NumX - 1; x++)
            {
                for (int y = 0; y < NumY - 1; y++)
                {
                    World.Add(new Spring(nodes[x, y], nodes[x + 1, y]));
                    World.Add(new Spring(nodes[x, y], nodes[x + 1, y + 1]));
                    World.Add(new Spring(nodes[x, y], nodes[x, y + 1]));
                    World.Add(new Spring(nodes[x + 1, y], nodes[x, y + 1]));
                }
            }

            for (int x = 0; x < NumX - 1; x++)
            {
                World.Add(new Spring(nodes[x, NumY - 1], nodes[x + 1, NumY - 1]));
            }

            for (int y = 0; y < NumY - 1; y++)
            {
                World.Add(new Spring(nodes[NumX - 1, y], nodes[NumX - 1, y + 1]));
            }
        }
    }
}
