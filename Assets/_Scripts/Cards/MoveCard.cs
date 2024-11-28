using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCard : ICard
{
    public string Name { get => $"Move {Value}"; }
    public int Value { get; }
    public bool CanBeUsed { get => true; }


    public void Use()
    {
        // TODO: implement
        //GameplayManager.CurrentPlayer.Cards.Remove(this);

        // somehow update UI

        //GameplayManager.Instance.CurrentPlayer.Move(Value);

        //GameplayManager.EndTurn(); ?
    }


    public MoveCard(int value)
    {
        Value = value;
    }

}
