using FormFileSpeed.Constants;
using FormFileSpeed.Models;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormFileSpeed
{
    public partial class Form1 : Form
    {
        public string host_speed = ConfigurationManager.AppSettings["HOST_SPEED"];
        public string host_speed_local = ConfigurationManager.AppSettings["HOST_SPEED_LOCAL"];

        public Form1()
        {
            InitializeComponent();
        }

        private async void btnUpload_Click(object sender, EventArgs e)
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
                txtUpload.Text = openFileDialog1.FileName;
                // Hiển thị kết quả upload file
                this.richTxtUpload.Text = await Task.Run(() => UploadFileAsync(txtUpload.Text));
            }
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialogDowload = new OpenFileDialog
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

            if (dialogDowload.ShowDialog() == DialogResult.OK)
            {
                txtDowload.Text = dialogDowload.FileName;
            }
        }

        private async Task<string> UploadFileAsync(string path)
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
                var url = host_speed + LinkApi.API_SPEED_UPLOADFILE; //var url = "https://localhost:5001/api/v1/FileSpeedProvider/UploadFile";
                var response = await client.PostAsync(url, multiForm);

                // Có lỗi khi gọi api Upload file
                if (!response.IsSuccessStatusCode)
                {
                    return @"Có lỗi gọi đến service cập nhật từ liệu từ file Upload";
                }    

                var contents = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<ResponseData>(contents);

                // Có lỗi trả về từ phía api upload file
                if (responseData.Status != "true")
                {
                    return responseData.Message;
                }

                return @"Upload file thành công";

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }

        private async void btnDowload_Click(object sender, EventArgs e)
        {
            string path = txtDowload.Text.Trim();

            if(string.IsNullOrEmpty(path))
            {
                richTxtDowload.Text = @"Chưa chọn file cần dowload";
                return;
            }    

            try
            {
                HttpClient client = new HttpClient();
                // we need to send a request with multipart/form-data
                var multiForm = new MultipartFormDataContent();

                // add file and directly upload it
                FileStream fs = File.OpenRead(path);
                multiForm.Add(new StreamContent(fs), "files", Path.GetFileName(path));

                // send request to API
                var url = host_speed + LinkApi.API_SPEED_GETFILELISTSPEED;//var url = host_speed_local+ "/api/v1/FileSpeedProvider/GetFileListSpeed";

                var response = await client.PostAsync(url, multiForm);

                // Có lỗi khi gọi api Upload file
                if (!response.IsSuccessStatusCode)
                {
                    richTxtDowload.Text = @"Có lỗi khi gọi api Upload file";
                    return;
                }

                // Nội dung file
                var content = response.Content.ReadAsStringAsync().Result;

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.InitialDirectory = @"C:\";
                saveFileDialog1.Title = "Save text Files";
                //saveFileDialog1.CheckFileExists = true;
                saveFileDialog1.CheckPathExists = true;
                saveFileDialog1.DefaultExt = "txt";
                saveFileDialog1.Filter = "Text files (*.txt)|*.txt";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.FileName = Path.GetFileName(path).Replace(".txt", "") + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString()
                                    + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string pathName = saveFileDialog1.FileName;
                    StreamWriter sw = new StreamWriter(pathName);
                    sw.WriteLine(content);//content.Result: nội dung file
                    //sw.WriteLine(color);
                    sw.Close();

                    richTxtDowload.Text = @"Dowload file thành công";
                }
            }
            catch (Exception ex)
            {
                richTxtDowload.Text = ex.ToString();
            }        
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // Reset Upload group
            txtUpload.Text = string.Empty;
            richTxtUpload.Text = string.Empty;

            // Reset Dowload group
            txtDowload.Text = string.Empty;
            richTxtDowload.Text = string.Empty;
        }
    }
}
