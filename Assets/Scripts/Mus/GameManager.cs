using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Mus
{
    public class GameManager : MonoBehaviour
    {
        private NoteMatrix noteMatrix;
        
        public Text debugNoteMatrix;
        public GameObject notePanel;
        public Canvas canvas;

        void Start()
        {
            noteMatrix = new NoteMatrix();
            visualizeNoteMatix();
            var notePanelPref = Instantiate(notePanel, new Vector3(0, 0, 0), Quaternion.identity);
            notePanelPref.transform.parent = canvas.transform;
        }
        
        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                noteMatrix.generateRendomNotes();
                visualizeNoteMatix();
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