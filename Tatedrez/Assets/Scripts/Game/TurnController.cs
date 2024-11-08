using Pieces;
using UnityEngine;

namespace Game
{
    public class TurnController
    {
        private const PieceColor StartingPlayer = PieceColor.White;
        private PieceColor currentPlayer;
        private PiecePlacingController piecePlacingController;
        
        public TurnController(BoardManager boardManager)
        {
            boardManager.OnPiecePlaced += OnPiecePlaced;
        }

        public void SetPiecePlacingController(PiecePlacingController piecePlacingController)
        {
            this.piecePlacingController = piecePlacingController;
            this.piecePlacingController.SetActivePlayer(currentPlayer);
        }

        private void OnPiecePlaced(PieceController piece, Vector2Int position)
        {
            var currentPlayerColor = piece.Color;
            var otherPlayerColor = currentPlayerColor == PieceColor.White ? PieceColor.Black : PieceColor.White;
            if (piecePlacingController.CanMovePiece(otherPlayerColor))
            {
                currentPlayer = otherPlayerColor;
                piecePlacingController.SetActivePlayer(currentPlayer);
            }

            if (!piecePlacingController.CanMovePiece(currentPlayerColor))
            {
                Debug.LogError("No player can move a piece. If this happened there is probably some kind of misconfiguration");
            }
        }
    }
}