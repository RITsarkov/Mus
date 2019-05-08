using UnityEngine;

namespace Mus
{
    public class Note : MonoBehaviour
    {
        public bool isActive = false;
        public int type; 
        public NoteCoord noteCoord; 
        
        public GameObject selectedAnimation;

        public void makeSelected(bool selected)
        {
            selectedAnimation.SetActive(selected);
        }
    }
}