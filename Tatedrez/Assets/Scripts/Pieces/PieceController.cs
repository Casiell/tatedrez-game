using System;
using UnityEngine;

namespace Pieces
{
    public class PieceController
    {
        public event Action<PieceController> OnClick;

        public readonly PieceSettings Settings;

        public readonly PieceColor Color;

        private readonly PieceView view;

        public PieceController(PieceView view, PieceSettings pieceSettings, PieceColor pieceColor)
        {
            this.view = view;
            Settings = pieceSettings;
            Color = pieceColor;
            var sprite = Settings.GetSpriteForColor(pieceColor);
            view.Setup(sprite);
            view.OnClick += OnViewClicked;
        }

        private void OnViewClicked()
        {
            OnClick?.Invoke(this);
        }

        public void MoveTo(Vector3 position)
        {
            view.transform.position = position;
        }

        public void Deselect()
        {
            view.SetHighlight(false);
        }

        public void Select()
        {
            view.SetHighlight(true);
        }

    }
}
