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

        //GameplayManager.CurrentPlayer.Move(Value);

        //GameplayManager.EndTurn(); ?
    }

    public void Take()
    {
        // TODO: implement
        //GameplayManager.DeckCards.Remove(this);
        //GameplayManager.CurrentPlayer.Cards.Add(this);

        // somehow update UI
    }

    public void Drop()
    {
        // TODO: implement
        //GameplayManager.CurrentPlayer.Remove(this);

        // somehow update UI
    }


    public MoveCard(int value)
    {
        Value = value;
    }

}
