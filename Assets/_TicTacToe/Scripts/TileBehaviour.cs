using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe.Gameplay {
	public class TileBehaviour : MonoBehaviour {
		[SerializeField] private SpriteRenderer tileSprite;
		[SerializeField] private TileState tileState;

		private Vector2Int gridPosition;

		public TileState TileState => tileState;

		public void SetGridPosition(Vector2Int gridPosition) {
			this.gridPosition = gridPosition;
		}

		public void OccupyTile(Player occupant) {
			tileSprite.sprite = occupant.AssignedSprite;
			tileState = occupant.AssignedTileState;
			GameManager.Instance.OnTileOccupied?.Invoke(gridPosition);
		}

		public bool IsOccupied => tileState != TileState.Empty;

		private void OnMouseDown() {
			if(IsOccupied || GameManager.Instance.HasWinner) return;

			OccupyTile(GameManager.Instance.CurrentPlayer);
		}
	} 
}
