using ECS.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECS.Systems
{
    public class SpinnerSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;

            Entities.ForEach((ref Rotation rot, in MoveData moveData) =>
            {
                quaternion normalizedRot = math.normalize((rot.Value));
                quaternion angleToRotate = quaternion.AxisAngle(math.up(), moveData.turnSpeed * deltaTime);

                rot.Value = math.mul(normalizedRot, angleToRotate);
            }).ScheduleParallel();
        }
    }
}
