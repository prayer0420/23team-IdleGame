using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject BaseUI;

    [Header("BGM Selection Buttons")]
    public Button Button_BGM1;
    public Button Button_BGM2;
    public Button Button_BGM3;

    [Header("Volume Sliders")]
    public Slider Slider_BGMVolume;
    public Slider Slider_SFXVolume;
    public Slider Slider_MasterVolume;

    [Header("Other Buttons")]
    public Button ExitButton;

    private void Start()
    {
        // BGM ��ư�� ������ �߰�
        Button_BGM1.onClick.AddListener(() => OnBGMButtonClicked("Audio/BGM/BGM1"));
        Button_BGM2.onClick.AddListener(() => OnBGMButtonClicked("Audio/BGM/BGM2"));
        Button_BGM3.onClick.AddListener(() => OnBGMButtonClicked("Audio/BGM/BGM3"));

        InitializeVolumeSliders();

        ExitButton.onClick.AddListener(ClickExit);

        BaseUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandleOnPause();
        }
    }

    public void HandleOnPause()
    {
        if (!GameManager.Instance.isPause)
        {
            OpenMenu();
        }
        else
        {
            CloseMenu();
        }
    }

    private void OpenMenu()
    {
        GameManager.Instance.isPause = true;
        BaseUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // TODO: �� ������ ���߱�
    }

    private void CloseMenu()
    {
        GameManager.Instance.isPause = false;
        BaseUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // TODO: �� ������ �簳
    }

    public void ClickExit()
    {
        Application.Quit();
    }

    public void OnBGMButtonClicked(string bgmPath)
    {
        AudioManager.Instance.PlayBGM(bgmPath);
    }

    private void InitializeVolumeSliders()
    {
        // BGM ���� �����̴� �ʱ�ȭ
        Slider_BGMVolume.minValue = 0f;
        Slider_BGMVolume.maxValue = 1f;
        Slider_BGMVolume.value = AudioManager.Instance.bgmVolume;
        Slider_BGMVolume.onValueChanged.AddListener(OnBGMVolumeChanged);

        // SFX ���� �����̴� �ʱ�ȭ
        Slider_SFXVolume.minValue = 0f;
        Slider_SFXVolume.maxValue = 1f;
        Slider_SFXVolume.value = AudioManager.Instance.sfxVolume;
        Slider_SFXVolume.onValueChanged.AddListener(OnSFXVolumeChanged);

        // ������ ���� �����̴� �ʱ�ȭ
        Slider_MasterVolume.minValue = 0f;
        Slider_MasterVolume.maxValue = 1f;
        Slider_MasterVolume.value = AudioManager.Instance.masterVolume;
        Slider_MasterVolume.onValueChanged.AddListener(OnMasterVolumeChanged);
    }

    public void OnBGMVolumeChanged(float volume)
    {
        AudioManager.Instance.SetBGMVolume(volume);
    }

    public void OnSFXVolumeChanged(float volume)
    {
        AudioManager.Instance.SetSFXVolume(volume);
    }

    public void OnMasterVolumeChanged(float volume)
    {
        AudioManager.Instance.SetMasterVolume(volume);
    }
}
