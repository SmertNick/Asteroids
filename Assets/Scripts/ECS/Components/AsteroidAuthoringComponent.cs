using Unity.Entities;

namespace ECS.Components
{
    [GenerateAuthoringComponent]
    public struct AsteroidAuthoringComponent : IComponentData
    {
        public Entity Prefab;
    }
}
