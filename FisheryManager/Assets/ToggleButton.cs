using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToggleButton: MonoBehaviour
{
    [SerializeField] private Sprite[] buttonSprites;
    [SerializeField] private Image startSoundButton;
    public void ChangeButtonSprite()
    {
        if(startSoundButton.sprite == buttonSprites[0])
        {
            startSoundButton.sprite = buttonSprites[1];
            AudioListener.pause = true;
            return;
        }
        AudioListener.pause = false;
        startSoundButton.sprite = buttonSprites[0];
    }

    public void Update()
    {
        if(AudioListener.pause == false)
        {
            startSoundButton.sprite = buttonSprites[0];
        }
        else
        {
            startSoundButton.sprite = buttonSprites[1];
        }
    }

}
