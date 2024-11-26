using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCard : ICard
{
    public GameplayManager GameplayManager { get; set; }

    public string Name { get => $"Move {Value}"; }
    public int Value { get; }
    public ResourceType Resource { get; }
    public bool CanBeUsed
    {
        get
        {
            // TODO: implement
            // Check if Player is on (its own) City Field
            //return GameplayManager.CurrentPlayer.CurrentField == FieldType.City;
            // or
            // if all Resource Cards are taken in FieldAction phase
            //return false;
            return true;
        }
    }


    public void Use()
    {
        // TODO: implement
        //GameplayManager.CurrentPlayer.Cards.Remove(this);

        // somehow update UI

        //GameplayManager.CurrentPlayer.City.AddResource(Resource, Value);

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

    public ResourceCard(GameplayManager gameplayManager, ResourceType resource, int value)
    {
        GameplayManager = gameplayManager;
        Resource = resource;
        Value = value;
    }


    public enum ResourceType
    {
        Wood,
        Stone,
        Food
    }
}
