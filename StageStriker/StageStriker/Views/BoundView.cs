using Xamarin.Forms;

namespace StageStriker
{
    public abstract class BoundView<T> : ContentView where T : ViewModel
    {
        private T viewModel;

        public T ViewModel
        {
            get => viewModel;
            set
            {
                viewModel = value;
                BindingContext = viewModel;
            }
        }
    }
}