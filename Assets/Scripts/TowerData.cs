using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
	public enum TowerType
	{
		MACHINE_GUN,
		ICE_BLASTER
	}

	[Serializable]
	public class TowerClass
	{
		public TowerType Type;

		public int Damage;

		public int AttackRate;

		public float AttackRange;

		public bool HasSlow;

		public int Cost;

		public Color IconColor;
	}
}
