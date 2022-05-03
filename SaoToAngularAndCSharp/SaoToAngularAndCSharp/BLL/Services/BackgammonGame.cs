using SaoToAngularAndCSharp.BLL.BLLModeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaoToAngularAndCSharp.BLL.Services
{
    public class BackgammonGame
    {
        public string WhitePlayerId { get; set; }
        public string BlackPlayerId { get; set; }
        // Black will be represented by the second index and white by the first index
        public int[] Eaten { get; set; }
        public Boardpiece[] Board { get; set; }
        public bool WhiteIsPlaying{ get; set; }

        public BackgammonGame(string player1, string player2)
        {
            Setupboard();
            this.WhitePlayerId = player1;
            this.BlackPlayerId = player2;
            Eaten = new int[2];
        }
        public void PassTurn() => this.WhiteIsPlaying = !this.WhiteIsPlaying; 

        private void Setupboard()
        {
            Board = new Boardpiece[25];
            for (int index = 1; index <= 24; index++)
            {
                if (index == 1)
                    Board[index] = new Boardpiece()
                    {
                        ColorOfUnit = "white",
                        NumberOfUnits = 2,
                        Index = index,
                        ClassName = ""
                    };
                else if (index == 19 || index == 12)
                {
                    Board[index] = new Boardpiece()
                    {
                        ColorOfUnit = "white",
                        NumberOfUnits = 5,
                        Index = index,
                        ClassName = ""
                    };
                }
                else if (index == 17)
                {
                    Board[index] = new Boardpiece()
                    {
                        ColorOfUnit = "white",
                        NumberOfUnits = 3,
                        Index = index,
                        ClassName = ""
                    };
                }
                else if (index == 24)
                {
                    Board[index] = new Boardpiece()
                    {
                        ColorOfUnit = "black",
                        NumberOfUnits = 2,
                        Index = index,
                        ClassName = ""
                    };
                }
                else if (index == 6 || index == 13)
                {
                    Board[index] = new Boardpiece()
                    {
                        ColorOfUnit = "black",
                        NumberOfUnits = 5,
                        Index = index,
                        ClassName = ""
                    };
                }
                else if (index == 8)
                {
                    Board[index] = new Boardpiece()
                    {
                        ColorOfUnit = "black",
                        NumberOfUnits = 3,
                        Index = index,
                        ClassName = ""
                    };
                }
                else
                {
                    Board[index] = new Boardpiece()
                    {
                        ColorOfUnit = "",
                        NumberOfUnits = 0,
                        Index = index,
                        ClassName = ""
                    };
                }
            }
        }
    }
}
