using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Inputs>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerUiManager>().FromComponentInHierarchy().AsSingle();  
        Container.Bind<PlayerInventory>().FromComponentInHierarchy().AsSingle();  
        Container.Bind<QuestManager>().FromComponentInHierarchy().AsSingle();  
        Container.Bind<InteractionHandler>().FromComponentInHierarchy().AsSingle();  
    }
}