using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchieveManager : MonoBehaviour
{
    public GameObject[] lockedCharacters;
    public GameObject[] unlockedCharacters;
    public GameObject uiNotice;
    
    private enum Achieve { UnlockPotato, UnlockBean }
    private Achieve[] achieves;
    private WaitForSecondsRealtime wait;

    private void Awake()
    {
        achieves = (Achieve[])Enum.GetValues(typeof(Achieve));
        wait = new WaitForSecondsRealtime(5f);
        
        if (PlayerPrefs.HasKey("MyData") == false)
        {
            Init();
        }
    }

    private void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);
        foreach (Achieve achieve in achieves)
        {
            PlayerPrefs.SetInt(achieve.ToString(), 0);
        }
    }

    private void Start()
    {
        UnlockCharacter();
    }

    private void UnlockCharacter()
    {
        for (int index = 0; index < lockedCharacters.Length; index++)
        {
            string achieveName = achieves[index].ToString();
            bool isUnlocked = PlayerPrefs.GetInt(achieveName) == 1;
            lockedCharacters[index].SetActive(isUnlocked == false);
            unlockedCharacters[index].SetActive(isUnlocked);
        }
    }

    private void LateUpdate()
    {
        foreach (Achieve achieve in achieves)
        {
            CheckAchieve(achieve);
        }
    }

    private void CheckAchieve(Achieve achieve)
    {
        bool isAchieved = false;

        switch (achieve)
        {
            case Achieve.UnlockPotato:
                isAchieved = GameManager.instance.kill >= 10;
                break;
            case Achieve.UnlockBean:
                isAchieved = GameManager.instance.gameTime == GameManager.instance.maxGameTime;
                break;
        }

        if (isAchieved && PlayerPrefs.GetInt(achieve.ToString()) == 0)
        {
            PlayerPrefs.SetInt(achieve.ToString(), 1);

            for (int index = 0; index < uiNotice.transform.childCount; index++)
            {
                bool isActive = index == (int)achieve;
                uiNotice.transform.GetChild(index).gameObject.SetActive(isActive);
            }
            
            StartCoroutine(NoticeRoutine());
        }
    }

    private IEnumerator NoticeRoutine()
    {
        uiNotice.SetActive(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
        
        yield return wait;

        uiNotice.SetActive(false);
    }
}
