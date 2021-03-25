using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using PMDesktopUI.EventModels;

namespace PMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEventModel>
    {
        private IndexViewModel _indexViewModel;
        private SimpleContainer _container;
        private IEventAggregator _events;

        public ShellViewModel(IEventAggregator events, IndexViewModel indexViewModel, SimpleContainer container)
        {
            _events = events;
            _indexViewModel = indexViewModel;
            _container = container;

            _events.Subscribe(this);
            ActivateItem(_container.GetInstance<LoginViewModel>());
        }

        public void Handle(LogOnEventModel message)
        {
            _indexViewModel = _container.GetInstance<IndexViewModel>();
            ActivateItem(_indexViewModel);
        }
        
        public async Task LoginScreen()
        {
            ActivateItem(_container.GetInstance<LoginViewModel>());
        }
    }
}
