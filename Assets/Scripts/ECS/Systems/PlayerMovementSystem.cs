using ECS.Components;
using ECS.Tags;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECS.Systems
{
    public class PlayerMovementSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;
        
            Entities.
                WithAny<PlayerTag>().
                ForEach((ref Translation pos, in MoveData moveData) =>
            {
                float3 normalizedDir = math.normalizesafe(moveData.direction);
                pos.Value += normalizedDir * moveData.speed * deltaTime;
            
            }).Run();
        }
    }
}
