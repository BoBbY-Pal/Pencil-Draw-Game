﻿using Enums;
using UnityEngine;

public class DirectionSlot : MonoBehaviour
{
    // Direction is an enum we've defined which will be used to identify the direction which this slot represents. 
    [SerializeField] public Direction direction;
    public Draggable draggable;
}