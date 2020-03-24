﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Lab_02
{
    class JolkaCSP
    {
        public static int HORIZONTAL = 0;
        public static int VERTICAL = 1;


        public static bool CheckConstraint(char[][] puzzle, string word, int startRow, int startColumn, int direction)
        {
            if (CheckLength(puzzle, word, startRow, startColumn, direction))
            {
                if (CheckLetters(puzzle, word, startRow, startColumn, direction))
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }


        // dodać sprawdzanie czy proponowany wyraz zapełnia całą wolną przestrzeń


        public static bool CheckLength(char[][] puzzle, string word, int startRow, int startColumn, int direction)
        {
            if(puzzle.Length <= startRow || puzzle[0].Length <= startColumn)
            {
                Console.WriteLine("Poza zakresem");
                return false;
            }


            if(direction == VERTICAL)
            {
                //if (puzzle.Length - startRow > word.Length)
                //    return true;
                //else return false;

                return (puzzle.Length - startRow > word.Length) ? true : false;

            }
            else if(direction == HORIZONTAL)
            {
                //if (puzzle[0].Length - startColumn > word.Length)
                //    return true;
                //else return false;

                return (puzzle[0].Length - startColumn > word.Length) ? true : false;
            }
            else
            {
                Console.WriteLine("No coś namieszałeś!");
                return false;
            }

        }

        public static bool CheckLetters(char[][] puzzle, string word, int startRow, int startColumn, int direction)
        {
            char[] wordChars = new char[word.Length];
            wordChars = word.ToCharArray();

            if (direction == VERTICAL)
            {
                for(int i = 0; i< word.Length; i++)
                {
                    if (!puzzle[startRow + i][startColumn].Equals('_'))
                    {
                        if ( !wordChars[i].Equals(puzzle[startRow + i][startColumn]) || puzzle[startRow + i][startColumn].Equals('#'))
                        {
                            return false;
                        }

                    }
                }

                return true;

            }
            else if (direction == HORIZONTAL)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (!puzzle[startRow][startColumn + i].Equals('_'))
                    {
                        if (!wordChars[i].Equals(puzzle[startRow][startColumn + i]) || puzzle[startRow][startColumn + i].Equals('#'))
                        {
                            return false;
                        }

                    }
                }

                return true;
            }
            else
            {
                Console.WriteLine("No coś namieszałeś!");
                return false;
            }
        }

    }
}