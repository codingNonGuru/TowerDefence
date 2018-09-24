using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
	public class GameOverPanel : Element 
	{
		public void RestartGame()
		{
			GameManager.Restart();
		}

		protected override void OnSetup()
		{
			CreepManager.OnWaveEnded += HandleWaveEnded;

			GameManager.OnGameRestarted += HandleGameRestarted;
		}

		void HandleWaveEnded()
		{
			if(!CreepManager.HasLastWavePassed)
				return;
				
			gameObject.SetActive(true);
		}

		void HandleGameRestarted()
		{
			gameObject.SetActive(false);
		}
	}
}