using Zenject;
using UnityEngine;

public class GameSceneInstaller : MonoInstaller {
    [SerializeField] private OrderManager orderManager;
    [SerializeField] private OrderUIManager orderUIManager;

    public override void InstallBindings() {
        Container.Bind<OrderManager>()
            .FromInstance(orderManager)
            .AsSingle();

        Container.Bind<OrderUIManager>()
            .FromInstance(orderUIManager)
            .AsSingle();

        Container.Bind<Stove>().AsSingle();
        Container.Bind<Fryer>().AsSingle();
    }
}