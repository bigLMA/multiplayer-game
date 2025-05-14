using UI.ProgressBars;
using UnityEngine;

namespace Dish.SlicedProducts
{
    public class SliceProductBase : MonoBehaviour
    {
        [Header("Slicing info")]
        [SerializeField]
        [Tooltip("How much to slice product to get sliced version")]
        [Range(2f, 6f)]
        private float maxSliceCount = 2f;

        [SerializeField]
        [Tooltip("What prefab to instantiate after the product is sliced")]
        private GameObject slicedProductPrefab;

        [SerializeField]
        private GameObject productMesh;

        [SerializeField]
        private GameObject progressBarGO;

        private float sliceProgress = 0f;

        public delegate void SliceNotify(GameObject prefab);
        public event SliceNotify OnSliced;

        private IProgressBar progressBar;

        private void Start()
        {
            progressBar = progressBarGO.GetComponent<IProgressBar>();

            gameObject.transform.rotation = Quaternion.identity;
            productMesh.transform.rotation = Quaternion.identity;
        }

        public void Slice()
        {
            gameObject.transform.rotation = Quaternion.identity;
            productMesh.transform.rotation = Quaternion.identity;

            sliceProgress += 1f;

            if (sliceProgress >= maxSliceCount)
            {
                OnSliced?.Invoke(slicedProductPrefab);
                Destroy(progressBarGO);
            }

            if(!progressBar.displaying)
            {
                progressBar.SetupProgressBar();
            }

            progressBar.SetProgress(sliceProgress/maxSliceCount);
        }

        public void ResetSliceBar()
        {
            progressBar.ResetProgressBar(transform);
        }

        public GameObject GetSlicedPrefab() => slicedProductPrefab;
    }
}