using System.Collections.Generic;
using System.Linq;
using Pieces;
using UnityEngine;

namespace Game
{
    public class FigureMoveController : PiecePlacingController
    {
        public FigureMoveController(BoardManager boardManager, List<PieceController> pieces) : base(boardManager, pieces)
        {
        }

        protected override IEnumerable<BoardSpace> GetAllValidSquares(PieceController piece)
        {
            var movement = piece.Settings.Movement;
            var canMoveInfinitely = piece.Settings.CanMoveInfinitely;
        
            var startPosition = BoardManager.GetPiecePosition(piece);

            var squares = new List<BoardSpace>();
        
            foreach (var direction in movement)
            {
                squares.AddRange(GetSquares(direction, startPosition, canMoveInfinitely));
            }

            return squares;
        }

        public override bool CanMovePiece(PieceColor color)
        {
            foreach (var piece in AllPieces)
            {
                if (piece.Color != color)
                {
                    continue;
                }

                if (GetAllValidSquares(piece).Any())
                {
                    return true;
                }
            }

            return false;
        }

        private IEnumerable<BoardSpace> GetSquares(Vector2Int direction, Vector2Int startPosition, bool canMoveInfinitely)
        {
            while (true)
            {
                var squarePosition = startPosition + direction;
                var boardSpace = BoardManager.GetBoardSpace(squarePosition);
                if (boardSpace == null)
                {
                    yield break;
                }

                if (boardSpace.IsBusy)
                {
                    break;
                }

                yield return boardSpace;

                if (!canMoveInfinitely)
                {
                    break;
                }
            
                startPosition = squarePosition;
            }
        }
    }
}