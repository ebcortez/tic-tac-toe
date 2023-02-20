using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe {
	public class PlayerManager : MonoBehaviour {
		private static PlayerManager instance;
		public static PlayerManager Instance {
			get {
				if(instance == null) {
					var go = new GameObject("PlayerManager", typeof(PlayerManager));
					instance = go.GetComponent<PlayerManager>();
				}
				return instance;
			}
		}

		public Player Player { get; private set; }

		private void Awake() {
			if(instance == null) {
				instance = this;
				DontDestroyOnLoad(gameObject);
			} else {
				Destroy(this);
			}
		}
	}
}