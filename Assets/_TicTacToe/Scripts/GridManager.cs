using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe.Gameplay {
	public class GridManager : MonoBehaviour {
		[SerializeField] private TileBehaviour baseTile;
		[SerializeField] private Vector2Int startPosition;

		private void Start() {
			var gridManagerTransform = transform;

			for(int row = 0; row < 3; row++) {
				for(int column = 0; column < 3; column++) {
					GameManager.Instance.Grid[row, column] = Instantiate(baseTile, new Vector3(row + startPosition.x, column - startPosition.y, 0), Quaternion.identity, gridManagerTransform);
					GameManager.Instance.Grid[row, column].SetGridPosition(new Vector2Int(row, column));
				}
			}

			baseTile.gameObject.SetActive(false);
		}
	}
}