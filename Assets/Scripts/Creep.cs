using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
	public class Creep : MonoBehaviour 
	{
		CreepData data;

		int currentHitpoints = 0;

		public CreepData Data
		{
			set {data = value;}
		}
		
		void Update () 
		{

		}

		public void Spawn()
		{
			gameObject.SetActive(true);

			var spawnPlace = TileManager.FirstRoadTile;
			if(spawnPlace != null)
			{
				transform.position = spawnPlace.transform.position;
			}

			//currentHitpoints = data.MaximumHitpoints;
		}
	}
}