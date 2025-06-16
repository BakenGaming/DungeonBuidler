using System.Collections.Generic;
using UnityEngine;
public enum LogicType
{ AND, OR, NOT, NAND, NOR, XOR, XNOR}
public class LogicNodeHandler : MonoBehaviour
{
    #region Variables
    [Header("SETUP")]
    [SerializeField] private LogicType logicType;
    [SerializeField] private List<NodeToggleHandler> nodeConnections;
    [SerializeField] private List<ButtonToggleHandler> buttonConnections;
    [SerializeField] private LightHandler connectedLight;
    [SerializeField] private GameObject[] connectionDisplay;
    private Dictionary<NodeToggleHandler, bool> nodeDictionary;
    private Dictionary<ButtonToggleHandler, bool> buttonDictionary;
    private List<bool> logicList;
    private int numberOfConnections = 0;
    private List<bool> activeConnections;
    private bool isOutputtingSignal = false;
    private NodeToggleHandler incomingNode;
    private ButtonToggleHandler incomingButton;
    #endregion

    #region Setup
    void Start()
    {
        nodeDictionary = new Dictionary<NodeToggleHandler, bool>();
        buttonDictionary = new Dictionary<ButtonToggleHandler, bool>();

        foreach (NodeToggleHandler _node in nodeConnections)
        {
            if (_node != null)
            {
                numberOfConnections++;
                nodeDictionary.Add(_node, true);
            }
            else nodeDictionary.Add(_node, false);
        }
        foreach (ButtonToggleHandler _button in buttonConnections)
        {
            if (_button != null)
            {
                numberOfConnections++;
                buttonDictionary.Add(_button, true);
            }
            else buttonDictionary.Add(_button, false);

        }

        if (numberOfConnections <= 1 || numberOfConnections > 3) Debug.LogError($"{this} has connection errors, number of connections is {numberOfConnections}");

        if (numberOfConnections == 2)
        {
            connectionDisplay[0].SetActive(true);
            connectionDisplay[1].SetActive(false);
        }
        if (numberOfConnections == 3)
        {
            connectionDisplay[0].SetActive(false);
            connectionDisplay[1].SetActive(true);
        }
        ResetLogicList();
    }
    #endregion
    #region Signal Handling
    public void SendSignal(NodeToggleHandler _incomingNode)
    {
        incomingNode = _incomingNode;
        LogicTest();
    }

    public void SendSignal(ButtonToggleHandler _incomingButton)
    {
        incomingButton = _incomingButton;
        LogicTest();
    }

    public void StopSignal(NodeToggleHandler _incomingNode)
    {
        incomingNode = _incomingNode;
        LogicTest();
    }

    public void StopSignal(ButtonToggleHandler _incomingButton)
    {
        incomingButton = _incomingButton;
        LogicTest();
    }
    #endregion
    #region Logic Test Handling
    private void LogicTest()
    {
        switch (logicType)
        {
            case LogicType.AND:
                isOutputtingSignal = ANDTest();
                break;
            case LogicType.OR:
                isOutputtingSignal = ORTest();
                break;
            case LogicType.NOT:
                isOutputtingSignal = NOTTest();
                break;
            case LogicType.NAND:
                isOutputtingSignal = NANDTest();
                break;
            case LogicType.NOR:
                isOutputtingSignal = NORTest();
                break;
            case LogicType.XOR:
                isOutputtingSignal = XORTest();
                break;
            case LogicType.XNOR:
                isOutputtingSignal = XNORTest();
                break;
        }
        HandleLogicResult();
    }
    
    private void HandleLogicResult()
    {
        if (connectedLight != null)
            connectedLight.GetComponent<LightHandler>().SignalLight(isOutputtingSignal);
    }

    private void ResetLogicList() { logicList = new List<bool>(); }

    private bool ANDTest()
    {
        bool isTrue;

        foreach(NodeToggleHandler _node in nodeDictionary.Keys)
            if (nodeDictionary.TryGetValue(incomingNode, out isTrue) == true) logicList.Add(true);

        foreach (ButtonToggleHandler _button in buttonDictionary.Keys)
            if (buttonDictionary.TryGetValue(incomingButton, out isTrue) == true) logicList.Add(true);

        Debug.Log($"Trues {logicList.Count} = Connections {numberOfConnections}");

        if (logicList.Count == numberOfConnections) { ResetLogicList(); return true; }
        else { ResetLogicList(); return false; }
    }
    private bool ORTest()
    {
        return false;
    }
    private bool NOTTest()
    {
        return false;
    }
    private bool NANDTest()
    {
        return false;
    }
    private bool NORTest()
    {
        return false;
    }
    private bool XORTest()
    {
        return false;
    }
    private bool XNORTest()
    { 
        return false;        
    }

    #endregion
}
