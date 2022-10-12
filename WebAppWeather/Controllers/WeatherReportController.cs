using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebAppWeather.Models;
using WebAppWeather.Services.Interface;

namespace WebAppWeather.Controllers
{
    public class WeatherReportController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly ServerToken _token;

        public WeatherReportController(ITokenService tokenService, IOptions<ServerToken> token)
        {
            _tokenService = tokenService;
            _token = token.Value;
        }

        public async Task<IActionResult> Weather()
        {
            var token = await _tokenService.GetToken(_token.Token);
            using HttpClient client = new();
            //client.BaseAddress = new Uri("https://localhost:44370/weatherforecast");
            client.SetBearerToken(token.AccessToken);
            var response = await client.GetAsync("https://localhost:44370/weatherforecast");
            if (response.IsSuccessStatusCode)
            {
                var responseModel = await response.Content.ReadAsStringAsync();
                var weatherReport = JsonConvert.DeserializeObject<List<WeatherResponseModel>>(responseModel);
                return View(weatherReport);
            }
            throw new Exception("Failed to get Data from API");

        }
    }
}
