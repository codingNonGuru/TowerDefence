using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
	public class CreepManager : MonoBehaviour 
	{
		static CreepManager instance = null;

		[SerializeField]
		GameObject creepPrefab = null;

		[SerializeField]
		List <CreepData> creepDatas = null;

		[SerializeField]
		List <CreepWave> creepWaves = null;

		const int maximumCreepCount = 32;

		List <Creep> creeps = null;

		int currentWaveIndex = 0;

		void Awake()
		{
			if(instance == null)
				instance = this;
		}
		
		void Start()
		{
			CreateCreeps();
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
	}
}