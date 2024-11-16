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

        //Chapter클리어 시 다음 Chapter해금
        public void UnlockChapterUI(int chapterNumber)
        {
            
        }
        //노말 전체 클리어시 하드 난이도 해금
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
