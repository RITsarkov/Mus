//using Unity.Entities;
//using Unity.Jobs;
//using UnityEngine;
//
//public class PlayerShootingSystem : JobComponentSystem
//{
//
//    private struct PlayerShootingJob : IJobParallelFor
//    {
//        [Unity.Collections.ReadOnly] public EntityArray EntityArray;
//        [Unity.Collections.ReadOnly] public EntityCommandBuffer EntityCommandBuffer;
//        public float CurrentTime;
//        
//        public void Execute(int index)
//        {
//            EntityCommandBuffer.AddComponent(EntityArray[index], new Firing {FiredAt = CurrentTime});
//        }
//    }
//
//    
//    private struct Data
//    {
//        public readonly int Length;
//        public EntityArray Entities;
//        public ComponentDataArray<Weapon> Weapon;
//        public SubtractiveComponent<Firing> Firings;
//    }
//
//    [Inject] private Data _data;
//    [Inject] private PlayerShootingBarrier _barrier;
//
//    protected override JobHandle OnUpdate(JobHandle inputDeps)
//    {
//        if (Input.GetButton("Fire1"))
//        {
//            return new PlayerShootingJob
//            {
//                EntityArray = _data.Entities,
//                EntityCommandBuffer = _barrier.CreateCommandBuffer(),
//                CurrentTime = Time.time
//            }.Schedule(_data.Length, 64, inputDeps);
//        }
//        return new JobHandle();
//    }
//    
//}
//
//public class PlayerShootingBarrier : BarrierSystem
//{
//}