using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Threading.Tasks;
using System;

namespace Acquaint.XForms
{
    public partial class AcquaintanceDetailPage : ContentPage
    {
        protected AcquaintanceDetailViewModel ViewModel => BindingContext as AcquaintanceDetailViewModel;

        public AcquaintanceDetailPage()
        {
            InitializeComponent();
        }


    }
}

