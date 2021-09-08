using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    [GenerateAuthoringComponent]
    public struct MoveSpeed : IComponentData
    {
        public float3 Value;
    }
}
