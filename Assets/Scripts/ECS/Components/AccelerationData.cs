using Unity.Entities;

namespace ECS.Components
{
    [GenerateAuthoringComponent]
    public struct AccelerationData : IComponentData
    {
        public float maxSpeed;
        public float acceleration;
        public float drag;
    }
}
