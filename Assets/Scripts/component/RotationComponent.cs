using System;
using Unity.Entities;
using UnityEngine;

[Serializable]
public struct Rotation : IComponentData
{
    public Quaternion rotation;
}

public class RotationComponent : ComponentDataProxy <Rotation> {}