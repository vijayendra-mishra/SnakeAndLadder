namespace SnakeAndLadder
{
    public class BoardPosition
    {
        public int Number { get; set; }
        public string Type { get; set; }
        public SnakeOrLadder SpecialPos { get; set; }

        public BoardPosition(int number, string type, int difference)
        {
            var snakeTail = number - difference <= 0 ? 2 : number - difference;
            var ladderTail = number + difference > 100 ? 99 : number + difference;

            Type = type;
            Number = number;

            switch (type)
            {
                case "S":
                    SpecialPos = new SnakeOrLadder(head: number, tail: snakeTail);
                    break;
                case "L":
                    SpecialPos = new SnakeOrLadder(head: number, tail: ladderTail);
                    break;
                default:
                    Type = "D";
                    break;
            }


        }
        public override string ToString()
        {
            return $"Number : {Number}, Type : {Type}, Positions:{SpecialPos}";
        }

    }

}
