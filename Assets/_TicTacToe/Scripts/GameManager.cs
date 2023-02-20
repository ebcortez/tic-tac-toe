using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TicTacToe.Gameplay {
	public class GameManager : MonoBehaviour {
		private static GameManager instance;
		public static GameManager Instance => instance;

		[SerializeField] private Sprite xTexture, oTexture;
		[HideInInspector] public UnityEvent<Vector2Int> OnTileOccupied;

		public TileBehaviour[,] Grid { get; set; } = new TileBehaviour[3, 3];

		private Player player1, player2;
		public Player Player1 => player1;
		public Player Player2 => player2;
		public Player CurrentPlayer { get; private set; }

		public int TotalMoves { get; private set; }
		public bool HasWinner { get; private set; }

		private void Awake() {
			instance = this;

			player1 = new Player(xTexture, TileState.X);
			player2 = new Player(oTexture, TileState.O);
			
			CurrentPlayer = player1;
		}

		private void Start() {
			OnTileOccupied.AddListener((tileGridPosition) => {
				CheckForWinner(tileGridPosition);
				if(!HasWinner) EndTurn();
			});
		}

		private void CheckForWinner(Vector2Int tileGridPosition) {
			int gridSize = Mathf.RoundToInt(Mathf.Sqrt(Grid.Length));
			int column = 0, row = 0, diagonal = 0, reverseDiagonal = 0;
			for(int gridIndex = 0; gridIndex < gridSize; gridIndex++) {
				if(Grid[gridIndex, tileGridPosition.y].TileState == CurrentPlayer.AssignedTileState) column++;
				if(Grid[tileGridPosition.x, gridIndex].TileState == CurrentPlayer.AssignedTileState) row++;
				if(Grid[gridIndex, gridIndex].TileState == CurrentPlayer.AssignedTileState) diagonal++;
				if(Grid[gridIndex, gridSize - gridIndex - 1].TileState == CurrentPlayer.AssignedTileState) reverseDiagonal++;

				if(column == gridSize || row == gridSize || diagonal == gridSize || reverseDiagonal == gridSize) {
					HasWinner = true;
				}
			}

			if(++TotalMoves >= Grid.Length) {
				Debug.Log("DRAW!");
			}
		}

		private void EndTurn() {
			CurrentPlayer = CurrentPlayer == player1 ? player2 : player1;
		}
	}
}