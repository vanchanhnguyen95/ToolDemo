using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenFileDialogueSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void BrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "txt files (*.txt)|*.txt",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
                txtResult.Text = await Task.Run(() => UploadFileAsync(textBox1.Text));
            }
        }

        public async Task<string> UploadFileAsync(string path)
        {
            try
            {
                HttpClient client = new HttpClient();
                // we need to send a request with multipart/form-data
                var multiForm = new MultipartFormDataContent();

                // add file and directly upload it
                FileStream fs = File.OpenRead(path);
                multiForm.Add(new StreamContent(fs), "files", Path.GetFileName(path));

                // send request to API
                var url = "https://localhost:5001/api/v1/FileSpeedProvider/UploadFile";
                var response = await client.PostAsync(url, multiForm);

                // Có lỗi khi gọi api Upload file
                if (!response.IsSuccessStatusCode)
                    return "Upload file error";

                var contents = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<ResponseData>(contents);

                // Có lỗi trả về từ phía api upload file
                if (responseData.Status != "true")
                    return responseData.Status;

                return responseData.Status;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }

        public class ResponseData
        {
            public string Status { get; set; }
            public string Data { get; set; }
            public string FilePath { get; set; }
            public string Message { get; set; }
           
        }

        private void BrowseMultipleButton_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Filter =
        "Images (*.BMP;*.JPG;*.GIF,*.PNG,*.TIFF)|*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF|" +
        "All files (*.*)|*.*";

            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.Title = "Select Photos";

            DialogResult dr = this.openFileDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                foreach (String file in openFileDialog1.FileNames)
                {
                    try
                    {
                        PictureBox imageControl = new PictureBox();
                        imageControl.Height = 400;
                        imageControl.Width = 400;

                        Image.GetThumbnailImageAbort myCallback =
                                new Image.GetThumbnailImageAbort(ThumbnailCallback);
                        Bitmap myBitmap = new Bitmap(file);
                        Image myThumbnail = myBitmap.GetThumbnailImage(300, 300,
                            myCallback, IntPtr.Zero);
                        imageControl.Image = myThumbnail;

                        PhotoGallary.Controls.Add(imageControl);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }
        public bool ThumbnailCallback()
        {
            return false;
        }
    }
}
