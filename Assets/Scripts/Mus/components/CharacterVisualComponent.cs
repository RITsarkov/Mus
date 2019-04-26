using System;
using Unity.Entities;
using UnityEngine;

namespace Mus
{
    [Serializable]
    public struct CharacterVisualComponent : IComponentData
    {
        public Prefab idleImage;
    }
}
