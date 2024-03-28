using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Occupant
{
    None,
    Player,
    AI
}

public class GridObject
{
    private GridSystem gridSystem;
    private GridPosition gridPosition;

    // Occupied Status
    private bool occupied = false;
    private Occupant occupant = Occupant.None;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
    }

    public override string ToString()
    {
        return gridPosition.ToString();
    }

    public void SetOccupiedStatus(bool status)
    {
        occupied = status;
    }

    public bool GetOccupiedStatus()
    {
        return occupied;
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

    public void SetOccupant(Occupant occupant)
    {
        this.occupant = occupant;
    }

    public Occupant GetOccupant()
    {
        return occupant;
    }

}
