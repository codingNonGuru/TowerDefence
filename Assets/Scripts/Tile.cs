using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
	public class Tile : MonoBehaviour 
	{
		static Color selectedColor = Color.red;

		static Color unselectedColor = Color.white;

		SpriteRenderer spriteRenderer = null;

		bool isMouseOver = false;

		public bool IsMouseOver
		{
			get {return isMouseOver;}
		}

		void Start()
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
		}

		public void RefreshStatus()
		{
			var color = GameManager.IsAddingTower && TileManager.SelectedTile == this ? selectedColor : unselectedColor;

			if(spriteRenderer != null)
			{
				spriteRenderer.color = color;
			}
		}

		void OnMouseEnter()
		{
			isMouseOver = true;
		}

		void OnMouseExit()
		{
			isMouseOver = false;
		}
	}
}
