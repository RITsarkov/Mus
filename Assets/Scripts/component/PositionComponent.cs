using System;
using Unity.Entities;

[Serializable]
public struct Position : IComponentData
{
    public float horizontal;
    public float vertical;
}

public class PositionComponent : ComponentDataProxy <Position> {}