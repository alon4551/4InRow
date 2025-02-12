using FourInRow;
using System.ComponentModel;

int[,] board = new int[6, 7];
int X = 0,Player =1,LoseValue = -10;
ConsoleKey key;
Random rand = new Random();
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
            return false;
        case ConsoleKey.RightArrow:
            Movement(true);
            return false;
        case ConsoleKey.Enter:
            if (!Drop(X, Player))
                return false;
            break;
        default:
            return false;
    }
    return true;
}


int  checkRow(int[] arr)
{
    int count = 0;
        for(int j=arr[1];j< board.GetLength(1);j++)
        {
        if (board[arr[0], j] == arr[2])
            count++;
        else if (count == 4)
            break;
        else
            break;

    }
        return count;
}
int checkCol(int[] arr)
{
    int count = 0;
    for (int i = arr[0]; i < board.GetLength(0); i++)
    {
        if (board[i,arr[1]] == arr[2])
            count++;
        else if (count == 4)
            break ;
        else
            break;

    }
    return count;
}
int checkAxis(int[] arr)
{
    int count = 0;
    for(int i = arr[0],j = arr[1];i<board.GetLength(0)&&j<board.GetLength(1);i++,j++) 
    {
        if (board[i, j] == arr[2])
            count++;
        else if (count == 4)
            break;
        else
            break;

    }
    return count;
}
int checkReverse(int[] arr)
{
    int count = 0;
    
    for (int i = arr[0], j = arr[1]; i < board.GetLength(0) && j >=0; i++, j--)
    {
        if (board[i, j] == arr[2])
            count++;
        else if (count == 4)
            break;
        else
            break;

    }
    return count;
}
int CheckWin(int value)
{
    bool draw = true;
    for (int i = 1; i < board.GetLength(0); i++)
        for (int j = 0; j < board.GetLength(1); j++)
        {
            int[] values = [i, j, value];
            if (board[i, j] != 0)
                if (checkRow(values)==4 || checkCol(values) ==4|| checkAxis(values)==4 || checkReverse(values)==4)
                    return board[i, j];
            if (board[i, j] == 0)
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
        Console.WriteLine("Please select your token\nPress 'X' for X\nPress 'O' for O");
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


void RandomDrop(int value)
{
    do { }
    while (!Drop(rand.Next(0, board.GetLength(1)), value));
}
int BlockOpponent(int opponent,int times)
{
    for (int j = 0; j < board.GetLength(1); j++)
    {
        if (board[1, j] != 0)
            continue;
        int i = 0;
        do
        {
            i++;
            if (i >= board.GetLength(0))
                break;
        } while (board[i, j] == 0);
        i--;
        int row = HowMuchInRow([i, j, opponent]), col = HowMuchInCol([i, j, opponent]), axis = HowMuchInAxis([i, j, opponent]), reverse = HowMuchInReverese([i, j, opponent]);
        if ((row == times || col == times || axis == times || reverse == times))
        {
            return j;
        }

    }
    return -1;
}
void Bot()
{
    int block = BlockOpponent(Player,3), win = BlockOpponent(-1 * Player,3);
    int second = BlockOpponent(-1 * Player, 2), one = BlockOpponent(-1 * Player, 1);
    if (win != -1)
        Drop(win, Player * -1);
    else if (block != -1)
        Drop(block, -1 * Player);
    else if (second != -1)
        Drop(second, -1 * Player);
    else if (one!=-1)
        Drop(one, -1 * Player);
    else
        RandomDrop(Player * -1);
}

int HowMuchInRow(int[] arr)
{
    int y = arr[0], x = arr[1],value = arr[2];
    int row_count = 0;
    for (int i = y + 1; i < board.GetLength(0) && board[i, x] == value; i++, row_count++) ;
    for (int i = y - 1; i >= 0 && board[i, x] == value; i--, row_count++) ;
    return row_count;
}
int HowMuchInCol(int[] arr)
{
    int y = arr[0], x = arr[1], value = arr[2];
    int col_count = 0;
    for (int i = x + 1; i < board.GetLength(1) && board[y, i] == value; i++, col_count++) ;
    for (int i = x - 1; i >= 0 && board[y, i] == value; i--, col_count++) ;
    return col_count;
}
int HowMuchInAxis(int[] arr)
{
    int y = arr[0], x = arr[1], value = arr[2];
    int axis_count = 0;
    for (int i = y + 1,j=x+1; i < board.GetLength(0)&&j<board.GetLength(1) && board[i, j] == value; i++,j++, axis_count++) ;
    for (int i = y - 1,j=x-1; i >= 0 && j>=0 && board[i, i] == value; i--,j--, axis_count++) ;
    return axis_count;
}
int HowMuchInReverese(int[] arr)
{
    int y = arr[0], x = arr[1], value = arr[2];
    int revese_axis = 0;
    for (int i = y + 1, j = x - 1; i < board.GetLength(0) && j >=0 && board[i, j] == value; i++, j--, revese_axis++) ;
    for (int i = y - 1, j = x + 1; i >= 0 && j <board.GetLength(1) && board[i, i] == value; i--, j++, revese_axis++) ;
    return revese_axis;
}


int checkWinner()
{
    int result = CheckWin(Player),enemy = CheckWin(-1*Player);
    if (result == enemy&& result == 0)
        return 0;
    else if (result!=0)
        return result;
    else if (enemy != 0)
        return enemy;
    return 0;
}
void GameVsBot()
{
    int result = 0,enemy=0;
    SelectCharcter();
    board[0, X] = Player;
    ConsoleKeyInfo info;
    do
    {
        board[0, X] = Player;
        Console.Clear();
        Console.WriteLine("its {0} player turn", Player == 1 ? "X" : "O");
        View.Print(board, X);
        info = Console.ReadKey();
        key = info.Key;
        if (KeyHandler(key))
            Bot();
        result = checkWinner();
    } while (result == 0);
    ResultGame(result);
}

void Game()
{
    int result=0;
    SelectCharcter();
    board[0, X] = Player;
    do
    {
        Console.Clear();
        Console.WriteLine("its {0} player turn",Player==1?"X":"O");
        View.Print(board,X);
        key = Console.ReadKey().Key;
        if (KeyHandler(key))
            SwitchPlayer();
        result = checkWinner();
    } while (result==0);
    ResultGame(result);
}


void StartGame()
{
    do
    {
        do
        {
            Console.WriteLine("hello wellcome to the Four in a row game\nThe Rules are simple, each player will drop a token\nthe first player that have 4 tokens in line (can be row,col,axis or reverse axis) wins the game\nPress '1' For Player vs Player\nPress '2' for Bot\npress 'escape' to exit");
            key = Console.ReadKey().Key;
            
            Console.Clear();
            if (key == ConsoleKey.Escape)
                return;
        } while (key != ConsoleKey.D1 && key != ConsoleKey.D2 && key != ConsoleKey.NumPad1 && key != ConsoleKey.NumPad2);
        switch (key)
        {
            case ConsoleKey.D1:
            case ConsoleKey.NumPad1:
                Game();
                break;
            case ConsoleKey.D2:
            case ConsoleKey.NumPad2:
                GameVsBot();
                break;
        }

        Console.WriteLine("Thank you for playing, to exit press 'escape' buttom, to restart press anykey");
        key = Console.ReadKey().Key;
        Console.Clear();
    }while(key!=ConsoleKey.Escape);
}
StartGame();    