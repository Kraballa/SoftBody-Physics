using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsEngine
{
    public class SoftBody : Entity
    {

        private int NumX;
        private int NumY;
        private Rectangle Rect;

        public SoftBody(int numX, int numY, Rectangle rect)
        {
            NumX = numX;
            NumY = numY;
            Rect = rect;
        }

        public override void Added(World world)
        {
            base.Added(world);

            Node[,] nodes = new Node[NumX, NumY];

            Vector2 delta = new Vector2((float)Rect.Width / (NumX + 1), (float)Rect.Height / (NumY + 1));

            for (int x = 0; x < NumX; x++)
            {
                for (int y = 0; y < NumY; y++)
                {
                    Vector2 pos = Rect.Location.ToVector2() + new Vector2(delta.X * x, delta.Y * y);
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
