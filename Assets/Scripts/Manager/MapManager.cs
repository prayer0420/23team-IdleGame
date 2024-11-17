using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameProject.Characters;
using GameProject.Managers;
using System;

namespace GameProject.Levels
{
    public class MapManager : MonoBehaviour
    {
        private GameObject background;
        [SerializeField]  private Image fade;
        private GameObject currentMap;
        private GameObject nextMap;

        public static MapManager Instance { get; private set; }

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

        public void ChangeMap(int chapterNumber, Action callback)
        {
            StartCoroutine(FadeOutAndIn(chapterNumber, callback));
        }

        private void LoadMap(int chapterNumber)
        {
            GameObject backgroundObj = Resources.Load<GameObject>($"Prefabs/Background_Chapter{chapterNumber}");
            currentMap = Instantiate(backgroundObj, backgroundObj.transform.position, backgroundObj.transform.rotation);

            if (GameManager.Instance.CurrentStage>1)
            {
                nextMap = backgroundObj;
                DestroyImmediate(currentMap,true);
                currentMap = nextMap;
                return;
            }
            currentMap = backgroundObj;

            
        }

        private IEnumerator FadeOutAndIn(int newChapterNumber, Action callback)
        {
            // 새로운 맵 로드
            LoadMap(newChapterNumber);

            if (GameManager.Instance.CurrentStage > 1)
            {
                // 페이드 인
                yield return StartCoroutine(Fade(0, 1));
                Debug.Log("페이드 인");
            
                // 페이드 아웃
                yield return StartCoroutine(Fade(1, 0));
                Debug.Log("페이드 아웃");
            }
            callback();

        }

        private IEnumerator Fade(float startAlpha, float endAlpha)
        {
            float duration = 1f;
            float elapsed = 0f;
            Color color = fade.color;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
                fade.color = new Color(color.r, color.g, color.b, alpha);
                yield return null;
            }
        }
    }
}
