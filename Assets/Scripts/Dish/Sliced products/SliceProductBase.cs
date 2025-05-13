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

        private float sliceProgress = 0f;

        public delegate void SliceNotify(GameObject prefab);
        public event SliceNotify OnSliced;

        public void Slice()
        {
            sliceProgress += 1f;

            if (sliceProgress >= maxSliceCount)
            {
                OnSliced?.Invoke(slicedProductPrefab);
            }
        }

        public GameObject GetSlicedPrefab() => slicedProductPrefab;
    }
}