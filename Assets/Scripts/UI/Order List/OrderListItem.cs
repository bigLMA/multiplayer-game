using Dish.Recipe;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderListItem : MonoBehaviour
{
    [field: SerializeField]
    public TextMeshProUGUI orderName { get; private set; }

    [SerializeField]
    private GameObject productsList;

    [SerializeField]
    private GameObject productItemPrefab;

    public void AddOrder(RecipeData data)
    {
        orderName.text = data.recipeName;

        foreach(var prod in data.recipe)
        {
            var item = Instantiate(productItemPrefab, productsList.transform);
            var itemImage = item.GetComponent<Image>();
            itemImage.sprite = prod.image;
            item.transform.localScale *= 220f;
        }
    }
}
