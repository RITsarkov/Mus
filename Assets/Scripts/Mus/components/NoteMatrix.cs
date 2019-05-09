using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Mus
{
    public class NoteMatrix
    {
        private int maxX;
        private int maxY;
        private int[,] noteMatix;
        private int uniqueNotes;

        public NoteMatrix(int maxX = 3, int maxY = 6, int unique = 3)
        {
            this.maxX = maxX;
            this.maxY = maxY;
            this.uniqueNotes = unique;
            createNotesMatrix();
        }

        public int[,] getNoteMatrix()
        {
            return noteMatix;
        }


        public void createNotesMatrix()
        {
            noteMatix = new int[maxX, maxY];
            generateRendomNotes();
        }


        public void generateRendomNotes()
        {
            noteMatix = new int[maxX, maxY];
            for (int i = 0; i < maxX; i++)
            {
                for (int j = 0; j < maxY; j++)
                {
                    noteMatix[i, j] = getRendomNote();
                }
            }
        }


        public void removeNote(List<NoteCoord> noteCoords)
        {
            //Все нотки которые будут удалены проставляем в матрицу
            foreach (var coord in noteCoords)
            {
                setNoteValue(coord, -2);
            }
            
            for (int i = 0; i < maxX; i++)
            {
                //Формируем строку с удаленными нотами
                List<int> newLine = new List<int>();
                for (int j = 0; j < maxY; j++)
                {
                    if (noteMatix[i, j] != -2)
                    {
                        newLine.Add(noteMatix[i, j]);
                    }
                }
                //Заменяем
                for (int j = 0; j < maxY; j++)
                {
                    if (j < newLine.Count)
                    {
                        noteMatix[i, j] = newLine[j];
                    }
                    else
                        noteMatix[i, j] = getRendomNote();
                }
            }
        }

        private int getRendomNote()
        {
            return Random.Range(1, uniqueNotes + 1);
        }


//================== Расчет позиций для нахождения соседних нот того-же типа =============================
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
                if (!validNotes.Contains(crossCoord) && !isCoordOutOfBounds(crossCoord) && getNoteValue(crossCoord) == type)  
                {
                    validNotes.Add(crossCoord);
                    //todo для рекурсии
                    //getValidePositionsRecursive (crossCoord, type, validNotes);
                }
            }      
        }
        
        
        private int getNoteValue(NoteCoord coords)
        {
            return noteMatix[coords.x,coords.y];
        }
        
        private void setNoteValue(NoteCoord coords, int val)
        {
            noteMatix[coords.x,coords.y] = val;
        }


        private bool isCoordOutOfBounds(NoteCoord coord)
        {
            return (coord.x < 0 || coord.x >= maxX || coord.y < 0 || coord.y >= maxY);
        }
    }
}