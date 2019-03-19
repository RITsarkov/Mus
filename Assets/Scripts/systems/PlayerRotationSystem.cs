using Unity.Entities;
using UnityEngine;

public class PlayerRotationSystem : ComponentSystem
{
    private struct Filter
    {
        public Transform transform;
        public RotationComponent rotationComponent;
    }

    
    
    protected override void OnUpdate()
    {
        var mousePosition = Input.mousePosition;
        var cameraRay = Camera.main.ScreenPointToRay(mousePosition);
        var layerMask = LayerMask.GetMask("Floor");

        if (Physics.Raycast(cameraRay, out var hit, 200, layerMask))
        foreach (Filter entity in GetEntities<Filter>())
        {
            var forward = hit.point - entity.transform.position;
            var rotation = Quaternion.LookRotation(forward);
            entity.rotationComponent.Value = new Quaternion(0, rotation.y, 0, rotation.w);
        }
    }
}