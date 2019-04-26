////Система для генерации пуль
//
//using Unity.Entities;
//using Unity.Jobs;
//using Unity.Transforms;
//
//public class FiringSystem : JobComponentSystem
//
//{
//    private ComponentGroup _componentGroup;
//    [Inject] private FiringBarrier _barrier;
//
//    protected override void OnCreateManager()
//    {
//        _componentGroup = GetComponentGroup(
//            ComponentType.Create<Firing>(),
//            ComponentType.Create<Position>(),
//            ComponentType.Create<Rotation>());
//
//        _componentGroup.SetFilterChanged(ComponentType.Create<Firing>());
//    }
//
//    private struct FiringJob : IJobParallelFor
//    {
//        [Unity.Collections.ReadOnly] public EntityCommandBuffer EntityCommandBuffer;
//        public ComponentDataArray<Position> Position;
//        public ComponentDataArray<Rotation> Rotation;
//
//        public void Execute(int index)
//        {
//            EntityCommandBuffer.CreateEntity();
////            EntityCommandBuffer.AddSharedComponent(Bootstraps.BuletRenderer);
////            EntityCommandBuffer.AddComponent(new TransformMatrix());
////            EntityCommandBuffer.AddSharedComponent(new MoveForward());
////            EntityCommandBuffer.AddComponent(new MoveSpeed {speed = 6});
//            EntityCommandBuffer.AddComponent(Position[index]);
//            EntityCommandBuffer.AddComponent(Rotation[index]);
//        }
//    }
//
//
//    protected override JobHandle OnUpdate(JobHandle inputDeps)
//    {
//        return new FiringJob
//        {
//            EntityCommandBuffer = _barrier.CreateCommandBuffer(),
//            Position = _componentGroup.GetComponentDataArray<Position>(),
//            Rotation =  _componentGroup.GetComponentDataArray<Rotation>()
//        }.Schedule(_componentGroup.CalculateLength(), 64, inputDeps);
//    }
//
//
//    public class FiringBarrier : BarrierSystem
//    {
//    }
//}