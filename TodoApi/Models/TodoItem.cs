namespace TodoApi.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int IsComplete { get; set; }

        public String? secret { get; set; }
    }


    public class TodoItemDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int IsComplete { get; set; }
    }

}
