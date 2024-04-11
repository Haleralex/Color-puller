using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private PowerWeapon yourBehaviour;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(yourBehaviour).AsImplementedInterfaces();
    }
}
