using UnityEngine;

namespace Board
{
    public class BoardGenerator
    {
        private readonly BoardSettings boardSettings;
        private readonly Transform boardTransform;

        public BoardGenerator(BoardSettings boardSettings, Transform boardTransform)
        {
            this.boardSettings = boardSettings;
            this.boardTransform = boardTransform;
        }

        public float GetHalfBoardHeight()
        {
            return boardSettings.BoardSize.y * boardSettings.SquareSize / 2f;
        }

        public SquareController[,] GenerateBoard()
        {
            var boardSize = boardSettings.BoardSize;

            var squares = new SquareController[boardSettings.BoardSize.x,boardSettings.BoardSize.y];
            
            for (var x = 0; x < boardSize.x; x++)
            {
                for (var y = 0; y < boardSize.y; y++)
                {
                    var prefab = GetPrefab(x, y);

                    var position = GridToWorldPosition(x, y);
                    var squareView = Object.Instantiate(prefab, position, Quaternion.identity, boardTransform);
                    squareView.name = $"{prefab.name}_{x}_{y}";

                    var square = new SquareController(squareView, new Vector2Int(x, y));
                    squares[x, y] = square;
                }
            }

            return squares;
        }

        private Vector3 GridToWorldPosition(int x, int y)
        {
            var boardSize = boardSettings.BoardSize;
            var squareSize = boardSettings.SquareSize;

            var horizontalOffset = boardSize.x / 2;
            var verticalOffset = boardSize.y / 2;

            return new Vector3((x - horizontalOffset) * squareSize, (y - verticalOffset) * squareSize, 1);
        }

        private SquareView GetPrefab(int x, int y)
        {
            return (x + y) % 2 == 1 ? boardSettings.DarkPrefab : boardSettings.LightPrefab;
        }
    }
}
