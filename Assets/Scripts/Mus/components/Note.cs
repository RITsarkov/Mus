using UnityEngine;
using UnityEngine.UI;

namespace Mus
{
    public class Note : MonoBehaviour
    {
        [SerializeField]
        private Image _image;
        private int type; 
        private NoteCoord noteCoord;
        

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

        public void setColor(Color color)
        {
            _image.color = color;
        }
        
        public void setCoord(NoteCoord coord)
        {
            this.noteCoord = coord;
        }
        
        public NoteCoord getCoord()
        {
            return noteCoord;
        }
        
        public void setType(int type)
        {
            this.type = type;
        }   
        
        public int getType()
        {
            return type;
        }
    }
}