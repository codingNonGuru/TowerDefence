using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
	public class Creep : MonoBehaviour 
	{
		CreepData data;

		int currentHitpoints = 0;

		public CreepData Data
		{
			set {data = value;}
		}
		
		void Update () 
		{
			
		}

		public void Setup()
		{
			currentHitpoints = data.MaximumHitpoints;
		}
	}
}