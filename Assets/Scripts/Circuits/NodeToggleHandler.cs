using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class NodeToggleHandler : MonoBehaviour
{
    #region Variables
    [Header("SPRITES")]
    [SerializeField] private Sprite onSprite_H;
    [SerializeField] private Sprite onSprite_V;
    [SerializeField] private Sprite offSprite_H;
    [SerializeField] private Sprite offSprite_V;

    [Header("CONNECTIONS")]

    [SerializeField] private bool topConnection;
    [SerializeField] private bool rightConnection;
    [SerializeField] private bool bottomConnection;
    [SerializeField] private bool leftConnection;
    [SerializeField] private List<NodeToggleHandler> connectedNodes;
    [SerializeField] private List<LogicNodeHandler> connectedLogicNodes;
    [SerializeField] private LightHandler connectedLight;

    [Header("LIGHTS")]
    [SerializeField] private GameObject topLight;
    [SerializeField] private GameObject rightLight;
    [SerializeField] private GameObject bottomLight;
    [SerializeField] private GameObject leftLight;

    [Header("TEST ONLY")]
    public bool isConnectedTop;
    public bool isConnectedRight;
    public bool isConnectedBottom;
    public bool isConnectedLeft;

    private bool activateToggle = false;
    private bool hasConnections;

    #endregion

    #region Setup and Maintainence
    void Start()
    {
        if (connectedNodes.Count > 0) hasConnections = true;
        if (topConnection)
        {
            topLight.SetActive(true);
            topLight.GetComponent<SpriteRenderer>().sprite = offSprite_H;
        }
        if (rightConnection)
        {
            rightLight.SetActive(true);
            rightLight.GetComponent<SpriteRenderer>().sprite = offSprite_V;
        }
        if (bottomConnection)
        {
            bottomLight.SetActive(true);
            bottomLight.GetComponent<SpriteRenderer>().sprite = offSprite_H;
        }
        if (leftConnection)
        {
            leftLight.SetActive(true);
            leftLight.GetComponent<SpriteRenderer>().sprite = offSprite_V;
        }
        isConnectedTop = false;
        isConnectedRight = false;
        isConnectedBottom = false;
        isConnectedLeft = false;
    }
    #endregion

    #region Node Toggle
    public void ActivateToggle() { activateToggle = true; HandleToggles(); }
    public void DeactivateToggle() { activateToggle = false; HandleToggles(); }

    private void HandleToggles()
    {
        #region True Toggle
        if (activateToggle)
        {
            if (topConnection)
                topLight.GetComponent<SpriteRenderer>().sprite = onSprite_H;
            if (rightConnection)
                rightLight.GetComponent<SpriteRenderer>().sprite = onSprite_V;
            if (bottomConnection)
                bottomLight.GetComponent<SpriteRenderer>().sprite = onSprite_H;
            if (leftConnection)
                leftLight.GetComponent<SpriteRenderer>().sprite = onSprite_V;
            if (hasConnections)
            {
                foreach (NodeToggleHandler handler in connectedNodes)
                    handler.ActivateToggle();

                foreach (LogicNodeHandler handler in connectedLogicNodes)
                    handler.SendSignal(this);
            }
        }
        #endregion
        #region False Toggle
        if (!activateToggle)
        {
            if (topConnection)
                topLight.GetComponent<SpriteRenderer>().sprite = offSprite_H;
            if (rightConnection)
                rightLight.GetComponent<SpriteRenderer>().sprite = offSprite_V;
            if (bottomConnection)
                bottomLight.GetComponent<SpriteRenderer>().sprite = offSprite_H;
            if (leftConnection)
                leftLight.GetComponent<SpriteRenderer>().sprite = offSprite_V;
            if (hasConnections)
            {
                foreach (NodeToggleHandler handler in connectedNodes)
                    handler.DeactivateToggle();
                foreach (LogicNodeHandler handler in connectedLogicNodes)
                    handler.StopSignal(this);                    
            }

        }
        #endregion
        }
    #endregion
}
