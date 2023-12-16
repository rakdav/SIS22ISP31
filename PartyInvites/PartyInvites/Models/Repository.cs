namespace PartyInvites.Models
{
    public static class Repository
    {
        private static string connection = "Server=(localdb)\\mssqllocaldb;Database=applicationdb;Trusted_Connection=True;";
        private static List<GuestResponse> responses = new();
        public static IEnumerable<GuestResponse> Response => responses;
        public static void AddResponse(GuestResponse response)
        {
                responses.Add(response);
        }
    }
}
