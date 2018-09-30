using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
	public class GameOverPanel : Element 
	{
		[SerializeField]
		Text messageLabel = null;

		public void RestartGame()
		{
			GameManager.Restart();
		}

		protected override void OnSetup()
		{
			CreepManager.OnWaveEnded += HandleWaveEnded;

			GameManager.OnCreepEscaped += HandleCreepEscaped;

			GameManager.OnGameRestarted += HandleGameRestarted;
		}

		void HandleWaveEnded()
		{
			if(!CreepManager.HasLastWavePassed)
				return;
				
			gameObject.SetActive(true);

			RefreshLabel();
		}

		void HandleCreepEscaped()
		{
			if(GameManager.IsAlive)
				return;

			gameObject.SetActive(true);

			RefreshLabel();
		}

		void RefreshLabel()
		{
			if(messageLabel != null)
			{
				messageLabel.text = GameManager.IsAlive ? "You won!" : "You lost!";
			}
		}

		void HandleGameRestarted()
		{
			gameObject.SetActive(false);
		}
	}
}