using UnityEngine;

public class DeviceSpecializer : MonoBehaviour {

    public GameObject desktopStuff;
    public GameObject mobileStuff;

    public enum DeviceType { Desktop, Mobile };

    public DeviceType Type { get; private set; }

    public void MobileClick() {
        Type = DeviceType.Mobile;
        desktopStuff.SetActive(false);
        GameObject.Find("DeviceSelectCanvas").SetActive(false);
    }

    public void DesktopClick() {
        Type = DeviceType.Desktop;
        mobileStuff.SetActive(false);
        GameObject.Find("DeviceSelectCanvas").SetActive(false);
    }

}
