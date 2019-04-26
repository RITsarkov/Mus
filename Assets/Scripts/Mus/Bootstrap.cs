using Unity.Entities;
using UnityEngine;

namespace Mus
{
    public class Bootstrap : MonoBehaviour
    {
        public Prefab playerChatacter;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private void Start()
        {
            var entityManager = World.Active.EntityManager;

            var battleCharacter = entityManager.CreateArchetype(
                typeof(CharacterComponent),
                typeof(CharacterVisualComponent)
            );

            var player = entityManager.CreateEntity(battleCharacter);
         
            entityManager.SetSharedComponentData(player, new CharacterVisualComponent
            {
                idleImage = playerChatacter
            });
        }
    }
}