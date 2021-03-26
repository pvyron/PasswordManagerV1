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
        private IEventAggregator _events;

        public ShellViewModel(IEventAggregator events, IndexViewModel indexViewModel)
        {
            _events = events;
            _indexViewModel = indexViewModel;

            _events.Subscribe(this);
            ActivateItem(IoC.Get<LoginViewModel>());
        }

        public void Handle(LogOnEventModel message)
        {
            _indexViewModel = IoC.Get<IndexViewModel>();
            ActivateItem(_indexViewModel);
        }
        
        public async Task LoginScreen()
        {
            ActivateItem(IoC.Get<LoginViewModel>());
        }
    }
}
