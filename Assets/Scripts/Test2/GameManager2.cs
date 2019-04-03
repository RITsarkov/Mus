
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

namespace Test2
{
//Просто быстрые настройки из редактора Unity
    public class GameManager : MonoBehaviour
    {

        public static EntityManager _manager;

        
        [Header("Enemy Setting")] 
        public GameObject enemyPrefab;
        public float enemySpeed = 1f;

        [Header("Spawn Settings")]
        public int enemyCount = 1;
        public int enemyIncrement = 1;

        [Header("Spown borders")]
        public float topBound = 10;
        public float bottomBound = -10;
        public float leftBound = -10;
        public float rightBound = 10;

//==============================================================================================


        void Start()
        {
            _manager = World.Active.GetOrCreateManager<EntityManager>();
            AddEnemy(enemyCount);
        }
        
        
        private void Update()
        {
            if(Input.GetButton("Jump"))
                AddEnemy(enemyCount);
        }

        
        private void AddEnemy(int amount)
        {
            NativeArray<Entity> entities = new NativeArray<Entity>(amount, Allocator.Temp);
            _manager.Instantiate(enemyPrefab, entities);

            for (int i=0; i < amount; i++)
            {
                float xVal = Random.Range(leftBound, rightBound);
                float zVal = Random.Range(0, 10f);
//                _manager.SetComponentData(entities[i], new PositionComponent(Value = new ));
            }
        }
    }
}
