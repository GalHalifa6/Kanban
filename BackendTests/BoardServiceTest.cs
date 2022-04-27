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
    ///<summary>
    ///This function test Requirement 12
    ///</summary>
    public void addTaskTest()
    {
        BoardService board = new BoardService();
        string jsonResponse = board.addTask("Task1", "Testing task1");
        Response res = JsonSerializer.Deserialize<Response>(jsonResponse);
        Console.WriteLine(res);
        
    }

    ///<summary>
    ///This function test Requirement 9
    ///</summary>
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

    ///<summary>
    ///This function test Requirement 14,15
    ///</summary>
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

    ///<summary>
    ///This function test Requirement 14,15
    ///</summary>
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

    ///<summary>
    ///This function test Requirement 14,15
    ///</summary>
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

    ///<summary>
    ///This function test Requirement 9
    ///</summary>
    public void addBoardTest()
    {
        BoardService board = new BoardService();
        string jsonResponse = board.addBoard("Board1");
        Response res = JsonSerializer.Deserialize<Response>(jsonResponse);
        Console.WriteLine(res);
      
        string jsonResponse1 = board.addBoard("Board1");
        Response res1 = JsonSerializer.Deserialize<Response>(jsonResponse);
        Console.WriteLine("The following test should failed:");
        Console.WriteLine(res1);
    }

    ///<summary>
    ///This function test Requirement 9
    ///</summary>
    public void removeBoardTest()
    {
        BoardService board = new BoardService();
        board.addBoard("Board1");
        string jsonResponse = board.removeBoard("Board1");
        Response res = JsonSerializer.Deserialize<Response>(jsonResponse);
        Console.WriteLine(res);

        string jsonResponse1 = board.removeBoard("Board1");
        Response res1 = JsonSerializer.Deserialize<Response>(jsonResponse);
        Console.WriteLine("The following test should failed:");
        Console.WriteLine(res1);
    }

    ///<summary>
    ///This function test Requirement 13
    ///</summary>
    public void advanceTaskPhaseTest(string title)
    {
        BoardService board = new BoardService();
        board.addTask("Task1", "Testing task1");
        string jsonResponse1 = board.advanceTask("Task1");
        Response res = JsonSerializer.Deserialize<Response>(jsonResponse1);
        Console.WriteLine(res);


        string jsonResponse2 = board.advanceTask("Task2");
        Console.WriteLine("The following test should failed:");
        Response res2 = JsonSerializer.Deserialize<Response>(jsonResponse2);
        Console.WriteLine(res2);

        board.advanceTask("Task1");
        string jsonResponse3 = board.advanceTask("Task1");
        Response res3 = JsonSerializer.Deserialize<Response>(jsonResponse3);
        Console.WriteLine("The following test should failed:");
        Console.WriteLine(res3);




    }
}