using UnityEngine;

namespace Mus
{
    public class Note : MonoBehaviour
    {
        public int type; 
        public NoteCoord noteCoord;
        

        private bool _selected = false;
        
        public GameObject selectedAnimation;
        public GameObject perspectiveAnimation;

        private void OnMouseOver()
        {
            
        }

        public void selectedModeOne(bool selected)
        {
            _selected = selected;
            selectedAnimation.SetActive(selected);
        }      
        
        public void perspectiveModeOne(bool selected)
        {
            if(!_selected)
                perspectiveAnimation.SetActive(selected);
        }
    }
}