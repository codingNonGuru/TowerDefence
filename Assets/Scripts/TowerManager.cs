using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
	public class TowerManager : MonoBehaviour 
	{
		static TowerManager instance = null;

		public static event Action OnTowerAdded;

		[SerializeField]
		List <TowerClass> towerClasses = null;

		[SerializeField]
		GameObject towerPrefab = null;

		[SerializeField]
		GameObject shellPrefab = null;

		int maximumShellCount = 128;

		List <Shell> shells = null;

		List <int> freeShellIndices = null;

		int lastFreeShellIndex = 0;

		TowerClass selectedTowerClass = null;

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
					if(GameManager.GoldCount >= towerClass.Cost)
						return true;
				}

				return false;
			}
		}

		void Awake()
		{
			if(instance == null)
				instance = this;
		}
		
		void Start()
		{
			InstantiateShells();
		}

		void Update()
		{
			if(Input.GetMouseButtonDown(0) && TileManager.SelectedTile != null)
			{
				AddTower();
			}
		}

		public static Shell CreateShell()
		{	
			if(instance.shells == null)
				return null;

			var index = instance.freeShellIndices[instance.lastFreeShellIndex];
			var shell = instance.shells[index];
			
			instance.lastFreeShellIndex--;

			return shell;
		}

		public static void DestroyShell(Shell shell)
		{	
			if(instance.shells == null)
				return;

			shell.gameObject.SetActive(false);

			var index = instance.shells.IndexOf(shell);

			instance.lastFreeShellIndex++;		
			instance.freeShellIndices[instance.lastFreeShellIndex] = index;
		}

		void InstantiateShells()
		{
			if(shellPrefab == null)
				return;

			lastFreeShellIndex = maximumShellCount - 1;

			shells = new List <Shell> (maximumShellCount);
			freeShellIndices = new List <int> (maximumShellCount);

			for(int i = 0; i < maximumShellCount; ++i)
			{
				var shellObject = Instantiate(shellPrefab);
				if(shellObject == null)
					continue;

				shellObject.transform.SetParent(transform);
				shellObject.SetActive(false);

				var shell = shellObject.GetComponent<Shell>();
				if(shell == null)
					continue;

				shells.Add(shell);

				freeShellIndices.Add(i);
			}
		}

		void AddTower()
		{
			if(!GameManager.IsAddingTower)
				return;

			if(TileManager.SelectedTile.Tower != null)
				return;

			if(towerPrefab == null)
				return;

			if(selectedTowerClass == null)
				return;

			if(selectedTowerClass.Cost > GameManager.GoldCount)
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

			GameManager.AddTower(selectedTowerClass);

			if(OnTowerAdded != null)
			{
				OnTowerAdded.Invoke();
			}
		}
	}
}