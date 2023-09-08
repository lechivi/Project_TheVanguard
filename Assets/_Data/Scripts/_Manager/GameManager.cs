using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : BaseManager<GameManager>
{
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private List<Character> listCharacter = new List<Character>();
    [SerializeField] private CharacterDataSO characterData;

    private bool isPlaying;

    public PlayerCtrl PlayerCtrl { get => this.playerCtrl; }
    public CharacterDataSO CharacterData { get => this.characterData; set => this.characterData = value; }
    public bool IsPlaying { get => this.isPlaying; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.playerCtrl == null)
            this.playerCtrl = GetComponentInChildren<PlayerCtrl>();
    }

    private void Start()
    {
        this.characterData = null;
    }

    public void StartGame()
    {
        this.isPlaying = true;
        StartCoroutine(this.LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void PauseGame()
    {
        if (!this.isPlaying) return;

        this.isPlaying = false;
        Time.timeScale = 0.0f;
    }

    public void ResumeGame()
    {
        this.isPlaying = true;
        Time.timeScale = 1.0f;
    }

    public void BackToMainMenu()
    {
        this.playerCtrl.ResetPlayer();

        this.ResumeGame();
        StartCoroutine(this.LoadScene(0));
    }

    public void QuitGame()
    {
        this.playerCtrl.ResetPlayer();

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public IEnumerator LoadScene(int sceneIndex)
    {
        if (UIManager.HasInstance)
        {
            //UIManager.Instance.AnimatorTransition.SetTrigger("End");
            yield return new WaitForSeconds(1.25f);

            UIManager.Instance.Enable_UI_LoadingPanel();
            StartCoroutine(UIManager.Instance.LoadingPanel.LoadScene(sceneIndex));
            yield return null;
        }
    }

    public void GenerateCharacter(Vector3 position, Quaternion rotation)
    {
        if (this.characterData == null) return;

        Character character = null;
        foreach (Character chr in this.listCharacter)
        {
            if (chr.CharacterData == this.characterData)
            {
                character = Instantiate(chr, position, rotation);
                break;
            }
        }
        if (character == null) return;

        this.playerCtrl.SetCharacter(character);
        this.playerCtrl.gameObject.SetActive(true);
    }
}
