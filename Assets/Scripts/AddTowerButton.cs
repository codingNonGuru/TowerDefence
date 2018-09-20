using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
	public class AddTowerButton : Element 
	{
		void Start()
		{
			GameManager.OnTowerBuildModeEntered += HandleBuildModeEntered;
		}

		public void AddTower()
		{
			GameManager.EnterTowerBuildMode();
		}

		void HandleBuildModeEntered()
		{
			gameObject.SetActive(false);
		}
	}
}