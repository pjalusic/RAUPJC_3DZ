using System.ComponentModel.DataAnnotations;

namespace WebAplikacija.Models.TodoViewModels
{
    public class AddTodoViewModel
    {
        [Required]
        public string Text { get; set; }

        public AddTodoViewModel(string text)
        {
            this.Text = text;
        }

        public AddTodoViewModel()
        {
        }
    }
}
