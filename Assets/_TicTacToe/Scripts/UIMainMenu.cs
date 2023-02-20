using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TicTacToe.MainMenu {
	public class UIMainMenu : MonoBehaviour {
		public void StartGame() {
			SceneManager.LoadScene("GameplayScene");
		}

		public void ExitGame() {
			Application.Quit();
		}
	} 
}
