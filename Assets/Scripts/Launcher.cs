using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectPrefab;
    [SerializeField] private int dimX = 10, dimY = 10;
    [SerializeField] private float spacing = 1f; 

    private Entity entityPrefab;
    private World defaultWorld;
    private EntityManager entityManager;
    
    void Start()
    {
        defaultWorld = World.DefaultGameObjectInjectionWorld;
        entityManager = defaultWorld.EntityManager;

        var settings = GameObjectConversionSettings.FromWorld(defaultWorld, null);
        entityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(gameObjectPrefab, settings);

        SpawnEntities(dimX, dimY, spacing);
        
//        DoJob();
    }

    void SpawnEntity(float3 position)
    {
        var entity = entityManager.Instantiate(entityPrefab);
        entityManager.SetComponentData(entity, new Translation{Value = position});
    }

    void SpawnEntities(int dimX, int dimY, float spacing = 1f)
    {
        for (int i = 0; i < dimX; i++)
        {
            for (int j = 0; j < dimY; j++)
            {
                SpawnEntity(new float3(i * spacing, j * spacing, 0f));
            }
        }
    }

    private void DoJob()
    {
        var resArr = new NativeArray<float>(1, Allocator.TempJob);

        // init jobs
        var myJob = new SimpleJob
        {
            a = 5f,
            result = resArr
        };

        var secondJob = new AnotherJob();
        secondJob.res = resArr;

        // schedule jobs
        JobHandle handle = myJob.Schedule();
        JobHandle secHandle = secondJob.Schedule(handle);
        
        secHandle.Complete();

        float res = resArr[0];
        Debug.Log("result = " + res);
        Debug.Log("myJob.a = " + myJob.a);

        resArr.Dispose();
    }
    
    private struct SimpleJob : IJob
    {
        public float a;
        public NativeArray<float> result;
        
        public void Execute()
        {
            result[0] = a;
            a = 1337f;
        }
    }

    private struct AnotherJob : IJob
    {
        public NativeArray<float> res;
        public void Execute()
        {
            res[0] = res[0] + 1;
        }
    }
}
