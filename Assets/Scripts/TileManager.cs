using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
	public class TileManager : MonoBehaviour 
	{
		static TileManager instance = null;

		[SerializeField]
		GameObject tilePrefab = null;

		[SerializeField]
		int mapSize;

		List <Tile> tiles = null;

		void Awake()
		{
			if(instance == null)
				instance = this;
		}

		void Start () 
		{
			GenerateTiles();
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