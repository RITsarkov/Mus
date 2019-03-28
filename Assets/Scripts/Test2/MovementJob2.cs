using Unity.Collections;
using UnityEngine;
using UnityEngine.Jobs;

namespace Test2
{
    public struct MovementJob2
    {
        [ReadOnly] public NativeArray<Vector3> velocity;
        [ReadOnly] public float deltaTime;
        [ReadOnly] public float topBound;
        [ReadOnly] public float bottomBound;
        public TransformAccessArray transformsArray;


        // The code actually running on the job
        public void Execute(int i)
        {
            Vector3 pos = transformsArray[i].position;
            pos += deltaTime * (transformsArray[i].rotation * velocity[i]);

            if (pos.z < bottomBound)
                pos.z = topBound;

            transformsArray[i].position = pos;
        }
    }
}