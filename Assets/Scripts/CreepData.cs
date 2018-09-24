using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public enum CreepType
    {
        GOBLIN,
        ORC
    }

    [Serializable]
	public class CreepData 
	{
        public CreepType Type;

        public int MaximumHitpoints;

        public float MoveSpeed;

        public float Size;
    }
}