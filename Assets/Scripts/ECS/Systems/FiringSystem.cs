using Unity.Entities;
using Unity.Transforms;

namespace ECS.Systems
{
    public class FiringSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((ref Translation translation, in Rotation rotation) => {
            }).Schedule();
        }
    }
}
