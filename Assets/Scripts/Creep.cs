using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
	public class Creep : MonoBehaviour 
	{
		CreepData data;

		int currentHitpoints = 0;

		float timer = 0.0f;

		Tile currentTile = null;

		public CreepData Data
		{
			set {data = value;}
			get {return data;}
		}

		public bool IsDead
		{
			get {return currentHitpoints == 0;}
		}
		
		void Update () 
		{
			if(timer > 1.0f)
			{
				timer -= 1.0f;
				currentTile = currentTile.NextTile;
			}

			if(currentTile.NextTile == null)
			{
				CreepManager.DespawnCreep(this);

				return;	
			}

			transform.position = currentTile.transform.position * (1.0f - timer) + currentTile.NextTile.transform.position * timer;

			timer += Time.deltaTime * data.MoveSpeed;
		}

		public void Spawn(CreepData data)
		{
			gameObject.SetActive(true);

			currentTile = TileManager.FirstRoadTile;
			if(currentTile != null)
			{
				transform.position = currentTile.transform.position;
			}

			this.data = data;

			currentHitpoints = data.MaximumHitpoints;
			transform.localScale = Vector3.one * data.Size;

			timer = 0.0f;
		}

		public bool IsAlive()
		{
			return currentHitpoints > 0;
		}

		public void Damage()
		{
			currentHitpoints--;

			if(!IsAlive())
			{
				CreepManager.DespawnCreep(this);
			}
		}
	}
}