using System;

namespace Study
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            string pathForMap = "map.txt";
            char [,] map = ReadFile(pathForMap);
            ConsoleKeyInfo pressedKey = new ConsoleKeyInfo('w', ConsoleKey.W, false, false, false);

            int playerX = 1;
            int playerY = 1;
            int score = 0;

            while (true)
            {
                Console.Clear();
                DrawMap(map);

                Console.SetCursorPosition(playerX, playerY);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"@");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(29, 0);
                Console.Write($"Score: {score}");

                pressedKey = Console.ReadKey();
                HandleInput(pressedKey, ref playerX, ref playerY, map, ref score);
            }
        }

        static char[,] ReadFile(string path)
        {
            string[] file = File.ReadAllLines(path);

            char[,] map = new char[GetMaxLengthOfLine(file), file.Length];

            for (int x = 0; x < map.GetLength(0); x++) {
                
                for (int y = 0; y < map.GetLength(1); y++) {
                    map[x, y] = file[y][x];
                        }
            }
            return map;
        } 

        static void DrawMap(char[,] map)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    if (map[x, y] == '$') 
                    { 
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    Console.Write(map[x, y]);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                Console.Write("\n");
            }
        }

        static void HandleInput(ConsoleKeyInfo pressedKey, ref int playerX, ref int playerY, char[,] map, ref int score)
        {
            int[] direction = GetDirection(pressedKey);

            int nextPlayerPositionX = playerX + direction[0];
            int nextPlayerPositionY = playerY + direction[1];
            char nextCell = map[nextPlayerPositionX, nextPlayerPositionY];

            if (nextCell == ' ' || nextCell == '$') 
            {
                playerX = nextPlayerPositionX;
                playerY = nextPlayerPositionY;

                if (nextCell == '$') {
                    score++;
                    map[nextPlayerPositionX, nextPlayerPositionY] = ' ';
                }
            }

            if (map[nextPlayerPositionX, nextPlayerPositionY] == ' ')
            {
                playerX = nextPlayerPositionX;
                playerY = nextPlayerPositionY;
            }
        }

        static int[] GetDirection(ConsoleKeyInfo pressedKey)
        {
            int[] direction = { 0, 0 };

            if (pressedKey.Key == ConsoleKey.UpArrow)
            {
                direction[1] = -1;
            }
            else if (pressedKey.Key == ConsoleKey.DownArrow)
            {
                direction[1] = 1;
            }
            else if (pressedKey.Key == ConsoleKey.LeftArrow)
            {
                direction[0] = -1;
            }
            else if (pressedKey.Key == ConsoleKey.RightArrow)
            {
                direction[0] = 1;
            }

            return direction;
        }
        static int GetMaxLengthOfLine(string[] lines)
        {
            int maxLength = lines[0].Length;

            foreach (string line in lines) { 
            if (line.Length > maxLength)
                {
                      maxLength = line.Length;
                }
            }
            return maxLength;
        }
    }
}