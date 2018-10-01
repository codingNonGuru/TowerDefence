using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
	public class TowerRangeDisplay : MonoBehaviour 
	{
		void Update()
		{
			if(TowerManager.SelectedTowerClass != null)
			{
				transform.localScale = Vector3.one * TowerManager.SelectedTowerClass.AttackRange * 2.0f;
			}
		}

		public void Setup()
		{
			TileManager.OnTileSelected += HandleTileSelected;
		}

		void HandleTileSelected()
		{
			var selectedTile = TileManager.SelectedTile;

			if(selectedTile != null)
			{
				transform.position = selectedTile.transform.position;
				gameObject.SetActive(true);
			}
			else
			{
				gameObject.SetActive(false);
			}
		}
	}
}