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

		[SerializeField]
		GameObject towerPrefab = null;

		bool isAddingTower = false;

		int goldCount = 0;

		public static bool IsAddingTower
		{
			get {return instance.isAddingTower;}
		}

		public static int GoldCount
		{
			get {return instance.goldCount;}
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

		void Update()
		{
			if(Input.GetMouseButtonDown(0) && TileManager.SelectedTile != null)
			{
				AddTower();
			}
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

		void AddTower()
		{
			if(!instance.isAddingTower)
				return;

			if(TileManager.SelectedTile.Tower != null)
				return;

			if(towerPrefab == null)
				return;

			var towerObject = Instantiate(towerPrefab);
			if(towerObject == null)
				return;

			var tower = towerObject.GetComponent<Tower>();
			if(tower == null)
				return;

			TileManager.SelectedTile.AddTower(tower);

			if(OnTowerAdded != null)
			{
				OnTowerAdded.Invoke();
			}
		}
	}
}