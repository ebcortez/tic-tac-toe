using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;

namespace TicTacToe.MainMenu {
	public class UIPlayFabLogin : MonoBehaviour {
		[SerializeField] private TextMeshProUGUI systemMessage;

		[Header("Login UI Elements")]
		[SerializeField] private TMP_InputField emailLoginInput;
		[SerializeField] private TMP_InputField passwordLoginInput;
		[SerializeField] private GameObject loginPanel, loggingInPanel;

		[Header("Register UI Elements")]
		[SerializeField] private TMP_InputField userNameRegisterInput;
		[SerializeField] private TMP_InputField emailRegisterInput;
		[SerializeField] private TMP_InputField passwordRegisterInput;
		[SerializeField] private GameObject registerPanel;

		[Header("Recover Password UI Elements")]
		[SerializeField] private TMP_InputField emailRecoveryInput;
		[SerializeField] private GameObject recoverPasswordPanel;

		public void OpenLoginPanel() {
			loginPanel.SetActive(true);
			registerPanel.SetActive(false);
			recoverPasswordPanel.SetActive(false);
		}
		public void OpenRegisterPanel() {
			loginPanel.SetActive(false);
			registerPanel.SetActive(true);
			recoverPasswordPanel.SetActive(false);
		}
		public void OpenRecoverPasswordPanel() {
			loginPanel.SetActive(false);
			registerPanel.SetActive(false);
			recoverPasswordPanel.SetActive(true);
		}

		public void SetSystemTextColor(string message, Color color) {
			systemMessage.color = color;
			systemMessage.text = message;
		}

		public void LoginUser() {
			var request = new LoginWithEmailAddressRequest {
				Email = emailLoginInput.text,
				Password = passwordLoginInput.text,
				InfoRequestParameters = new GetPlayerCombinedInfoRequestParams {
					GetPlayerProfile = true
				}
			};

			PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
		}

		public void RegisterUser() {
			if(passwordRegisterInput.text.Length < 6) {
				SetSystemTextColor("Password is too short. Must have more than 6 letters", Color.red);
				return;
			}

			var request = new RegisterPlayFabUserRequest {
				DisplayName = userNameRegisterInput.text,
				Email = emailRegisterInput.text,
				Password = passwordRegisterInput.text,
				RequireBothUsernameAndEmail = false
			};

			PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
		}

		public void LoginGuest() {
			var request = new LoginWithCustomIDRequest { CustomId = $"Guest {Random.Range(1000, 10000)}", CreateAccount = true, InfoRequestParameters = new GetPlayerCombinedInfoRequestParams {
				GetPlayerProfile = true
				}
			};
			PlayFabClientAPI.LoginWithCustomID(request, OnLoginGuestSuccess, OnError);
		}

		public void RecoverUserPassword() {
			var request = new SendAccountRecoveryEmailRequest {
				Email = emailRecoveryInput.text,
				TitleId = PlayFabSettings.staticSettings.TitleId
			};

			PlayFabClientAPI.SendAccountRecoveryEmail(request, OnRecoverySuccess, OnError);
		}

		private void OnError(PlayFabError error) {
			SetSystemTextColor(error.ErrorMessage, Color.red);
			Debug.LogError(error.GenerateErrorReport());
		}

		private void OnLoginGuestSuccess(LoginResult result) {
			SetSystemTextColor("Logging in as guest...", Color.yellow);

			if(result.InfoResultPayload != null) {
				PlayerManager.Instance.Player = new Player("Guest");
			}

			loggingInPanel.SetActive(true);

			StartCoroutine(DelayLogin());
			IEnumerator DelayLogin() {
				yield return new WaitForSeconds(3);
				SetSystemTextColor("Logged in!", Color.green);
				SceneManager.LoadScene("MainMenuScene");
			}
		}

		private void OnLoginSuccess(LoginResult result) {
			SetSystemTextColor("Logging in...", Color.yellow);

			if(result.InfoResultPayload != null) {
				PlayerManager.Instance.Player = new Player(result.InfoResultPayload.PlayerProfile.DisplayName);
			}

			loggingInPanel.SetActive(true);

			StartCoroutine(DelayLogin());
			IEnumerator DelayLogin() {
				yield return new WaitForSeconds(3);
				SetSystemTextColor("Logged in!", Color.green);
				SceneManager.LoadScene("MainMenuScene");
			}
		}

		private void OnRegisterSuccess(RegisterPlayFabUserResult result) {
			SetSystemTextColor("Account successfully created!", Color.green);
			OpenLoginPanel();
		}

		private void OnRecoverySuccess(SendAccountRecoveryEmailResult result) {
			SetSystemTextColor("Recovery mail sent.", Color.white);
			OpenLoginPanel();
		}
	}
}