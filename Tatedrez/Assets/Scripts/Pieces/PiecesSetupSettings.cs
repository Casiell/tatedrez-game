using System.Collections.Generic;
using UnityEngine;

namespace Pieces
{
    [CreateAssetMenu(menuName = "Create PiecesSetupSettings", fileName = "PiecesSetupSettings", order = 0)]
    public class PiecesSetupSettings : ScriptableObject
    {
        [SerializeField] private int pieceSize = 6;
        [SerializeField] private List<PieceType> availablePieces;

        [SerializeField] private PieceView piecePrefab;
        [SerializeField] private List<PieceSettings> pieceConfigs;

        public int PieceSize => pieceSize;
        public PieceView PiecePrefab => piecePrefab;

        public List<PieceSettings> PieceConfigs => pieceConfigs;
        
        public List<PieceType> AvailablePieces => availablePieces;
    }
}