using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
	public class TowerManager : MonoBehaviour 
	{
		static TowerManager instance = null;

		[SerializeField]
		GameObject shellPrefab = null;

		int maximumShellCount = 128;

		List <Shell> shells = null;

		List <int> freeShellIndices = null;

		int lastFreeShellIndex = 0;

		void Awake()
		{
			if(instance == null)
				instance = this;
		}
		
		void Start()
		{
			InstantiateShells();
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
	}
}