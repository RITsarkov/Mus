//using Unity.Collections;
//using Unity.Entities;
//using Unity.Mathematics;
//using UnityEngine;
//using Random = UnityEngine.Random;
//
//public class GameManager2 : MonoBehaviour
//{
//    
//    public static EntityManager _manager;
//    public static GameManager2 GM;
//
//    [Header("Enemy Setting")] public GameObject enemyPrefab;
//    public float enemySpeed = 1f;
//
//    [Header("Spawn Settings")] public int enemyCount = 1;
//    public int enemyIncrement = 1;
//
//    [Header("Spown borders")] public float topBound = 10;
//    public float bottomBound = -10;
//    public float leftBound = -10;
//    public float rightBound = 10;
//
//    public int count = 0;
////==============================================================================================
//
//
//    void Start()
//    {
//        GM = this;
//        _manager = World.Active.GetOrCreateManager<EntityManager>();
//        AddEnemy(enemyCount);
//    }
//
//
//    private void Update()
//    {
//        if (Input.GetButton("Jump"))
//            AddEnemy(enemyCount);
//    }
//
//
//    private void AddEnemy(int amount)
//    {
//        NativeArray<Entity> entities = new NativeArray<Entity>(amount, Allocator.Temp);
//        _manager.Instantiate(enemyPrefab, entities);
//
//        for (int i = 0; i < amount; i++)
//        {
//            float xVal = Random.Range(leftBound, rightBound);
//            float zVal = Random.Range(0, 10f);
//            _manager.SetComponentData(entities[i], new Position {Value = new float3(xVal, 0f, topBound + zVal)});
//            _manager.SetComponentData(entities[i], new Rotation {Value = new quaternion(0, 1, 0, 0)});
//            _manager.SetComponentData(entities[i], new Speed {Value = enemySpeed});
//        }
//
//        entities.Dispose();
//        count += amount;
//    }
//}