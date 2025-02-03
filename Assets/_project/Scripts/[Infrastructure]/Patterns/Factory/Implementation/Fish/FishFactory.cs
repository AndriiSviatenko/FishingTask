using _project.Scripts.Patterns.Factory.Core;

namespace _project.Scripts.Patterns.Factory.Implementation.Fish
{
    public class FishFactory : GenericFactory<Scripts.Fish.Fish>
    {
        public FishFactory(Scripts.Fish.Fish prefab) : base(prefab)
        {
        }
    }
}