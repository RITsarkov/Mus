using System.Collections.Generic;
using UnityEngine;

namespace Mus
{
    public class NoteVisualizer : MonoBehaviour
    {
        //UNITY ПАБЛИКИ
        public Note notePrefab;

        
        //Содержит ссылки на все нотки
        private Dictionary<NoteCoord, Note> notesGameObjects = new Dictionary<NoteCoord, Note>();
        private Dictionary<NoteCoord, Vector3> notesGuiPositions = new Dictionary<NoteCoord, Vector3>();

        //TODO хм... а зачем это тут?
        public Note getNoteObject(NoteCoord coord)
        {
            return notesGameObjects[coord];
        }
        
        //TODO положение первой ноты
//        private Transform noteFirst = new Transform(); 
//        private RectTransform firstTransform = new RectTransform
//        {
//            anchorMin = new Vector2(0,1F),
//            anchorMax = new Vector2(0,1F),
//            position = new Vector3(120,0,120)
//        };
        private int startX = 73;
        private int startY = -73;
        private int deltaX = 120;
        private int deltaY = -120;

//        public void createVisualRepresentation(int[,] noteMatrixData)
//        {
//            var enumNotePlace = transform.GetEnumerator();
//            for (int x = 0; x <= noteMatrixData.GetUpperBound(0); x++)
//            {
//                for (int y = 0; y <= noteMatrixData.GetUpperBound(1); y++)
//                {
//                    if (enumNotePlace.MoveNext())
//                        addNote((Transform) enumNotePlace.Current, noteMatrixData[x, y], x, y);
//                }
//            }
//        }
        
        public void createVisualRepresentation(int[,] noteMatrixData)
        {

            for (int y = 0; y <= noteMatrixData.GetUpperBound(0); y++)
            {
                for (int x = 0; x <= noteMatrixData.GetUpperBound(1); x++)
                {
                    addNote(noteMatrixData[y, x], x, y);
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


//        private void addNote(Transform transform, int noteId, int x, int y)
//        {
//            Note newNote = Instantiate(notePrefab, transform, false);
//            newNote.setColor(getColor(noteId));
//            NoteCoord nc = new NoteCoord(x, y);
//            newNote.setCoord(nc);
//            newNote.setType(noteId);
//            notesGameObjects.Add(nc, newNote);
//            notesGuiPositions.Add(nc, transform.position);
//        }
        
        private void addNote(int noteId, int x, int y)
        {
            Note newNote = Instantiate(notePrefab, transform, false);
            
            //Выставляем позицию ноты
            ((RectTransform) newNote.transform).anchorMin = new Vector2(0, 1F);
            ((RectTransform) newNote.transform).anchorMax = new Vector2(0, 1F);
//            ((RectTransform) newNote.transform).localPosition = new Vector3(startY+deltaY*y,startX+deltaX*x, 0);
            ((RectTransform) newNote.transform).anchoredPosition = new Vector2(startX+deltaX*x,startY+deltaY*y);
            ((RectTransform) newNote.transform).localScale = Vector3.one;
            
            newNote.name = "x" + x + "y" + y;
            
            newNote.setColor(getColor(noteId));
            NoteCoord nc = new NoteCoord(x, y);
            newNote.setCoord(nc);
            newNote.setType(noteId);
            notesGameObjects.Add(nc, newNote);
            notesGuiPositions.Add(nc, transform.position);
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