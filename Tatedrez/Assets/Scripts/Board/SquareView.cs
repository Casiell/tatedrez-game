using System;
using UnityEngine;

namespace Board
{
    public class SquareView : MonoBehaviour
    {
        [SerializeField] private GameObject highlight;

        public event Action OnClick; 
        
        private void OnMouseDown()
        {
            OnClick?.Invoke();
        }

        public void SetHighlight(bool isHighlighted)
        {
            highlight.SetActive(isHighlighted);
        }
    }
}