using UnityEngine;
using System.Collections;

public class GUICursor : MonoBehaviour {

    CursorLockMode wantedMode;

    void SetCursorState()
    {
        Cursor.lockState = wantedMode;
        Cursor.visible = (CursorLockMode.Locked != wantedMode);
    }

    void onGUI()
    {

    }
}
