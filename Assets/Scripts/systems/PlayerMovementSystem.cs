using Unity.Entities;
using UnityEngine;

public class PlayerMovementSystem: ComponentSystem
{
    private struct filter
    {
        public Rigidbody rigidbody;
        public PositionComponent inputComponent;
    }
    
    protected override void OnUpdate()
    {
        var deltaTime = Time.deltaTime;
        foreach (filter entity in GetEntities<filter>())
        {
            var moveVector = new Vector3(entity.inputComponent.horizontal, 0, entity.inputComponent.vertical);
            var movePosition = entity.rigidbody.position + moveVector.normalized * 3 * deltaTime;
            entity.rigidbody.MovePosition(movePosition);
        }
    }
}