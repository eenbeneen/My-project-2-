using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TypeWheelScript : MonoBehaviour
{

    public static TypeWheelScript Instance { get; private set; }

    private const int NUMTYPES = 4;

    [SerializeField] private int[] currentMoodValueArray = new int[NUMTYPES];
    [SerializeField] private JokeSOScript.JokeType[] jokeTypeArray = new JokeSOScript.JokeType[NUMTYPES];
    [SerializeField] private Vector2[] jokeTypePositionsOnCircleArray = new Vector2[NUMTYPES];
    [SerializeField] private Image typeWheelImage;
    [SerializeField] private Image typeWheelIndicatorImage;
    [SerializeField] private Image typeWheelIndicatorPreviewImage;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayerDeckManagerScript.Instance.OnJokePlayed += PlayerDeckManagerScript_OnJokePlayed;
        EnemyDeckManagerScript.Instance.OnEnemyJokePlayed += EnemyDeckManagerScript_OnEnemyJokePlayed;
        JokeUIScript.OnJokeSelected += JokeUIScript_OnJokeSelected;
        JokeUIScript.OnJokeUnselected += JokeUIScript_OnJokeUnselected;

        int radius = (int)typeWheelImage.GetComponent<RectTransform>().rect.width;
        float angle = (45f * Mathf.PI) / 180;
        for (int i = 0; i < NUMTYPES; i++)
        {
            jokeTypePositionsOnCircleArray[i] = new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);

            angle -= (90f * Mathf.PI) / 180;
        }

    }

    private void OnDisable()
    {
        PlayerDeckManagerScript.Instance.OnJokePlayed -= PlayerDeckManagerScript_OnJokePlayed;
        EnemyDeckManagerScript.Instance.OnEnemyJokePlayed -= EnemyDeckManagerScript_OnEnemyJokePlayed;
        JokeUIScript.OnJokeSelected -= JokeUIScript_OnJokeSelected;
        JokeUIScript.OnJokeUnselected -= JokeUIScript_OnJokeUnselected;
    }

    private void JokeUIScript_OnJokeUnselected(object sender, System.EventArgs e)
    {
        typeWheelIndicatorPreviewImage.gameObject.SetActive(false);
    }

    private void JokeUIScript_OnJokeSelected(object sender, JokeUIScript.OnJokeSelectedEventArgs e)
    {
        int[] moodValueArrayPreview= new int[NUMTYPES];
        currentMoodValueArray.CopyTo(moodValueArrayPreview, 0);
        moodValueArrayPreview = AddMoodToType(e.jokeSO, moodValueArrayPreview);
        typeWheelIndicatorPreviewImage.transform.localPosition = GetNewIndicatorPosition(moodValueArrayPreview);
        typeWheelIndicatorPreviewImage.gameObject.SetActive(true);

    }

    private void EnemyDeckManagerScript_OnEnemyJokePlayed(object sender, EnemyDeckManagerScript.OnEnemyJokePlayedEventArgs e)
    {
        currentMoodValueArray = AddMoodToType(e.jokeSO, currentMoodValueArray);
    }

    private void PlayerDeckManagerScript_OnJokePlayed(object sender, PlayerDeckManagerScript.OnJokePlayedEventArgs e)
    {
        currentMoodValueArray = AddMoodToType(e.jokeSO, currentMoodValueArray);
    }

    private void Update()
    {
        typeWheelIndicatorImage.transform.localPosition = GetNewIndicatorPosition(currentMoodValueArray);
        
    }


    private Vector2 GetNewIndicatorPosition(int[] moodValueArray)
    {
        Vector2 position = Vector2.zero;
        
        for (int i = 0; i < NUMTYPES; i++)
        {
            position += jokeTypePositionsOnCircleArray[i] * ((float)moodValueArray[i] / 100);
        }

        int radius = (int)typeWheelImage.GetComponent<RectTransform>().rect.width;
        if (position.magnitude > radius / 2)
        {
            position = position.normalized * (radius / 2);
        }

        return position;

    }

    public int[] AddMoodToType(int numToAdd, JokeSOScript.JokeType type, int[] moodValueArray)
    {
        for (int i = 0; i < NUMTYPES; i++)
        {
            if (jokeTypeArray[i] == type)
            {
                if (moodValueArray[i] + numToAdd < 100)
                {
                    moodValueArray[i] += numToAdd;
                }
                else
                {
                    moodValueArray[i] = 100;
                }
            }
            else
            {
                if (moodValueArray[i] > 10)
                {
                    moodValueArray[i] /= 2;
                }
                else
                {
                    moodValueArray[i] = 0;
                }
                
            }
        }
        return moodValueArray;
    }

    public int[] AddMoodToType(JokeSOScript jokeSO, int[] moodValueArray)
    {
        return AddMoodToType(jokeSO.moodChange, jokeSO.type, moodValueArray);
    }

    public float GetMultiplierForType(JokeSOScript.JokeType jokeType) 
    {
        int mainJokeTypeIndex = ArrayUtility.IndexOf(jokeTypeArray, GetJokeTypeMood());
        int jokeTypeIndex = ArrayUtility.IndexOf(jokeTypeArray, jokeType);


        if (mainJokeTypeIndex == -1)
        {
            
            return 1;
            
        }

        int distanceBetweenIndexes = Mathf.Abs(mainJokeTypeIndex - jokeTypeIndex);
        distanceBetweenIndexes = Mathf.Min(distanceBetweenIndexes, NUMTYPES - distanceBetweenIndexes);

        //Multipliers based on distance from main mood;
        switch (distanceBetweenIndexes)
        {
            case 0:
                return 2f;
            case 1:
                return 1f;
            default:
            case 2:
                return 0f;

        }

    }

    private JokeSOScript.JokeType GetJokeTypeMood()
    {

        Vector2 indicatorVector2 = typeWheelIndicatorImage.transform.localPosition;

        //if the indicator is too close to the middle, return a mood of Normal
        float margin = 5f;
        if (indicatorVector2.magnitude < margin)
        {
            return JokeSOScript.JokeType.Normal;
            
        }

        float indicatorAngle = Vector2.SignedAngle(Vector2.up, indicatorVector2);
        int positionIndex;

        if (indicatorAngle <= 0)
        {
            //Right side of circle
            positionIndex = (int)-indicatorAngle / 90;
        }
        else
        {
            //Left side of circle
            positionIndex = (180 - (int)indicatorAngle) / 90 + 2;
        }

        return jokeTypeArray[positionIndex];
    }

}
