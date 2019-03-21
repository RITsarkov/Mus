using Unity.Entities;
using UnityEngine;

class RotatingSystem : ComponentSystem
{
    
    struct Filter
    {
        public Rotater rotater;
        public Transform transform;
    }
    
    //will run every frame
    protected override void OnUpdate()
    {
        foreach (Filter ent in GetEntities<Filter>())
        {
            ent.transform.Rotate(0f, ent.rotater.speed, 0f);
        }
    }
}