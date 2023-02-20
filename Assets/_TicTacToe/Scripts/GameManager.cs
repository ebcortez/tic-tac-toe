using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace TicTacToe.Gameplay {
	public class GameManager : MonoBehaviour {
		private static GameManager instance;
		public static GameManager Instance => instance;

		[SerializeField] private Sprite xTexture, oTexture;
		[HideInInspector] public UnityEvent<Vector2Int> OnTileOccupied;
		[HideInInspector] public UnityEvent<Player> OnPlayerScore;
		[HideInInspector] public UnityEvent<Player> OnGameEnd;

		public TileBehaviour[,] Grid { get; set; }

		private Player player1, player2;
		public Player Player1 => player1;
		public Player Player2 => player2;
		public Player CurrentPlayer { get; private set; }

		public int TotalMoves { get; private set; }
		public bool HasWinner { get; private set; }

		private void Awake() {
			instance = this;

			HasWinner = false;
			TotalMoves = 0;
		}

		private void Start() {
			SetPlayer1();
			SetPlayer2();
			CurrentPlayer = player1;

			OnTileOccupied.AddListener(tileGridPosition => {
				CheckForWinner(tileGridPosition);
				if(!HasWinner) EndTurn();
			});
		}

		private void SetPlayer1() {
			player1 = PlayerManager.Instance.Player == null ? new Player("Player 1", xTexture, TileState.X) : new Player(PlayerManager.Instance.Player.PlayerName, xTexture, TileState.X);
		}

		private void SetPlayer2() {
			player2 = new Player("Player 2", oTexture, TileState.O);
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
					CurrentPlayer.AddScore(1);
					OnGameEnd?.Invoke(CurrentPlayer);
				}
			}

			if(!HasWinner && ++TotalMoves >= Grid.Length) {
				OnGameEnd?.Invoke(null);
				Debug.Log("DRAW!");
			}
		}

		private void EndTurn() {
			if(CurrentPlayer == player1) {
				CurrentPlayer = player2;
				if(!HasWinner && TotalMoves < Grid.Length) {
					RandomAIMove();
				}
			} else {
				CurrentPlayer = player1;
			}
		}

		private void RandomAIMove() {
			var r = new System.Random();
			int values = Grid.GetLength(0) * Grid.GetLength(1);
			int index = r.Next(values);

			var tile = Grid[index / Grid.GetLength(0), index % Grid.GetLength(0)];

			if(tile.IsOccupied) {
				RandomAIMove();
			} else tile.OccupyTile(player2);
		}

		public void RestartGame() => SceneManager.LoadScene("GameplayScene");
		public void NextRound() {
			HasWinner = false;
			TotalMoves = 0;
			CurrentPlayer = player1;

			foreach(TileBehaviour tile in Grid) {
				tile.ResetTile();
			}
		}
	}
}