using Prism.Modularity;
using Prism.Regions;
using System.Linq;
using Module1.Views;
using Autofac;
using Prism.Autofac;

namespace Module1
{
    public class Module1Module : IModule
    {
        IRegionManager _regionManager;
        IContainer _container;
        ContainerBuilder _builder;
        public Module1Module(RegionManager regionManager, ContainerBuilder builder)
        {
            _regionManager = regionManager;
            _builder = builder;
        }
        public void Initialize()
        {
            
            _builder.RegisterTypeForNavigation<ViewA>();
        }
    }
}