using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
	public class Element : MonoBehaviour 
	{
		public void Setup()
		{
			OnSetup();
		}
		
		protected virtual void OnSetup() {}
	}
}