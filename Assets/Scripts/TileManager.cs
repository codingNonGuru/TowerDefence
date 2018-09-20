using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
	public class TileManager : MonoBehaviour 
	{
		static TileManager instance = null;

		public static event Action OnTileSelected;

		public const float scaleFactor = 0.6f;

		[SerializeField]
		GameObject tilePrefab = null;

		[SerializeField]
		int mapSize;

		List <Tile> tiles = null;

		Tile selectedTile = null;

		public static Tile SelectedTile
		{
			get {return instance.selectedTile;}
		}

		void Awake()
		{
			if(instance == null)
				instance = this;
		}

		void Start () 
		{
			GenerateTiles();
		}

		void Update()
		{
			if(!GameManager.IsAddingTower)
			{
				if(selectedTile != null)
				{
					selectedTile.RefreshStatus();
				}

				selectedTile = null;
				return;
			}

			Tile previousTile = selectedTile;

			selectedTile = null;
			foreach(var tile in tiles)
			{
				if(tile.IsMouseOver)
				{
					selectedTile = tile;
					break;
				}
			}

			if(previousTile != null)
			{
				previousTile.RefreshStatus();
			}

			if(selectedTile != null)
			{
				selectedTile.RefreshStatus();
			}
		}

		void GenerateTiles()
		{
			if(tilePrefab == null)
				return;

			var tileCount = mapSize * mapSize;
			tiles = new List <Tile> (tileCount);

			float positionOffset = (float)(mapSize - 1) * 0.5f;
			for(int i = 0; i < mapSize; ++i)
			{
				for(int j = 0; j < mapSize; ++j)
				{
					var position = new Vector3((float)i - positionOffset, (float)j - positionOffset, 0.0f);
					position *= scaleFactor;

					var tileObject = Instantiate(tilePrefab, position, Quaternion.identity);
					if(tileObject == null)
						continue;

					var tile = tileObject.GetComponent<Tile>();
					if(tile == null)
						continue;

					tiles.Add(tile);
					tileObject.transform.SetParent(gameObject.transform);
				}	
			}
		}
	}
}