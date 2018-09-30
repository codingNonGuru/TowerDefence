using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
	public class Creep : MonoBehaviour 
	{
		static readonly float chillDuration = 1.0f;

		static readonly float chillSpeedModifier = 0.5f;

		static readonly Color normalColor = Color.green;

		static readonly Color chilledColor = Color.cyan;

		CreepData data;

		int currentHitpoints = 0;

		float timer = 0.0f;

		bool isChilled = false;

		float chillTimer = 0.0f;

		Tile currentTile = null;

		SpriteRenderer spriteRenderer = null;

		public CreepData Data
		{
			set {data = value;}
			get {return data;}
		}

		public bool IsDead
		{
			get {return currentHitpoints == 0;}
		}

		void Start()
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
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

			timer += Time.deltaTime * data.MoveSpeed * (isChilled ? chillSpeedModifier : 1.0f);

			if(isChilled)
			{
				chillTimer += Time.deltaTime;

				if(chillTimer > chillDuration)
				{
					isChilled = false;
				}
			}

			if(spriteRenderer != null)
			{
				spriteRenderer.color = isChilled ? chilledColor : normalColor;
			}
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

			isChilled = false;
		}

		public bool IsAlive()
		{
			return currentHitpoints > 0;
		}

		public void Damage(Shell shell)
		{
			currentHitpoints--;

			if(shell.HasChillEffect)
			{
				chillTimer = 0.0f;

				isChilled = true;
			}

			if(!IsAlive())
			{
				CreepManager.DespawnCreep(this);
			}
		}
	}
}