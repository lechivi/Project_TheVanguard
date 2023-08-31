using Photon.Pun.Demo.PunBasics;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingPanel : BaseUIElement
{
    [Header("LOADING PANEL")]
    [SerializeField] private TMP_Text loadingPercentText;
    [SerializeField] private Slider loadingSlider;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.loadingPercentText == null)
            this.loadingPercentText = GetComponentInChildren<TMP_Text>();

        if (this.loadingSlider == null)
            this.loadingSlider = GetComponentInChildren<Slider>();
    }

    public IEnumerator LoadScene(int sceneIndex)
    {
        this.loadingSlider.value = 0;
        this.loadingPercentText.SetText("Loading");
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            this.loadingSlider.value = asyncOperation.progress;
            this.loadingPercentText.SetText($"Loading: {(int)asyncOperation.progress * 100}%");

            if (asyncOperation.progress >= 0.9f) //0 - 0.9: load scene //0.9 - 1: chuyen scene
            {
                this.loadingSlider.value = 1f;
                this.loadingPercentText.SetText("Press any button to continue");

                if (Input.anyKeyDown)
                {
                    asyncOperation.allowSceneActivation = true;
                    //if (UIManager.HasInstance)
                    //{
                    //    UIManager.Instance.ActiveLoadingPanel(false);
                    //    UIManager.Instance.AnimatorTransition.Rebind();

                    //    if (sceneIndex == 0) //MainMenu
                    //    {
                    //        UIManager.Instance.StartMainMenu();
                    //    }
                    //    else if (sceneIndex == 1) //HomeScene
                    //    {
                    //        UIManager.Instance.ActiveHomeScenePanel(true);
                    //        UIManager.Instance.ActiveMenuPanel(false);
                    //        PlayerManager.Instance.ResetInventory();
                    //    }
                    //    else if (sceneIndex == 2) //TutorialScene
                    //    {
                    //        UIManager.Instance.ActiveHomeScenePanel(false);
                    //        UIManager.Instance.ActiveGamePanel(true);
                    //    }
                    //    else if (sceneIndex == 6) //TestScene
                    //    {
                    //        UIManager.Instance.ActiveTestPanel(true);
                    //        UIManager.Instance.ActiveGamePanel(true);
                    //        UIManager.Instance.ActiveMenuPanel(false);
                    //    }
                    //    else //PlayScene
                    //    {
                    //        UIManager.Instance.ActiveMenuPanel(false);
                    //        UIManager.Instance.ActiveGamePanel(true);
                    //    }
                    //}

                    //if (GameManager.HasInstance)
                    //{
                    //    GameManager.Instance.StartGame();
                    //}
                }
            }
            yield return null;

        }
    }
}
