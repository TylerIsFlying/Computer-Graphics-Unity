using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct TextureButtonsObj 
{
    [SerializeField]
    public Button button;
    [SerializeField]
    public string matObjName;
}

public class ChangeMaterial : MonoBehaviour
{
    public Camera camera;
    public string tag;
    public GameObject panel;
    public MaterialApply matApply;
    public List<TextureButtonsObj> textureButtons;

    public GameObject[] disableObjects;
    private GameObject currentObject;

    private void Awake()
    {
        panel.SetActive(false);
        foreach (TextureButtonsObj tb in textureButtons) 
        {
            tb.button.onClick.AddListener(delegate { SetMatObject(tb.matObjName); });
        }
    }

    private void Update()
    {
        // checking for raycasts and if it hits it will display a panel
        if (!(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))) return;
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit) && hit.transform.CompareTag(tag))
        {
            if (!panel.activeSelf) 
            {
                foreach(GameObject o in disableObjects) 
                {
                    o.SetActive(false);
                }
                panel.SetActive(true);
            }
            currentObject = hit.transform.gameObject;
        }
    }
    // setting the material for the object
    void SetMatObject(string matObjectName) 
    {
        Renderer renderer;
        if(currentObject.TryGetComponent<Renderer>(out renderer)) 
        {
            if (matApply.isAlreadyIn(renderer)) 
            {
                matApply.SetRenderer(renderer, matObjectName);
            }
            else 
            {
                matApply.AddRenderer(renderer, matObjectName);
            }
        }
    }

}
