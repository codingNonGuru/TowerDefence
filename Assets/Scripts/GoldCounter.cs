using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
	public class GoldCounter : Element 
	{
		[SerializeField]
		Text label = null;

		protected override void OnSetup()
		{
			GameManager.OnTowerAdded += HandleGoldAmountChanged;

			GameManager.OnGameRestarted += HandleGoldAmountChanged;

			GameManager.OnCreepKilled += HandleGoldAmountChanged;

			GameManager.OnWaveEnded += HandleGoldAmountChanged;

			HandleGoldAmountChanged();
		}

		void HandleGoldAmountChanged()
		{
			if(label != null)
			{
				label.text = string.Format("Gold: {0}", GameManager.GoldCount);
			}
		}
	}
}