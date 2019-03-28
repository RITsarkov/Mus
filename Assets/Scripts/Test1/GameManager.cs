using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Test1;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

namespace Test1
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


        private TransformAccessArray transformsArray;
        private MovementJob movementJob;
        private JobHandle moveHandle;


        //При закрытии 
        private void OnDisable()
        {
            //Выполнить все работы
            moveHandle.Complete();
            //Отчистить все данные
            transformsArray.Dispose();
        }


        void Start()
        {
            transformsArray = new TransformAccessArray(0, -1);
            AddEnemy(enemyPrefab, enemyCount);
        }


        private void Update()
        {
            moveHandle.Complete();

            if (Input.GetButtonDown("space"))
                AddEnemy(enemyPrefab, enemyCount);

            movementJob = new MovementJob()
            {
                moveSpeed = enemySpeed,
                topBound = topBound,
                bottomBound = bottomBound,
                deltaTime = Time.deltaTime
            };
            //х3 но вроде бы на этом месте все потоки и запускаются
            moveHandle = movementJob.Schedule(transformsArray);

            JobHandle.ScheduleBatchedJobs();
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
                var obj = Instantiate(enemyPrefab, pos, rot) as GameObject;
                transformsArray.Add(obj.transform);
            }
        }
    }
}