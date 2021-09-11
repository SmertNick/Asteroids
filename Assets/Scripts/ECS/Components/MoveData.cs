using Unity.Entities;
using Unity.Mathematics;

namespace ECS.Components
{
    [GenerateAuthoringComponent]
    public struct MoveData : IComponentData
    {
        public float3 direction;
        public float speed;
        public float turnSpeed;
    }
}
