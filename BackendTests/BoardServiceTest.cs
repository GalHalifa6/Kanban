using System;
using System.Globalization;
using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.BusinessLayer;
using System.Text.Json;

class BoardServiceTest
{
    
    public BoardServiceTest()
    {

    }

    public void addTaskTest()
    {
        BoardService board = new BoardService();
        string jsonResponse = board.addTask("Task1", "Testing task1");
        Response res = JsonSerializer.Deserialize<Response>(jsonResponse);
        Console.WriteLine(res);
        
    }

    public void removeTaskTest()
    {
        BoardService board = new BoardService();
        board.addTask("Task1", "Testing task1");
        string jsonResponse = board.removeTask("Task1");
        Response res = JsonSerializer.Deserialize<Response>(jsonResponse);
        Console.WriteLine(res);

        string jsonResponse1 = board.removeTask("Task2");
        Response res1 = JsonSerializer.Deserialize<Response>(jsonResponse);
        Console.WriteLine("The following test should failed:");
        Console.WriteLine(res1);
    }

    public void editTaskTest()
    {
        BoardService board = new BoardService();
        board.addTask("Task1", "Testing task1");
        string jsonResponse = board.editTaskTitle("Task1", "Task2");
        Response res = JsonSerializer.Deserialize<Response>(jsonResponse);
        Console.WriteLine(res);

        string jsonResponse1 = board.editTaskTitle("Task3", "Task4");
        Response res1 = JsonSerializer.Deserialize<Response>(jsonResponse);
        Console.WriteLine("The following test should failed:");
        Console.WriteLine(res1);
    }

    public void editTaskDescriptionTest()
    {
        BoardService board = new BoardService();
        board.addTask("Task1", "Testing task1");
        string jsonResponse = board.editTaskDescription("Task1","Testing new");
        Response res = JsonSerializer.Deserialize<Response>(jsonResponse);
        Console.WriteLine(res);

        string jsonResponse1 = board.editTaskDescription("Task3", "Testing New");
        Response res1 = JsonSerializer.Deserialize<Response>(jsonResponse);
        Console.WriteLine("The following test should failed:");
        Console.WriteLine(res1);
    }

    public void editTaskDueDateTest()
    {
        DateTime date = new DateTime();
        BoardService board = new BoardService();
        board.addTask("Task1", "Testing task1");
        string jsonResponse = board.editTaskDueDate("Task1", date);
        Response res = JsonSerializer.Deserialize<Response>(jsonResponse);
        Console.WriteLine(res);

        string jsonResponse1 = board.editTaskDueDate("Task3", date);
        Response res1 = JsonSerializer.Deserialize<Response>(jsonResponse);
        Console.WriteLine("The following test should failed:");
        Console.WriteLine(res1);
    }
    public void addBoardTest()
    {

    }
    public void removeBoardTest()
    {

    }

    public void advanceTaskPhaseTest(string title)
    {

    }
}