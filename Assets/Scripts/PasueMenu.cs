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


    void Awake()
    {
    }

    void Start()
    {
        Button_BGM1.onClick.AddListener(() => OnBGMButtonClicked(0));
        Button_BGM2.onClick.AddListener(() => OnBGMButtonClicked(1));
        Button_BGM3.onClick.AddListener(() => OnBGMButtonClicked(2));

        InitializeVolumeSliders();

        ExitButton.onClick.AddListener(ClickExit);

        BaseUI.SetActive(false);
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

        //TODO: 적 못움직이게
    }

    private void CloseMenu()
    {
        GameManager.Instance.isPause = false;
        BaseUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //TODO: 적 다시 움직이게
    }

    public void ClickExit()
    {
        Application.Quit();
    }

    public void OnBGMButtonClicked(int index)
    {
        AudioManager.Instance.PlayBGM(index);
    }

    private void InitializeVolumeSliders()
    {
        // BGM 볼륨 슬라이더 초기화
        Slider_BGMVolume.minValue = 0f;
        Slider_BGMVolume.maxValue = 1f;
        Slider_BGMVolume.value = AudioManager.Instance.GetBGMVolume();
        Slider_BGMVolume.onValueChanged.AddListener(OnBGMVolumeChanged);

        Slider_SFXVolume.minValue = 0f;
        Slider_SFXVolume.maxValue = 1f;
        Slider_SFXVolume.value = AudioManager.Instance.GetSFXVolume();
        Slider_SFXVolume.onValueChanged.AddListener(OnSFXVolumeChanged);

        Slider_MasterVolume.minValue = 0f;
        Slider_MasterVolume.maxValue = 1f;
        Slider_MasterVolume.value = AudioManager.Instance.GetMasterVolume();
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
