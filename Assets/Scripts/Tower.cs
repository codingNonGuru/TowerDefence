using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
	public class Tower : MonoBehaviour 
	{
		SpriteRenderer spriteRenderer = null;

		TowerClass towerClass = null;

		float shootTimer = 0.0f;

		float timeBetweenShots = 0.0f;

		public TowerClass TowerClass
		{
			set {towerClass = value;}
		}

		void Start () 
		{
			spriteRenderer = GetComponent<SpriteRenderer>();

			if(spriteRenderer != null)
			{
				spriteRenderer.color = towerClass.IconColor;
			}

			timeBetweenShots = 1.0f / (float)towerClass.AttackRate;
		}

		void Update()
		{
			shootTimer += Time.deltaTime;

			var creeps = CreepManager.Creeps;
			if(creeps == null)
				return;

			if(shootTimer < timeBetweenShots)
				return;

			float closestDistance = 9999.9f;
			Creep closestCreep = null;
			foreach(var creep in creeps)
			{
				if(creep.gameObject.activeSelf == false)
					continue;

				float distance = Vector3.Distance(creep.transform.position, transform.position);

				if(distance > towerClass.AttackRange)
					continue;

				if(distance < closestDistance)
				{
					closestDistance = distance;
					closestCreep = creep;
				}
			}

			Fire(closestCreep);
		}

		void Fire(Creep targetCreep)
		{
			if(targetCreep == null)
				return;

			var shell = TowerManager.CreateShell();
			if(shell == null)
				return;

			shell.transform.position = transform.position;

			var fireDirection = targetCreep.transform.position - shell.transform.position;
			fireDirection.z = 0.0f;
			shell.transform.up = fireDirection.normalized;

			shell.Fire();

			shootTimer = 0.0f;
		}
	}
}