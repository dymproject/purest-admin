// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Prism.Mvvm;

namespace PurestAdmin.Wpf.Shared.CommonViewModels
{
    public class PuaPagedParams : BindableBase
    {
        public PuaPagedParams()
        {
            PageSize = 10;
            PageIndex = 1;
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
    }
}
