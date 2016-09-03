using System.Collections.Generic;

using Crux.Models;
using Crux.Mvvm;

using Reactive.Bindings;

namespace Crux.ViewModels.Search
{
    public class SearchBaseViewModel : ViewModel
    {
        public IEnumerable<LiveSort> SortTypes => LiveSort.Sorts;

        public ReactiveProperty<int> SelectedIndex { get; }

        protected LiveSort SelectedItem => LiveSort.Sorts[SelectedIndex.Value];

        protected SearchBaseViewModel()
        {
            SelectedIndex = new ReactiveProperty<int>(0);
        }
    }
}