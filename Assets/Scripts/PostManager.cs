using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PostManager : MonoBehaviour
{
    [Header("Player 1 UI")]
    public TextMeshProUGUI player1Text;
    public Button player1TrueButton;
    public Button player1FalseButton;
    public Button player1SkipButton;
    public Button player1InvestigateButton;
    public TextMeshProUGUI player1SPText;
    //public TextMeshProUGUI player1StreakText;
    public int player1Score = 0;
    private int player1Streak = 0;
    private float player1Timer;
    private int player1SP = 3;
    
    public TextMeshProUGUI investigationTipText;

    [Header("Player 2 UI")]
    public TextMeshProUGUI player2Text;
    public Button player2TrueButton;
    public Button player2FalseButton;
    public Button player2SkipButton;
    public Button player2InvestigateButton;
    public TextMeshProUGUI player2SPText;

    
    public TextMeshProUGUI investigationTipText2;
    //public TextMeshProUGUI player2StreakText;
    public int player2Score = 0;
    private int player2Streak = 0;
    private float player2Timer;
    private int player2SP = 3;

    [Header("Posts")]
    public ContentItem[] contentItems;

    [Header("End / Day Summary Panels")]
    public GameObject endPanel;
    public TextMeshProUGUI endScoreText;

    public GameObject daySummaryPanel;
    public TextMeshProUGUI daySummaryText;
    public Button dayContinueButton;

    [Header("Day Timer Settings")]
    public float dayDuration = 60f;
    public TextMeshProUGUI dayTimerText;
    public float timePerPost = 10f;

    private float dayTimer;
    private bool isDayActive = true;

    private List<ContentItem> remainingPostsPlayer1 = new List<ContentItem>();
    private List<ContentItem> remainingPostsPlayer2 = new List<ContentItem>();

    private ContentItem currentPlayer1Item;
    private ContentItem currentPlayer2Item;

    void Start()
    {
        remainingPostsPlayer1 = new List<ContentItem>(contentItems);
        remainingPostsPlayer2 = new List<ContentItem>(contentItems);

        dayTimer = dayDuration;
        isDayActive = true;
        if (daySummaryPanel) daySummaryPanel.SetActive(false);
        if (endPanel) endPanel.SetActive(false);

        if (dayContinueButton != null)
        {
            dayContinueButton.onClick.RemoveAllListeners();
            dayContinueButton.onClick.AddListener(StartNextDay);
        }

        UpdateSPUI();

        ShowNextPostPlayer1();
        ShowNextPostPlayer2();
    }

    void Update()
    {
        if (isDayActive)
        {
            dayTimer -= Time.deltaTime;
            UpdateDayTimerUI();

            if (dayTimer <= 0f)
            {
                EndDay();
            }

            if (remainingPostsPlayer1.Count > 0)
            {
                player1Timer -= Time.deltaTime;
                if (player1Timer <= 0f) HandleDecision(2, "Player 1");
            }

            if (remainingPostsPlayer2.Count > 0)
            {
                player2Timer -= Time.deltaTime;
                if (player2Timer <= 0f) HandleDecision(2, "Player 2");
            }
        }
    }

   
    public void Investigate(string player)
{
    if (player == "Player 1" && player1SP > 0 && currentPlayer1Item != null)
    {
        player1SP--;
        investigationTipText.text = currentPlayer1Item.investigationHint;
    }
    else if (player == "Player 2" && player2SP > 0 && currentPlayer2Item != null)
    {
        player2SP--;
        investigationTipText2.text = currentPlayer2Item.investigationHint;
    }

    UpdateSPUI();
}


    void ShowHint(ContentItem item, string player)
    {
        string hint = "";
        if (item.correctAnswer == ContentItem.CorrectAnswer.True)
            hint = "Hint: Evidence supports this post being TRUE.";
        else
            hint = "Hint: Evidence suggests this post is FALSE.";

        Debug.Log($"{player} investigates: {hint}");

        if (player == "Player 1") player1Text.text += $"\n<i>{hint}</i>";
        else player2Text.text += $"\n<i>{hint}</i>";
    }
    public void SetPost(string postContent, string investigationTip)
    {
      
        investigationTipText.text = investigationTip;
        
        investigationTipText2.text = investigationTip;
    }
    void UpdateSPUI()
    {
        if (player1SPText != null) player1SPText.text = $"SP: {player1SP}";
        if (player2SPText != null) player2SPText.text = $"SP: {player2SP}";

        if (player1InvestigateButton != null) player1InvestigateButton.interactable = player1SP > 0;
        if (player2InvestigateButton != null) player2InvestigateButton.interactable = player2SP > 0;
    }
    void UpdateStreakUI()
    {
       /* if (player1StreakText != null)
            player1StreakText.text = $"Streak: {player1Streak}";

        if (player2StreakText != null)
            player2StreakText.text = $"Streak: {player2Streak}";*/
    }


    // ==== Post Handling ====
    void ShowNextPostPlayer1()
    {
        if (remainingPostsPlayer1.Count == 0)
        {
            player1Text.text = "Player 1 finished!";
            CheckEndCondition();
            return;
        }

        int index = Random.Range(0, remainingPostsPlayer1.Count);
        currentPlayer1Item = remainingPostsPlayer1[index];
        remainingPostsPlayer1.RemoveAt(index);

        player1Text.text = currentPlayer1Item.content;

        player1TrueButton.onClick.RemoveAllListeners();
        player1FalseButton.onClick.RemoveAllListeners();
        player1SkipButton.onClick.RemoveAllListeners();
        player1InvestigateButton.onClick.RemoveAllListeners();

        player1TrueButton.onClick.AddListener(() => HandleDecision(1, "Player 1"));
        player1FalseButton.onClick.AddListener(() => HandleDecision(0, "Player 1"));
        player1SkipButton.onClick.AddListener(() => HandleDecision(2, "Player 1"));
        player1InvestigateButton.onClick.AddListener(() => Investigate("Player 1"));

        player1Timer = timePerPost;
    }

    void ShowNextPostPlayer2()
    {
        if (remainingPostsPlayer2.Count == 0)
        {
            player2Text.text = "Player 2 finished!";
            CheckEndCondition();
            return;
        }

        int index = Random.Range(0, remainingPostsPlayer2.Count);
        currentPlayer2Item = remainingPostsPlayer2[index];
        remainingPostsPlayer2.RemoveAt(index);

        player2Text.text = currentPlayer2Item.content;

        player2TrueButton.onClick.RemoveAllListeners();
        player2FalseButton.onClick.RemoveAllListeners();
        player2SkipButton.onClick.RemoveAllListeners();
        player2InvestigateButton.onClick.RemoveAllListeners();

        player2TrueButton.onClick.AddListener(() => HandleDecision(1, "Player 2"));
        player2FalseButton.onClick.AddListener(() => HandleDecision(0, "Player 2"));
        player2SkipButton.onClick.AddListener(() => HandleDecision(2, "Player 2"));
        player2InvestigateButton.onClick.AddListener(() => Investigate("Player 2"));

        player2Timer = timePerPost;
    }

    public void HandleDecision(int choice, string player)
    {
        if (!isDayActive) return;

        if ((player == "Player 1" && remainingPostsPlayer1.Count == 0) ||
            (player == "Player 2" && remainingPostsPlayer2.Count == 0))
            return;

        ContentItem item = (player == "Player 1") ? currentPlayer1Item : currentPlayer2Item;
        int pointsEarned = 0;

        if (choice == 2) // skip
        {
            if (player == "Player 1") player1Streak = 0;
            else player2Streak = 0;
        }
        else
        {
            bool playerChoseTrue = choice == 1;
            bool isCorrect = (item.correctAnswer == ContentItem.CorrectAnswer.True && playerChoseTrue) ||
                             (item.correctAnswer == ContentItem.CorrectAnswer.False && !playerChoseTrue);
            if (isCorrect)
            {
                pointsEarned = 1;

                if (item.specialEffect == ContentItem.Effect.DoublePoints)
                    pointsEarned *= 2;

                if (player == "Player 1") player1Streak++;
                else player2Streak++;

                // Bonus point for streak
                if ((player == "Player 1" && player1Streak >= 3) ||
                    (player == "Player 2" && player2Streak >= 3))
                {
                    pointsEarned += 1;
                    Debug.Log($"{player} streak bonus!");
                }

                // Example: reward SP on milestone
                if (player == "Player 1" && player1Streak == 5) { player1SP++; }
                if (player == "Player 2" && player2Streak == 5) { player2SP++; }
            }
            else
            {
                if (player == "Player 1") player1Streak = 0;
                else player2Streak = 0;
            }

            UpdateSPUI();
            UpdateStreakUI();
        }

        if (player == "Player 1") player1Score += pointsEarned;
        else player2Score += pointsEarned;

        Debug.Log($"{player} Score: {(player == "Player 1" ? player1Score : player2Score)} (+{pointsEarned})");

        if (player == "Player 1") ShowNextPostPlayer1();
        else ShowNextPostPlayer2();


    }

    // ==== Day & End Logic ====
    void EndDay()
    {
        isDayActive = false;
        SetButtonsInteractable(false);

        if (daySummaryPanel != null)
        {
            daySummaryPanel.SetActive(true);
            string summary = $"Day ended!\n\nPlayer 1 Score: {player1Score}\nPlayer 2 Score: {player2Score}\n\n";
            summary += "Press Continue to resume.";
            if (daySummaryText) daySummaryText.text = summary;
        }
    }

    void StartNextDay()
    {
        if (daySummaryPanel != null) daySummaryPanel.SetActive(false);

        dayTimer = dayDuration;
        isDayActive = true;

        // Restore SP each new day (optional)
        player1SP = 3;
        player2SP = 3;
        UpdateSPUI();

        SetButtonsInteractable(true);
        UpdateDayTimerUI();
    }

    void UpdateDayTimerUI()
    {
        if (dayTimerText == null) return;
        float t = Mathf.Max(0, dayTimer);
        int minutes = Mathf.FloorToInt(t / 60f);
        int seconds = Mathf.FloorToInt(t % 60f);
        dayTimerText.text = $"{minutes:00}:{seconds:00}";
    }

    void SetButtonsInteractable(bool interactable)
    {
        if (player1TrueButton) player1TrueButton.interactable = interactable && remainingPostsPlayer1.Count > 0;
        if (player1FalseButton) player1FalseButton.interactable = interactable && remainingPostsPlayer1.Count > 0;
        if (player1SkipButton) player1SkipButton.interactable = interactable && remainingPostsPlayer1.Count > 0;
        if (player1InvestigateButton) player1InvestigateButton.interactable = interactable && player1SP > 0;

        if (player2TrueButton) player2TrueButton.interactable = interactable && remainingPostsPlayer2.Count > 0;
        if (player2FalseButton) player2FalseButton.interactable = interactable && remainingPostsPlayer2.Count > 0;
        if (player2SkipButton) player2SkipButton.interactable = interactable && remainingPostsPlayer2.Count > 0;
        if (player2InvestigateButton) player2InvestigateButton.interactable = interactable && player2SP > 0;
    }

    void CheckEndCondition()
    {
        if (remainingPostsPlayer1.Count == 0 && remainingPostsPlayer2.Count == 0)
        {
            if (endPanel != null)
            {
                endPanel.SetActive(true);
                if (endScoreText != null)
                    endScoreText.text = $"Player 1 Score: {player1Score}\nPlayer 2 Score: {player2Score}";
            }
        }
    }
}
