using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
	public class ReturnButton : Element 
	{
		protected override void OnSetup()
		{
			GameManager.OnTowerBuildModeEntered += HandleBuildModeEntered;

			GameManager.OnTowerBuildModeExited += HandleBuildModeExited;
		}

		public void Return()
		{
			GameManager.ExitTowerBuildMode();
		}

		void HandleBuildModeEntered()
		{
			gameObject.SetActive(true);
		}

		void HandleBuildModeExited()
		{
			gameObject.SetActive(false);
		}
	}
}