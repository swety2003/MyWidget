using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MyDesktopCards.Common
{
    internal abstract class SimpleVM:ObservableObject
    {
        public DispatcherTimer? _Timer { get; set; }

        public event EventHandler<bool>? OnActiveChanged;

        private bool _active;

        public bool Active
        {
            get { return _active; }
            set
            {
                _active = value;

                OnActiveChanged?.Invoke(this, value);
                
            }
        }


    }
}
