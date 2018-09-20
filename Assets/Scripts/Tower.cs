using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
	public class Tower : MonoBehaviour 
	{
		SpriteRenderer spriteRenderer = null;

		TowerClass towerClass = null;

		public TowerClass TowerClass
		{
			set {towerClass = value;}
		}

		void Start () 
		{
			spriteRenderer = GetComponent<SpriteRenderer>();

			if(spriteRenderer != null)
			{
				spriteRenderer.color = towerClass.IconColor;
			}
		}
	}
}