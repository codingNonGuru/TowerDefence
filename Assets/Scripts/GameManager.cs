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
		public static event Action OnTowerAdded;

		[SerializeField]
		List <TowerClass> towerClasses = null;

		bool isAddingTower = false;

		int gold = 0;

		public static bool IsAddingTower
		{
			get {return instance.isAddingTower;}
		}

		public static List <TowerClass> TowerClasses
		{
			get {return instance.towerClasses;}
		}

		void Awake()
		{
			if(instance == null)
				instance = this;
		}

		public static void StartAddingTower()
		{
			if(instance.isAddingTower)
				return;

			instance.isAddingTower = true;

			if(OnTowerAddStarted != null)
			{
				OnTowerAddStarted.Invoke();
			}
		}

		public static void AddTower()
		{
			if(!instance.isAddingTower)
				return;

			instance.isAddingTower = false;

			if(OnTowerAdded != null)
			{
				OnTowerAdded.Invoke();
			}
		}
	}
}