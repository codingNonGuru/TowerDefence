using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
	public class AddTowerButton : Element 
	{
		void Start()
		{
			GameManager.OnTowerAddStarted += HandleTowerAddStarted;
		}

		public void AddTower()
		{
			GameManager.StartAddingTower();
		}

		void HandleTowerAddStarted()
		{
			gameObject.SetActive(false);
		}
	}
}