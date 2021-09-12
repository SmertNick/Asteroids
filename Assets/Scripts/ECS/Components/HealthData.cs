using Unity.Entities;

namespace ECS.Components
{
    [GenerateAuthoringComponent]
    public struct HealthData : IComponentData
    {
        public bool isDead;
    }
}
