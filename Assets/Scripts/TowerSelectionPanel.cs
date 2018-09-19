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

		protected override void OnSetup()
		{
			CreateItems();

			GameManager.OnTowerAddStarted += HandleTowerAddStarted;
		}

		void CreateItems()
		{
			if(itemPrefab == null)
				return;

			var towerClasses = GameManager.TowerClasses;
			if(towerClasses == null)
				return;

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
			}
		}

		void HandleTowerAddStarted()
		{
			gameObject.SetActive(true);
		}
	}
}
