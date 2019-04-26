//using Unity.Entities;
//using Unity.Mathematics;
//using UnityEngine;
//
//public class InputSystemInputSystem : ComponentSystem
//{
//    private struct Data
//    {
//        public readonly int Length;
//        public ComponentDataArray<Position> positionComponent;
//        public ComponentDataArray<Player> player;
//    }
//
//    //Inject = need data!
//    [Inject] private Data _data;
//
//    protected override void OnUpdate()
//    {
//        var horizontal = Input.GetAxis("Horizontal");
//        var vertical = Input.GetAxis("Vertical");
//        
//        for (int i = 0; i < _data.Length; i++)
//        {
//            _data.positionComponent[i] = new Position{Value = new float3(horizontal, 0, vertical)};
//        }
//    }
//}