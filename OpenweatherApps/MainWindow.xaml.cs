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
            //forecast = null;
            client = new HttpClient();
            InitializeComponent();
            //getForecast();
            getClientHTTP();

        }

        private async void btnSubmitKota_Click(object sender, RoutedEventArgs e)
        {

            //String isi = await getTextKota();
            if (textBox.Text != null)
            {
                //await getForecast();
                //setKota();
                //String isi = await getTextKota();
                //         string isi = "id " + forecast.id.ToString() + "\n name : " + forecast.name.ToString()
                //+ "\n cod : " + forecast.cod.ToString() + "\ntemp: " + kelvinToCelcius(forecast.main.temp) + " Celcius";

                string kotaget = textBox.Text;
                string dariGet = await getTextKota2(kotaget);
                //Task.Run(() => getTextKota(kotaget))
                //textBlock.Text = isi;
                textBlock.Text = dariGet;

                imageWeather.Source = getImage(forecast.weather[0].icon);
            }
            else
            {
                textBlock.Text = "Masukkan dahulu nama kota Anda";
            }

        }
        private async Task<string> getTextKota(String kota)
        {
            await getForecast(kota).ConfigureAwait(false);
            return "id " + forecast.id.ToString() + "\n name : " + forecast.name.ToString()
       + "\n cod : " + forecast.cod.ToString() + "\ntemp: " + kelvinToCelcius(forecast.main.temp);

        }

        BitmapImage getImage(string iconPath)
        {
            var image = new Image();
            var imagePath = "http://openweathermap.org/img/w/" + iconPath + ".png";

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath, UriKind.Absolute);
            bitmap.EndInit();

            return bitmap;
        }

        string kelvinToCelcius(String kelvin)
        {

            Double kelvinInt = Convert.ToDouble(kelvin);

            Double celcius = kelvinInt - 273.15;

            return Convert.ToString(celcius);
        }

        async Task getForecast()
        {
            client.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/weather");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //string kota = "Malang";
            /* string kota = await getKota();*/
            string kota = "Malang";
            string key = "{YOUR API KEY}";
            string path = "?q=" + kota + "&APPID=" + key;

            try
            {
                //GET
                forecast = await getForecasteKotaAsync(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }



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
        async Task getForecast(String kota)
        {
            client.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/weather");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string key = "ec467b7061db8ad1aeeade9178c34a4f";
            string path = "?q=" + kota + "&APPID=" + key;

            try
            {
                //GET
                forecast = await getForecasteKotaAsync(path).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        async Task getClientHTTP()
        {
            client.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/weather");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        async Task getForecast2(string kotaPar)
        {
            string key = "YOUR KEY";
            string path = "?q=" + kotaPar + "&APPID=" + key;
            try
            {
                //GET
                forecast = await getForecasteKotaAsync(path).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        private async Task<string> getTextKota2(String kota)
        {
            await getForecast2(kota).ConfigureAwait(false);
            return "id " + forecast.id.ToString() + "\n name : " + forecast.name.ToString()
       + "\n cod : " + forecast.cod.ToString() + "\ntemp: " + kelvinToCelcius(forecast.main.temp);

        }
    }

}
