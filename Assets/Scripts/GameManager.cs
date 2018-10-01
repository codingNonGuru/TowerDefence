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
		public static event Action OnTowerBuildModeExited;
		public static event Action OnGameRestarted;
		public static event Action OnCreepKilled;
		public static event Action OnCreepEscaped;
		public static event Action OnWaveEnded;

		[SerializeField]
		int initialGoldCount = 0;

		[SerializeField]
		int initialHitpointCount = 0;

		bool isAddingTower = false;

		int goldCount = 0;

		int hitpointCount = 0;

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

		public static void AddTower(TowerClass towerClass)
		{
			instance.goldCount -= towerClass.Cost;
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