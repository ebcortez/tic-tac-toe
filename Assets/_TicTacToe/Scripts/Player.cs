using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe.Gameplay {
	[System.Serializable]
	public class Player {
		private bool isTurn;
		private Sprite assignedSprite;
		private TileState assignedTileState;

		public bool IsTurn => isTurn;
		public Sprite AssignedSprite => assignedSprite;
		public TileState AssignedTileState => assignedTileState;

		public Player(Sprite assignedSprite, TileState assignedTileState) {
			isTurn = false;
			this.assignedSprite = assignedSprite;
			this.assignedTileState = assignedTileState;
		}
	} 
}