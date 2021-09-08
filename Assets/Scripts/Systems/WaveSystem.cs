using Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class WaveSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float elapsedTime = (float)Time.ElapsedTime;
        
        Entities.ForEach((ref Translation trans, in MoveSpeed moveSpeed, in WaveParameters waveParameters) =>
        {
            float z = waveParameters.amplitude * math.sin(elapsedTime * moveSpeed.Value.z
                                                          + trans.Value.x * waveParameters.offset.x +
                                                          trans.Value.y * waveParameters.offset.y);
            trans.Value = new float3(trans.Value.x, trans.Value.y, z);
        }).ScheduleParallel();
    }
}
