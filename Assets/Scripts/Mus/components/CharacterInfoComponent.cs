using System;
using Unity.Entities;

namespace Mus
{
    public class CharacterInfoComponent : ComponentDataProxy <CharacterInfo> {}
    
    [Serializable]
    public struct CharacterInfo : IComponentData
    {
        public string name;
        public Int16 id;
    }
}
