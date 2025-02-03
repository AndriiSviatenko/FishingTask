using _project.Scripts.Patterns.Factory.Core;

namespace _project.Scripts.Patterns.Factory.Implementation.Bobber
{
    public class BobberFactory : GenericFactory<Scripts.Bobber>
    {
        public BobberFactory(Scripts.Bobber prefab) : base(prefab)
        {
            
        }
    }
}