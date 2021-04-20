using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace PhysicsEngine
{
    public class World : IComparer<Entity>
    {
        public static Vector2 G = new Vector2(0, 98.0665f);
        public static bool Walled = true;
        public static float Step = 1 / 60f;

        public List<Entity> Entities = new List<Entity>();
        private bool sorted = true;

        public World()
        {
            Add(new SoftBody(6, 4, new RectangleF(100, 50, 300, 200)));
            Add(new Polygon(new Vector2(0, 300), new Vector2(500, 630), new Vector2(700, 630), new Vector2(700, 650), new Vector2(20, 650)));
            Add(new Polygon(new Vector2(900, 400), new Vector2(900, 500), new Vector2(1100, 600), new Vector2(1100, 300)));

            Add(new Polygon(new Vector2(30, 30), new Vector2(1250, 30), new Vector2(1250, 690), new Vector2(30, 690), new Vector2(30, 30),
                new Vector2(1, 1), new Vector2(1, 719), new Vector2(1279, 719), new Vector2(1279, 1), new Vector2(1, 1)));
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
