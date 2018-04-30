using UnityEngine;

public abstract class ShipState : State
{
    public GameObject ship;

    public ShipState(GameObject ship)
    {
        this.ship = ship;
    }
}