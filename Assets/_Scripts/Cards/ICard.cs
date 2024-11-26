using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICard
{
    public GameplayManager GameplayManager { get; set; }

    string Name { get; }

    bool CanBeUsed { get; }

    void Use();

    void Take();

    void Drop();

}
