using Unity.Entities;
using UnityEngine;

public class RotationSystem : ComponentSystem
{
    private struct Data
    {
        public readonly int Length;
        public ComponentDataArray<Rotation> RotationComponent;
        public ComponentArray<Rigidbody> Rigidbody;
        public ComponentDataArray<Player> player;
    }

    [Inject] private Data _data;

    protected override void OnUpdate()
    {
        for (int i = 0; i < _data.Length; i++)
        {
            Quaternion rotation = _data.RotationComponent[i].Value;
            _data.Rigidbody[i].MoveRotation(rotation.normalized);
        }
    }
}