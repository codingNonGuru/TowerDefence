using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
	public class GameManager : MonoBehaviour 
	{
		static GameManager instance = null;

		public static event Action OnTowerBuildModeEntered;
		public static event Action OnTowerAdded;

		[SerializeField]
		List <TowerClass> towerClasses = null;

		[SerializeField]
		GameObject towerPrefab = null;

		[SerializeField]
		int initialGoldCount = 0;

		bool isAddingTower = false;

		int goldCount = 0;

		TowerClass selectedTowerClass = null;

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

		public static TowerClass SelectedTowerClass
		{
			set {instance.selectedTowerClass = value;}
		}

		public static bool CanBuildTower
		{
			get 
			{
				foreach(var towerClass in instance.towerClasses)
				{
					if(instance.goldCount >= towerClass.Cost)
						return true;
				}

				return false;
			}
		}

		void Awake()
		{
			if(instance == null)
				instance = this;

			goldCount = initialGoldCount;
		}

		void Update()
		{
			if(Input.GetMouseButtonDown(0) && TileManager.SelectedTile != null)
			{
				AddTower();
			}
		}

		public static void EnterTowerBuildMode()
		{
			if(instance.isAddingTower)
				return;

			instance.isAddingTower = true;

			if(OnTowerBuildModeEntered != null)
			{
				OnTowerBuildModeEntered.Invoke();
			}
		}

		void AddTower()
		{
			if(!isAddingTower)
				return;

			if(TileManager.SelectedTile.Tower != null)
				return;

			if(towerPrefab == null)
				return;

			if(selectedTowerClass == null)
				return;

			if(selectedTowerClass.Cost > goldCount)
				return;

			var towerObject = Instantiate(towerPrefab);
			if(towerObject == null)
				return;

			var tower = towerObject.GetComponent<Tower>();
			if(tower == null)
			{
				Destroy(towerObject);
				return;
			}

			tower.TowerClass = selectedTowerClass;

			TileManager.SelectedTile.AddTower(tower);

			goldCount -= selectedTowerClass.Cost;

			if(!CanBuildTower)
			{
				isAddingTower = false;
			}

			if(OnTowerAdded != null)
			{
				OnTowerAdded.Invoke();
			}
		}
	}
}