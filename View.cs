using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourInRow
{
    public static class View
    {
        public static void Color(int value)
        {
            switch (value)
            {
                case 0:
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor= ConsoleColor.White;
                    break;
                case 1:
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor= ConsoleColor.Black;
                    break;
                case -1:
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor= ConsoleColor.Black;
                    break;
                case 2:
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case -2:
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case 4:
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor= ConsoleColor.Black;
                    break;  
            }
           
        }
        public static void Print(int[,] borad,int X)
        {
           for (int i = 0; i < borad.GetLength(0); i++,Console.WriteLine())
            {
                for(int j = 0; j < borad.GetLength(1); j++)
                {
                    if (i == 0)
                        if (j == X)
                            Color((Math.Abs(borad[i, j])+1) * borad[i, j]);
                        else
                            Color(4);
                    else
                        Color(borad[i, j]);
                    switch(borad[i,j]) 
                    {
                        case 0:
                            Console.Write("_");
                            break;
                        case 1:
                            Console.Write("X");
                            break;
                        case -1:
                            Console.Write("O");
                            break;
                    }
                }
            }
            Color(0);
        }
    }
}
