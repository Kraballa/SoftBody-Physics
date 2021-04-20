using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace PhysicsEngine
{
    public class World : IComparer<Entity>
    {
        public static Vector2 G = new Vector2(0, 9.80665f);
        public static bool Walled = true;
        public static float Step = 1 / 35f;

        public List<Entity> Entities = new List<Entity>();
        private bool sorted = true;

        private Polygon poly;

        public World()
        {
            Add(new SoftBody(12, 8, new RectangleF(100, 100, 300, 200)));
            Add(poly = new Polygon(new Vector2(0, 500), new Vector2(800, 630), new Vector2(20, 650)));
        }

        public void Update()
        {
            MInput.Update();
            KInput.Update();

            if (!sorted)
            {
                Entities.Sort(this);
                sorted = true;
            }

            //updating in 3 steps
            Entities.ForEach((o) => o.BeforeUpdate());
            Entities.ForEach((o) => o.Update());
            Entities.ForEach((o) => o.AfterUpdate());
        }

        public void Draw()
        {
            Entities.ForEach((o) => o.Draw());


            Render.Circle(poly.ClosestPoint(MInput.Position.ToVector2()), 10, Color.Red, 3f, 3);
        }

        public void Add(Entity ent)
        {
            Entities.Add(ent);
            ent.Added(this);
            sorted = false;
        }

        public int Compare(Entity x, Entity y)
        {
            return Math.Sign(y.Depth - x.Depth);
        }
    }
}
