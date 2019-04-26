using System;
using Unity.Entities;

namespace Mus
{
    public class CharacterComponent : ComponentDataProxy <Character> {}
    
    [Serializable]
    public struct Character : IComponentData
    {
        public string name;
        public Int16 id;
        public Int16 defaultHp;
        public Int16 currentHp;
    }
}
