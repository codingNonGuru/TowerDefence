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

		int damagePotential = 1;

		public void Fire()
		{
			lifeTime = 0.0f;

			gameObject.SetActive(true);

			damagePotential = 1;
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

			creep.Damage();

			damagePotential--;

			if(damagePotential <= 0)
			{
				TowerManager.DestroyShell(this);
			}
		}
	}
}