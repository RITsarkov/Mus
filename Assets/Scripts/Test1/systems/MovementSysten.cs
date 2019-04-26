//using Test1;
//using Unity.Entities;
//using Unity.Jobs;
//using Unity.Mathematics;
//using UnityEngine;
//using Unity.Transforms;
//using Unity.Collections;
//
//public class MovementSysten : JobComponentSystem
//{
//    
//    struct MovementJob : IJobProcessComponentData<Position, Rotation, Speed>
//    {
//        public float topBound;
//        public float bottomBound;
//        public float deltaTime;
//        
//        public void Execute(ref Position position, ref Rotation rotation, ref Speed speed)
//        {
//            float3 curPosituin = position.Value;
//            Quaternion r = rotation.Value;
//            Vector3 v = r.eulerAngles;
//            curPosituin = deltaTime * speed.Value * v.normalized;
//
//            if (curPosituin.z < bottomBound)
//                curPosituin = topBound;
//            
//            position.Value = curPosituin;
//        }
//    }
//    
//    
//    protected override JobHandle OnUpdate(JobHandle inputDeps)
//    {
//        MovementJob moveJob = new MovementJob
//        {
//            topBound = GameManager2.GM.topBound,
//            bottomBound = GameManager2.GM.bottomBound,
//            deltaTime = Time.deltaTime
//        };
//        JobHandle moveHandle = moveJob.Schedule(this);
////        JobHandle moveHandle = moveJob.Schedule(this, 64, inputDeps);
//        return moveHandle;
//    }
//}