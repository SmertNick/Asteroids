using Unity.Entities;
using Unity.Transforms;

namespace ECS.Systems
{
    public class FiringSystem : SystemBase
    {
        private EntityArchetype projectile;
        
        protected override void OnCreate()
        {
            base.OnCreate();
            projectile = EntityManager.CreateArchetype();
        }

        protected override void OnUpdate()
        {
            Entities.ForEach((ref Translation translation, in Rotation rotation) => {
            }).Schedule();
        }
    }
}
