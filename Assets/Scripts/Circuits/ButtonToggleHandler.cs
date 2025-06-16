using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonToggleHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    #region Variables
    [SerializeField] private GameObject lightOn;
    [SerializeField] private GameObject lightOff;
    [SerializeField] private GameObject connectedNode;
    [SerializeField] private GameObject connectedLogic;
    [SerializeField] private LightHandler connectedLight;
    
    private bool isOn = false, nodeConnection, logicConnection, lightConnection;
    private float delayTime = .4f, _delayTimer;
    #endregion
    #region Setup and Maintainence

    private void Start()
    {
        lightOn.SetActive(false);
        lightOff.SetActive(true);
        isOn = false;
        if (connectedNode != null)
        {
            nodeConnection = true;
            logicConnection = false;
            lightConnection = false;
            return;
        }
        else if (connectedLogic != null)
        {
            nodeConnection = false;
            logicConnection = true;
            lightConnection = false;
            return;
        }
        else if (connectedLight != null)
        {
            nodeConnection = false;
            logicConnection = false;
            lightConnection = true;
            return;
        }
        else Debug.LogError($"{this} has connection errors");


    }

    void Update()
    {
        UpdateTimers();
    }

    private void UpdateTimers()
    {
        _delayTimer -= Time.deltaTime;
    }
    #endregion
    #region Pointer Functions

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isOn && _delayTimer <= 0)
        {
            lightOn.SetActive(false);
            lightOff.SetActive(true);
            isOn = false;
            if (nodeConnection) connectedNode.GetComponent<NodeToggleHandler>().DeactivateToggle();
            if (logicConnection) connectedLogic.GetComponent<LogicNodeHandler>().StopSignal(this);
            if (lightConnection) connectedLogic.GetComponent<LightHandler>().SignalLight(false);
            
            _delayTimer = delayTime;
        }

        if (!isOn && _delayTimer <= 0)
        {
            lightOn.SetActive(true);
            lightOff.SetActive(false);
            isOn = true;
            if (nodeConnection) connectedNode.GetComponent<NodeToggleHandler>().ActivateToggle();
            if (logicConnection) connectedLogic.GetComponent<LogicNodeHandler>().SendSignal(this);
            if (lightConnection) connectedLogic.GetComponent<LightHandler>().SignalLight(true);
            _delayTimer = delayTime;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
    #endregion
}
