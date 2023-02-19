using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TicTacToe.Gameplay {
	public class GameManager : MonoBehaviour {
		private static GameManager instance;
		public static GameManager Instance => instance;

		[SerializeField] private Sprite xTexture, oTexture;
		[HideInInspector] public UnityEvent OnTileOccupied;

		private Player player1, player2;
		public Player Player1 => player1;
		public Player Player2 => player2;
		public Player CurrentPlayer { get; private set; }

		private void Awake() {
			instance = this;

			player1 = new Player(xTexture, TileState.X);
			player2 = new Player(oTexture, TileState.O);
			
			CurrentPlayer = player1;
		}

		private void Start() {
			OnTileOccupied.AddListener(() => {
				if(CurrentPlayer == player1) CurrentPlayer = player2;
				else CurrentPlayer = player1;
			});
		}
	}
}