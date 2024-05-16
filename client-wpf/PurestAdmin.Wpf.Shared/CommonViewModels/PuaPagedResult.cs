// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System.Collections.ObjectModel;

using Prism.Mvvm;

namespace PurestAdmin.Wpf.Shared.CommonViewModels
{
    public class PuaPagedResult<T> : BindableBase where T : class
    {
        public PuaPagedResult()
        {
            PageIndex = 1;
            PageSize = 10;
        }

        private int _pageSize;

        public int PageSize
        {
            get { return _pageSize; }
            set { SetProperty(ref _pageSize, value); }
        }

        private int _pageIndex;

        public int PageIndex
        {
            get { return _pageIndex; }
            set { SetProperty(ref _pageIndex, value); }
        }

        private int _total;

        public int Total
        {
            get { return _total; }
            set { SetProperty(ref _total, value); }
        }

        private ObservableCollection<T> _items;

        public ObservableCollection<T> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

    }
}
