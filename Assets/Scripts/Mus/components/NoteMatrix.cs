using System.Collections.Generic;
using UnityEngine;

namespace Mus
{
    public class NoteMatrix
    {
        private int maxX;
        private int maxY;
        private int[,] noteMatix;

        public NoteMatrix(int maxX = 3, int maxY = 6, int unique = 3)
        {
            this.maxX = maxX;
            this.maxY = maxY;
            createNotesMatrix(unique);
        }

        public int[,] getNoteMatrix()
        {
            return noteMatix;
        }


        public void createNotesMatrix(int uniqueNotes)
        {
            noteMatix = new int[maxX, maxY];
            generateRendomNotes(uniqueNotes);
        }


        public void generateRendomNotes(int uniqueNotes = 3)
        {
            noteMatix = new int[maxX, maxY];
            for (int i = 0; i < maxX; i++)
            {
                for (int j = 0; j < maxY; j++)
                {
                    noteMatix[i, j] = Random.Range(1, uniqueNotes + 1);
                }
            }
        }

        //List<NoteCoord> validNotes = new List<NoteCoord>();
        
        public List<NoteCoord> getValidePositions(Note curNote)
        {
            List<NoteCoord> validNotes = new List<NoteCoord>();
            getValidePositionsRecursive(curNote.noteCoord, curNote.type, validNotes);
            return validNotes;
        }

        private void getValidePositionsRecursive(NoteCoord coord, int type, List<NoteCoord> validNotes)
        {
            NoteCoord[] crossCoords =
            {
                new NoteCoord(coord.x + 1, coord.y),
                new NoteCoord(coord.x - 1, coord.y),
                new NoteCoord(coord.x, coord.y + 1),
                new NoteCoord(coord.x, coord.y - 1)
            };

            foreach (NoteCoord crossCoord in crossCoords)
            {
                if (!isCoordOutOfBounds(crossCoord) && getCoordsValue(crossCoord) == type  )
                {
                    validNotes.Add(crossCoord);
//                    getValidePositionsRecursive (coord, type, validNotes);
                    
                }
            }      
        }

        private int getCoordsValue(NoteCoord coords)
        {
            return noteMatix[coords.x,coords.y];
        }


        private bool isCoordOutOfBounds(NoteCoord coord)
        {
            return (coord.x < 0 || coord.x >= maxX || coord.y < 0 || coord.y >= maxY);
        }
    }
}