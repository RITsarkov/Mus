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

        public void removeNotes()
        {
            
        }
    }
}