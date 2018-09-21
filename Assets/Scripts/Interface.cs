using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
	public class Interface : MonoBehaviour 
	{
		[SerializeField]
		TowerSelectionPanel selectionPanel = null;

		[SerializeField]
		GoldCounter goldCounter = null;

		[SerializeField]
		StartWaveButton startWaveButton = null;

		[SerializeField]
		ReturnButton returnButton = null;
	
		void Start()
		{
			if(selectionPanel != null)
			{
				selectionPanel.Setup();
			}

			if(goldCounter != null)
			{
				goldCounter.Setup();
			}

			if(startWaveButton != null)
			{
				startWaveButton.Setup();
			}

			if(returnButton != null)
			{
				returnButton.Setup();
			}
		}
	}
}
