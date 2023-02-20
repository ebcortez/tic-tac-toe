using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;

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

		private const string TITLE_ID = "87890";

		public Player Player { get; set; }

		private void Awake() {
			if(instance == null) {
				instance = this;
				DontDestroyOnLoad(gameObject);
			} else {
				Destroy(this);
			}

			if(string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId)) {
				PlayFabSettings.staticSettings.TitleId = TITLE_ID;
			}
		}
	}
}