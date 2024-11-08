using UnityEngine;

namespace Board
{
    [CreateAssetMenu(menuName = "Create BoardSettings", fileName = "BoardSettings", order = 0)]
    public class BoardSettings : ScriptableObject
    {
        [SerializeField]
        private int squareSize = 6;
        
        [SerializeField]
        private Vector2Int boardSize = new(3, 3);
        
        [SerializeField]
        private SquareView darkPrefab;
        
        [SerializeField]
        private SquareView lightPrefab;

        public int SquareSize => squareSize;
        public Vector2Int BoardSize => boardSize;
        public SquareView DarkPrefab => darkPrefab;
        public SquareView LightPrefab => lightPrefab;
    }
}