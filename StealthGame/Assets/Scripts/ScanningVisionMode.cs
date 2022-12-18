using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ScanningVisionMode : MonoBehaviour
{
    [SerializeField] private UniversalAdditionalCameraData additionalCameraData = null;
    [SerializeField] private GameObject vision;

    void Start()
    {
        SetIsActive(false);
    }

    void Update()
    {
        Key();
    }

    void Key()
    {


        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            SetIsActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            SetIsActive(false);
        }
    }

    private void SetIsActive(bool isActive)
    {
        if (isActive)
        {
            additionalCameraData.SetRenderer(1);
            vision.SetActive(true);
        }
        else
        {
            additionalCameraData.SetRenderer(0);
            vision.SetActive(false);
        }
    }
}