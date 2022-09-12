

namespace project
{
    class Project
    {

        public class Snake
        {
            private List<Coordinates> Coordinates { get; set; }
            private Coordinates Head { get; set; }
            private Fruit Fruit { get; set; }
            public int Points { get; private set; }
            public bool GameOver { get; set; } = false;



            private readonly int _col;
            private readonly int _row;
            public Snake(int col, int row)
            {

                _col = col;
                _row = row;
                Head = new Coordinates(row/2,col/2);
                Fruit = GenerateFruit();
                Coordinates = new List<Coordinates>();
            }

            private Fruit GenerateFruit()
            {
                var random = new Random();

                var FruitX = random.Next(_row - 3) + 1;
                var FruitY = random.Next(_col - 2) + 1;

                return new Fruit(FruitX, FruitY);
            }

            private void CheckPositions()
            {
                if (Head.y == 0 || Head.y == _col - 1 || Head.x == 0 || Head.x == _row - 1)
                {
                    GameOver = true;
                    return;
                }

                for (int i = 0; i < Coordinates.Count; i++)
                {
                    if (Coordinates[i].x == Head.x && Coordinates[i].y == Head.y)
                    {
                        GameOver = true;
                        break;
                    }
                }
            }

            private void AddTail(Coordinates tail, ConsoleKey key)
            {
                var tailX = tail.x;
                var tailY = tail.y;
                if (key == ConsoleKey.W)
                {
                    tailY++;
                }
                else if (key == ConsoleKey.S)
                {
                    tailY--;
                }
                else if (key == ConsoleKey.A)
                {
                    tailX++;
                }
                else if (key == ConsoleKey.D)
                {
                    tailX--;

                }
                Coordinates.Add(new Coordinates(tailX, tailY));
            }

            private void MoveHead(ConsoleKey key)
            {
                switch (key)
                {
                    case ConsoleKey.W:
                        Head.y--;
                        break;
                    case ConsoleKey.A:
                        Head.x--;
                        break;
                    case ConsoleKey.S:
                        Head.y++;
                        break;
                    case ConsoleKey.D:
                        Head.x++;
                        break;
                    default:
                        break;
                }
            }

            private void MoveTail(int x, int y)
            {

                if (Coordinates.Count != 0)
                {
                    int prevX = Coordinates[0].x;
                    int prevY = Coordinates[0].y;
                    Coordinates[0].x = x;
                    Coordinates[0].y = y;


                    for (int i = 1; i < Coordinates.Count; i++)
                    {
                        int tempX = Coordinates[i].x;
                        int tempY = Coordinates[i].y;
                        Coordinates[i].x = prevX;
                        Coordinates[i].y = prevY;

                        prevX = tempX;
                        prevY = tempY;
                    }
                }

            }


            public void Move(ConsoleKey key)
            {
                int prevX = Head.x;
                int prevY = Head.y;
                MoveHead(key);
                MoveTail(prevX, prevY);
                CheckPositions();


                if (Head.x == Fruit.x && Head.y == Fruit.y)
                {
                    Points += 10;
                    if (Coordinates.Count == 0)
                    {
                        AddTail(Head, key);
                    }
                    else
                    {
                        var tail = Coordinates[^1];
                        AddTail(tail, key);

                    }
                    Fruit = GenerateFruit();
                }



            }

            public void Display()
            {

                for (int col = 0; col < _col; col++)
                {
                    for (int row = 0; row < _row; row++)
                    {
                        var isHeadOrTailPrinted = false;
                        if (Head.x == row && Head.y == col)
                        {
                            Console.Write("@");
                            isHeadOrTailPrinted = true;
                        }
                        for (int k = 0; k < Coordinates.Count; k++)
                        {
                            if (Coordinates[k].x == row && Coordinates[k].y == col)
                            {
                                Console.Write("*");
                                isHeadOrTailPrinted = true;
                                break;
                            }
                        }
                        if (Fruit.x == row && Fruit.y == col)
                        {
                            Console.Write(".");
                        }
                        else if (col == 0 || col == _col - 1 || row == 0 || row == _row - 1)
                        {
                            Console.Write("#");
                        }
                        else
                        {
                            if (!isHeadOrTailPrinted)
                            {
                                Console.Write(" ");
                            }
                        }
                    }
                    Console.WriteLine("");
                }
                Console.WriteLine("Points: " + Points);
                Console.WriteLine("");
            }

        }


        public class Fruit
        {
            public int x { get; set; }
            public int y { get; set; }

            public Fruit(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

        }


        public class Coordinates
        {
            public int x { get; set; }
            public int y { get; set; }

            public Coordinates(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }



        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var s = new Snake(15, 30);

            var key = ConsoleKey.X;

            var quit = false;

            while(!quit)
            {
                while (!s.GameOver)
                {
                    s.Display();
                    Thread.Sleep(100);
                    if (key == ConsoleKey.W || key == ConsoleKey.S || key == ConsoleKey.A || key == ConsoleKey.D)
                    {
                        s.Move(key);
                    }
                    if (Console.KeyAvailable)
                    {
                        key = Console.ReadKey(true).Key;
                    }

                    Console.Clear();
                }
                Console.WriteLine("Game Over :(");
                Console.WriteLine("Points: " + s.Points);
                Console.WriteLine("");
                Console.WriteLine("Want to play again? y/n");
                string choice = Console.ReadLine();

                Console.WriteLine(choice);
                if (choice[0] != 'y')
                {
                    break;
                }
                key = ConsoleKey.X;
                s = new Snake(15, 30);

            }

            Console.Clear();

            Console.WriteLine("Thanks for playing!");

            

            
        }


    }


}




