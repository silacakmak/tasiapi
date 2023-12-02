namespace tasiapi.Models
{
    public class User
    {
        public User(){
            tasinmazlar=new List<Tasinmaz>();
    }
        
            public int id { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string UserName { get; set; }
        
        public List<Tasinmaz> tasinmazlar { get; set; }

}
}

