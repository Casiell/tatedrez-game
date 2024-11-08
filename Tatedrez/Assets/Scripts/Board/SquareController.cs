using System;
using UnityEngine;

namespace Board
{
    public class SquareController
    {
        public event Action<SquareController> OnSquareSelected;
        private readonly SquareView view;
        private readonly Vector2Int gridPosition;

        public SquareController(SquareView view, Vector2Int gridPosition)
        {
            this.view = view;
            this.gridPosition = gridPosition;
            view.OnClick += () => OnSquareSelected?.Invoke(this);
        }

        public void SetHighlight(bool highlighted)
        {
            view.SetHighlight(highlighted);
        }

        public Vector2Int GetGridPosition() => gridPosition;
        public Vector3 GetWorldPosition() => view.transform.position;
    }
}