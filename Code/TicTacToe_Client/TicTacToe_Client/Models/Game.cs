using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_Client.Models
{
    public static class Game
    {
        public static Board CurrentBoard { get; set; }
        public static bool IsMyTurn { get; set; }

        public static void StartNewGame(bool IsClientTurn)
        {
            CurrentBoard = new Board();
            IsMyTurn = IsClientTurn;

            //Switch to game page

            if (!IsMyTurn)
            {
                SocketManager.WaitForOpponentMove();
            }
        }
        
        public static bool ValidateMove (Move move)
        {
            if (move.IsServerMove == IsMyTurn)
            {
                return false;
            }
            else if (move.coordY < 1 || move.coordX > 3)
            {
                return false;
            }
            else if (move.coordY < 1 || move.coordY > 3)
            {
                return false;
            }else if (CurrentBoard.Moves[move.coordX, move.coordY] != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void NextTurn()
        {
            IsMyTurn = !IsMyTurn;
        }

        public static void EndGame(string result)
        {
            if(result == "Win")
            {

            }else if(result == "Lose")
            {

            }else if(result == "Tie")
            {

            }
            else
            {

            }
        }
    }
}
