namespace test_practica.Model
{
    public class Request
    {
        public int Id { get; set; }
        public string Special1 { get; set; }
        public string Special2 { get; set; }
        public string Special3 { get; set; }

        private string fullName;
        public string FullName {
            get
            {
                return fullName;    // возвращаем значение свойства
            }
            set
            {
                fullName = value.Trim();   // устанавливаем новое значение свойства
            }
        }
        public DateTime DateRequest { get; set; }
        public string Phone { get; set; }
        private string email;
        public string Email
        {
            get
            {
                return email;    // возвращаем значение свойства
            }
            set
            {
                email = value.Trim();   // устанавливаем новое значение свойства
            }
        }

        public Request()
        {
            DateRequest = DateTime.Now;
        }
    }


}
