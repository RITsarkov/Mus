using System;
using Unity.Entities;

namespace Mus
{
    public class CharacterHpComponent : ComponentDataProxy <CharacterHp> {}
    
    [Serializable]
    public struct CharacterHp : IComponentData
    {
        public Int16 defaultHp;
        public Int16 currentHp;
    }
}
