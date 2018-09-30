using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
	public class Shell : MonoBehaviour 
	{
		const float lifeSpan = 5.0f;

		const float speedModifier = 10.0f;

		float lifeTime = 0.0f;

		int damagePotential = 0;

		bool hasChillEffect = false;

		public bool HasChillEffect
		{
			get {return hasChillEffect;}
		}

		void Update()
		{
			transform.position += transform.up * speedModifier * Time.deltaTime;

			if(lifeTime > lifeSpan)
			{
				TowerManager.DestroyShell(this);
			}

			lifeTime += Time.deltaTime;
		}

		void OnTriggerEnter2D(Collider2D collider)
		{
			if(damagePotential <= 0)
				return;

			var creep = collider.gameObject.GetComponent<Creep>();
			if(creep == null)
				return;

			if(!creep.IsAlive())
				return;

			creep.Damage(this);

			damagePotential--;

			if(damagePotential <= 0)
			{
				TowerManager.DestroyShell(this);
			}
		}

		public void Fire(Tower parentTower, Creep targetCreep)
		{
			transform.position = parentTower.transform.position;

			var fireDirection = targetCreep.transform.position - transform.position;
			fireDirection.z = 0.0f;
			transform.up = fireDirection.normalized;

			lifeTime = 0.0f;

			gameObject.SetActive(true);

			damagePotential = parentTower.TowerClass.Damage;

			hasChillEffect = parentTower.TowerClass.HasSlow;
		}
	}
}