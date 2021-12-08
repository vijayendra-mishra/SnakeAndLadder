using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeAndLadder
{
    public class Player
    {
        private int position;
        public int Position
        {
            get { return position; }
            set
            {
                if (value <= 100) position = value;
            }
        }
        public string Color { get; set; }
        public string Name { get; set; }
        public bool IsPlaying { get; set; }

        public Player(string name, int color)
        {
            Name = name;
            Position = 1;
            IsPlaying = false;
            switch (color)
            {
                case 1:
                    Color = "Red";
                    break;
                case 2:
                    Color = "Blue";
                    break;
                case 3:
                    Color = "Green";
                    break;
                case 4:
                    Color = "Yellow";
                    break;
            }
        }

        public override string ToString()
        {
            return $"Name : {Name}, Position : {Position}, Color: {Color}, IsPlaying : {IsPlaying}";
        }


    }
}
