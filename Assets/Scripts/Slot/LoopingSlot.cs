using System;
using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Slot
{
    public class LoopingSlot : MonoBehaviour
    {
        
        // Direction is an enum we've defined which will be used to identify the direction which this slot represents. 
        [SerializeField] public Direction direction;
        public Draggable draggable;

        public int loopCount = 1;
        public Button loopCountBtn;
        public TextMeshProUGUI loopCountTxt;
        public Image directionSlot;
        
        private void Awake()
        {
            loopCountBtn.onClick.AddListener(IncreaseLoopCount);
        }

        private void IncreaseLoopCount()
        {
            loopCount++;
            loopCountTxt.SetText(loopCount.ToString());
        }
    }
}