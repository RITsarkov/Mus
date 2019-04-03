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
    }

    [Inject] private Data _data;
    
    protected override void OnUpdate()
    {
        var deltaTime = Time.deltaTime;
        for (int i = 0; i < _data.Length; i++)
        {
            var moveVector = new Vector3(_data.InputComponent[i].horizontal, 0, _data.InputComponent[i].vertical);
            var movePosition = _data.Rigidbody[i].position + moveVector.normalized * 3 * deltaTime;
            _data.Rigidbody[i].MovePosition(movePosition);
        }
    }
}