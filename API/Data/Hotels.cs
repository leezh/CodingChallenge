namespace API.Data
{
    public interface IHotelSource
    {
        Task<List<Hotel>> GetHotels();
    }

    class HotelApiSource : IHotelSource
    {
        private class Response
        {
            public string name { get; set; }

            public int starRating { get; set; }

            public string resortCode { get; set; }
        }

        public async Task<List<Hotel>> GetHotels()
        {
            var HTTP = new HttpClient();
            var response = await HTTP.GetAsync("https://skizoominterviewchallenge.azurewebsites.net/api/hotels?code=7hnVjCOrnMYAA49pAy/sBnxf4OZZpv8j8fwQ4B8tSNyowaN4cfaKYQ==");
            var content = await response.Content.ReadFromJsonAsync<IEnumerable<Response>>();

            var result = new List<Hotel>();
            foreach (var data in content)
            {
                var hotel = new Hotel
                {
                    Name = data.name,
                    ResortCode = data.resortCode,
                    StarRating = data.starRating
                };
                result.Add(hotel);
            }

            return result;
        }
    }

    class ResidenzaApiSource : IHotelSource
    {
        private class Response
        {
            public string accommodation_Name { get; set; }

            public int star_Rating { get; set; }

            public string r_code { get; set; }
        }

        public async Task<List<Hotel>> GetHotels()
        {
            var HTTP = new HttpClient();
            var response = await HTTP.GetAsync("https://skizoominterviewchallenge.azurewebsites.net/api/residenza?code=En1OvN8w29jYh0BYv5ogeN2JVZt_zB8PZqTtpRK_PvB9AzFuTk3FYQ==");
            var content = await response.Content.ReadFromJsonAsync<IEnumerable<Response>>();

            var result = new List<Hotel>();
            foreach (var data in content)
            {
                var hotel = new Hotel
                {
                    Name = data.accommodation_Name,
                    ResortCode = data.r_code,
                    StarRating = data.star_Rating
                };
                result.Add(hotel);
            }

            return result;
        }
    }
}