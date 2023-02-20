using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TicTacToe.Gameplay.UI {
	public class UIGameplayManager : MonoBehaviour {
		[System.Serializable]
		private struct PlayerDetails {
			public TextMeshProUGUI playerName;
			public TextMeshProUGUI playerScore;
		}

		[SerializeField] private PlayerDetails player1Details, player2Details;

		public void SetPlayer1Name(string name) => player1Details.playerName.text = name;
		public void SetPlayer2Name(string name) => player2Details.playerName.text = name;

		public void SetPlayer1Score(int score) => player1Details.playerScore.text = $"Score: {score}";
		public void SetPlayer2Score(int score) => player2Details.playerScore.text = $"Score: {score}";

		public void RestartGame() => GameManager.Instance.RestartGame();

		private void SetPlayersScore(Player player) {
			if(player.AssignedTileState == TileState.X) SetPlayer1Score(player.GetScore());
			else if(player.AssignedTileState == TileState.O) SetPlayer2Score(player.GetScore());
		}

		private void Awake() {
			GameManager.Instance.OnPlayerScore.AddListener(SetPlayersScore);
		}
	}
}