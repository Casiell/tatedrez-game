using System.Collections.Generic;
using Board;
using Pieces;
using UI;
using UnityEngine;

namespace Game
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Transform boardTransform;
        [SerializeField] private BoardSettings boardSettings;
        [SerializeField] private Camera gameCamera;
        [SerializeField] private PiecesSetupSettings piecesSettings;
        [SerializeField] private Transform whitePieceParent;
        [SerializeField] private Transform blackPieceParent;
        [SerializeField] private InfoPanel infoPanel;

        private List<PieceController> pieces;
    
        private IPiecePlacingController piecePlacingController;
        private BoardGenerator boardGenerator;
        private BoardManager boardManager;
        private VictoryChecker victoryChecker;
        private TurnController turnController;
    
        private void Start()
        {
            SetupBoard();
            AdjustFigureSpawnPoints();
            SetupPieces();
            SetupGameRules();
        }

        private void SetupGameRules()
        {
            victoryChecker = new VictoryChecker(boardManager);
            turnController = new TurnController(victoryChecker, infoPanel, boardManager, pieces);
        }

        private void SetupPieces()
        {
            pieces = new PieceGenerator().CreatePieces(piecesSettings, whitePieceParent, blackPieceParent);
        }

        private void SetupBoard()
        {
            boardGenerator = new BoardGenerator(boardSettings, boardTransform);
            var squares = boardGenerator.GenerateBoard(gameCamera);
            
            boardManager = new BoardManager(squares);
        }

        private void AdjustFigureSpawnPoints()
        {
            const int offset = 1;

            var halfPieceSize = piecesSettings.PieceSize / 2f;
            var boardHeight = boardGenerator.GetHalfBoardHeight();
            whitePieceParent.transform.position = Vector3.up * (boardHeight + halfPieceSize + offset);
            blackPieceParent.transform.position = Vector3.down * (boardHeight + halfPieceSize + offset);
        }
    }
}
