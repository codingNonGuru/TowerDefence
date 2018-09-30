using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
	public class HealthCounter : Element 
	{
		[SerializeField]
		Text label = null;

		protected override void OnSetup()
		{
			GameManager.OnCreepEscaped += HandleHealthChanged;

			HandleHealthChanged();
		}

		void HandleHealthChanged()
		{
			if(label != null)
			{
				label.text = string.Format("HP: {0}", GameManager.HitpointCount);
			}
		}
	}
}