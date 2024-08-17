using System.Net.Http.Json;

namespace StudentApiClient
{
    class program
    {
        static readonly HttpClient httpClient = new HttpClient();

        static async Task Main(string[] args)
        {
            httpClient.BaseAddress = new Uri("https://localhost:7255/api/StudentApi/");

            await GetAllStudents();

        }

        static async Task GetAllStudents()
        {
            try
            {
                Console.WriteLine("\n---------------------\n");
                Console.WriteLine("\nFetching All Students .... \n");
                var students = await httpClient.GetFromJsonAsync<List<Student>>("All");
                if (students != null)
                {
                    foreach (var student in students)
                    {
                        Console.WriteLine($"ID:{student.Id} ,Name: {student.Name} ,Age: {student.Age} ,Grade: {student.Grade}");

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An Error Accuord :" + ex.Message);
            }
        }

    }   
}
