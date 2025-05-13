using System.Diagnostics.Metrics;
using static System.Runtime.InteropServices.JavaScript.JSType;

class TicTacToe
{

    static int currentPlayer = 1; // 1 - крестики, 2 - нолики
    static int numberOfMoves;
    static int n;
    static int currentPosition;
    static bool validSize;
    static bool validInput;
    static string[] board = new string[n * n];
    static int choice;
    static bool colorTheWinnings = false;
    static string checkForWin;
    static void Colorize(string position, ConsoleColor colour)
    {
        Console.ForegroundColor = colour;
        Console.Write($" {position} ");
        Console.ForegroundColor = ConsoleColor.White;
    }
    static void DrawBoard()
    {
        for (int i = 0; i < board.Length; i++)
        {
            int size = n * n;
            int strLenght = size.ToString().Length;
            int elLength = board[i].Length;
            int numberOfSpaces = strLenght - elLength;
            int num = i + 1;

            Console.Write("|");

            //Определяем условие для выигрышной комбинации
            if (checkForWin == "column")
            {
                int col = ((currentPosition % n) == 0) ? n : currentPosition % n;
                colorTheWinnings = ((i >= col - 1) && (i < col + n * (n - 1)) && (i % n == col - 1)) ? true : false;
            }
            if (checkForWin == "string")
            {
                double str1 = Math.Ceiling(Convert.ToDouble(currentPosition) / n);
                int str = Convert.ToInt32(str1);
                colorTheWinnings = (i >= n * (str - 1) && i < n + n * (str - 1)) ? true : false;

            }
            if (checkForWin == "rightDiagonal")
            {
                colorTheWinnings = (i >= 0 && i < n * n && i % (n + 1) == 0) ? true : false;
            }
            if (checkForWin == "leftDiagonal")
            {
                colorTheWinnings = (i >= n - 1 && i <= n * (n - 1) && i % (n - 1) == 0) ? true : false;
            }
            //Выводим ячейки игрового поля

            //Раскрашиваем выигрышную комбинацию в случае победы одного из игроков
            if (colorTheWinnings)
            {
                Colorize(board[i], ConsoleColor.Red);
            }

            // Раскрашиваем текущую позицию
            else if (i == currentPosition - 1)
            {
                Colorize(board[i], ConsoleColor.Green);
            }
            else
            {
                Console.Write($" {board[i]} ");
            }
            //Выводим вертикальные и горизонтальные линии            
            for (int z = 0; z < numberOfSpaces; z++)
            {
                Console.Write(" ");
            }
            if (num % n == 0)
            {
                Console.Write("|");
                Console.WriteLine();
                for (int j = 0; j < n; j++)
                {
                    for (int k = 0; k < strLenght + 3; k++)
                    {
                        Console.Write("-");
                    }

                }
                Console.WriteLine();
            }

        }
        Console.WriteLine();
    }
    static void Move(int choice)
    {
        currentPosition = choice;
        board[currentPosition - 1] = (currentPlayer == 1) ? "X" : "O";
        numberOfMoves++;
    }
    static bool CheckForWin()
    {
        string symbol = (currentPlayer == 1) ? "X" : "O";

        //Проверяем символы в строке позиции
        //Находим номер строки
        double str1 = Math.Ceiling(Convert.ToDouble(currentPosition) / n);
        int str = Convert.ToInt32(str1);
        int countStr = 0;
        for (int i = n * (str - 1); i < n * str; i++)
        {
            if (board[i] != symbol)
            {
                break;
            }
            countStr++;
        }
        //Проверяем символы в колонке позиции
        //Находим номер колонки
        int column = ((currentPosition % n) == 0) ? n : currentPosition % n;

        int countCol = 0;
        for (int i = column - 1; i < column + n * (n - 1); i = i + n)
        {
            if (board[i] != symbol)
            {
                break;
            }
            countCol++;
        }

        //Проверяем правую диагональ
        int rightDiagonal = 0;
        if (column == str)
        {
            for (int i = 0; i < (n * n); i = i + n + 1)
            {
                if (board[i] != symbol)
                {
                    break;
                }
                rightDiagonal++;
            }
        }

        //Проверяем левую диагональ
        int leftDiagonal = 0;
        if (str + column == n + 1)
        {
            for (int i = n - 1; i <= n * (n - 1); i = i + n - 1)
            {
                if (board[i] != symbol)
                {
                    break;
                }
                leftDiagonal++;
            }
        }

        //Передаем выигрышную комбинацию в случае победы игрока
        if (countCol == n)
        {
            checkForWin = "column";
        }
        if (countStr == n)
        {
            checkForWin = "string";
        }
        if (rightDiagonal == n)
        {
            checkForWin = "rightDiagonal";
        }
        if (leftDiagonal == n)
        {
            checkForWin = "leftDiagonal";
        }

        return (countStr == n || countCol == n || rightDiagonal == n || leftDiagonal == n);
    }
    static bool CheckForDraw()
    {
        return numberOfMoves == n * n;
    }

    static void Main()
    {
        numberOfMoves = 0;
        currentPosition = 0;
        do
        {
            Console.WriteLine("Введите размер игрового поля (от 3 до 15):");
            validSize = int.TryParse(Console.ReadLine(), out n) && n >= 3 && n <= 15;

            if (!validSize)
            {
                Console.WriteLine("Некорректный ввод");
            }
        } while (validSize == false);

        Array.Resize(ref board, n * n);

        for (int i = 0; i < n * n; i++)
        {
            int index = i + 1;
            board[i] = index.ToString();
        }

        do
        {
            Console.Clear();
            DrawBoard();

            do
            {
                Console.WriteLine($"Игрок {currentPlayer}, введите номер ячейки:");
                // Проверяем корректность ввода: число от 1 до n, и ячейка не должна быть занята
                validInput = int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= n * n && board[choice - 1] != "X" && board[choice - 1] != "0";
                if (!validInput)
                {
                    Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                }
            }
            while (validInput == false);

            //Делаем ход                
            Move(choice);

            // Проверяем на наличие выигрышной комбинации
            if (CheckForWin())
            {
                Console.Clear();
                DrawBoard();
                Console.WriteLine($"Победил игрок {currentPlayer}!");
                break;
            }

            // Проверяем на наступление ничьей
            if (CheckForDraw())
            {
                Console.Clear();
                DrawBoard();
                Console.WriteLine("Ничья!");
                break;
            }

            // Меняем текущего игрока
            currentPlayer = (currentPlayer == 1) ? 2 : 1;

        } while (true);

    }
            
         
}