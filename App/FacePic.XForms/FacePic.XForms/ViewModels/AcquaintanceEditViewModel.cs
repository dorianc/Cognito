using System.Threading.Tasks;
using Acquaint.Util;
using FormsToolkit;
using Xamarin.Forms;
using AutoMapper;
using Acquaint.Models;
using Plugin.Media.Abstractions;
using System;
using System.IO;

namespace Acquaint.XForms
{
	public class AcquaintanceEditViewModel : BaseNavigationViewModel
	{
		bool _IsNewAcquaintance;

		public AcquaintanceEditViewModel(Client acquaintance = null)
		{
			if (acquaintance == null)
			{
				Acquaintance = new Client();
				_IsNewAcquaintance = true;
			}
			else
			{
				Mapper.Initialize(cfg => cfg.CreateMap<Client, Client>());

				// Use AutoMapper to make a copy of the Acquaintance.
				// On the edit screen, we want to only deal with this copy until we're ready to save.
				// If we didn't make this copy, then the item would be updated instantaneously without saving, 
				// by virtue of the ObservableObject type that the Acquaint model inherits from.
				Acquaintance = Mapper.Map<Client>(acquaintance);
			}
		}

		public Client Acquaintance { private set; get; }

		Command _SaveAcquaintanceCommand;

		public Command SaveAcquaintanceCommand => _SaveAcquaintanceCommand ?? (_SaveAcquaintanceCommand = new Command(async () => await ExecuteSaveAcquaintanceCommand()));

		async Task ExecuteSaveAcquaintanceCommand()
		{
			if (string.IsNullOrWhiteSpace(Acquaintance.LastName) || string.IsNullOrWhiteSpace(Acquaintance.FirstName))
			{
				MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.DisplayAlert, new MessagingServiceAlert()
					{
						Title = "Invalid name!", 
						Message = "An acquaintance must have both a first and last name.",
						Cancel = "OK"
					});
				return;
			}

			if (!RequiredAddressFieldCombinationIsFilled)
			{
				MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.DisplayAlert, new MessagingServiceAlert()
					{
						Title = "Invalid address!", 
						Message = "You must enter either a street, city, and state combination, or a postal code.",
						Cancel = "OK"
					});
				return;
			}

			if (_IsNewAcquaintance)
			{
				MessagingService.Current.SendMessage<Client>(MessageKeys.AddAcquaintance, Acquaintance);
			}
			else 
			{
				MessagingService.Current.SendMessage<Client>(MessageKeys.UpdateAcquaintance, Acquaintance);
			}
			await PopAsync();
		}

        internal void SetAvatar(MediaFile file)
        {
            byte[] avatar = null;
            using (MemoryStream ms = new MemoryStream())
            {
                file.GetStream().CopyTo(ms);
                avatar = ms.ToArray();
            }

            Acquaintance.PhotoUrl = file.Path;
            Acquaintance.AvatarBase64 = Convert.ToBase64String(avatar);
        }

        bool RequiredAddressFieldCombinationIsFilled
		{
			get
			{
				if (!Acquaintance.Street.IsNullOrWhiteSpace() && !Acquaintance.City.IsNullOrWhiteSpace() && !Acquaintance.State.IsNullOrWhiteSpace())
				{
					return true;
				}

				if (!Acquaintance.PostalCode.IsNullOrWhiteSpace() && (Acquaintance.Street.IsNullOrWhiteSpace() || Acquaintance.City.IsNullOrWhiteSpace() || Acquaintance.State.IsNullOrWhiteSpace()))
				{
					return true;
				}

				if (Acquaintance.AddressString.IsNullOrWhiteSpace())
				{
					return true;
				}

				return false;
			}
		}
	}
}

