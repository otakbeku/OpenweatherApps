using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OpenweatherApps
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HttpClient client;
        ForecastKota forecast;
        string kota;


        public MainWindow()
        {
            client = new HttpClient();
            InitializeComponent();
            getClientHTTP();

        }
        private async void btnSubmitKota_Click(object sender, RoutedEventArgs e)
        {

            if (textBoxKota.Text != null)
            {
                string kotaget = textBoxKota.Text;
                //string dariGet = await getTextKota(kotaget);

                await getForecast(kotaget);
                if (forecast != null)
                {
                    //Taruh pada Label
                    labelCloud2.Content = forecast.weather[0].description;
                    labelPressure2.Content = forecast.main.pressure + " hpa";
                    labelHumidity2.Content = forecast.main.humidity + "%";
                    labelSunrise2.Content = FromUnixTime(Convert.ToInt64(forecast.sys.sunrise));
                    labelSunset2.Content = FromUnixTime(Convert.ToInt64(forecast.sys.sunset));
                    labelCountry2.Content = forecast.sys.country;
                    labelSuhu.Content = kelvinToCelcius(forecast.main.temp) + " °C";

                    labelGetAt.Content = "Get At " + FromUnixTime(Convert.ToInt64(forecast.dt));

                    imageWeather.Source = getImage(forecast.weather[0].icon);
                }
                else
                {
                    MessageBox.Show("Terjadi kesalahan pada proses berjalannya aplikasi ini.\nPastikan Anda terhubung dengan internet atau kota yang Anda masukkan sudah benar ", "ERROR", MessageBoxButton.OK);
                }
            }
        }
        /// <summary>
        /// Method untuk men-set Image pada imageview berdasarkan gambar yang di per
        /// </summary>
        /// <param name="iconPath"></param>
        /// <returns></returns>
        BitmapImage getImage(string iconPath)
        {
            var image = new Image();
            var imagePath = "http://openweathermap.org/img/w/" + iconPath + ".png";

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath, UriKind.Absolute);
            bitmap.DecodePixelWidth = 165;
            bitmap.DecodePixelHeight = 84;
            bitmap.EndInit();

            return bitmap;
        }
        /// <summary>
        /// Mehtod ini digunakan untuk merubah dari satuan Imperial ke Metric
        /// </summary>
        /// <param name="kelvin"></param>
        /// <returns></returns>
        string kelvinToCelcius(String kelvin)
        {

            Double kelvinInt = Convert.ToDouble(kelvin);

            Double celcius = kelvinInt - 273.15;

            string number = Convert.ToString(celcius);
            if (number.Length >= 4)
            {
                number = number.Substring(0, 4);
            }

            return number;
        }
        /// <summary>
        /// Method ini digunakan untuk mengubah UNIX menjadi date time
        /// </summary>
        /// <param name="unixTime"></param>
        /// <returns></returns>
        public DateTime FromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }
        /// <summary>
        /// Method digunakan untuk mengambil dan membinding JSON dengan object forecast
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        async Task<ForecastKota> getForecasteKotaAsync(String path)
        {
            ForecastKota forecastKota = null;
            HttpResponseMessage respon = await client.GetAsync(path);
            if (respon.IsSuccessStatusCode)
            {
                forecastKota = await respon.Content.ReadAsAsync<ForecastKota>();
            }
            return forecastKota;
        }
        /// <summary>
        /// Method ini digunakan untuk membuat koneksi terlebih dahulu dengan server. Catatan: Hanya perlu sekali di panggil
        /// </summary>
        /// <returns></returns>
        async Task getClientHTTP()
        {
            client.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/weather");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        /// <summary>
        /// //Method ini digunakan untuk mengambil JSON dari Server. Disimpan dalam object forecast
        /// </summary>
        /// <param name="kotaPar"></param>
        /// <returns></returns>
        async Task getForecast(string kotaPar)
        {
            string key = "ec467b7061db8ad1aeeade9178c34a4f";
            string path = "?q=" + kotaPar + "&APPID=" + key;
            try
            {
                //GET
                forecast = await getForecasteKotaAsync(path).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MessageBox.Show("Terjadi kesalahan pada proses berjalannya aplikasi ini.\nUntuk info lebih lanjut silahkan mencari GALAT: " + e, "Error", MessageBoxButton.OK);

            }

        }
    }

}
