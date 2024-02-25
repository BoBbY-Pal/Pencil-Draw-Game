using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;

    [CreateAssetMenu(fileName = "LevelSettings", menuName = "ScriptableObject/LevelSettings")]
    public class LevelSettings : ScriptableObject
    {
        public List<Level> Levels = new List<Level>();
    }

    [Serializable]
    public class Level
    {
        public int gridWidth;
        public int gridHeight;
        
        public float nodeSize = 50;
        public float nodeSpacing = 50;
        
        public GameObject dotPrefab;
        public Vector2 firstNodeIndex;
        public List<Direction> patternPath = new List<Direction>();

    }
