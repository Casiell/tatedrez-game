using System.Collections.Generic;
using System.Linq;
using Board;
using Pieces;

namespace Game
{
    public class FirstPhasePlacingController : PiecePlacingController
    {
        private readonly Game gameManager;

        public FirstPhasePlacingController(BoardManager boardManager, Game gameManager, List<PieceController> pieces) : base(boardManager, pieces)
        {
            this.gameManager = gameManager;
            boardManager.OnSquareSelected += OnSquareSelected;
        }

        protected override void PlacePiece(SquareController square)
        {
            SelectedPiece.OnClick -= PieceClicked;
            AllPieces.Remove(SelectedPiece);

            if (AllPieces.Count == 0)
            {
                gameManager.MoveToNextStep();
            }

            base.PlacePiece(square);
        }

        protected override IEnumerable<BoardSpace> GetAllValidSquares(PieceController piece)
        {
            return BoardManager.GetAllSquares(x => !x.IsBusy).ToList();
        }
    }
}