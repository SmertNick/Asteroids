using ECS.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECS.Systems
{
    public class FaceDirectionSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((ref Rotation rot, in Translation pos, in MoveData moveData) =>
            {
                if (!moveData.direction.Equals(float3.zero))
                {
                    quaternion targetRotation = quaternion.LookRotationSafe(moveData.direction, math.up());
                    rot.Value = math.slerp(rot.Value, targetRotation, moveData.turnSpeed);
                }
            }).ScheduleParallel();
        }
        
    }
}
