using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : Field
{
    public string Name;
    public Pawn Owner;
    public Dictionary<ResourceCard.ResourceType, int> CurrentResources = new Dictionary<ResourceCard.ResourceType, int>
    {
        { ResourceCard.ResourceType.Wood, 0 },
        { ResourceCard.ResourceType.Stone, 0 },
        { ResourceCard.ResourceType.Food, 0 },
    };
    public Dictionary<ResourceCard.ResourceType, int> NeededResources = new Dictionary<ResourceCard.ResourceType, int>
    {
        { ResourceCard.ResourceType.Wood, 10 },
        { ResourceCard.ResourceType.Stone, 10 },
        { ResourceCard.ResourceType.Food, 10 },
    };

}
