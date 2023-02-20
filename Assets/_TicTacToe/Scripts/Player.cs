using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TicTacToe.Gameplay;

namespace TicTacToe {
	[System.Serializable]
	public class Player {
		private string playerName;
		private Sprite assignedSprite;
		private TileState assignedTileState;
		private int score;

		public string PlayerName => playerName;
		public Sprite AssignedSprite => assignedSprite;
		public TileState AssignedTileState => assignedTileState;

		public Player(string playerName, Sprite assignedSprite, TileState assignedTileState) {
			this.playerName = playerName;
			this.assignedSprite = assignedSprite;
			this.assignedTileState = assignedTileState;
			AddScore(0);
		}

		public Player(string playerName) {
			this.playerName = playerName;
		}

		public void AddScore(int scoreToAdd) {
			score += scoreToAdd;
			GameManager.Instance.OnPlayerScore?.Invoke(this);
		}

		public int GetScore() => score;
	} 
}