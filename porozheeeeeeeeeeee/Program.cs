using System;

// کلاس سودوکو
class Sudoku
{
    private int[,] grid;
    private int size;

    public Sudoku(int[,] g)
    {
        grid = g;
        size = (int)Math.Sqrt(grid.Length);
    }


    private bool CheckRow(int row, int num)
    {
        for (int col = 0; col < size; col++)
        {
            if (grid[row, col] == num)
                return false;
        }

        return true;
    }


    private bool CheckCol(int col, int num)
    {
        for (int row = 0; row < size; row++)
        {
            if (grid[row, col] == num)
                return false;
        }

        return true;
    }


    private bool CheckBox(int boxRow, int boxCol, int num)
    {
        int rowOffset = boxRow * (size / 3);
        int colOffset = boxCol * (size / 3);
        for (int row = 0; row < (size / 3); row++)
        {
            for (int col = 0; col < (size / 3); col++)
            {
                if (grid[row + rowOffset, col + colOffset] == num)
                    return false;
            }
        }

        return true;
    }


    private bool CheckNum(int row, int col, int num)
    {
        int boxRow = row / (size / 3);
        int boxCol = col / (size / 3);
        return CheckRow(row, num) && CheckCol(col, num) && CheckBox(boxRow, boxCol, num);
    }


    public bool Solve()
    {
        int row = -1;
        int col = -1;
        bool isEmpty = true;
        for (int i = 0; i < size && isEmpty; i++)
        {
            for (int j = 0; j < size && isEmpty; j++)
            {
                if (grid[i, j] == 0)
                {
                    row = i;
                    col = j;
                    isEmpty = false;
                }
            }
        }


        if (isEmpty)
            return true;


        for (int num = 1; num <= size; num++)
        {
            if (CheckNum(row, col, num))
            {
                grid[row, col] = num;
                if (Solve())
                    return true;
                grid[row, col] = 0;
            }
        }


        return false;
    }


    public void Display()
    {
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                Console.Write("{0} ", grid[row, col]);
                if ((col + 1) % Math.Sqrt(size) == 0 && col != size - 1)
                    Console.Write("| ");
            }

            Console.WriteLine();
            if ((row + 1) % Math.Sqrt(size) == 0 && row != size - 1)
                Console.WriteLine(new string('-', size * 2 + (int)Math.Sqrt(size) * 2 - 1));
        }
    }
}


class Program
{
    static void Main()
    {
        int[,] sudokuGrid = new int[9, 9]
        {
            { 0, 0, 5, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 8, 0, 0 },
            { 2, 7, 8, 3, 5, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 3, 0, 0, 2, 8 },
            { 0, 6, 0, 0, 4, 3, 0, 1, 0 },
            { 8, 1, 0, 0, 7, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 2, 5, 6, 3, 7 },
            { 0, 0, 3, 7, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 4, 0, 0 }
        };

        Sudoku sudoku = new Sudoku(sudokuGrid);
        Console.WriteLine("Sudoku puzzle:");
        sudoku.Display();
        Console.WriteLine();

        if (sudoku.Solve())
        {
            Console.WriteLine("Sudoku solution:");
            sudoku.Display();
        }
        else
        {
            Console.WriteLine("No solution exists!");
        }
    }
}