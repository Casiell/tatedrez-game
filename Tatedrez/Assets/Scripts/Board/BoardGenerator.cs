using UnityEngine;

namespace Board
{
    public class BoardGenerator : MonoBehaviour
    {
        [SerializeField] 
        private BoardSettings boardSettings;

        private void Start()
        {
            GenerateBoard();
        }

        [ContextMenu("Generate Board")]
        private void RegenerateBoard()
        {
            ClearBoard();

            GenerateBoard();
        }

        [ContextMenu("Clear Board")]
        private void ClearBoard()
        {
            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                var square = transform.GetChild(i).gameObject;
                if (Application.isPlaying)
                {
                    Destroy(square);
                }
                else
                {
                    DestroyImmediate(square);
                }
            }
        }

        private void GenerateBoard()
        {
            var boardSize = boardSettings.BoardSize;
            
            for (var x = 0; x < boardSize.x; x++)
            {
                for (var y = 0; y < boardSize.y; y++)
                {
                    var prefab = GetTilePrefab(x, y);

                    var position = GetTilePosition(x, y);
                    var tile = Instantiate(prefab, position, Quaternion.identity, transform);
                    tile.name = $"{prefab.name}_{x}_{y}";
                }
            }
        }

        private Vector3 GetTilePosition(int x, int y)
        {
            var boardSize = boardSettings.BoardSize;
            var squareSize = boardSettings.SquareSize;

            var horizontalOffset = boardSize.x / 2;
            var verticalOffset = boardSize.y / 2;

            return new Vector3((x - horizontalOffset) * squareSize, (y - verticalOffset) * squareSize, 0);
        }

        private GameObject GetTilePrefab(int x, int y)
        {
            return (x + y) % 2 == 1 ? boardSettings.DarkPrefab : boardSettings.LightPrefab;
        }
    }
}
