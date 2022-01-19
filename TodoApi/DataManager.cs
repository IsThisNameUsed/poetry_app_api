using Microsoft.AspNetCore.Mvc;
using TodoApi.Controllers;
using TodoApi.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace TodoApi
{
    public class DataManager 
    {
        private static TodoItemsController controller;
        private static TodoContext context;

        public DataManager()
        {
           
        }

        public static void SetController(TodoItemsController controllerRef)
        {
            if (controller != null)
                return;

            controller = controllerRef;
            TodoContext contextRef = controller.GetContext();
            context = new TodoContext(contextRef.options);
            controller = new TodoItemsController(context);
            GetData();
        }

        public static bool IsReady()
        {
            return context != null;
        }

        public static async void MajDatabase()
        {
            Task<ActionResult<IEnumerable<TodoItemDTO>>> task = GetData();
            IEnumerable<TodoItemDTO> list = task.Result.Value;
            foreach (TodoItemDTO item in list)
            {
                Console.WriteLine(item.Poem + item.dateTime + "id=" + item.Id);
                DateTime datetime = Convert.ToDateTime(item.dateTime);
                TimeSpan timeSpan = DateTime.Now - datetime;
                Console.WriteLine(timeSpan.TotalSeconds);
                if(timeSpan.TotalSeconds > 20)
                {
                    IActionResult taskResult = await DeleteItem(item.Id);
                }
            }     
        }

        public static Task<IActionResult> DeleteItem(int id)
        {

            Task<IActionResult> task = controller.DeleteTodoItem(id);
            return task;
            
        }

        public static Task<ActionResult<IEnumerable<TodoItemDTO>>> GetData()
        {

            Task<ActionResult<IEnumerable<TodoItemDTO>>> task = controller.GetTodoItems();
            return task;
        }

        /*public static async void Start()
        {
            Task<Print> print1Task  = PrintMsg();
            Console.WriteLine("main1");
            Print print2 = await PrintMsg2();
            Print print1 = await print1Task;
            
        }
        public async static Task<Print> PrintMsg()
        {
            await Task.Delay(5000);
            Console.WriteLine("COUCOU");
            return new Print();
        }

        public async static Task<Print> PrintMsg2()
        {
            await Task.Delay(1000);
            Console.WriteLine("HEUUUUUU");
            return new Print();
        }
   

    public class Print
    {

    }*/
    }
}
