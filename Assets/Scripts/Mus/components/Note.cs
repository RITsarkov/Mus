using UnityEngine;

namespace Mus
{
    public class Note : MonoBehaviour
    {
        public bool isActive = false;
        public int type; 
        public NoteCoord noteCoord; 
        
        public GameObject selectedAnimation;
        public GameObject perspectiveAnimation;

        public void selectedModeOne(bool selected)
        {
            selectedAnimation.SetActive(selected);
        }      
        
        public void perspectiveModeOne(bool selected)
        {
            selectedAnimation.SetActive(selected);
        }
    }
}