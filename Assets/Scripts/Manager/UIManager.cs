using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameProject.Managers;

namespace GameProject.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        public TextMeshProUGUI chapterText;
        public Image[] starImages;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void InitializeUI(int InitNumber)
        {
            // UI ��� �ʱ�ȭ
            UpdateChapterUI(InitNumber);
            UpdateStageUI(InitNumber);
            ResetStarUI();
        }

        public void UpdateChapterUI(int chapterNumber)
        {
            chapterText.text = $"Chapter {chapterNumber}";
        }

        public void UpdateStageUI(int stageNumber)
        {
            ResetStarUI();
            UpdateStarUI(stageNumber);
        }

        public void UpdateStarUI(int stageNumber, bool isCleared = false)
        {
            for (int i = 0; i < stageNumber-1; i++)
            {
                if (stageNumber >= 1 && stageNumber <= 3)
                {
                    starImages[i].sprite = Resources.Load<Sprite>(isCleared ? "Sprites/Star_Filled" : "Sprites/Star_Empty");
                }
            }
        }

        private void ResetStarUI()
        {
            for (int i = 0; i < starImages.Length; i++)
            {
                starImages[i].sprite = Resources.Load<Sprite>("Sprites/Star_Empty");
            }
        }

        public void UnlockDifficultyUI(DifficultyLevel difficulty)
        {
            // ���̵� �ر� UI ������Ʈ
            Debug.Log($"{difficulty} ���̵� �ر�");
        }
    }
}
