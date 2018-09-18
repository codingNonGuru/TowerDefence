using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
	public class TowerItem : MonoBehaviour 
	{
		[SerializeField]
		Text costLabel = null;

		[SerializeField]
		Image icon = null;

		TowerClass towerClass = null;

		public TowerClass TowerClass
		{
			set {towerClass = value;}
		}
		
		void Start()
		{
			if(costLabel != null)
			{
				costLabel.text = towerClass.Cost.ToString();
			}

			if(icon != null)
			{
				icon.color = towerClass.IconColor;
			}
		}
	}
}