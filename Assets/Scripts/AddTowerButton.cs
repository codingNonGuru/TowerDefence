﻿using System.Collections;
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

			GameManager.OnGameRestarted += HandleGameRestarted;

			CreepManager.OnWaveLaunched += HandleWaveStarted;

			CreepManager.OnWaveEnded += HandleWaveEnded;
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

		void HandleGameRestarted()
		{
			gameObject.SetActive(true);
		}

		void HandleWaveStarted()
		{
			gameObject.SetActive(false);
		}

		void HandleWaveEnded()
		{
			if(CreepManager.HasLastWavePassed)
				return;

			gameObject.SetActive(true);
		}
	}
}