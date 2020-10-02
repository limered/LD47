using System;

namespace SystemBase
{
    [AttributeUsage(AttributeTargets.Class)]
    public class GameSystemAttribute : Attribute
    {
        public Type[] Dependencies { get; set; }

        public GameSystemAttribute(params Type[] dependencies)
        {
            Dependencies = dependencies;
        }

        public GameSystemAttribute()
        {
            Dependencies = new Type[0];
        }
    }
}
