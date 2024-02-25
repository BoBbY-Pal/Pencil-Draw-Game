using System;
using System.Collections.Generic;
using Enums;
using Frolicode;
using UnityEngine;
using UnityEngine.Serialization;


namespace ManagersAndControllers
{
    public class GameManager : Singleton<GameManager>
    {
        private int currentLevelIndex = 1;
        [NonSerialized] public Level currentLevel;
        private LevelSettings levelSettings;
        
        [SerializeField] public List<DirectionSlot> directionSlots = new List<DirectionSlot>();
        [SerializeField] public Queue<Direction> directions = new Queue<Direction>();
        public PencilController pencilController;
        public void Awake()
        {
            levelSettings = (LevelSettings) Resources.Load("LevelSettings");
            currentLevel = levelSettings.Levels[currentLevelIndex-1];
        }

        public void CheckPattern()
        {
            Debug.Log("Go button pressed");
            foreach (var slot in directionSlots)
            {
                directions.Enqueue(slot.direction);
            }
            pencilController.MovePencil(directions);
        }
    }
}