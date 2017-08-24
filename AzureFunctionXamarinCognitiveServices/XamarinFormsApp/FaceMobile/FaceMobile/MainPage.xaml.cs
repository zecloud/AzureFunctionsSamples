using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Media;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace FaceMobile
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            CameraButton.Clicked += CameraButton_Clicked;
        }

        public static async Task performBlobOperation(string path)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=your_account_name_here;AccountKey=your_account_key_here");

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("mycontainer");

            // Create the container if it doesn't already exist.
            await container.CreateIfNotExistsAsync();

            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("myblob");

            // Create the "myblob" blob with the text "Hello, world!"
            //await blockBlob.UploadTextAsync("Hello, world!");
            await blockBlob.UploadFromByteArrayAsync();
        }


        private async void CameraButton_Clicked(object sender, EventArgs e)
        {
          
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
                return;

            await DisplayAlert("File Location", file.Path, "OK");

            PhotoImage.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
           
            //or:
            //image.Source = ImageSource.FromFile(file.Path);
            //image.Dispose();
        }
    }
}
