using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BaseUIManager : MonoBehaviour
{

    #region VisualElements
    [SerializeField] UIDocument uiDocument;
    VisualElement root;

    void GetRootElement()
    {
        root = uiDocument.rootVisualElement;
    }

    void SetupVisualElements()
    {
        
    }

    #endregion

    #region Events

    void SubscribeToEvents()
    {

    }

    void UnsubscribeToEvents()
    {

    }

    #endregion

    #region MonoBehaviour
    void Awake()
    {

        SetupVisualElements();

    }

    void Start()
    {
        
    }

    void OnEnable()
    {
        
    }

    void OnDisable()
    {
        
    }

    #endregion

    void ShowElement(VisualElement visualElement)
    {
        visualElement.style.display = DisplayStyle.Flex;
    }

    void HideElement(VisualElement visualElement)
    {
        visualElement.style.display = DisplayStyle.None;
    }
}
