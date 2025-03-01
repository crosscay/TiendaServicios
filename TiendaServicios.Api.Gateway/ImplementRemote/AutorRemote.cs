using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TiendaServicios.Api.Gateway.InterfaceRemote;
using TiendaServicios.Api.Gateway.LibroRemote;

namespace TiendaServicios.Api.Gateway.ImplementRemote
{
    public class AutorRemote : IAutorRemote
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger<AutorRemote> _logger;

        public AutorRemote(IHttpClientFactory httpClient, ILogger<AutorRemote> logger) {
            _httpClient = httpClient;
            _logger = logger;
        }
        
        public async Task<(bool resultado, AutorModeloRemote autor, string ErrorMessage)> GetAutor(Guid AutorId)
        {
            try
            {
                var cliente = _httpClient.CreateClient("AutorService");
                var response = await cliente.GetAsync($"/Autor/{AutorId}");
                if (response.IsSuccessStatusCode) {
                    var contenido = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var resultado = JsonSerializer.Deserialize<AutorModeloRemote>(contenido, options);
                    return (true, resultado, null);
                }

                return (false, null, response.ReasonPhrase);
            }
            catch (Exception e) {
                _logger.LogError(e.ToString());
                return (false, null, e.Message);
            }

        }
    }
}
