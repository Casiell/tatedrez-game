using System;
using Pieces;
using UnityEngine;

namespace Game
{
    public interface IPiecePlacingController
    {
        event Action<PieceController, Vector2Int> OnPiecePlaced;
        bool CanMovePiece(PieceColor color);
        void SetActivePlayer(PieceColor color);
    }
}