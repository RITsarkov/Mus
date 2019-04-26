using System;
using Unity.Entities;

[Serializable]
public struct Player : IComponentData {}
public class PlayerComponent : ComponentDataProxy <Player> {}