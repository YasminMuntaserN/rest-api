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

            await GetPassedStudents();

            await GetAverageGrade();

            await GetStudentById(2);


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

        static async Task GetPassedStudents()
        {
            try
            {
                Console.WriteLine("\n_____________________________");
                Console.WriteLine("\nFetching passed students...\n");
                var response = await httpClient.GetAsync("Passed");

                if (response.IsSuccessStatusCode)
                {
                    var passedStudents = await response.Content.ReadFromJsonAsync<List<Student>>();
                    if (passedStudents != null && passedStudents.Count > 0)
                    {
                        foreach (var student in passedStudents)
                        {
                            Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, Age: {student.Age}, Grade: {student.Grade}");
                        }
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("No passed students found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static async Task GetAverageGrade()
        {
            try
            {
                Console.WriteLine("\n_____________________________");
                Console.WriteLine("\nFetching average grade...\n");
                var response = await httpClient.GetAsync("AverageGrade");

                if (response.IsSuccessStatusCode)
                {
                    var averageGrade = await response.Content.ReadFromJsonAsync<double>();
                    Console.WriteLine($"Average Grade: {averageGrade}");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("No students found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static async Task GetStudentById(int id)
        {
            try
            {
                Console.WriteLine("\n_____________________________");
                Console.WriteLine($"\nFetching student with ID {id}...\n");

                var response = await httpClient.GetAsync($"{id}");

                if (response.IsSuccessStatusCode)
                {
                    var student = await response.Content.ReadFromJsonAsync<Student>();
                    if (student != null)
                    {
                        Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, Age: {student.Age}, Grade: {student.Grade}");
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Console.WriteLine($"Bad Request: Not accepted ID {id}");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"Not Found: Student with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

    }   
}
