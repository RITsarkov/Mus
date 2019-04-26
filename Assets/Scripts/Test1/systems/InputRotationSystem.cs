//using Unity.Entities;
//using UnityEngine;
//
//public class PlayerRotationSystem : ComponentSystem
//{
//    private struct Data
//    {
//        public readonly int Length;
//        public ComponentArray<Transform> transform;
//        public ComponentDataArray<Rotation> rotationComponent;
//        public ComponentDataArray<Player> player;
//    }
//    
//    [Inject] private Data _data;
//
//    protected override void OnUpdate()
//    {
//        var mousePosition = Input.mousePosition;
//        var cameraRay = Camera.main.ScreenPointToRay(mousePosition);
//        var layerMask = LayerMask.GetMask("Floor");
//
//        if (Physics.Raycast(cameraRay, out var hit, 200, layerMask))
//            for (int i = 0; i < _data.Length; i++)
//            {
//                Vector3 forward = hit.point - _data.transform[i].transform.position;
//                var rotation = Quaternion.LookRotation(forward);
//                _data.rotationComponent[i] = new Rotation{Value =  new Quaternion(0, rotation.y, 0, rotation.w)};
//            }
//    }
//}