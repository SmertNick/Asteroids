using ECS.Components;
using Unity.Entities;
using Unity.Transforms;

namespace ECS.Systems
{
    [UpdateBefore(typeof(TransformSystemGroup))]
    public class DeathSystem : SystemBase
    {
        private EndSimulationEntityCommandBufferSystem commandBufferSystem;
        protected override void OnCreate()
        {
            base.OnCreate();
            commandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var entityCommandBuffer = commandBufferSystem.CreateCommandBuffer();

            Entities.ForEach((Entity entity, in HealthData healthData) =>
            {
                if (healthData.isDead)
                {
                    entityCommandBuffer.DestroyEntity(entity);
                }
            }).Schedule();
        
            commandBufferSystem.AddJobHandleForProducer(this.Dependency);
        }
    }
}
