using Pieces;
using UI;
using UnityEngine;

namespace Game
{
    public class TurnController
    {
        private readonly VictoryChecker victoryChecker;
        private readonly InfoPanel infoPanel;
        private PieceColor currentPlayer;
        private IPiecePlacingController piecePlacingController;

        public TurnController(VictoryChecker victoryChecker, InfoPanel infoPanel)
        {
            this.victoryChecker = victoryChecker;
            this.infoPanel = infoPanel;
            var randomPlayer = Random.Range(0, 2) == 1 ? PieceColor.White : PieceColor.Black;
            ChangePlayer(randomPlayer);
        }

        public void SetPiecePlacingController(IPiecePlacingController piecePlacingController)
        {
            this.piecePlacingController = piecePlacingController;
            piecePlacingController.OnPiecePlaced += OnPiecePlaced;
            ChangePlayer(currentPlayer);
        }

        private void OnPiecePlaced(PieceController piece, Vector2Int position)
        {
            if (victoryChecker.IsGameWon(piece, position))
            {
                _ = infoPanel.SetPlayerVictory(piece.Color.ToString());
                return;
            }
            var currentPlayerColor = piece.Color;
            var otherPlayerColor = currentPlayerColor == PieceColor.White ? PieceColor.Black : PieceColor.White;
            if (piecePlacingController.CanMovePiece(otherPlayerColor))
            {
                ChangePlayer(otherPlayerColor);
            }
            else
            {
                if (!piecePlacingController.CanMovePiece(currentPlayerColor))
                {
                    Debug.LogError("No player can move a piece. If this happened there is probably some kind of misconfiguration");
                }
            }
        }

        private async void ChangePlayer(PieceColor otherPlayerColor)
        {
            currentPlayer = otherPlayerColor;
            if (infoPanel)
            {
                piecePlacingController?.SetActivePlayer(PieceColor.None);

                await infoPanel.SetPlayerInfo(currentPlayer.ToString());
            }
            piecePlacingController?.SetActivePlayer(currentPlayer);
        }
    }
}