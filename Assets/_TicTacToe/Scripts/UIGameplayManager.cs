using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace TicTacToe.Gameplay.UI {
	public class UIGameplayManager : MonoBehaviour {
		[System.Serializable]
		private struct PlayerDetails {
			public TextMeshProUGUI playerName;
			public TextMeshProUGUI playerScore;
		}

		[SerializeField] private PlayerDetails player1Details, player2Details;
		[SerializeField] private GameObject nextRoundButton;
		[SerializeField] private TextMeshProUGUI resultText;

		public void SetPlayer1Name(string name) => player1Details.playerName.text = name;
		public void SetPlayer2Name(string name) => player2Details.playerName.text = name;

		public void SetPlayer1Score(int score) => player1Details.playerScore.text = $"Score: {score}";
		public void SetPlayer2Score(int score) => player2Details.playerScore.text = $"Score: {score}";

		public void RestartGame() => GameManager.Instance.RestartGame();
		public void BackToMainMenu() => SceneManager.LoadScene("MainMenuScene");
		public void NextRound() {
			GameManager.Instance.NextRound();
			resultText.text = "";
			nextRoundButton.SetActive(false);
		}

		private void SetPlayersScore(Player player) {
			if(player.AssignedTileState == TileState.X) SetPlayer1Score(player.GetScore());
			else if(player.AssignedTileState == TileState.O) SetPlayer2Score(player.GetScore());
		}

		private void Awake() {
			GameManager.Instance.OnPlayerScore.AddListener(SetPlayersScore);
			GameManager.Instance.OnGameEnd.AddListener(winner => { 
				nextRoundButton.SetActive(true);
				if(winner == GameManager.Instance.Player1) {
					resultText.text = "You Won!";
					resultText.color = Color.green;
				} else if (winner == GameManager.Instance.Player2) {
					resultText.text = "You Lose!";
					resultText.color = Color.red;
				} else {
					resultText.text = "Draw.";
					resultText.color = Color.yellow;
				}
			});

			nextRoundButton.SetActive(false);
		}

		private void Start() {
			SetPlayer1Name(GameManager.Instance.Player1.PlayerName);
			SetPlayer2Name(GameManager.Instance.Player2.PlayerName);
		}
	}
}