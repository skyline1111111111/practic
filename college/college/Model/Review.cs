namespace college.Model
{
    public class Review
    {
        public Review()
        {
            Date = DateTime.Now;
        }

        public int Id { get; set; }
        public int Rate { get; set; }
        private string comment;
        public string? Comment {
            get
            {
                return comment;    // возвращаем значение свойства
            }
            set
            {
                if (value != null)
                {
                    comment = value.Trim();   // устанавливаем новое значение свойства
                }
            }
        }
        public DateTime Date { get; set; }
    }
}
