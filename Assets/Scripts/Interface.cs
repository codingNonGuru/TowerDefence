using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
	public class Interface : MonoBehaviour 
	{
		[SerializeField]
		TowerSelectionPanel selectionPanel = null;
	
		void Start()
		{
			if(selectionPanel != null)
			{
				selectionPanel.Setup();
			}
		}
	}
}
