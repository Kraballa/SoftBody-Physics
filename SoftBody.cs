using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsEngine
{
    public class SoftBody : Entity
    {
        List<Node> Nodes = new List<Node>();
        public RectangleF Bounds;
        private int NumX;
        private int NumY;

        public SoftBody(int numX, int numY, RectangleF rect)
        {
            Depth = 4;

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
                    Nodes.Add(nodes[x, y]);
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

        public override void BeforeUpdate()
        {
            base.BeforeUpdate();

            float leftmost = Nodes[0].Position.X;
            float upmost = Nodes[0].Position.Y;
            float downmost = Nodes[0].Position.Y;
            float rightmost = Nodes[0].Position.X;

            foreach (Node node in Nodes)
            {
                if (node.Position.X > rightmost)
                    rightmost = node.Position.X;
                if (node.Position.X < leftmost)
                    leftmost = node.Position.X;
                if (node.Position.Y > downmost)
                    downmost = node.Position.Y;
                if (node.Position.Y < upmost)
                    upmost = node.Position.Y;

            }

            Bounds = new RectangleF(leftmost, upmost, rightmost - leftmost, downmost - upmost);
        }

        public override void Update()
        {
            base.Update();
            //collide with polygon
            foreach (Entity ent in World.Entities)
            {
                if (ent is Polygon)
                {
                    Polygon poly = ent as Polygon;

                    //check bound intersection - optimization
                    if (Bounds.Intersects(poly.Bounds))
                    {
                        foreach (Node n in Nodes)
                        {
                            if (poly.Contains(n.Position))
                            {
                                n.ProcessCollision(poly);
                            }
                        }
                    }
                }
            }
        }

        public override void Draw()
        {
            base.Draw();
            Render.HollowRect(Bounds.Position, Bounds.Width, Bounds.Height, Color.Red);
        }
    }
}
