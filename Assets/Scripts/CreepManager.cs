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
		public static event Action<Creep> OnCreepDespawned;

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

		int livingCreepCount = 0;

		public static CreepWave CurrentWave
		{
			get {return instance.currentWave;}
		}

		public static bool HasLastWavePassed
		{
			get {return instance.currentWaveIndex == instance.creepWaves.Count;}
		}

		public static List <Creep> Creeps
		{
			get {return instance.creeps;}
		}

		void Awake()
		{
			if(instance == null)
				instance = this;
		}
		
		void Start()
		{
			CreateCreeps();

			GameManager.OnGameRestarted += HandleGameRestart;
		}

		public static void LaunchWave()
		{
			if(GameManager.IsAddingTower)
				return;

			instance.currentCreepIndex = 0;

			instance.currentWave = instance.creepWaves[instance.currentWaveIndex];

			instance.currentWaveIndex++;

			instance.livingCreepCount = instance.currentWave.CreepTypes.Count;
		
			instance.StartCoroutine(instance.LaunchWaveCoroutine());

			if(OnWaveLaunched != null)
			{
				OnWaveLaunched.Invoke();
			}
		}

		public static void DespawnCreep(Creep creep)
		{
			creep.gameObject.SetActive(false);

			instance.livingCreepCount--;

			if(OnCreepDespawned != null)
			{
				OnCreepDespawned.Invoke(creep);
			}

			if(instance.livingCreepCount > 0)
				return;

			if(OnWaveEnded != null)
			{
				OnWaveEnded.Invoke();
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

		IEnumerator LaunchWaveCoroutine()
		{
			for(int i = 0; i < currentWave.CreepTypes.Count; ++i)
			{
				SpawnCreep();

				yield return new WaitForSeconds(1.0f);
			}
		}

		void SpawnCreep()
		{
			if(creeps == null)
				return;

			var creep = creeps[currentCreepIndex];
			if(creep == null)
				return;

			var currentType = currentWave.CreepTypes[currentCreepIndex];
			var currentData = GetCreepData(currentType);

			creep.Spawn(currentData);

			currentCreepIndex++;
		}

		CreepData GetCreepData(CreepType creepType)
		{
			if(creepDatas == null)
				return null;

			foreach(var data in creepDatas)
			{
				if(data.Type == creepType)
					return data;
			}

			return null;
		}

		void HandleGameRestart()
		{
			currentWaveIndex = 0;

			foreach(var creep in creeps)
			{
				creep.gameObject.SetActive(false);
			}
		}
	}
}