using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pieces
{
    [CreateAssetMenu(menuName = "Create PieceSettings", fileName = "PieceSettings", order = 0)]
    public class PieceSettings : ScriptableObject
    {
        [SerializeField] private PieceType pieceType;
        [SerializeField] private Sprite whiteSprite;
        [SerializeField] private Sprite blackSprite;
        [SerializeField] private List<Vector2Int> movement;
        [Tooltip("If true, the piece will be able to move in specified directions until the end of the board")]
        [SerializeField] private bool canMoveInfinitely;

        public Sprite WhiteSprite => whiteSprite;
        public Sprite BlackSprite => blackSprite;
        public List<Vector2Int> Movement => movement;
        public bool CanMoveInfinitely => canMoveInfinitely;
        public PieceType PieceType => pieceType;

        public Sprite GetSpriteForColor(PieceColor pieceColor)
        {
            return pieceColor switch
            {
                PieceColor.White => whiteSprite,
                PieceColor.Black => blackSprite,
                _ => throw new ArgumentOutOfRangeException(nameof(pieceColor), pieceColor, null)
            };
        }
    }

    public enum PieceType
    {
        Knight,
        Rook,
        Bishop
    }

    public enum PieceColor
    {
        None,
        White,
        Black
    }
}