using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe.Gameplay {
	public class GridManager : MonoBehaviour {
		[SerializeField] private TileBehaviour baseTile;

		private void Start() {
			var gridManagerTransform = transform;

			for(int row = 0; row < 3; row++) {
				for(int column = 0; column < 3; column++) {
					Instantiate(baseTile, new Vector3(row + baseTile.Position.x, column - baseTile.Position.y, 0), Quaternion.identity, gridManagerTransform);
				}
			}

			baseTile.gameObject.SetActive(false);
		}
	}
}