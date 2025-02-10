using FourInRow;

int[,] board = new int[6, 7];
int X = 0,Player =1,LoseValue = -10;
ConsoleKey key;
bool Drop(int x,int value)
{//droping animation 
    bool canDrop= false;
    Console.Clear();
    int y = 0;
    do
    {
        
        if (y>0)
            board[y - 1, x] = 0;
        if (y != 0)
        {
            board[y, x] = value;
            canDrop = true;
        }
        else if (y == 0 && board[1, x] == 0)
            board[1, x] = 0;
        View.Print(board,-1);
        Thread.Sleep(300);
        Console.Clear();
        if (y >= board.GetLength(0) - 1)
            break;
        y++;

    } while (board[y, x] == 0);
    return canDrop; 
}
void SwitchPlayer()
{
    Player *= -1;
    board[0, X] = Player;
}
void Movement(bool forward)
{//movement on the first row in the to drop the token in the board
    int steps,step;
    if (forward) {
        steps = 1;
        step = 1;
    }
    else {
        steps = -1;
        step = -1;
    }
    bool CanProceed = false;
    for (int i = X+steps;i <= board.GetLength(0)&&i>=0; i+=step)
    {
        if (board[0, i] != 0)
        {
            if (forward)
                steps++;
            else
                steps--;
        }
        else
            CanProceed = true;  
    }
    if (!CanProceed)
        return;
    if (X + steps <= board.GetLength(1)-1&&X+steps >=0)
    {
        board[0, X + steps] = Player;
        board[0, X] = 0;
        X += steps;
    }
}
bool KeyHandler(ConsoleKey key)
{//key handler for movement and droping token
    switch (key)
    {
        case ConsoleKey.LeftArrow:
            Movement(false);
            break;
            case ConsoleKey.RightArrow:
            Movement(true);
            break;
        case ConsoleKey.Enter:
            if (Drop(X, Player))
                SwitchPlayer();
            
            break;
    }
    return true;
}


bool checkRow(int[] arr)
{
    int count = 0;
        for(int j=arr[1];j< board.GetLength(1);j++)
        {
        if (board[arr[0], j] == arr[2])
            count++;
        else if (count == 4)
            return true;
        else
            break;

    }
        if (count ==4)
            return true;
        return false;
}
bool checkCol(int[] arr)
{
    int count = 0;
    for (int i = arr[0]; i < board.GetLength(0); i++)
    {
        if (board[i,arr[1]] == arr[2])
            count++;
        else if (count == 4)
            return true;
        else
            break;

    }
    if (count == 4)
        return true;
    return false;
}
bool checkAxis(int[] arr)
{
    int count = 0;
    for(int i = arr[0],j = arr[1];i<board.GetLength(0)&&j<board.GetLength(1);i++,j++) 
    {
        if (board[i, j] == arr[2])
            count++;
        else if (count == 4)
            return true;
        else
            break;

    }
    if (count == 4)
        return true;
    return false;
}
bool checkReverse(int[] arr)
{
    int count = 0;
    
    for (int i = arr[0], j = arr[1]; i < board.GetLength(0) && j >=0; i++, j--)
    {
        if (board[i, j] == arr[2])
            count++;
        else if (count == 4)
            return true;
        else
            break;

    }
    if (count == 4)
        return true;
    return false;
}

int CheckWin()
{
    bool draw = true;
    for(int i = 1; i < board.GetLength(0); i++)
        for (int j = 0,value; j < board.GetLength(1); j++)
        {
            int[] values = [i,j,board[i,j]];
            if (board[i, j] != 0)
                if(checkRow(values) ||checkCol(values)||checkAxis(values)||checkReverse(values))
                    return board[i, j];
            if (board[i, j] ==0)
                draw = false;
        }
    if (draw)
        return LoseValue;
    return 0;
}

void SelectCharcter()
{
    key = ConsoleKey.X;
    do
    {
        Console.WriteLine("Wellcome to 4 In A Row game, plase select your cherchter\nPress 'X' for X\nPress 'O' for O");
        if (key != ConsoleKey.X && key != ConsoleKey.O)
        {
            Console.WriteLine("Incorrect Key please select again");
        }
        key = Console.ReadKey().Key;
        if (key == ConsoleKey.X)
            Player = 1;
        else if (key == ConsoleKey.O)
            Player = -1;
        Console.Clear();

    } while (key != ConsoleKey.O && key != ConsoleKey.X);
}
void ResultGame(int result)
{
    View.Print(board, X);
    if (result == LoseValue)
        Console.WriteLine("Draw");
    else if (result == 1)
        Console.WriteLine("X player Won");
    else if (result == -1)
        Console.WriteLine("O player Won");
}
void Game()
{
    int result;
    SelectCharcter();
    board[0, X] = Player;
    do
    {
        Console.Clear();
        Console.WriteLine("its {0} player turn",Player==1?"X":"O");
        View.Print(board,X);
        key = Console.ReadKey().Key;
        KeyHandler(key);
        result = CheckWin();
    } while (result==0);
    ResultGame(result);
}
Game();