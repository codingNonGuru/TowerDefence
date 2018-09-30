﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
	public class GameManager : MonoBehaviour 
	{
		static GameManager instance = null;

		public static event Action OnTowerBuildModeEntered;
		public static event Action OnTowerBuildModeExited;
		public static event Action OnTowerAdded;
		public static event Action OnGameRestarted;
		public static event Action OnCreepKilled;
		public static event Action OnCreepEscaped;
		public static event Action OnWaveEnded;

		[SerializeField]
		List <TowerClass> towerClasses = null;

		[SerializeField]
		GameObject towerPrefab = null;

		[SerializeField]
		int initialGoldCount = 0;

		[SerializeField]
		int initialHitpointCount = 0;

		bool isAddingTower = false;

		int goldCount = 0;

		int hitpointCount = 0;

		TowerClass selectedTowerClass = null;

		public static bool IsAddingTower
		{
			get {return instance.isAddingTower;}
		}

		public static int GoldCount
		{
			get {return instance.goldCount;}
		}

		public static int HitpointCount
		{
			get {return instance.hitpointCount;}
		}

		public static bool IsAlive
		{
			get {return instance.hitpointCount > 0;}
		}

		public static List <TowerClass> TowerClasses
		{
			get {return instance.towerClasses;}
		}

		public static TowerClass SelectedTowerClass
		{
			get {return instance.selectedTowerClass;}
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

			hitpointCount = initialHitpointCount;
		}

		void Start()
		{
			CreepManager.OnCreepDespawned += HandleCreepDespawned;

			CreepManager.OnWaveEnded += HandleWaveEnded;
		}

		void Update()
		{
			if(Input.GetMouseButtonDown(0) && TileManager.SelectedTile != null)
			{
				AddTower();
			}
		}

		public static void Restart()
		{
			instance.goldCount = instance.initialGoldCount;

			instance.hitpointCount = instance.initialHitpointCount;

			if(OnGameRestarted != null)
			{
				OnGameRestarted.Invoke();
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

		public static void ExitTowerBuildMode()
		{
			if(!instance.isAddingTower)
				return;

			instance.isAddingTower = false;

			if(OnTowerBuildModeExited != null)
			{
				OnTowerBuildModeExited.Invoke();
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

			if(OnTowerAdded != null)
			{
				OnTowerAdded.Invoke();
			}
		}

		void HandleCreepDespawned(Creep creep)
		{
			if(creep == null || creep.Data == null)
				return;

			if(creep.IsDead)
			{
				goldCount += creep.Data.GoldBounty;

				if(OnCreepKilled != null)
				{
					OnCreepKilled.Invoke();
				}
			}
			else
			{
				hitpointCount--;

				if(OnCreepEscaped != null)
				{
					OnCreepEscaped.Invoke();
				}
			}
		}

		void HandleWaveEnded()
		{
			goldCount += CreepManager.CurrentWave.GoldBonus;

			if(OnWaveEnded != null)
			{
				OnWaveEnded.Invoke();
			}
		}
	}
}