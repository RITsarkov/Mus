using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

public class CleanupFiringSystem: JobComponentSystem
{

    private struct CleanupFiringJob : IJobParallelFor
    {
        [Unity.Collections.ReadOnly] public EntityArray Entities;
        [Unity.Collections.ReadOnly] public EntityCommandBuffer EntityCommandBuffer;
        public float CurrentTime;
        public ComponentDataArray<Firing> Firings;
        
        public void Execute(int index)
        {
            if (CurrentTime - Firings[index].FiredAt < 0.5)
                return;
            EntityCommandBuffer.RemoveComponent<Firing>(Entities[index]);
        }
    }


    private struct Data
    {
        public readonly int Length;
        public EntityArray Entities;
        public ComponentDataArray<Firing> Firing;

    }

    [Inject] private Data _data;
    [Inject] private CleanupFiringBarrier _barrier;
    
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return new CleanupFiringJob
        {
            Entities = _data.Entities,
            EntityCommandBuffer = _barrier.CreateCommandBuffer(),
            CurrentTime = Time.time,
            Firings = _data.Firing
        }.Schedule(_data.Length, 64, inputDeps);
    }
}

public class CleanupFiringBarrier : BarrierSystem
{
}