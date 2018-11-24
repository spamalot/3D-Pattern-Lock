using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BigNetworkExperiment : NetworkBehaviour {

    private PlayerTemplateData data;
    private TechniqueServerController serverController;
    private TechniqueClientController clientController;

    [Command]
    void CmdBeginDrag() {
        serverController.OnBeginDrag();
        LoggingClass.AppendToLog("DRAG BEGIN: ", "Drag begin!");
    }

    [Command]
    void CmdEndDrag() {
        serverController.OnEndDrag();
        LoggingClass.AppendToLog("DRAG END: ", "Drag end!");
    }

    [Command]
    void CmdOnDrag(Vector2 pos) {
        serverController.OnDrag(pos);
        LoggingClass.AppendToLog("DRAG: ", pos.ToString());
    }

    [Command]
    void CmdOnButtonPress(string text) {
        if (text == ExperimentServerController.START) {
            LoggingClass.AppendToLog("CLIENT START", "");
            data.experimentServerController.ClientStart();
            return;
        }
        if (text == ExperimentServerController.CONTINUE) {
            LoggingClass.AppendToLog("CLIENT CONTINUE", "");
            data.experimentServerController.ClientContinue();
            return;
        }
        serverController.OnButtonPress(text);
        // TODO: remove commented line once experiment server controller knows
        // when EnteredNumbers changes
        //data.experimentServerController.DigitEntered(text);
        LoggingClass.AppendToLog("BUTTON PRESSED: ", text);
    }

    void XBeginDrag() {
        if (!isLocalPlayer) {
            return;
        }
        CmdBeginDrag();
    }

    void XEndDrag() {
        if (!isLocalPlayer) {
            return;
        }
        CmdEndDrag();
    }

    void XOnDrag(Vector2 pos) {
        if (!isLocalPlayer) {
            return;
        }
        CmdOnDrag(pos);
    }

    void XOnButtonPress(string text) {
        if (!isLocalPlayer) {
            return;
        }
        CmdOnButtonPress(text);
    }

    void XEnteredNumbersChanged(int[] numbers) {
        RpcEnteredNumbersChanged(numbers);
    }

    [ClientRpc]
    void RpcEnteredNumbersChanged(int[] numbers) {
        clientController.EnteredNumbers = new List<int>(numbers);
    }


    void XCursorPositionChanged(Vector2 position) {
        RpcCursorPositionChanged(position);
    }

    [ClientRpc]
    void RpcCursorPositionChanged(Vector2 position) {
        clientController.CursorPosition = position;
    }


    void XCursorDepthChanged(TechniqueServerController.Depth depth) {
        RpcCursorDepthChanged(depth);
    }

    [ClientRpc]
    void RpcCursorDepthChanged(TechniqueServerController.Depth depth) {
        clientController.CursorDepth = depth;
    }


    [ClientRpc]
    void RpcSendMobile() {
        data.deviceSpecializer.MobileClick();
        PrepControllers();
    }

    [ClientRpc]
    void RpcSetTechnique(ExperimentController.Technique technique) {
        data.experimentClientController.ApplyClientTechnique(technique);
        PrepControllers();
    }

    void XSetTechnique(ExperimentController.Technique technique) {
        data.deviceSpecializer.DesktopClick();
        data.experimentServerController.ApplyServerTechnique(technique);
        PrepControllers();

        RpcSendMobile();
        RpcSetTechnique(technique);
    }

    [Command]
    void CmdSetClientReady() {
        data.experimentServerController.ClientReady();
    }

    void XSetClientReady() {
        CmdSetClientReady();
    }


    [ClientRpc]
    void RpcSetClientMode(TechniqueClientController.ModeType modeType) {
        data.experimentClientController.techniqueClientPointer.Controller.ChangeMode(modeType);
    } 

    void XSetClientMode(TechniqueClientController.ModeType modeType) {
        RpcSetClientMode(modeType);
    }


    [ClientRpc]
    void RpcSetClientFeedbackEnabled(bool enabled_) {
        data.experimentClientController.techniqueClientPointer.Controller.ChangeFeedbackEnabled(enabled_);
    }

    void XSetClientFeedbackEnabled(bool enabled_) {
        RpcSetClientFeedbackEnabled(enabled_);
    }

    void PrepControllers() {
        if (isLocalPlayer) {
            clientController = GameObject.Find("TechniqueClientPointer").GetComponent<TechniqueClientPointer>().Controller;
        } else {
            serverController = GameObject.Find("TechniqueServerPointer").GetComponent<TechniqueServerPointer>().Controller;
            serverController.OnEnteredNumbersChanged += XEnteredNumbersChanged;
            serverController.OnCursorPositionChanged += XCursorPositionChanged;
            serverController.OnCursorDepthChanged += XCursorDepthChanged;
        }
    }

    void Start() {
        data = GameObject.Find("PlayerTemplateData").GetComponent<PlayerTemplateData>();
        data.dragController.OnPressed += XBeginDrag;
        data.dragController.OnReleased += XEndDrag;
        data.dragController.OnDragged += XOnDrag;
        data.buttonController.OnButtonPress += XOnButtonPress;
        data.experimentServerController.OnTechniqueChanged += XSetTechnique;
        data.experimentServerController.OnClientModeChanged += XSetClientMode;
        data.experimentServerController.OnClientFeedbackEnabledChanged += XSetClientFeedbackEnabled;
        data.experimentClientController.OnClientReady += XSetClientReady;
    }


}
