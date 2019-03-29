
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

namespace Test2
{
//Просто быстрые настройки из редактора Unity
    public class GameManager : MonoBehaviour
    {

        public static GameManager GM;

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

//        private NativeArray<Vector3> positionArray;
//        private NativeArray<Vector3> velocityArray;
        private TransformAccessArray transformsArray;
        private MovementJob2 movementJob;
        private JobHandle moveHandle;
//        private GameObject[] EnemyArray;


        //При закрытии 
        private void OnDisable()
        {
            //Выполнить все работы
            moveHandle.Complete();
            //Отчистить все данные
            transformsArray.Dispose();
        }


        private void Update()
        {
            moveHandle.Complete();

            if (Input.GetButtonDown("space"))
                AddEnemy(enemyPrefab, enemyCount);
            
            
            var velocity = new NativeArray<Vector3>(500, Allocator.Persistent);
            for (var i = 0; i < velocity.Length; i++)
                velocity[i] = new Vector3(0, 0, 1);

            movementJob = new MovementJob2()
            {
                transformsArray = transformsArray,
                velocity = velocity,
                topBound = topBound,
                bottomBound = bottomBound,
                deltaTime = Time.deltaTime
            };
            //х3 но вроде бы на этом месте все потоки и запускаются
//            moveHandle = movementJob.Schedule(transformsArray.length, 64);

            for (int i = 0; i < transformsArray.length; i++)
            {
                Debug.Log(transformsArray[i].position);
            }

            //JobHandle.ScheduleBatchedJobs();
        }
        
        private void AddEnemy(GameObject enemyPrefab, int amount)
        {
            moveHandle.Complete();
            transformsArray.capacity = transformsArray.length + amount;
            for (int i = 0; i < amount; i++)
            {
                float xVal = Random.Range(leftBound, rightBound);
                float zVal = Random.Range(0, 10f);
                Vector3 pos = new Vector3(xVal, 0, zVal + topBound);
                Quaternion rot = Quaternion.Euler(0f, 180f, 0f);
                GameObject obj = Instantiate(enemyPrefab, pos, rot);
                transformsArray.Add(obj.transform);
            }
        }
    }
}
