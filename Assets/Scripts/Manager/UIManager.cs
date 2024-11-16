using UnityEngine;
using UnityEngine.UI;
using GameProject.Managers;
using TMPro;

namespace GameProject.Managers
{
    public class UIManager : MonoBehaviour
    {
        public TextMeshProUGUI chapterText;
        public TextMeshProUGUI stageText;
        public Image[] starImages;
        public GameObject deathScreen;

        public void UpdateUI(int chapterNumber, int stageNumber)
        {
            chapterText.text = $"Chapter {chapterNumber}";
            stageText.text = $"Stage {stageNumber}";
            UpdateStarUI(stageNumber, false);
        }


        public void UpdateStarUI(int stageNumber, bool isCleared)
        {
            if (stageNumber >= 1 && stageNumber <= 3)
            {
                starImages[stageNumber - 1].sprite = Resources.Load<Sprite>(isCleared ? "UI/Star_Filled" : "UI/Star_Empty");
            }
        }

        //ChapterŬ���� �� ���� Chapter�ر�
        public void UnlockChapterUI(int chapterNumber)
        {
            
        }
        //�븻 ��ü Ŭ����� �ϵ� ���̵� �ر�
        public void UnlockDifficultyUI(DifficultyLevel difficulty)
        {
        }

        public void ShowDeathScreen()
        {
            deathScreen.SetActive(true);

            Invoke("HideDeathScreen", 2f);
        }

        private void HideDeathScreen()
        {
            deathScreen.SetActive(false);
        }
    }
}
