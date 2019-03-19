using Unity.Entities;
using UnityEngine;

public class InputSystemInputSystem : ComponentSystem
{
    private struct Data
    {
        public readonly int Length;
        public ComponentArray<InputComponent> inputComponent;
    }

    //Inject = need data!
    [Inject] private Data _data;

    protected override void OnUpdate()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        
        for (int i = 0; i < _data.Length; i++)
        {
            _data.inputComponent[i].horizontal = horizontal;
            _data.inputComponent[i].vertical = vertical;
        }
    }
    
    
 
    
    
}