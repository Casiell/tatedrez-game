using System;
using System.Collections.Generic;
using Board;
using Pieces;
using UnityEngine;

namespace Game
{
    public class BoardManager
    {
        public event Action<PieceController, Vector2Int> OnPiecePlaced;
        public event Action<BoardSpace> OnSquareSelected; 
        private BoardSpace[,] boardSpaces;
        private readonly Dictionary<PieceController, BoardSpace> pieceToBoardSpace = new();
        
        private readonly BoardGenerator boardGenerator;
        
        public BoardManager(SquareController[,] squares)
        {
            InitializeBoardSpaces(squares);
        }

        private void InitializeBoardSpaces(SquareController[,] squares)
        {
            boardSpaces = new BoardSpace[squares.GetLength(0), squares.GetLength(1)];
            for (var x = 0; x < squares.GetLength(0); x++)
            {
                for (var y = 0; y < squares.GetLength(1); y++)
                {
                    var square = squares[x, y];
                    var boardSpace = new BoardSpace(square);
                    boardSpaces[x, y] = boardSpace;
                    square.OnSquareSelected += _ => SquareSelected(boardSpace);
                }
            }
        }

        public bool TryPlacePiece(PieceController piece, Vector2Int position)
        {
            var boardSpace = boardSpaces[position.x, position.y];

            if (boardSpace.IsBusy)
            {
                return false;
            }

            if (pieceToBoardSpace.TryGetValue(piece, out var currentPieceSpace))
            {
                currentPieceSpace.Piece = null;
            }
            boardSpace.Piece = piece;
            pieceToBoardSpace[piece] = boardSpace;
            OnPiecePlaced?.Invoke(piece, position);
            return true;
        }
        
        private void SquareSelected(BoardSpace boardSpace)
        {
            OnSquareSelected?.Invoke(boardSpace);
        }
        
        public void ClearBoardHighlights()
        {
            foreach (var boardSpace in GetAllSquares())
            {
                boardSpace.Square.SetHighlight(false);
            }
        }

        public IEnumerable<BoardSpace> GetAllSquares(Func<BoardSpace, bool> predicate = null)
        {
            foreach (var boardSpace in boardSpaces)
            {
                if (predicate == null || predicate(boardSpace))
                {
                    yield return boardSpace;
                }
            }
        }

        public Vector2Int GetPiecePosition(PieceController piece)
        {
            return pieceToBoardSpace[piece].Square.GetGridPosition();
        }

        public BoardSpace GetBoardSpace(Vector2Int position)
        {
            return GetBoardSpace(position.x, position.y);
        }

        private BoardSpace GetBoardSpace(int x, int y)
        {
            if (x < 0 || x >= boardSpaces.GetLength(0) || y < 0 || y >= boardSpaces.GetLength(1))
            {
                return null;
            }
            return boardSpaces[x, y];
        }
    }

    public class BoardSpace
    {
        public readonly SquareController Square;
        public PieceController Piece;
        
        public bool IsBusy => Piece != null;

        public BoardSpace(SquareController square)
        {
            Square = square;
        }
    }
}