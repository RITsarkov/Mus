using System.ComponentModel;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

public class PlayerShootingSystem : JobComponentSystem
{

    private struct PlayerShootingJob : IJobParallelFor
    {
        public EntityArray EntityArray;
        public EntityCommandBuffer EntityCommandBuffer;
        public bool IsFiring;
        
        public void Execute(int index)
        {
            if (!IsFiring)
                return;
            EntityCommandBuffer.AddComponent(EntityArray[index], new Firing());
        }
    }
    
    private struct Data
    {
        public readonly int Length;
        public EntityArray Entities;
        public ComponentDataArray<Weapon> Weapon;
        public SubtractiveComponent<Firing> Firings;
    }

    [Inject] private Data _data;
    [Inject] private PlayerShootingBarrier _barrier;

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return new PlayerShootingJob
        {
            EntityArray = _data.Entities,
            EntityCommandBuffer = _barrier.CreateCommandBuffer(),
            IsFiring = Input.GetButton("Fire1")
        }.Schedule(_data.Length, 64, inputDeps);
    }
}

public class PlayerShootingBarrier : BarrierSystem
{
}