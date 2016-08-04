using UnityEngine;
using System.Collections;

public class Recycler : MonoBehaviour {

    void OnEnable()
    {
        Invoke("Destroy", 3f);
    }

    void Destroy()
    {
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        CancelInvoke();
    }
}
