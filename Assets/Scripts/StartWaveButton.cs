using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
	public class StartWaveButton : Element 
	{
		protected override void OnSetup()
		{
			GameManager.OnTowerBuildModeEntered += HandleBuildModeEntered;

			//GameManager.OnTowerBuildModeExited += HandleBuildModeExited;
		}

		void HandleBuildModeEntered()
		{
			gameObject.SetActive(false);
		}

		void HandleBuildModeExited()
		{
			gameObject.SetActive(true);
		}

		void HandleWaveStarted()
		{
			gameObject.SetActive(false);
		}

		void HandleWaveEnded()
		{
			gameObject.SetActive(true);
		}
	}
}