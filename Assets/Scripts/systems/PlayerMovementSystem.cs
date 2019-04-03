using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class PlayerMovementSystem : ComponentSystem
{
    private struct Data
    {
        public readonly int Length;
        public ComponentDataArray<Position> InputComponent;
        public ComponentArray<Rigidbody> Rigidbody;
        public ComponentDataArray<Player> player;
    }

    [Inject] private Data _data;
    
    protected override void OnUpdate()
    {
        var deltaTime = Time.deltaTime;
        for (int i = 0; i < _data.Length; i++)
        {
            var moveVector = new Vector3(_data.InputComponent[i].Value.x, 0, _data.InputComponent[i].Value.z);
            var movePosition = _data.Rigidbody[i].position + moveVector.normalized * 3 * deltaTime;
            _data.Rigidbody[i].MovePosition(movePosition);
        }
    }
}