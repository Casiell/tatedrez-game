using System;
using UnityEngine;

namespace Pieces
{
    public class PieceView : MonoBehaviour
    {
        public event Action OnClick;
        
        [SerializeField] private SpriteRenderer mainImage;
        [SerializeField] private SpriteRenderer highlightImage;

        public void Setup(Sprite sprite)
        {
            mainImage.sprite = sprite;
            highlightImage.sprite = sprite;
        }
        
        public void SetHighlight(bool highlighted)
        {
            highlightImage.enabled = highlighted;
        }

        private void OnMouseDown()
        {
            OnClick?.Invoke();
        }
    }
}