using UnityEngine;
using UnityEngine.UI;

namespace UI.Plate
{
    public class PlateDisplay : MonoBehaviour
    {
        [SerializeField]
        private GameObject plateItemPrefab;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AddProduct(Sprite sprite)
        {
            var item = Instantiate(plateItemPrefab, transform);
            var itemImage = item.GetComponent<Image>();
            itemImage.sprite = sprite;
        }
    }
}