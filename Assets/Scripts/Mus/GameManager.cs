using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Mus
{
    public class GameManager : MonoBehaviour
    {
        private NoteMatrix noteMatrix;
        private GameObject notePanelPref;
        private HashSet<Collider2D> colidersBuff = new HashSet<Collider2D>();

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
//            if (Input.GetButtonDown("Fire1"))
            if (Input.GetButtonDown("Jump"))
            {
                noteMatrix.generateRendomNotes();
                visualizeNoteMatix();
                visualizeNote();
            }
            
            if (Input.GetButton("Fire1"))
            {
//                Debug.Log("Mouse Clicked!!!!");
                //Debug.Log("Mouse Clicked" + this.transform);
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                
                //TODO debug
                //debugNoteMatrix.text = "x=" + mousePos.x + " ; y=" + mousePos.y + " ; z=" + mousePos.z;
                
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                
                if (hit.collider != null)
                {
                    colidersBuff.Add(hit.collider);
//                    hit.collider.attachedRigidbody.AddForce(Vector2.up);
                }
            }

            if (Input.GetButtonUp("Fire1"))
            {
                foreach (Collider2D coll2d in colidersBuff)
                {
                    Debug.Log("Hit" + coll2d.gameObject.transform.parent.name);
                }
                colidersBuff.Clear();
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
                case 1:
                    Instantiate(rNore, parrentTransform, false);
                    break;
                case 2:
                    Instantiate(gNore, parrentTransform, false);
                    break;
                case 3:
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