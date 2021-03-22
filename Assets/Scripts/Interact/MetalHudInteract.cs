using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalHudInteract : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        WorkerManager.Instance.MetalHutBuild();
    }

    // Update is called once per frame
    void Destroy()
    {
        WorkerManager.Instance.MetalHutDestroyed();
    }
}
