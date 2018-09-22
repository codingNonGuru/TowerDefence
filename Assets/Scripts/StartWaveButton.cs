﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
	public class StartWaveButton : Element 
	{
		public void StartWave()
		{
			CreepManager.LaunchWave();
		}

		protected override void OnSetup()
		{
			GameManager.OnTowerBuildModeEntered += HandleBuildModeEntered;

			GameManager.OnTowerBuildModeExited += HandleBuildModeExited;

			CreepManager.OnWaveLaunched += HandleWaveStarted;

			CreepManager.OnWaveEnded += HandleWaveEnded;
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