namespace assignment_4.Models
{
    public class Post
    {
        public int Id { get; set; }
     
        public string Title { get; set;  }
        public string Summary { get; set;  }
        public string Content { get; set;  }
        
        public string Time { get; set;  }
        
        public string OwnerId { get; set;  }
        public ApplicationUser Owner { get; set;  }
    }
}