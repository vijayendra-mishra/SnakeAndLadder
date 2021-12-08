namespace SnakeAndLadder
{
    public class SnakeOrLadder
    {
        public int Head { get; set; }
        public int Tail { get; set; }

        public SnakeOrLadder(int head, int tail)
        {
            Head = head;
            Tail = tail;
        }

        public override string ToString()
        {
            return $" Head : {Head}, Tail : {Tail}";
        }

    }
}
