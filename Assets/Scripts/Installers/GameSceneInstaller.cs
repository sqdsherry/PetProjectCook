using Zenject;
using UnityEngine;

public class GameSceneInstaller : MonoInstaller {
    [SerializeField] private OrderManager orderManager;
    [SerializeField] private OrderUIManager orderUIManager;
    [SerializeField] private GameObject foodItemWorldPrefab;

    public override void InstallBindings() {
        Container.Bind<OrderManager>()
            .FromInstance(orderManager)
            .AsSingle();

        Container.Bind<OrderUIManager>()
            .FromInstance(orderUIManager)
            .AsSingle();

        Container.BindFactory<FoodItemWorld, FoodItemWorldFactory>()
            .FromComponentInNewPrefab(foodItemWorldPrefab);

        Container.Bind<Stove>().AsSingle();
        Container.Bind<Fryer>().AsSingle();
    }
}