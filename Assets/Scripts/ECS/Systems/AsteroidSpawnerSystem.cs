using System.Diagnostics;
using ECS.Components;
using ECS.Tags;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECS.Systems
{
    public class AsteroidSpawnerSystem : SystemBase
    {
        private EntityQuery asteroidQuery;
        private BeginSimulationEntityCommandBufferSystem beginSimulationECB;
        private EntityQuery gameSettingsQuery;
        private Entity prefab;


        protected override void OnCreate()
        {
            asteroidQuery = GetEntityQuery(ComponentType.ReadWrite<AsteroidTag>());
            beginSimulationECB = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
            gameSettingsQuery = GetEntityQuery(ComponentType.ReadWrite<GameSettingsComponent>());

            RequireForUpdate(gameSettingsQuery);
        }

        protected override void OnUpdate()
        {
            if (prefab == Entity.Null)
            {
                prefab = GetSingleton<AsteroidAuthoringComponent>().Prefab;
            }
            
            var settings = GetSingleton<GameSettingsComponent>();
            var commandBuffer = beginSimulationECB.CreateCommandBuffer();
            var count = asteroidQuery.CalculateEntityCountWithoutFiltering();
            var localPrefab = prefab; // Quirk of ECS
            
            //We will use this to generate random positions
            var rand = new Unity.Mathematics.Random((uint)Stopwatch.GetTimestamp());
            var offset = settings.offset;
            var xMax = .5f * settings.levelWidth + offset;
            var xMin = -xMax;
            var zMax = .5f * settings.levelHeight + offset;
            var zMin = -zMax;
            
            Job.WithCode(() => {
                for (int i = count; i < settings.numAsteroids; ++i)
                {
                    // Generating a random point within a rectangle
                    var x = rand.NextFloat(xMin, xMax);
                    var z = rand.NextFloat(zMin, zMax);

                    // Choosing a random side and moving point there to get a random point on the perimeter
                    var chooseFace = rand.NextFloat(0f, 4f);
                    if (chooseFace < 1f) x = xMin;
                    else if (chooseFace < 2f) x = xMax;
                    else if (chooseFace < 3f) z = zMin;
                    else if (chooseFace < 4f) z = zMax;
                    
                    var entity = commandBuffer.Instantiate(localPrefab);
                    commandBuffer.SetComponent(entity, new Translation{Value = new float3(x, 0f, z)});
                }
            }).Schedule();
            
            //This will add our dependency to be played back on the BeginSimulationEntityCommandBuffer
            beginSimulationECB.AddJobHandleForProducer(Dependency);
        }
    }
}