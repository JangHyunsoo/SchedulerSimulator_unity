using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveCoreButton : MonoBehaviour
{
    [SerializeField]
    private ProcessorType processor_type_;

    public void removeCore()
    {
        if (processor_type_ == ProcessorType.PERFOR) SetUpManager.instance.reducePCore();
        else  SetUpManager.instance.reduceECore();

        GameObject.Destroy(gameObject);
    }
}
