using System.Diagnostics.Metrics;
using static System.Runtime.InteropServices.JavaScript.JSType;

class TicTacToe
{
    const int X = 1;//крестики
    const int O = 2;//нолики
    static int currentPlayer = 1; 
    static int numberOfMoves;
    static int n;
    static int currentPosition;
    static bool validSize;
    static bool validInput;
    static string[] board = new string[n * n];
    static int choice;
    static bool colorTheWinnings = false;
    static string checkForWin;
    static string symbol;
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
            if (colorTheWinnings)
            {
                Colorize(board[i], ConsoleColor.Red);
            }
            else if (i == currentPosition - 1)
            {
                Colorize(board[i], ConsoleColor.Green);
            }
            else
            {
                Console.Write($" {board[i]} ");
            }                
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
        board[currentPosition - 1] = (currentPlayer == X) ? "X" : "O";
        symbol = (currentPlayer == X) ? "X" : "O";
        numberOfMoves++;        
    }
    static bool CheckForWin()
    {  
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
        if (countStr == n)
        {
            checkForWin = "string";
        }
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
        if (countCol == n)
        {
            checkForWin = "column";
        }
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
        if (rightDiagonal == n)
        {
            checkForWin = "rightDiagonal";
        }
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
    static void ChangeOfCourse()
    {
        currentPlayer = (currentPlayer == X) ? O : X;
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
                
                validInput = int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= n * n && board[choice - 1] != "X" && board[choice - 1] != "0";
                if (!validInput)
                {
                    Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                }
            }
            while (validInput == false);                                
            Move(choice);           
            if (CheckForWin())
            {
                Console.Clear();
                DrawBoard();
                Console.WriteLine($"Победил игрок {currentPlayer}!");
                break;
            }           
            if (CheckForDraw())
            {
                Console.Clear();
                DrawBoard();
                Console.WriteLine("Ничья!");
                break;
            }
            ChangeOfCourse();
        } while (true);
    }
}
