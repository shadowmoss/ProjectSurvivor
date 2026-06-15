using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSurvior{
    [CreateAssetMenu]
    public class LevelConfig : ScriptableObject
    {
        // Start is called before the first frame update
        [SerializeField]
        public List<EnemyWaveGroup> EnemyWaveGroups = new List<EnemyWaveGroup>();
    }
    [Serializable]
    public class EnemyWaveGroup
    {
        public string Name;
        [TextArea]public string Description = string.Empty;
        [SerializeField]
        public List<EnemyWave> Waves = new List<EnemyWave>();
    }
    [Serializable]
	public class EnemyWave
	{
        public string Name;
        public bool Active = true;
		public float GenerateDuration = 1;
		public GameObject EnemyPrefab;
		public int seconds = 10;
        public float HPScale = 1.0f;
        public float speedScale = 1.0f;
	}
}
