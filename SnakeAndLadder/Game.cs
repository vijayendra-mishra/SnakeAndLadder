using System;
using System.Collections.Generic;

namespace SnakeAndLadder
{
    public class Game
    {
        static List<BoardPosition> board = new List<BoardPosition>();
        static Dictionary<int, Player> players = new Dictionary<int, Player>(4);

        public static void Main(string[] args)
        {
            Console.WriteLine("Creating Board...");

            var snakes = new List<int> { 7, 21, 33, 45, 59, 68, 72, 97 };
            var ladders = new List<int> { 9, 26, 37, 49, 52, 60, 74, 88 };

            PopulateBoard(snakes, ladders);
            DisplayBoard();
            PlayGame();

            Console.ReadLine();
        }

        private static void PlayGame()
        {
            bool gameOn = true;
            Console.WriteLine($"{Environment.NewLine}Enter no of players between 1-4:");

            if ((!int.TryParse(Console.ReadLine(), out int noOfPlayers)) || noOfPlayers < 1 || noOfPlayers > 4)
            {
                Console.WriteLine("Entered value incorrect, going with default of 4!");
                noOfPlayers = 4;
            }

            Console.WriteLine($"{Environment.NewLine}Starting game play...");

            for (int i = 1; i <= noOfPlayers; i++) { players.Add(i, new Player($"Player {i}", i)); }
            foreach (var p in players) { Console.WriteLine(p.ToString()); }

            while (gameOn)
            {
                for (int i = 1; i <= noOfPlayers; i++)
                {
                    if (!gameOn) break;

                    Console.WriteLine($"{Environment.NewLine}------------Press Enter to Roll Dice / Ctrl-C to Exit--------------");
                    Console.ReadLine();

                    var playerPos = players[i].Position;

                    RollDice(players[i], out bool isPlayAgain, ref gameOn);
                    if (isPlayAgain)
                    {
                        RollDice(players[i], out bool isPlayAgain1, ref gameOn);
                        if (isPlayAgain1)
                        {
                            RollDice(players[i], out bool isPlayAgain2, ref gameOn);
                            players[i].Position = playerPos;
                            if (isPlayAgain2) Console.WriteLine($"3 consequent sixes, player turn burnt and position changes back to : {playerPos}");
                        }

                    }

                }
            }
        }

        private static void RollDice(Player player, out bool isPlayAgain, ref bool gameOn)
        {
            isPlayAgain = false;
            Console.WriteLine($"Rolling dice for player : {player}");
            var diceFace = new Random().Next(1, 7);
            Console.WriteLine($"Dice Face : {diceFace}");

            if (diceFace == 6 && !player.IsPlaying)
            {
                player.IsPlaying = true;
                player.Position += 1;
                Console.WriteLine($"{player.Name} has started the game, plays again");
                isPlayAgain = true;
                return;
            }
            if (diceFace == 6 && player.IsPlaying)
            {
                player.Position = GetSpecialPos(diceFace, player.Position);

                Console.WriteLine($"{player.Name} gets to roll the dice again");
                isPlayAgain = true;
                return;
            }
            else if (player.IsPlaying) player.Position = GetSpecialPos(diceFace, player.Position);

            if (player.Position == 100)
            {
                Console.WriteLine($"{Environment.NewLine}{player.Name} IS THE WINNER !!!");
                gameOn = false;
            }
            else Console.WriteLine($"{player.Name}'s position now is : {player.Position}");
        }

        private static int GetSpecialPos(int diceFace, int position)
        {
            var pos = diceFace + position - 1;
            if (pos > 99) return position;

            var boardPos = board[pos];

            if (boardPos.Type == "D") return diceFace + position;
            else
            {
                var typeName = boardPos.Type == "S" ? "SNAKE" : "LADDER";
                Console.WriteLine($"-----{typeName}-----> { boardPos.SpecialPos}");
                return boardPos.SpecialPos.Tail;
            }
        }

        private static void DisplayBoard()
        {
            foreach (var b in board)
            {
                Console.WriteLine(b.ToString());
            }
        }

        private static void PopulateBoard(List<int> snakes, List<int> ladders)
        {
            for (int i = 1; i <= 100; ++i)
            {
                var rand = new Random();

                if (snakes.Exists(x => x == i))
                {
                    board.Add(new BoardPosition(i, "S", rand.Next(2, 15)));
                    continue;

                }

                if (ladders.Exists(x => x == i))
                {
                    board.Add(new BoardPosition(i, "L", rand.Next(2, 15)));
                    continue;

                }

                board.Add(new BoardPosition(i, string.Empty, 0));

            }
        }
    }
}
