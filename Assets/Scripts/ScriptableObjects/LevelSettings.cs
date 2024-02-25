using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;

    [CreateAssetMenu(fileName = "LevelSettings", menuName = "ScriptableObject/LevelSettings")]
    public class LevelSettings : ScriptableObject
    {
        public List<Level> loopingLevels = new List<Level>();
        public List<Level> sequenceLevels = new List<Level>();
    }

    [Serializable]
    public class Level
    {
        public int gridWidth;
        public int gridHeight;
        
        public float nodeSpacing = 50;
        
        public GameObject dotPrefab;
        public Vector2 firstNodeIndex;
        public List<Direction> patternPath = new List<Direction>();

    }
