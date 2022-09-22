using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScenePositions : ScriptableObject, ISerializationCallbackReceiver
{
    public Vector2 baseHome;
    private Vector2 startPos;
    public Vector2 innerHouseIn;
    public Vector2 innerHouseOut;
    public Vector2 dungeon1In;
    public Vector2 dungeon1Out;

    public Vector2 GetStartPos()
    {
        return startPos;
    }

    public void SetStartPos(string currentScene, string sceneToLoad)
    {
        Vector2 vector2 = Vector2.zero;
        switch (currentScene)
        {
            case "SampleScene":
                switch (sceneToLoad)
                {
                    case "HouseInterior":
                        vector2 = innerHouseIn;

                        break;
                    case "Dungeon1":
                        vector2 = dungeon1In;
                        break;                    
                }
                break;
            case "HouseInterior":
                vector2 = innerHouseOut;
                break;
            case "Dungeon1":
                vector2 = dungeon1Out;
                break;
            default:
                Debug.Log("scene deðiþkeni yok");
                break;
        }
        startPos = vector2;
    }

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        startPos = baseHome;
    }

    
}