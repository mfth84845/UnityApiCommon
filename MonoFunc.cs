using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoFunc : MonoBehaviour
{



}
[CreateAssetMenu(fileName = "aa", menuName = "aa")]
public class ExampleScriptableObject : ScriptableObject
{
#if UNITY_EDITOR
    [UnityEditor.MenuItem("Example/Take Screenshot of Game View %^s")]
    static void TakeScreenshot()
    {
        ScreenCapture.CaptureScreenshot(Application.dataPath + "/SSSS.png");
    }
#endif

}
