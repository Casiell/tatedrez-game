using Pieces;
using UnityEngine;

namespace Game
{
    public class TurnController
    {
        private const PieceColor StartingPlayer = PieceColor.White;
        
        public TurnController(BoardManager boardManager)
        {
            boardManager.OnPiecePlaced += OnPiecePlaced;
        }

        private void OnPiecePlaced(PieceController arg1, Vector2Int arg2)
        {
            //if other played can make move
            //switch to other player
            //else
            //stay with current player
        }
    }
}