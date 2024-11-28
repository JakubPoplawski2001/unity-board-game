using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class CardList
{
    public VisualElement Container;
    public List<VisualElement> ItemsVisuals = new List<VisualElement>();
    public List<ICard> Items;
    Action<VisualElement, ICard> itemSelectedAction;
    Action<bool> maxItemsSelectedAction;
    //Action itemSelectedAction;
    List<ICard> selectedItems = new List<ICard>();
    int maxSelectedItems;
    VisualTreeAsset itemTemplateAsset;


    public CardList(VisualElement container, int maxSelectedItems, VisualTreeAsset cardTemplateAsset)
    {
        Container = container;
        this.maxSelectedItems = maxSelectedItems;
        this.itemTemplateAsset = cardTemplateAsset;
        //this.itemSelectedAction = itemSelectedAction;
    }

    public void UpdateItems(List<ICard> items)
    {
        Container.Clear();
        Items = items;

        for (int i = 0; i < Items.Count; i++)
        {
            var cardVisual = itemTemplateAsset.Instantiate();
            ItemsVisuals.Add(cardVisual);
            cardVisual.Q<Label>(USSElementNames.CARD_NAME).text = Items[i].Name;
            cardVisual.Q(USSElementNames.CARD_SELECTED_FX).style.display = DisplayStyle.None; // Need to be manually set to overcome null value bug
            cardVisual.RegisterCallback<ClickEvent, int>(OnItemClicked, i);
            Container.Add(cardVisual);
        }
    }

    public void AddOnItemClickedAction(Action<VisualElement, ICard> action)
    {
        this.itemSelectedAction = action;
    }

    public void AddOnMaxItemSelectedAction(Action<bool> action)
    {
        maxItemsSelectedAction = action;
    }

    void OnItemClicked(ClickEvent e, int index)
    {
        if (selectedItems.Contains(Items[index]))
        {
            selectedItems.Remove(Items[index]);
        }
        else
        {
            if (selectedItems.Count >= maxSelectedItems) return;
            selectedItems.Add(Items[index]);

        }
        maxItemsSelectedAction?.Invoke(selectedItems.Count == maxSelectedItems);

        itemSelectedAction?.Invoke(ItemsVisuals[index], Items[index]);
    }

    List<ICard> GetSelectedItems() => selectedItems;
}
