using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ObjectMover : MonoBehaviour
{
    private S_ObjectModel objectModel = null;

    public S_ObjectModel ObjectModel
    {
        get { return objectModel; }
        set { objectModel = value; }
    }

    private int direction = 0;
    private float speed = 0.03f;
    private float speedRotation = 100f;
    private float minLimit;
    private float maxLimit;
    private float angleXAxis;
    private float angleYAxis;

    [HideInInspector]
    public bool isMoving = false;

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (ObjectModel != null)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    direction = 1;
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    direction = -1;
                }
                else
                {
                    direction = 0;
                }
            }
            else
            {
                direction = 0;
            }
            if (direction != 0)
            {
                isMoving = true;

                switch (ObjectModel.ObjectType)
                {
                    case S_ObjectModel.TypeOfObject.MOVABLE_PART:
                        ObjectModel.ActivePart.transform.Translate(speed * Time.deltaTime * direction, 0, 0, Space.Self);
                        ObjectModel.ActivePart.transform.localPosition = new Vector3(Mathf.Clamp(ObjectModel.ActivePart.transform.localPosition.x, ObjectModel.MinLimit, ObjectModel.MaxLimit),
                            ObjectModel.ActivePart.transform.localPosition.y, ObjectModel.ActivePart.transform.localPosition.z);
                        break;
                    case S_ObjectModel.TypeOfObject.ROTATABLE_PART:
                        minLimit = ObjectModel.MinLimit % 360;
                        maxLimit = (ObjectModel.MaxLimit) % 360;
                        ObjectModel.ActivePart.transform.Rotate(0, 0, -1 * speedRotation * Time.deltaTime * direction / 2);
                        angleYAxis = ObjectModel.ActivePart.transform.eulerAngles.y;
                        float tempVar = maxLimit;
                        maxLimit = maxLimit >= minLimit ? maxLimit : minLimit;
                        minLimit = maxLimit == minLimit ? tempVar : minLimit;

                        ObjectModel.ActivePart.transform.eulerAngles = new Vector3(ObjectModel.ActivePart.transform.eulerAngles.x, Mathf.Clamp(
                            angleYAxis -= angleYAxis < 360 ? 0 : 360, minLimit, maxLimit), ObjectModel.ActivePart.transform.eulerAngles.z);
                        break;
                    case S_ObjectModel.TypeOfObject.SOURCE_SWITCH:
                        ObjectModel.ActivePart.transform.Rotate(0, speedRotation * Time.deltaTime * direction / 2, 0);
                        angleXAxis = ObjectModel.ActivePart.transform.eulerAngles.x;
                        maxLimit = (ObjectModel.MinLimit + 180) % 360 - 180;
                        minLimit = (ObjectModel.MaxLimit + 180) % 360 - 180;
                        ObjectModel.ActivePart.transform.eulerAngles = new Vector3(Mathf.Clamp(angleXAxis -= angleXAxis < 180 ? 0 : 360, minLimit, maxLimit),
                            ObjectModel.ActivePart.transform.eulerAngles.y, ObjectModel.ActivePart.transform.eulerAngles.z);
                        break;
                }
            }
            else
            {
                isMoving = false;
            }
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }

}
