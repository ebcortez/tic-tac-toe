using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TicTacToe.Gameplay;

namespace TicTacToe {
	[System.Serializable]
	public class Player {
		private string playerName;
		private bool isTurn;
		private Sprite assignedSprite;
		private TileState assignedTileState;
		private int score;

		public string PlayerName => playerName;
		public bool IsTurn => isTurn;
		public Sprite AssignedSprite => assignedSprite;
		public TileState AssignedTileState => assignedTileState;

		public Player(string playerName, Sprite assignedSprite, TileState assignedTileState) {
			this.playerName = playerName;
			isTurn = false;
			this.assignedSprite = assignedSprite;
			this.assignedTileState = assignedTileState;
			AddScore(0);
		}

		public void AddScore(int scoreToAdd) {
			score += scoreToAdd;
			GameManager.Instance.OnPlayerScore?.Invoke(this);
		}

		public int GetScore() => score;
	} 
}