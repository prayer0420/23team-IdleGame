using System.Collections;
using UnityEngine;

namespace GameProject.Levels
{
    public class Map : MonoBehaviour
    {
        public int ChapterNumber { get; private set; }
        private SpriteRenderer backgroundRenderer;

        private void Start()
        {
            backgroundRenderer = GetComponent<SpriteRenderer>();
        }

        public void LoadMap(int chapterNumber)
        {
            ChapterNumber = chapterNumber;
            Sprite backgroundSprite = Resources.Load<Sprite>($"Sprites/Background_Chapter{ChapterNumber}");
            backgroundRenderer.sprite = backgroundSprite;
            backgroundRenderer.size = new Vector2(760, 1280);
        }

        public void FadeIn()
        {
            StartCoroutine(Fade(0, 1));
        }

        public void FadeOut()
        {
            StartCoroutine(Fade(1, 0));
        }

        private IEnumerator Fade(float startAlpha, float endAlpha)
        {
            float duration = 1f; // 1초 동안 페이드
            float elapsed = 0f;
            Color color = backgroundRenderer.color;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
                backgroundRenderer.color = new Color(color.r, color.g, color.b, alpha);
                yield return null;
            }
        }
    }
}
