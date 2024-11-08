using System.Collections.Generic;
using System.Linq;
using Board;
using Pieces;

namespace Game
{
    public class FirstPhasePlacingController : PiecePlacingController
    {
        public FirstPhasePlacingController(BoardManager boardManager, List<PieceController> pieces) : base(boardManager, pieces)
        {
            boardManager.OnSquareSelected += OnSquareSelected;
        }

        protected override void PlacePiece(SquareController square)
        {
            SelectedPiece.OnClick -= PieceClicked;
            AllPieces.Remove(SelectedPiece);

            base.PlacePiece(square);
        }

        protected override IEnumerable<BoardSpace> GetAllValidSquares(PieceController piece)
        {
            return BoardManager.GetAllSquares(x => !x.IsBusy);
        }

        public override bool CanMovePiece(PieceColor color)
        {
            return AllPieces.Any(x => x.Color == color) && GetAllValidSquares(null).Any();
        }

        public override bool IsPhaseFinished()
        {
            return !AllPieces.Any();
        }
    }
}