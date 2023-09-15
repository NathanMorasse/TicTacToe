using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using TicTacToe_Server.Views;

namespace TicTacToe_Server.Models
{
    public static class Game
    {
        public static Board CurrentBoard { get; set; }
        public static bool IsMyTurn { get; set; }

        public static void StartNewGame()
        {
            Random rnd = new Random();
            bool IsMyTurn = rnd.Next(0,1) == 0;

            CurrentBoard = new Board();

            SocketManager.NotifyClientNewGame(!IsMyTurn);

            //switch to game page

            if (!IsMyTurn)
            {
                SocketManager.WaitForOpponentMove();
            }

        }

        /// <summary>
        /// Cette fonction recoit des moves du serveur et du client et dois les valider et décider quelle action faire selon le résultat
        /// </summary>
        /// <param name="move"></param>
        public static void ValidateMove(Move move)
        {
            bool valid = true;

            if (move.IsClientMove == IsMyTurn)
            {
                valid = false;
            }

            if ( move.CoordinateX < 1 || move.CoordinateX > 3)
            {
                valid = false;
            }

            if (move.CoordinateY < 1 || move.CoordinateY > 3)
            {
                valid = false;
            }

            if (valid)
            {
                CurrentBoard.SaveNewMove(move);
                string winner = CurrentBoard.IsWinner();
                if (IsMyTurn)
                {
                    switch (winner)
                    {
                        case "Server":
                            move.PossibleWin = true;
                            Game.NextTurn();
                            SocketManager.SendMove(move);
                            SocketManager.WaitForOpponentMove();
                            break;
                        case "Tied":
                            SocketManager.SendTieMessage();
                            EndGame("Tied");
                            break;
                        case "Aucun":
                            SocketManager.SendMove(move);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    if (winner == "Client")
                    {
                        SocketManager.ValidateOpponentWin();
                        EndGame("Lost");
                    }
                }
            }
            else
            {
                if (IsMyTurn)
                {
                    MessageBox.Show("Coup invalide");
                }
                else
                {
                    SocketManager.SendInvalidMoveMessage();
                }
            }
        }

        public static void NextTurn()
        {
            IsMyTurn = !IsMyTurn;

            //update button
        }

        public static void EndGame(string result)
        {
            //
        }
    }
}
