using UnityEngine;

public class LightHandler : MonoBehaviour
{
    #region Variables
    [SerializeField] private NodeToggleHandler nodeConnection;
    [SerializeField] private ButtonToggleHandler buttonConnection;
    [SerializeField] private LogicNodeHandler logicConnection;
    [SerializeField] private GameObject[] lightModes;
    #endregion
    #region Setup
    void Start()
    {
        lightModes[0].SetActive(true);
        lightModes[1].SetActive(false);
    }
    #endregion
    #region Handle Light
    public void SignalLight(bool _isReceiving)
    {
        if (_isReceiving) TurnOnLight();
        else TurnOffLight();
    }
    private void TurnOnLight()
    {
        lightModes[0].SetActive(false);
        lightModes[1].SetActive(true);
    }

    private void TurnOffLight()
    { 
        lightModes[0].SetActive(true);
        lightModes[1].SetActive(false);
    }
    #endregion
}
