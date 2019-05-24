using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mus
{
    public class NoteVisualizer : MonoBehaviour
    {
        //UNITY ПАБЛИКИ
        public GameObject notePrefab;


//        private NoteMatrix noteMatrixData;
        //Содержит ссылки на все нотки
        private Dictionary<NoteCoord, GameObject> notesGameObjects = new Dictionary<NoteCoord, GameObject>();

        public GameObject getNoteObject(NoteCoord coord)
        {
            return notesGameObjects[coord];
        }


        public void initNotePanel(NoteMatrix noteMatrixData)
        {
            var enumNotePlace = transform.GetEnumerator();

            int[,] notes = noteMatrixData.getNoteMatrix();
            for (int x = 0; x <= notes.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= notes.GetUpperBound(1); y++)
                {
                    if (enumNotePlace.MoveNext())
                        addNote((Transform) enumNotePlace.Current, notes[x, y], x, y);
                }
            }
        }


        private void addNote(Transform parrentTransform, int noteId, int x, int y)
        {
//            foreach (Transform child in parrentTransform)
//            {
//                Destroy(child.gameObject);
//            }

            GameObject noteGameObject = Instantiate(notePrefab, parrentTransform, false);
            var image = noteGameObject.GetComponent<Image>();
            switch (noteId)
            {
                case 1:
                    image.color = Color.red;
                    break;
                case 2:
                    image.color = Color.green;
                    break;
                case 3:
                    image.color = Color.blue;
                    break;
            }

            Note note = noteGameObject.GetComponent<Note>();
            note.type = noteId;
            NoteCoord nc = new NoteCoord(x, y);
            note.noteCoord = nc;
            notesGameObjects.Add(nc, noteGameObject);
        }


        //Включить отображение следующих нот
        private void perspectiveModeOne(NoteCoord noteCoord, bool on)
        {
            Note note = notesGameObjects[noteCoord].GetComponent<Note>();
            note.perspectiveModeOne(on);
        }

        //Включить отображение отмеченных нот
        private void selectedModeOne(NoteCoord noteCoord, bool on)
        {
            Note note = notesGameObjects[noteCoord].GetComponent<Note>();
            note.selectedModeOne(on);
        }
    }
}