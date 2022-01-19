namespace TodoApi.Models
{
    public class PoemItem
    {
        public int Id { get; set; }
        public string? Poem { get; set; }

        public string? dateTime { get; set; }
    }


    public class TodoItemDTO
    {
        public int Id { get; set; }
        public string? Poem { get; set; }

        public string? dateTime { get; set; }
    }

}
