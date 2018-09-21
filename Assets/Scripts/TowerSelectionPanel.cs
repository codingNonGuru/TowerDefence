using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{	
	public class TowerSelectionPanel : Element 
	{
		[SerializeField]
		GameObject itemPrefab = null;

		[SerializeField]
		Transform itemList = null;

		[SerializeField]
		Transform itemSelector = null;

		int selectionIndex = 0;

		List <TowerItem> items = null;

		void Update()
		{
			UpdateSelection();
		}

		protected override void OnSetup()
		{
			CreateItems();

			GameManager.OnTowerBuildModeEntered += HandleBuildModeEntered;

			GameManager.OnTowerBuildModeExited += HandleBuildModeExited;

			UpdateSelection();
		}

		void CreateItems()
		{
			if(itemPrefab == null)
				return;

			var towerClasses = GameManager.TowerClasses;
			if(towerClasses == null)
				return;

			items = new List <TowerItem> (towerClasses.Count);

			foreach(var towerClass in towerClasses)
			{
				var itemObject = Instantiate(itemPrefab);
				if(itemObject == null)
					continue;

				itemObject.transform.SetParent(itemList);

				var item = itemObject.GetComponent<TowerItem>();
				if(item == null)
					continue;

				item.TowerClass = towerClass;

				items.Add(item);
			}
		}

		void HandleBuildModeEntered()
		{
			gameObject.SetActive(true);
		}

		void HandleBuildModeExited()
		{
			gameObject.SetActive(false);
		}

		void UpdateSelection()
		{
			var towerClasses = GameManager.TowerClasses;
			if(towerClasses == null)
				return;

			if(Input.GetKeyDown(KeyCode.LeftArrow))
			{
				selectionIndex--;
				if(selectionIndex < 0)
				{
					selectionIndex = towerClasses.Count - 1;
				}
			}
			else if(Input.GetKeyDown(KeyCode.RightArrow))
			{
				selectionIndex++;
				if(selectionIndex >= towerClasses.Count)
				{
					selectionIndex = 0;
				}
			}

			GameManager.SelectedTowerClass = towerClasses[selectionIndex];

			if(items == null)
				return;

			var selectedItem = items[selectionIndex];

			if(itemSelector != null)
			{
				itemSelector.SetParent(selectedItem.transform);
				itemSelector.localPosition = Vector3.zero;
			}
		}
	}
}
