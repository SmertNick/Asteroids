using ECS.Tags;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

namespace ECS.Systems
{
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

        //[BurstCompile]
        private struct CollisionJob : ICollisionEventsJob
        {
            [ReadOnly] public ComponentDataFromEntity<PlayerTag> players;
            [ReadOnly] public ComponentDataFromEntity<AsteroidTag> asteroids;

            public void Execute(CollisionEvent collision)
            {
                Debug.Log("Collision. A: " + collision.EntityA.Index + ", B: " + collision.EntityB.Index);
            }
        }
    
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new CollisionJob
            {
                players = GetComponentDataFromEntity<PlayerTag>(true),
                asteroids = GetComponentDataFromEntity<AsteroidTag>(true)
            };

            var jobHandle = job.Schedule(stepPhysicsWorld.Simulation, ref buildPhysicsWorld.PhysicsWorld, inputDeps);
            return jobHandle;
        }
    }
}
