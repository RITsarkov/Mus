using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public GameObject notePrefab;

        void Start()
        {
            noteMatrix = new NoteMatrix();

            //Грузим панельку
            notePanelPref = Instantiate(notePanel, canvas.transform, false);

//            visualizeNoteMatix();
            visualizeNote();
        }


        private void Update()
        {
//            if (Input.GetButtonDown("Fire1"))
            if (Input.GetButtonDown("Jump"))
            {
                noteMatrix.generateRendomNotes();
                visualizeNoteMatix();
                //visualizeNote();
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
                    Note note = hit.collider.gameObject.GetComponent<Note>();
                    //Если была выбрана первая нотка, то вычисляем какие позиции сможет выбрать игрк                    
                    if (isFirstNote())
                    {
                        valideNotes = noteMatrix.getValidePositions(note);
                        foreach (NoteCoord valideNote in valideNotes)
                        {
                            GameObject goNote = notesGameObjects[valideNote];
                            Note n = goNote.gameObject.GetComponent<Note>();
                            n.perspectiveModeOne(true);
                        }
                    }

                    if (valideNotes.Contains(note.noteCoord))
                    {
                        note.selectedModeOne(true);
                        colidersBuff.Add(hit.collider);
                    }
                }
            }

            if (Input.GetButtonUp("Fire1"))
            {
                foreach (Collider2D coll2d in colidersBuff)
                {
                    //Debug.Log("Hit" + coll2d.gameObject.transform.parent.name);
                    Note note = coll2d.gameObject.GetComponent<Note>();
                    note.selectedModeOne(false);
                    note.perspectiveModeOne(false);
                }
                //TODO дублирование
                foreach (NoteCoord noteCoord in valideNotes)
                {
                    GameObject goNote = notesGameObjects[noteCoord];
                    Note n = goNote.gameObject.GetComponent<Note>();
                    n.perspectiveModeOne(false);
                }

                colidersBuff.Clear();
                valideNotes.Clear();
            }
        }
        
        //todo заменить. брать из notesGameObjects по  NoteCoord
        private List<Collider2D> colidersBuff = new List<Collider2D>();
        private List<NoteCoord> valideNotes;
        

        private bool isFirstNote()
        {
            return colidersBuff.Count == 0;
        }


        //todo вверх
        //Содержит ссылки на все нотки
        private Dictionary<NoteCoord, GameObject> notesGameObjects = new Dictionary<NoteCoord, GameObject>();

        private void visualizeNote()
        {
            var e = notePanelPref.transform.GetEnumerator();

            int[,] notes = noteMatrix.getNoteMatrix();
            for (int x = 0; x <= notes.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= notes.GetUpperBound(1); y++)
                {
                    if (e.MoveNext())
                        addNote((Transform) e.Current, notes[x, y], x, y);
                }
            }
        }


        private void addNote(Transform parrentTransform, int noteId, int x, int y)
        {
            foreach (Transform child in parrentTransform)
            {
                Destroy(child.gameObject);
            }

            GameObject noteGameObject = Instantiate(notePrefab, parrentTransform, false);
            var colour = noteGameObject.GetComponent<Image>();
            switch (noteId)
            {
                case 1:
                    colour.color = Color.red;
                    break;
                case 2:
                    colour.color = Color.green;
                    break;
                case 3:
                    colour.color = Color.blue;

                    break;
            }

            Note note = noteGameObject.GetComponent<Note>();
            note.type = noteId;
            NoteCoord nc = new NoteCoord(x, y);
            note.noteCoord = nc;
            notesGameObjects.Add(nc, noteGameObject);
        }

        private void visualizeNoteMatix()
        {
            int[,] notes = noteMatrix.getNoteMatrix();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= notes.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= notes.GetUpperBound(1); j++)
                {
                    sb.Append(notes[i, j]).Append("\t");
                }

                sb.Append(";\n");
            }

            debugNoteMatrix.text = sb.ToString();
        }
    }
}