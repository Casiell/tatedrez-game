using System.Collections.Generic;
using Pieces;
using UI;
using UnityEngine;

namespace Game
{
    public class TurnController
    {
        private readonly VictoryChecker victoryChecker;
        private readonly InfoPanel infoPanel;
        private readonly BoardManager boardManager;
        private PieceColor currentPlayer;
        private IPiecePlacingController piecePlacingController;
        private readonly List<PieceController> pieces;

        public TurnController(VictoryChecker victoryChecker, InfoPanel infoPanel, BoardManager boardManager, List<PieceController> pieces)
        {
            this.victoryChecker = victoryChecker;
            this.infoPanel = infoPanel;
            this.boardManager = boardManager;
            this.pieces = pieces;
            UpdatePiecePlacingController(new FirstPhasePlacingController(boardManager, pieces));
            var randomPlayer = Random.Range(0, 2) == 1 ? PieceColor.White : PieceColor.Black;
            ChangePlayer(randomPlayer);
        }

        private void UpdatePiecePlacingController(IPiecePlacingController piecePlacingController)
        {
            this.piecePlacingController = piecePlacingController;
            piecePlacingController.OnPiecePlaced += OnPiecePlaced;
        }

        private void OnPiecePlaced(PieceController piece, Vector2Int position)
        {
            if (victoryChecker.IsGameWon(piece, position))
            {
                _ = infoPanel.SetPlayerVictory(piece.Color.ToString());
                return;
            }

            if (piecePlacingController.IsPhaseFinished())
            {
                piecePlacingController?.Dispose();
                UpdatePiecePlacingController(new FigureMoveController(boardManager, pieces));
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