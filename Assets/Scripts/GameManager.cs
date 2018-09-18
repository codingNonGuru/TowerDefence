using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
	public class GameManager : MonoBehaviour 
	{
		static GameManager instance = null;

		public static event Action OnTowerAddStarted;

		bool isAddingTower = false;

		int gold = 0;

		public static bool IsAddingTower
		{
			get {return instance.isAddingTower;}
		}

		void Start()
		{
			if(instance == null)
				instance = this;
		}

		public static void StartAddingTower()
		{
			if(instance.isAddingTower)
				return;

			if(OnTowerAddStarted != null)
			{
				OnTowerAddStarted.Invoke();
			}
		}
	}
}