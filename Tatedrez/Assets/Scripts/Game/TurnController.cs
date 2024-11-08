﻿using System.Collections.Generic;
using Pieces;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        public TurnController(InfoPanel infoPanel, BoardManager boardManager, List<PieceController> pieces)
        {
            victoryChecker = new VictoryChecker(boardManager);
            this.infoPanel = infoPanel;
            this.boardManager = boardManager;
            this.pieces = pieces;
            UpdatePiecePlacingController(new FirstPhasePlacingController(boardManager, pieces));
            var randomPlayer = Random.Range(0, 2) == 1 ? PieceColor.White : PieceColor.Black;
            ChangePlayer(randomPlayer);
        }

        private void UpdatePiecePlacingController(IPiecePlacingController newPiecePlacingController)
        {
            piecePlacingController?.Dispose();
            piecePlacingController = newPiecePlacingController;
            newPiecePlacingController.OnPiecePlaced += OnPiecePlaced;
        }

        private void OnPiecePlaced(PieceController piece, Vector2Int position)
        {
            if (victoryChecker.IsGameWon(piece, position))
            {
                EndGame(piece);
                return;
            }

            if (piecePlacingController.IsPhaseFinished())
            {
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

        private async void EndGame(PieceController piece)
        {
            currentPlayer = PieceColor.None;
            await infoPanel.SetPlayerVictory(piece.Color.ToString());
            SceneManager.LoadScene(0);
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