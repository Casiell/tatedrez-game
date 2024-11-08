using Pieces;
using UnityEngine;

namespace Game
{
    public class VictoryChecker
    {
        private readonly BoardManager boardManager;
        
        private readonly Vector2Int[] vectorsToCheck = {
            new(1, 0),
            new(0, 1),
            new(1, -1),
            new(1, 1),
        };

        public VictoryChecker(BoardManager boardManager)
        {
            this.boardManager = boardManager;
        }

        public bool IsGameWon(PieceController piece, Vector2Int position)
        {
            var color = piece.Color;
            const int distanceToCheck = 2;

            return CheckForMatches(position, distanceToCheck, color);
        }

        private bool CheckForMatches(Vector2Int position, int distanceToCheck, PieceColor color)
        {
            foreach (var direction in vectorsToCheck)
            {
                if (CheckDirection(position, distanceToCheck, color, direction))
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckDirection(Vector2Int position, int distanceToCheck, PieceColor color, Vector2Int direction)
        {
            var consecutivePieces = 0;
            for (var i = -distanceToCheck; i <= distanceToCheck; i++)
            {
                if (i == 0)
                {
                    //this is the square where piece was just placed, no point in checking that
                    continue;
                }

                if (CheckBoardSpaceForPieceOfColor(position, color, direction, i))
                {
                    consecutivePieces++;
                }
                else
                {
                    consecutivePieces = 0;
                }

                if (consecutivePieces == distanceToCheck)
                {
                    Debug.Log($"{color.ToString()} won");
                    return true;
                }
            }

            return false;
        }

        private bool CheckBoardSpaceForPieceOfColor(Vector2Int position, PieceColor color, Vector2Int direction, int i)
        {
            return boardManager.GetBoardSpace(position + direction * i)?.Piece?.Color == color;
        }
    }
}