using System;

class Program
{
    static void Main()
    {
        bool repeat = true; // Флаг для управления повторением выбора операций
        string knightPos = string.Empty; // Координаты коня
        string piecePos = string.Empty; // Координаты другой фигуры

        // Основной цикл программы, продолжается до тех пор, пока пользователь не выберет выход
        while (repeat)
        {
            // Вывод меню выбора действий
            Console.WriteLine("Выберите одно из действий:");
            Console.WriteLine("1. Разместить фигуры на шахматной доске");
            Console.WriteLine("2. Определить, бьет ли конь фигуру");
            Console.WriteLine("3. Выйти из программы");
            Console.Write("Ваш выбор: ");

            // Проверка на ввод числа
            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Некорректный ввод. Попробуйте еще раз.");
                Console.WriteLine();
                continue;
            }

            // Выполнение выбранного действия
            switch (choice)
            {
                case 1:
                    SetupBoard(out knightPos, out piecePos);
                    break;
                case 2:
                    if (knightPos != string.Empty && piecePos != string.Empty)
                    {
                        CheckCapture(knightPos, piecePos);
                    }
                    else
                    {
                        Console.WriteLine("Фигуры на шахматной доске не размещены");
                    }
                    break;
                case 3:
                    repeat = false; // Завершение программы
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте еще раз.");
                    break;
            }

            // Переход на новую строку для удобства чтения
            Console.WriteLine();
        }
    }

    static void SetupBoard(out string knightPos, out string piecePos)
    {
        // Инициализация доски
        char[,] board = new char[8, 8];
        InitializeBoard(board);

        // Запрос координат для размещения коня и фигуры
        Console.WriteLine("Введите координаты коня и фигуры (в формате x1y1 x2y2):");
        string input = Console.ReadLine();

        // Разделение введённых координат
        string[] coordinates = input.Split(' ');

        // Проверка корректности введённых координат
        if (coordinates.Length != 2 || coordinates[0] == coordinates[1] || !ValidateCoordinates(coordinates[0]) || !ValidateCoordinates(coordinates[1]))
        {
            Console.WriteLine("Введены некорректные координаты");
            knightPos = string.Empty;
            piecePos = string.Empty;
            return;
        }

        // Размещение фигур на доске
        knightPos = coordinates[0];
        piecePos = coordinates[1];
        PlacePieces(board, knightPos, piecePos);

        // Вывод доски
        DrawBoard(board);
    }

    static void CheckCapture(string knightPos, string piecePos)
    {
        // Вывод информации о проверке
        Console.WriteLine("Операция   2: Определение, бьет ли конь фигуру");
        Console.WriteLine();

        // Вычисление координат коня и фигуры
        int knightX = knightPos[0] - 'a';
        int knightY = knightPos[1] - '1';

        int pieceX = piecePos[0] - 'a';
        int pieceY = piecePos[1] - '1';

        // Проверка, может ли конь побить фигуру
        if (Math.Abs(knightX - pieceX) == 2 && Math.Abs(knightY - pieceY) == 1 || Math.Abs(knightX - pieceX) == 1 && Math.Abs(knightY - pieceY) == 2)
        {
            Console.WriteLine("Конь сможет побить фигуру за   1 ход");
        }
        else
        {
            Console.WriteLine("Конь не может побить фигуру за   1 ход");
        }
    }

    static void InitializeBoard(char[,] board)
    {
        // Заполнение доски пустыми клетками
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                board[row, col] = '-';
            }
        }
    }

    static void PlacePieces(char[,] board, string knightPos, string piecePos)
    {
        // Вычисление координат коня и фигуры
        int knightX = knightPos[0] - 'a';
        int knightY = knightPos[1] - '1';

        int pieceX = piecePos[0] - 'a';
        int pieceY = piecePos[1] - '1';

        // Размещение коня и фигуры на доске
        MoveKnight(board, knightX, knightY);
        PlacePiece(board, pieceX, pieceY, 'F');
    }

    static void MoveKnight(char[,] board, int x, int y)
    {
        // Проверка корректности координат
        if (x >= 0 && x < 8 && y >= 0 && y < 8)
        {
            board[y, x] = 'N'; // Размещаем коня на выбранных координатах
        }
    }

    static void PlacePiece(char[,] board, int x, int y, char piece)
    {
        // Проверка корректности координат
        if (x >= 0 && x < 8 && y >= 0 && y < 8)
        {
            board[y, x] = piece; // Размещаем фигуру на выбранных координатах
        }
    }

    static void DrawBoard(char[,] board)
    {
        // Вывод заголовка доски
        Console.WriteLine("   a b c d e f g h");

        // Вывод доски в обратном порядке (с верхней стороны)
        for (int row = 7; row >= 0; row--)
        {
            Console.Write($"{row + 1} ");

            for (int col = 0; col < 8; col++)
            {
                Console.Write(board[row, col] + " ");
            }

            Console.WriteLine();
        }
    }

    static bool ValidateCoordinates(string coordinate)
    {
        // Проверка длины строки координат
        if (coordinate.Length != 2)
        {
            return false;
        }

        // Проверка диапазона символов координат
        char file = coordinate[0];
        char rank = coordinate[1];

        if (file < 'a' || file > 'h' || rank < '1' || rank > '8')
        {
            return false;
        }

        return true;
    }
}
//