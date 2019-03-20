using Unity.Entities;
using UnityEngine;

public struct Weapon : IComponentData
{
}

public class WeaponsComponent : ComponentDataProxy<Weapon>

{
}