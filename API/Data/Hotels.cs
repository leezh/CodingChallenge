namespace API.Data
{
    public interface IHotelSource
    {
        Task<IEnumerable<Hotel>> GetHotels();
    }

    class HotelApiSource : IHotelSource
    {
        private readonly string url;

        public HotelApiSource(string url)
        {
            this.url = url;
        }

        public async Task<IEnumerable<Hotel>> GetHotels()
        {
            var HTTP = new HttpClient();
            var response = await HTTP.GetAsync(this.url);
            return await response.Content.ReadFromJsonAsync<IEnumerable<Hotel>>();
        }
    }

    class ResidenzaApiSource : IHotelSource
    {
        private readonly string url;

        public ResidenzaApiSource(string url)
        {
            this.url = url;
        }

        private class Response
        {
            public string accommodation_Name { get; set; }

            public int star_Rating { get; set; }

            public string r_code { get; set; }
        }

        public async Task<IEnumerable<Hotel>> GetHotels()
        {
            var HTTP = new HttpClient();
            var response = await HTTP.GetAsync(this.url);
            var content = await response.Content.ReadFromJsonAsync<IEnumerable<Response>>();
            return content.Select(data => new Hotel{
                    Name = data.accommodation_Name,
                    ResortCode = data.r_code,
                    StarRating = data.star_Rating
            });
        }
    }
}