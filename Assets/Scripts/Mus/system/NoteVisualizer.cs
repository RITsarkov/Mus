using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

namespace Mus
{
    public class NoteVisualizer : MonoBehaviour
    {
        //UNITY ПАБЛИКИ
        public Note notePrefab;

        
        //Содержит ссылки на все нотки
        private Dictionary<NoteCoord, Note> notesGameObjects = new Dictionary<NoteCoord, Note>();

        //TODO хм... а зачем это тут?
        public Note getNoteObject(NoteCoord coord)
        {
            return notesGameObjects[coord];
        }


        public void createVisualRepresentation(int[,] noteMatrixData)
        {
            var enumNotePlace = transform.GetEnumerator();
            for (int x = 0; x <= noteMatrixData.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= noteMatrixData.GetUpperBound(1); y++)
                {
                    if (enumNotePlace.MoveNext())
                        addNote((Transform) enumNotePlace.Current, noteMatrixData[x, y], x, y);
                }
            }
        }
        

        private Color getColor(int index)
        {
            switch (index)
            {
                case 1: return Color.red;
                case 2: return Color.green;
                default: return Color.blue;
            }
        }


        private void addNote(Transform parrentTransform, int noteId, int x, int y)
        {
            Note newNote = Instantiate(notePrefab, parrentTransform, false);
            newNote.setColor(getColor(noteId));
            NoteCoord nc = new NoteCoord(x, y);
            newNote.setCoord(nc);
            newNote.setType(noteId);
            notesGameObjects.Add(nc, newNote);
        }
        
        
        //Включить подсветку для следующих нот из списка
        public void perspectiveModeMass(IEnumerable<NoteCoord> valideNotes, bool isOn)
        {
            if (valideNotes == null)
                return;
            foreach (NoteCoord noteCoord in valideNotes)
            {
                notesGameObjects[noteCoord].perspectiveModeOne(isOn);
            }
        }
        
        
        //Включить подсветку для выделенных нот
        public void selectedModeMass(IEnumerable<NoteCoord> selectedNotes, bool isOn)
        {
            if (selectedNotes == null)
                return;
            foreach (NoteCoord coord in selectedNotes)
            {
                Note note = notesGameObjects[coord];
                note.selectedModeOne(isOn);
                note.perspectiveModeOne(!isOn);
            }
        }
        

        //Включить отображение следующих нот
        private void perspectiveMode(NoteCoord noteCoord, bool isOn)
        {
            Note note = notesGameObjects[noteCoord];
            note.perspectiveModeOne(isOn);
        }

        //Включить отображение отмеченных нот
        private void selectedMode(NoteCoord noteCoord, bool isOn)
        {
            Note note = notesGameObjects[noteCoord];
            note.selectedModeOne(isOn);
        }
    }
}