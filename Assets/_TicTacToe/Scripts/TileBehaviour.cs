using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe.Gameplay {
	public class TileBehaviour : MonoBehaviour {
		[SerializeField] private SpriteRenderer tileSprite;
		[SerializeField] private TileState tileState;

		public Vector3 Position { get; private set; }
		public TileState TileState => tileState;

		public void OccupyTile(Player occupant) {
			tileSprite.sprite = occupant.AssignedSprite;
			tileState = occupant.AssignedTileState;
			GameManager.Instance.OnTileOccupied?.Invoke();
		}

		public bool IsOccupied => tileState != TileState.Empty;

		private void Awake() {
			Position = transform.position;
		}

		private void OnMouseDown() {
			if(IsOccupied) return;

			OccupyTile(GameManager.Instance.CurrentPlayer);
		}
	} 
}
