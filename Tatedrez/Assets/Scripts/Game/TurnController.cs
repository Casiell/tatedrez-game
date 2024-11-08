using Pieces;
using UnityEngine;

namespace Game
{
    public class TurnController
    {
        private readonly VictoryChecker victoryChecker;
        private PieceColor currentPlayer;
        private IPiecePlacingController piecePlacingController;

        public TurnController(VictoryChecker victoryChecker)
        {
            this.victoryChecker = victoryChecker;
            currentPlayer = Random.Range(0, 2) == 1 ? PieceColor.White : PieceColor.Black;
            Debug.Log($"Starting player {currentPlayer}");
        }

        public void SetPiecePlacingController(IPiecePlacingController piecePlacingController)
        {
            this.piecePlacingController = piecePlacingController;
            piecePlacingController.OnPiecePlaced += OnPiecePlaced;
            this.piecePlacingController.SetActivePlayer(currentPlayer);
        }

        private void OnPiecePlaced(PieceController piece, Vector2Int position)
        {
            if (victoryChecker.IsGameWon(piece, position))
            {
                Debug.Log($"Player {piece.Color} won!");
                return;
            }
            var currentPlayerColor = piece.Color;
            var otherPlayerColor = currentPlayerColor == PieceColor.White ? PieceColor.Black : PieceColor.White;
            if (piecePlacingController.CanMovePiece(otherPlayerColor))
            {
                currentPlayer = otherPlayerColor;
                piecePlacingController.SetActivePlayer(currentPlayer);
            }
            else
            {
                if (!piecePlacingController.CanMovePiece(currentPlayerColor))
                {
                    Debug.LogError("No player can move a piece. If this happened there is probably some kind of misconfiguration");
                }
            }
        }
    }
}