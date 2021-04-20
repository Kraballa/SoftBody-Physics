using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsEngine
{
    public class World
    {
        public const float G = 0.981f;

        public List<Entity> Objects = new List<Entity>();

        public World()
        {

        }

        public void Update()
        {
            Objects.ForEach((o) => o.Update());
        }

        public void Render()
        {
            Objects.ForEach((o) => o.Draw());
        }
    }
}
