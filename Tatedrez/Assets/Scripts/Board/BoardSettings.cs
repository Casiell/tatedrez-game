using UnityEngine;

namespace Board
{
    [CreateAssetMenu(menuName = "Create BoardSettings", fileName = "BoardSettings", order = 0)]
    public class BoardSettings : ScriptableObject
    {
        [SerializeField]
        private int squareSize = 6;
        
        [SerializeField]
        private Vector2Int boardSize = new Vector2Int(3, 3);
        
        [SerializeField]
        private GameObject darkPrefab;
        
        [SerializeField]
        private GameObject lightPrefab;
        
        public int SquareSize => squareSize;
        public Vector2Int BoardSize => boardSize;
        public GameObject DarkPrefab => darkPrefab;
        public GameObject LightPrefab => lightPrefab;
    }
}