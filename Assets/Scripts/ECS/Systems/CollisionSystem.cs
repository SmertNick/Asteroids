using ECS.Components;
using ECS.Tags;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;

namespace ECS.Systems
{
    [UpdateAfter(typeof(FixedStepSimulationSystemGroup))]
    public class CollisionSystem : JobComponentSystem
    {
        private BuildPhysicsWorld buildPhysicsWorld;
        private StepPhysicsWorld stepPhysicsWorld;

        protected override void OnCreate()
        {
            base.OnCreate();
            buildPhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
            stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
        }

        [BurstCompile]
        private struct CollisionPlayerAsteroidJob : ICollisionEventsJob
        {
            [ReadOnly] public ComponentDataFromEntity<PlayerTag> players;
            [ReadOnly] public ComponentDataFromEntity<AsteroidTag> asteroids;
            [ReadOnly] public ComponentDataFromEntity<ProjectileTag> projectiles;
            public ComponentDataFromEntity<HealthData> healthData;

            public void Execute(CollisionEvent collision)
            {
                if (players.HasComponent(collision.EntityA) && asteroids.HasComponent(collision.EntityB))
                {
                    var modifiedHealthData = healthData[collision.EntityA];
                    modifiedHealthData.isDead = true;
                    healthData[collision.EntityA] = modifiedHealthData;
                }
                else if (players.HasComponent(collision.EntityB) && asteroids.HasComponent(collision.EntityA))
                {
                    var modifiedHealthData = healthData[collision.EntityB];
                    modifiedHealthData.isDead = true;
                    healthData[collision.EntityB] = modifiedHealthData;
                }
                else if (projectiles.HasComponent(collision.EntityA) && asteroids.HasComponent(collision.EntityB) ||
                         projectiles.HasComponent(collision.EntityB) && asteroids.HasComponent(collision.EntityA))
                {
                    var modifiedHealthData = healthData[collision.EntityA];
                    modifiedHealthData.isDead = true;
                    healthData[collision.EntityA] = modifiedHealthData;
                    
                    modifiedHealthData = healthData[collision.EntityB];
                    modifiedHealthData.isDead = true;
                    healthData[collision.EntityB] = modifiedHealthData;
                }
            }
        }
       
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new CollisionPlayerAsteroidJob
            {
                players = GetComponentDataFromEntity<PlayerTag>(true),
                asteroids = GetComponentDataFromEntity<AsteroidTag>(true),
                projectiles = GetComponentDataFromEntity<ProjectileTag>(true),
                healthData = GetComponentDataFromEntity<HealthData>(false)
            };

            var jobHandle = job.Schedule(stepPhysicsWorld.Simulation, ref buildPhysicsWorld.PhysicsWorld, inputDeps);
            jobHandle.Complete();
            return jobHandle;
        }
    }
}
