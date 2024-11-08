using System.Collections.Generic;
using UnityEngine;

namespace Pieces
{
    public class PieceGenerator
    {
        public List<PieceController> CreatePieces(PiecesSetupSettings pieceSettings, Transform whitePieceParent, Transform blackPieceParent)
        {
            var pieceList = new List<PieceController>();
            
            var factory = new PieceFactory(pieceSettings);

            var piecesCount = pieceSettings.AvailablePieces.Count;
            for (var i = 0; i < piecesCount; i++)
            {
                var position = new Vector2((i - piecesCount / 2) * 6, 0);
            
                var availablePiece = pieceSettings.AvailablePieces[i];
                var piece = factory.Create(availablePiece, PieceColor.Black, blackPieceParent, position);
                pieceList.Add(piece);

                piece = factory.Create(availablePiece, PieceColor.White, whitePieceParent, position);
                pieceList.Add(piece);
            }

            return pieceList;
        }
    }
}