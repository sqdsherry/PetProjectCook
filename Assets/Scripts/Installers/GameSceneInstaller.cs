using Zenject;
using UnityEngine;

public class GameSceneInstaller : MonoInstaller {
    [SerializeField] private OrderManager orderManager;
    [SerializeField] private OrderUIManager orderUIManager;
    [SerializeField] private RecipeDatabaseSO recipeDatabase;

    public override void InstallBindings() {
        Container.Bind<OrderManager>()
            .FromInstance(orderManager)
            .AsSingle();

        Container.Bind<OrderUIManager>()
            .FromInstance(orderUIManager)
            .AsSingle();

        Container.BindFactory<FoodTypeSO, FoodItem, FoodItem.Factory>();

        Container.BindFactory<GameObject, FoodItemWorld, FoodItemWorldFactory>()
                     .FromFactory<FoodItemWorldFactory>();

        Container.BindInstance(recipeDatabase);
        Container.Bind<RecipeMatcher>().AsSingle();

        Container.Bind<Stove>().AsSingle();
        Container.Bind<Fryer>().AsSingle();

        Container.Bind<ItemDropper>().AsSingle();
    }
}