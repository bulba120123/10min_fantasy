using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Carmone
{
    public class TargetingLine : MonoBehaviour
    {
        private Color transparency = new Color(0, 0, 0, 0);
        public Color startColor = new Color(1, 0, 0, 1); // 라인의 시작 색상 (불투명한 빨간색)
        public Color endColor = new Color(1, 0, 0, 0); // 라인의 끝 색상 (투명한 빨간색)
        private LineRenderer lineRenderer;
        bool isTargeting;
        BaseBoss boss;
        void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.startWidth = 1.5f;
            lineRenderer.endWidth = 1.5f;
            lineRenderer.positionCount = 2;
            lineRenderer.useWorldSpace = true;
            lineRenderer.startColor = transparency;
            lineRenderer.endColor = transparency;
        }

        private void FixedUpdate()
        {
            if (isTargeting)
            {
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, GameManager.instance.playerPosWithZConvert);
            }
        }

        public void Init(BaseBoss boss)
        {
            this.boss = boss;
        }

        public void DrawTargetingLine(float waitTime)
        {
            isTargeting = true;
            StartCoroutine(IDrawTargetingLine(waitTime));
        }

        public void StopTargeting()
        {
            isTargeting = false;
        }

        IEnumerator IDrawTargetingLine(float waitTime)
        {
            float i = 0;
            while (true) {

                if (GameManager.instance.isLive)
                {

                    yield return new WaitForSeconds(0.1f);
                    i++;

                    Color currentColor = Color.Lerp(endColor, startColor, i / 30f);

                    // 라인의 색상을 업데이트
                    lineRenderer.startColor = startColor;
                    lineRenderer.endColor = currentColor;
                }
                if (!isTargeting)
                {
                    lineRenderer.startColor = transparency;
                    lineRenderer.endColor = transparency;
                    yield break;
                }
            }
        }
    }
}
