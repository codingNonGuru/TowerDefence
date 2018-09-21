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

			GameManager.OnTowerBuildModeExited += HandleBuildModeExited;
		}

		public void AddTower()
		{
			GameManager.EnterTowerBuildMode();
		}

		void HandleBuildModeEntered()
		{
			gameObject.SetActive(false);
		}

		void HandleBuildModeExited()
		{
			gameObject.SetActive(true);
		}
	}
}