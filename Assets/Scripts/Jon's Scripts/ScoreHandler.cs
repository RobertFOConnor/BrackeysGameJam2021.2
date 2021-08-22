using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    TextMeshProUGUI scoreTextField;

    [SerializeField]
    Slider scoreBarFill;

    [SerializeField]
    Transform scoreBarGO;

    [Range(0, 0.1f)]
    public float slamShrinkFactor;


    private float score = 0;
    private float maxScorePossible;

    private Vector3 baselineBarScale;

    private bool slamming = false;

    public void Start()
    {
        maxScorePossible = GetMaxScorePossible();
        baselineBarScale = scoreBarGO.transform.localScale;

        ChangeScoreText();
        SetBarFill();

    }

    public void Update()
    {
        if (scoreBarGO.transform.localScale.x <= baselineBarScale.x)
        {
            slamming = false;
        }
        if (slamming)
        {
            scoreBarGO.transform.localScale = Vector3.Lerp(scoreBarGO.transform.localScale, baselineBarScale, slamShrinkFactor);
        }

    }

    public void UpdateScore(int delta)
    {
        score += delta;
        ChangeScoreText();
        SetBarFill();
        BarSlamEffect();

    }

    public float GetScore()
    {
        return score;
    }

    public int GetMaxScorePossible()    //TODO
    {
        return 10000;
    }

    void ChangeScoreText()
    {
        scoreTextField.text = score.ToString() + '/' + maxScorePossible;
    }
    void SetBarFill() 
    {
        scoreBarFill.value = score / maxScorePossible;
    }

    void BarSlamEffect()
    {
        scoreBarGO.transform.localScale = new Vector3(baselineBarScale.x + 1, baselineBarScale.y + 1, 1);
        slamming = true;
    }
}
