using UnityEngine;
using UnityEngine.Networking;

public class BigNetworkExperiment : NetworkBehaviour {

    public DeviceSpecializer deviceSpecializer;

    public DragController dragController;

    [Command]
    void CmdBeginDrag() {
        Debug.Log("yay");
    }

    [Command]
    void CmdEndDrag() {
        Debug.Log("bye bye");
    }

    [Command]
    void CmdOnDrag(Vector2 pos) {
        Debug.Log("wee " + pos);
    }

    void Start() {
        // Only call behaviour from player local to our device (each device has its own player)
        if (!isLocalPlayer) {
            return;
        }

        if (deviceSpecializer.Type == DeviceSpecializer.DeviceType.Mobile) {
            MobileStart();
        } else if (deviceSpecializer.Type == DeviceSpecializer.DeviceType.Desktop) {
            DesktopStart();
        } else {
            Debug.LogError("What device is this?");
        }
    }

    void Update() {
        // Only call behaviour from player local to our device (each device has its own player)
        if (!isLocalPlayer) {
            return;
        }

        if (deviceSpecializer.Type == DeviceSpecializer.DeviceType.Mobile) {
            MobileUpdate();
        } else if (deviceSpecializer.Type == DeviceSpecializer.DeviceType.Desktop) {
            DesktopUpdate();
        } else {
            Debug.LogError("What device is this?");
        }
    }

    void MobileStart() {
        dragController.OnPressed += CmdBeginDrag;
        dragController.OnReleased += CmdEndDrag;
        dragController.OnDragged += CmdOnDrag;
    }

    void MobileUpdate() {

    }


    void DesktopStart() {

    }

    void DesktopUpdate() {

    }

}
