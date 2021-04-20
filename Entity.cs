using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsEngine
{
    public class Entity
    {

        public World World;
        public int Depth = 0;

        public Entity()
        {

        }

        public virtual void Added(World world)
        {
            World = world;
        }

        public virtual void BeforeUpdate()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void AfterUpdate()
        {

        }

        public virtual void Draw()
        {

        }
    }
}
