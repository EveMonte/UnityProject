using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Click : MonoBehaviour
{
    private Camera mainCamera;
    public GameObject cursor;
    private Vector3 point;
    public GameObject prefab;
    public int currentSlide = 1;
    public int numOfSlides = 7;
    private RectTransform sightTransform;
    public string path = "Slide1";

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            Cursor.visible = false;
            prefab.GetComponent<Renderer>().material.mainTexture = Resources.Load(path) as Texture;

            mainCamera = gameObject.GetComponent<Camera>();
            sightTransform = cursor.GetComponent<RectTransform>();
            sightTransform.sizeDelta = new Vector2(4, 4);
            point = new Vector3(mainCamera.pixelWidth / 2, mainCamera.pixelHeight / 2, 0);
            cursor.transform.position = point;
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckMouseClick();
        }
    }
    private void CheckMouseClick()
    {
        try
        {
            Ray ray = mainCamera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            { // объект, в который попал луч
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.tag == "arrowUp")
                {
                    path = path.Substring(0, path.Length - 1);
                    if (currentSlide > 1)
                    {
                        currentSlide--;
                    }
                    else
                        currentSlide = 7;

                    path = path.Substring(0, path.Length) + currentSlide;
                    prefab.GetComponent<Renderer>().material.mainTexture = Resources.Load(path) as Texture;
                }
                else if (hitObject.tag == "arrowDown")
                {
                    path = path.Substring(0, path.Length - 1);
                    if (currentSlide < 7)
                    {
                        currentSlide++;
                    }
                    else
                        currentSlide = 1;
                    path = path.Substring(0, path.Length) + currentSlide;

                    prefab.GetComponent<Renderer>().material.mainTexture = Resources.Load(path) as Texture;
                }
            }
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }
}

