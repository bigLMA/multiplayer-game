using System.Collections;
using UnityEngine;

namespace Level
{
    public class EndLevel : MonoBehaviour
    {
        [field: SerializeField]
        [Tooltip("Level duration in minutes")]
        [Range(0.1f, 10f)]
        public float levelDurationMins { get; private set; } = 0.1f;

        public float levelDurationSec { get; private set; }

        public static EndLevel instance { get; private set; } =null;

        private void Start()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                if(instance!=this)
                {
                    Destroy(gameObject);
                }
            }

            levelDurationSec = levelDurationMins * 60f;
            StartCoroutine(EndLevelCoroutine());
        }

        public void LevelEnd()
        {
            Time.timeScale = 0f;
        }

        private IEnumerator EndLevelCoroutine()
        {
            while(levelDurationSec>0)
            {
                yield return new WaitForFixedUpdate();

                levelDurationSec -= Time.fixedDeltaTime;
            }

            LevelEnd();
        }
    }
}