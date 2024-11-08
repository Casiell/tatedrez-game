using System.Collections.Generic;
using Board;
using Pieces;

namespace Game
{
    public abstract class PiecePlacingController
    {
        protected readonly BoardManager BoardManager;
    
        protected readonly List<PieceController> AllPieces;

        protected PieceController SelectedPiece
        {
            get;
            private set;
        }

        private readonly List<BoardSpace> validSquaresForCurrentPiece = new();
        private PieceColor currentPlayer;

        protected PiecePlacingController(BoardManager boardManager, List<PieceController> pieces)
        {
            AllPieces = new List<PieceController>(pieces);
            
            foreach (var piece in AllPieces)
            {
                piece.OnClick += PieceClicked;
            }

            BoardManager = boardManager;
            boardManager.OnSquareSelected += OnSquareSelected;
        }

        protected void OnSquareSelected(BoardSpace square)
        {
            if (SelectedPiece == null)
            {
                return;
            }

            if (!validSquaresForCurrentPiece.Contains(square))
            {
                return;
            }

            if (BoardManager.TryPlacePiece(SelectedPiece, square.Square.GetGridPosition()))
            {
                PlacePiece(square.Square);
            }
        }
    
        protected virtual void PlacePiece(SquareController square)
        {
            SelectedPiece.MoveTo(square.GetWorldPosition());
        
            DeselectCurrentPiece();
        }

        protected void PieceClicked(PieceController piece)
        {
            if (piece.Color != currentPlayer)
            {
                return;
            }
            if (SelectedPiece == piece)
            {
                DeselectCurrentPiece();
                return;
            }

            DeselectCurrentPiece();
            SelectedPiece = piece;
            if (SelectedPiece != null)
            {
                SelectedPiece.Select();
                validSquaresForCurrentPiece.AddRange(GetAllValidSquares(SelectedPiece));
                validSquaresForCurrentPiece.ForEach(x => x.Square.SetHighlight(true));
            }
        }

        protected abstract IEnumerable<BoardSpace> GetAllValidSquares(PieceController piece);

        private void DeselectCurrentPiece()
        {
            if (SelectedPiece == null)
            {
                return;
            }
            SelectedPiece.Deselect();
            SelectedPiece = null;
            validSquaresForCurrentPiece.Clear();
            BoardManager.ClearBoardHighlights();
        }
        
        public abstract bool CanMovePiece(PieceColor color);

        public void SetActivePlayer(PieceColor color)
        {
            currentPlayer = color;
        }
    }
}
