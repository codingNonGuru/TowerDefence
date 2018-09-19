using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
	public class Tower : MonoBehaviour 
	{
		TowerClass towerClass = null;

		public TowerClass TowerClass
		{
			set {towerClass = value;}
		}

		void Start () 
		{
			
		}
	}
}