using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public PostManager postManager;

    void Update()
    {
        // --- Player 1 (left side) ---
        if (Input.GetKeyDown(KeyCode.A)) postManager.HandleDecision(1, "Player 1"); // True
        if (Input.GetKeyDown(KeyCode.S)) postManager.HandleDecision(0, "Player 1"); // False
        if (Input.GetKeyDown(KeyCode.D)) postManager.HandleDecision(2, "Player 1"); // Skip

        // --- Player 2 (right side) ---
        if (Input.GetKeyDown(KeyCode.J)) postManager.HandleDecision(1, "Player 2"); // True
        if (Input.GetKeyDown(KeyCode.K)) postManager.HandleDecision(0, "Player 2"); // False
        if (Input.GetKeyDown(KeyCode.L)) postManager.HandleDecision(2, "Player 2"); // Skip
    }
}
