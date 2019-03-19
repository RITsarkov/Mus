using Unity.Entities;
using UnityEngine;

class RotatingSystem : ComponentSystem
{
    //will run every frame
    protected override void OnUpdate()
    {
        foreach (Components ent in GetEntities<Components>())
        {
            ent.transform.Rotate(0f, ent.rotater.speed, 0f);
        }
    }

    struct Components
    {
        public Rotater rotater;
        public Transform transform;
    }
}