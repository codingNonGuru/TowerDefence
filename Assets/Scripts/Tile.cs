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

		static Color roadColor = Color.gray;

		SpriteRenderer spriteRenderer = null;

		bool isMouseOver = false;

		Tower tower = null;

		bool isRoad = false;

		Tile nextTile = null;

		public bool IsMouseOver
		{
			get {return isMouseOver;}
		}

		public Tower Tower 
		{
			get {return tower;}
		}

		public bool IsRoad
		{
			set {isRoad = value;}
		}

		public Tile NextTile
		{
			get {return nextTile;}
			set {nextTile = value;}
		}

		void Start()
		{
			spriteRenderer = GetComponent<SpriteRenderer>();

			RefreshStatus();
		}

		public void RefreshStatus()
		{
			var defaultColor = isRoad ? roadColor : unselectedColor;
			var color = GameManager.IsAddingTower && TileManager.SelectedTile == this ? selectedColor : defaultColor;

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
