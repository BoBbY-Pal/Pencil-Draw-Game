using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Frolicode;
using Slot;
using UnityEngine;
using UnityEngine.Serialization;


namespace ManagersAndControllers
{
    public class GameManager : Singleton<GameManager>
    {
        public int currentLevelIndex = 1;
        [NonSerialized] public Level currentLevel;
        private LevelSettings levelSettings;
        
        [SerializeField] public List<DirectionSlot> directionSlots = new List<DirectionSlot>();
        [SerializeField] public List<LoopingSlot> loopingSlots = new List<LoopingSlot>();
        [SerializeField] public List<Direction> playerDirectionsInput = new List<Direction>();
        public PencilController pencilController;

        [SerializeField] private GameObject droppableArea; 
            
        public void Awake()
        {
            levelSettings = (LevelSettings) Resources.Load("LevelSettings");
            currentLevel = levelSettings.Levels[currentLevelIndex-1];
        }

        public void DrawPattern()
        {
            Debug.Log("Go button pressed");
           
            playerDirectionsInput.AddRange(directionSlots.Where(slot => slot.direction != Direction.NONE).Select(slot => slot.direction ));
            
            pencilController.MovePencil(playerDirectionsInput);
        }

        public void DrawLoopingPattern()
        {
            for (int i = 0; i < droppableArea.transform.childCount; i++)
            {
                loopingSlots.Add(droppableArea.transform.GetChild(i).GetComponent<LoopingSlot>());
            }
            
            foreach (var loopingSlot in loopingSlots)
            {
                for (int i = 0; i < loopingSlot.loopCount; i++)
                {
                    playerDirectionsInput.Add(loopingSlot.direction);
                }
            }
            
            pencilController.MovePencil(playerDirectionsInput);
        }
        
        public void CheckPatternMatch()
        {
            for (int i = 0; i < playerDirectionsInput.Count; i++)
            {
                // Compare elements at the current index
                if (playerDirectionsInput[i] != currentLevel.patternPath[i])
                {
                    // Found a mismatch, stop execution
                    Debug.Log($"Mismatch found at index {i}. Pattern does not match.");
                    UiManager.Instance.LevelFailed();
                    return; // Exit the method
                }
            }

            // If we get here, all compared elements match. Now check if there's more elements left in any list.
            if (playerDirectionsInput.Count == currentLevel.patternPath.Count)
            {
                Debug.Log("Patterns match exactly.");
                UiManager.Instance.LevelPassed();
            }
            else
            {
                Debug.Log("Partial match: Player's pattern matches the beginning of the level's pattern.");
                UiManager.Instance.LevelFailed();
            }
        }

    }
}