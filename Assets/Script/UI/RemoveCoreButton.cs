using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveCoreButton : MonoBehaviour
{
    [SerializeField]
    private ProcessorType processor_type_;

    public void removeCore()
    {
        if (processor_type_ == ProcessorType.PERFOR) SetUpManager.instance.discreasePCore();
        else  SetUpManager.instance.discreaseECore();

        GameObject.Destroy(gameObject);
    }
}
