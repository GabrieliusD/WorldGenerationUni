using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodHudInteract : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        WorkerManager.Instance.WoodHutBuild();
    }

    void Destroy()
    {
        WorkerManager.Instance.woodHutDestroyed();
    }
}
