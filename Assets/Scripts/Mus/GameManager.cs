using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Mus
{
    public class GameManager : MonoBehaviour
    {
        private NoteMatrix noteMatrixData;
        private GameObject notePanelPref;
        //Содержит ссылки на все нотки
        private Dictionary<NoteCoord, GameObject> notesGameObjects = new Dictionary<NoteCoord, GameObject>();
        private int currentScore = 0;
        
        public Text debugNoteMatrix;
        public Text scoreText;
        public GameObject notePanel;
        public Canvas canvas;

        public GameObject notePrefab;

        void Start()
        {
            noteMatrixData = new NoteMatrix();

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
                noteMatrixData.generateRendomNotes();
                visualizeNoteMatix();
                //visualizeNote();
            }

            if (Input.GetButton("Fire1"))
            {
//                Debug.Log("Mouse Clicked!!!!");
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

                if (hit.collider != null)
                {
                    Note note = hit.collider.gameObject.GetComponent<Note>();
                    
                    if (isFirstNote() || (!selectedNotes.Contains(note.noteCoord) && valideNotes.Contains(note.noteCoord)))
                    {
                        perspectiveModeOne(false);
                        //Если была выбрана первая нотка, то вычисляем какие позиции сможет выбрать игрк                    
                        valideNotes = noteMatrixData.getValidePositions(note);
                        perspectiveModeOne(true);

                        note.selectedModeOne(true);
                        selectedNotes.Add(note.noteCoord);
                    }
                }
            }
            if (Input.GetButtonUp("Fire1"))
            {
                selectedModeOne(false);
                perspectiveModeOne(false);
                noteMatrixData.removeNote(selectedNotes);
                visualizeNote();
                currentScore = currentScore + selectedNotes.Count;
                scoreText.text = "Score: " + currentScore;
                
                selectedNotes.Clear();
                valideNotes.Clear();
                
            }
        }
        

        private void perspectiveModeOne(bool on)
        {
            if (valideNotes == null)
                return;
            
            foreach (NoteCoord noteCoord in valideNotes)
            {
                GameObject goNote = notesGameObjects[noteCoord];
                Note n = goNote.gameObject.GetComponent<Note>();
                n.perspectiveModeOne(on);
            }
        }
        
        private void selectedModeOne(bool on)
        {
            if (valideNotes == null)
                return;
            
            foreach (NoteCoord coord in selectedNotes)
            {
                Note note = notesGameObjects[coord].GetComponent<Note>();
                note.selectedModeOne(on);
                note.perspectiveModeOne(on);
            }
        }

        private List<NoteCoord> selectedNotes = new List<NoteCoord>();
        private List<NoteCoord> valideNotes;
        

        private bool isFirstNote()
        {
            return selectedNotes.Count == 0;
        }
        

        private void visualizeNote()
        {
            //TODO отчистку этой мапы надо бы оптимизировать, а не перезатерать ее всю при обновлении.
            notesGameObjects.Clear();
            var e = notePanelPref.transform.GetEnumerator();

            int[,] notes = noteMatrixData.getNoteMatrix();
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
            int[,] notes = noteMatrixData.getNoteMatrix();
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