using FFImageLoading.Forms;
using System.ComponentModel;
using Xamarin.Forms;
using System;
using Plugin.Media;

namespace Acquaint.XForms
{
    public partial class AcquaintanceEditPage : ContentPage
    {
		protected AcquaintanceEditViewModel ViewModel => BindingContext as AcquaintanceEditViewModel;

        public AcquaintanceEditPage()
        {
            InitializeComponent();

            if (Device.OS == TargetPlatform.iOS)
                Title = null; // because iOS already displays the previous page's title with the back button, we don't want to display it twice.

            var contactAvatar = this.FindByName<CachedImage>("ContactAvatar");
            contactAvatar.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => TakePhoto())
            });

        }

        private async void TakePhoto()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Avatars",
                Name = ViewModel.Acquaintance.Id,
                CompressionQuality = 50,
                AllowCropping = true,
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                SaveToAlbum = false
            });

            if (file == null)
            {
                return;
            }

            await DisplayAlert("File Location", file.Path, "OK");

            ViewModel.SetAvatar(file);
        }

        /// <summary>
        /// Ensures the state field has 2 characters at most.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The PropertyChangedEventArgs</param>
        void StateEntry_PropertyChanged (object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Text")
            {
                var entryCell = sender as EntryCell;

                string val = entryCell.Text;

				if (val != null)
				{

					if (val.Length > 2)
					{
						val = val.Remove(val.Length - 1);
					}

					entryCell.Text = val.ToUpperInvariant();
				}
            }
        }

        /// <summary>
        /// Ensures the zip code field has 5 characters at most.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The PropertyChangedEventArgs</param>
        void PostalCode_PropertyChanged (object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Text")
            {
                var entryCell = sender as EntryCell;

                string val = entryCell.Text;

				if (val != null && val.Length > 5)
                {
                    val = val.Remove(val.Length - 1);
                    entryCell.Text = val;
                }
            }
            
        }
    }
}

