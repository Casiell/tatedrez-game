using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pieces
{
    public class PieceFactory
    {
        private readonly PieceView piecePrefab;
        
        private readonly Dictionary<PieceType, PieceSettings> pieceConfigsDictionary;

        public PieceFactory(PiecesSetupSettings pieceSettings)
        {
            piecePrefab = pieceSettings.PiecePrefab;
            pieceConfigsDictionary = pieceSettings.PieceConfigs.ToDictionary(x => x.PieceType, x => x);
        }
        
        public PieceController Create(PieceType pieceType, PieceColor pieceColor, Transform parent, Vector3 localPosition)
        {
            if (!pieceConfigsDictionary.TryGetValue(pieceType, out var pieceSettings))
            {
                return null;
            }
            
            var view = Object.Instantiate(piecePrefab, parent.position, Quaternion.identity, parent);
            view.name = $"{pieceType}_{pieceColor}";
            view.transform.localPosition = localPosition;
            var pieceController = new PieceController(view, pieceSettings, pieceColor);
            return pieceController;
        }
    }
}