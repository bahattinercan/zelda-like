using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CameraMinMaxPos : ScriptableObject, ISerializationCallbackReceiver
{
    public Vector2 baseHomeMin;
    public Vector2 baseHomeMax;
    private Vector2 startPosMin;
    private Vector2 startPosMax;
    public Vector2 innerHouseInMin;
    public Vector2 innerHouseInMax;
    public Vector2 innerHouseOutMin;
    public Vector2 innerHouseOutMax;
    public Vector2 dungeon1InMin;
    public Vector2 dungeon1InMax;
    public Vector2 dungeon1OutMin;
    public Vector2 dungeon1OutMax;

    public Vector2 getStartPosMin() {
        if (startPosMin == Vector2.zero)
            return baseHomeMin;
        return startPosMin;
    }

    public Vector2 getStartPosMax()
    {
        if (startPosMax == Vector2.zero)
            return baseHomeMax;
        return startPosMax;
    }

    public void SetStartPos(string currentScene, string sceneToLoad)
    {
        Vector2 min=Vector2.zero,max=Vector2.zero;
        
        switch (currentScene)
        {
            case "SampleScene": 
                switch (sceneToLoad)
                {
                    case "HouseInterior":
                        min = innerHouseInMin;
                        max = innerHouseInMax;
                        break;
                    case "Dungeon1":
                        min = dungeon1InMin;
                        max = dungeon1InMax;
                        break;
                }
                break;
            case "HouseInterior":
                min = innerHouseOutMin;
                max = innerHouseOutMax;
                break;
            case "Dungeon1":
                min = dungeon1OutMin;
                max = dungeon1OutMax;
                break;
            default:
                Debug.Log("scene deðiþkeni yok");
                break;
        }
        startPosMin = min;
        startPosMax = max;
    }

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        startPosMax = baseHomeMax;
        startPosMin = baseHomeMin;
    }
}
