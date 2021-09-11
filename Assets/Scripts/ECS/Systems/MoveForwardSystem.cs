using ECS.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECS.Systems
{
    public class MoveForwardSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;

            Entities.ForEach((ref Translation pos, in MoveData moveData, in Rotation rot) =>
            {
                float3 forwardDirection = math.forward(rot.Value);
                pos.Value += forwardDirection * moveData.speed * deltaTime;
            }).ScheduleParallel();
        }
    }
}
