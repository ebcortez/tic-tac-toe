using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe.Gameplay {
	public class GridManager : MonoBehaviour {
		[SerializeField] private TileBehaviour baseTile;
		[SerializeField] private Vector2Int startPosition;
		[SerializeField, Range(3, 5)] private int gridSize;

		private void Awake() {
			if(gridSize < 3) gridSize = 3;
		}

		private void Start() {
			var gridManagerTransform = transform;

			TileBehaviour[,] Grid = new TileBehaviour[gridSize, gridSize];

			for(int row = 0; row < gridSize; row++) {
				for(int column = 0; column < gridSize; column++) {
					Grid[row, column] = Instantiate(baseTile, new Vector3(row + startPosition.x, column - startPosition.y, 0), Quaternion.identity, gridManagerTransform);
					Grid[row, column].SetGridPosition(new Vector2Int(row, column));
				}
			}

			GameManager.Instance.Grid = Grid;

			baseTile.gameObject.SetActive(false);
		}
	}
}