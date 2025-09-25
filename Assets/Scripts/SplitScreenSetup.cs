using UnityEngine;

public class SplitScreenSetup : MonoBehaviour
{
    public Camera player1Cam;
    public Camera player2Cam;
    void Start()
    {
        if (player1Cam != null) player1Cam.rect = new Rect(0f, 0f, 0.5f, 1f);
        if (player2Cam != null) player2Cam.rect = new Rect(0.5f, 0f, 0.5f, 1f);
    }
}
