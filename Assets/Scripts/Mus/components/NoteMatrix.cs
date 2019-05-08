using System.Collections.Generic;
using UnityEngine;

namespace Mus
{
    public class NoteMatrix
    {
        private int x;
        private int y;
        private int[,] noteMatix;

        public NoteMatrix(int x = 3, int y = 6 , int unique = 3 )
        {
            this.x = x;
            this.y = y;
            createNotesMatrix(unique);
        }

        public int[,] getNoteMatrix()
        {
            return noteMatix;
        }


        public void createNotesMatrix(int uniqueNotes)
        {
            noteMatix = new int[x,y];
            generateRendomNotes(uniqueNotes);
        }
        

        public void generateRendomNotes(int uniqueNotes = 3)
        {
            noteMatix = new int[x,y];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    noteMatix[i, j] = Random.Range(1, uniqueNotes+1);
                }
            }               
        }

        public void getValidePositions(Note curNote)
        {
            List<NoteCoord> validNotes = new List<NoteCoord>();
            int cx = curNote.noteCoord.x;
            int cy = curNote.noteCoord.y;
            getValidePositionsRecursive (cx, cy, curNote.type, validNotes);
        }

        private void getValidePositionsRecursive (int cx, int cy, int type, List<NoteCoord> validNotes)
        {
            if (cx + 1 <= x && noteMatix[cx + 1, cy] == type)
            {
                //todo подумать как не порождать новый NoteCoord()
                validNotes.Add(new NoteCoord(cx + 1, cy));
                getValidePositionsRecursive (cx + 1, cy, type, validNotes);
            }
            if (cx - 1 >= 0 && noteMatix[cx - 1, cy] == type)
            {
                validNotes.Add(new NoteCoord(cx - 1, cy));
                getValidePositionsRecursive (cx - 1, cy, type, validNotes);
            }
            if (cy + 1 <= y && noteMatix[cx, cy + 1] == type)
            {
                validNotes.Add(new NoteCoord(cx, cy + 1));
                getValidePositionsRecursive (cx, cy + 1, type, validNotes);
            }
            if (cy -1  >= 0 && noteMatix[cx, cy - 1] == type)
            {
                validNotes.Add(new NoteCoord(cx, cy - 1));
                getValidePositionsRecursive (cx, cy - 1, type, validNotes);
            }
        }


    }
}