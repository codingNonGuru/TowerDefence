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

		Tower tower = null;

		public bool IsMouseOver
		{
			get {return isMouseOver;}
		}

		public Tower Tower 
		{
			get {return tower;}
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

		public void AddTower(Tower tower)
		{
			this.tower = tower;

			tower.transform.SetParent(transform);
			tower.transform.localPosition = Vector3.zero;
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
