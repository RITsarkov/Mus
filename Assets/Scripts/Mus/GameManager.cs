using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Mus
{
    public class GameManager : MonoBehaviour
    {
        private NoteMatrix noteMatrix;
        private GameObject notePanelPref;
        
        public Text debugNoteMatrix;
        public GameObject notePanel;
        public Canvas canvas;

        public GameObject rNore;
        public GameObject gNore;
        public GameObject bNore;

        void Start()
        {
            noteMatrix = new NoteMatrix();
            
            //Грузим панельку
            notePanelPref = Instantiate(notePanel, canvas.transform, false);
            
            visualizeNoteMatix();
            visualizeNote();
        }
        
        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                noteMatrix.generateRendomNotes();
                visualizeNoteMatix();
                visualizeNote();
            }
        }



        private void visualizeNote()
        {
            var e = notePanelPref.transform.GetEnumerator();
    
           int[,] notes = noteMatrix.getNoteMatrix();
            for (int i = 0; i <= notes.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= notes.GetUpperBound(1); j++)
                {
                    if(e.MoveNext())
                        addNote( (Transform) e.Current, notes[i,j]);
                }
            }
        }

        private void addNote(Transform parrentTransform, int noteId)
        {
            foreach (Transform child in parrentTransform)
            {
                Destroy(child.gameObject);
            }
            
            
            switch (noteId)
            {
                case 0:
                    Instantiate(rNore, parrentTransform, false);
                    break;
                case 1:
                    Instantiate(gNore, parrentTransform, false);
                    break;
                case 2:
                    Instantiate(bNore, parrentTransform, false);
                    break;
            }
        }

        private void visualizeNoteMatix()
        {
            int[,] notes = noteMatrix.getNoteMatrix();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= notes.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= notes.GetUpperBound(1); j++)
                {
                    sb.Append(notes[i,j]).Append("\t");
                }
                sb.Append(";\n");
            }
            debugNoteMatrix.text = sb.ToString();
        }
    }
}