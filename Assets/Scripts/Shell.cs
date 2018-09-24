using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
	public class Shell : MonoBehaviour 
	{
		const float lifeSpan = 5.0f;

		const float speedModifier = 5.0f;

		float lifeTime = 0.0f;

		public void Fire()
		{
			lifeTime = 0.0f;

			gameObject.SetActive(true);
		}

		void Update()
		{
			transform.position += transform.up * speedModifier * Time.deltaTime;

			if(lifeTime > lifeSpan)
			{
				TowerManager.DestroyShell(this);
				gameObject.SetActive(false);
			}

			lifeTime += Time.deltaTime;
		}
	}
}