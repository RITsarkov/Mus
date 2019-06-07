using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Mus
{
    public class GameManager : MonoBehaviour
    {
        private NoteMatrix _noteMatrixData;
        //Содержит ссылки на все нотки
        private int currentScore = 0;
        private NoteVisualizer _noteVisualizer;
        
        public Text scoreText;
        public NoteVisualizer notePanel;
        public Canvas canvas;

        public GameObject notePrefab;

        void Start()
        {
            _noteMatrixData = new NoteMatrix();

            //Грузим панельку
            _noteVisualizer = Instantiate(notePanel, canvas.transform, false);
            _noteVisualizer.createVisualRepresentation(_noteMatrixData.getNoteMatrix());
        }


        private void Update()
        {
            if (Input.GetButton("Fire1"))
            {
                //Debug.Log("Mouse Clicked!!!!");
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

                if (hit.collider != null)
                {
                    Note note = hit.collider.GetComponent<Note>();
                    if (isFirstNote() || (!selectedNotes.Contains(note.getCoord()) && valideNotes.Contains(note.getCoord())))
                    {
                        _noteVisualizer.perspectiveModeMass(valideNotes, false);
                        //Если была выбрана первая нотка, то вычисляем какие позиции сможет выбрать игрк                    
                        valideNotes = _noteMatrixData.getValidePositions(note.getCoord(), note.getType());
                        _noteVisualizer.perspectiveModeMass(valideNotes, true);
                        note.selectedModeOne(true);
                        selectedNotes.Add(note.getCoord());
                    }
                }
            }
            if (Input.GetButtonUp("Fire1"))
            {
                _noteVisualizer.selectedModeMass(selectedNotes,false);
                _noteVisualizer.perspectiveModeMass(valideNotes, false);
                if (selectedNotes.Count > 1)
                {
                    //Очки зачислены
                    _noteMatrixData.removeNote(selectedNotes);
                    //TODO - на потом вместо перерисовки - завести метод плавного движения ноток
                    _noteVisualizer.createVisualRepresentation(_noteMatrixData.getNoteMatrix());
                    currentScore = currentScore + selectedNotes.Count;
                    scoreText.text = "Score: " + currentScore;
                }
                selectedNotes.Clear();
                valideNotes.Clear();
                
            }
        }
        

        private List<NoteCoord> selectedNotes = new List<NoteCoord>();
        private List<NoteCoord> valideNotes = new List<NoteCoord>();
        

        private bool isFirstNote()
        {
            return selectedNotes.Count == 0;
        }
    }
}