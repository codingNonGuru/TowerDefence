using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
	public class CreepManager : MonoBehaviour 
	{
		static CreepManager instance = null;

		public static event Action OnWaveLaunched;
		public static event Action OnWaveEnded;

		[SerializeField]
		GameObject creepPrefab = null;

		[SerializeField]
		List <CreepData> creepDatas = null;

		[SerializeField]
		List <CreepWave> creepWaves = null;

		const int maximumCreepCount = 32;

		List <Creep> creeps = null;

		int currentWaveIndex = 0;

		CreepWave currentWave = null;

		int currentCreepIndex = 0;

		public static CreepWave CurrentWave
		{
			get {return instance.currentWave;}
		}

		void Awake()
		{
			if(instance == null)
				instance = this;
		}
		
		void Start()
		{
			CreateCreeps();
		}

		public static void LaunchWave()
		{
			if(GameManager.IsAddingTower)
				return;

			instance.currentWave = instance.creepWaves[instance.currentWaveIndex];

			instance.currentWaveIndex++;

			instance.SpawnCreep();

			if(OnWaveLaunched != null)
			{
				OnWaveLaunched.Invoke();
			}
		}

		void CreateCreeps()
		{
			if(creepPrefab == null)
				return;

			creeps = new List <Creep> (maximumCreepCount);

			for(int i = 0; i < maximumCreepCount; ++i)
			{
				var creepObject = Instantiate(creepPrefab);
				if(creepObject == null)
					continue;

				creepObject.transform.SetParent(transform);

				var creep = creepObject.GetComponent<Creep>();
				if(creep == null)
					continue;

				creeps.Add(creep);

				creepObject.SetActive(false);
			}
		}

		void SpawnCreep()
		{
			if(creeps == null)
				return;

			var creep = creeps[currentCreepIndex];
			if(creep == null)
				return;

			creep.Spawn();
		}
	}
}