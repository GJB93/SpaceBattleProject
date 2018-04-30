using UnityEngine;

public abstract class CruiserState : State
{
    public GameObject cruiser;

    public CruiserState(GameObject cruiser)
    {
        this.cruiser = cruiser;
    }
}