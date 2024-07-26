using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Variables")]
    [SerializeField]
    private TextMeshProUGUI moneyText;
    [SerializeField]
    private TextMeshProUGUI _wavesCount;
    [SerializeField]
    private Image removeTurretButtonImage;
    [SerializeField]
    private float pasiveMoneyTimer = 1;
    float _timeToPasiveMoneyTimer;
    [SerializeField]
    private int pasiveMoneyAmount = 10;
    Action<int> PasiveMoney;
    [SerializeField]
    private GameObject winPanel, defeatPanel;

    public event Action OnCompleteLevel;
    public event Action OnDefeatLevel;

    private bool _canRemoveTurret;
    public bool CanRemoveTurret
    {
        set
        {
            _canRemoveTurret = value;
            if (removeTurretButtonImage != null)
            {
                if (_canRemoveTurret) removeTurretButtonImage.color = Color.red;
                else removeTurretButtonImage.color = Color.cyan;
            }
            Debug.Log("Remove activo: " + _canRemoveTurret);
        }
        get
        {
            return _canRemoveTurret;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        OnCompleteLevel += CompleteLevelGm;
        OnDefeatLevel += DefeatLevelGm;
        SetActivePasiveMoney(false);
        CanRemoveTurret = false;
    }

    private void Update()
    {
        //Timer que invoque a pasiveMoney
        if (Time.time > _timeToPasiveMoneyTimer)
        {
            PasiveMoney(pasiveMoneyAmount);
            _timeToPasiveMoneyTimer = Time.time + pasiveMoneyTimer;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("MainMenu");

        if (moneyText != null) moneyText.text = "Money: " + Shop.instance.GetMoney;
    }

    public Action<int> SetActivePasiveMoney(bool setActiveValue) => setActiveValue == true ? PasiveMoney = Shop.instance.AddMoney : PasiveMoney = delegate { };

    public void ExitGame() => Application.Quit();
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }

    public void SetActivePanel(GameObject goToSetActive, bool setActiveBool, float timeScale)
    {
        goToSetActive.SetActive(setActiveBool);
        Time.timeScale = timeScale;
    }

    private void CompleteLevelGm()
    {
        SetActivePanel(winPanel, true, 0.5f);
        SetActivePasiveMoney(false);
    }

    private void DefeatLevelGm()
    {
        SetActivePanel(defeatPanel, true, 0.5f);
        SetActivePasiveMoney(false);
    }

    public void Win() => OnCompleteLevel?.Invoke();

    public void Defeat() => OnDefeatLevel?.Invoke();

    public void UpdateWavesCountUI(int count) => _wavesCount.text = $"Wave: {count} ";

    public void RemoveTorret()
    {
        if (!_canRemoveTurret) CanRemoveTurret = true;
        else CanRemoveTurret = false;
    }
}