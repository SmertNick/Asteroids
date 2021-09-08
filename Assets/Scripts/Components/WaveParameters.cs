using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct WaveParameters : IComponentData
{
    public float amplitude;
    public float frequency;
    public float3 offset;
}
