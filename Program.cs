using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;

class multipleVal
{
    public DateTime time { get; set; }
    public DateTime date { get; set; }
    public string name { get; set; }
}


class Organizer
{
    private static List<multipleVal> MainList = new List<multipleVal>();
    private static List<string> defaultTask = new List<string> { "Review for Final Examination", "Finish Final Project in ITEC 102", "Finish Video Presentation in P.I 100", "Peform ZumbaForAll" };
    private static List<string> defaultDate = new List<string> { "December 31, 2023", "January 3, 2024", "January 5, 2024", "January 9, 2024" };
    private static List<string> defaultTime = new List<string> { "7:00am", "9:00am", "7:00am", "7:00am" };
    private static string[] defaultOption = { "<<<<     Back     >>>>", "<<<< Exit Program >>>>" };
    private static int indexSelector; private static ConsoleKeyInfo keyPressed;
    static void Main()
    {

        DateTime dateTime = DateTime.Now;
        indexSelector = 0;
        string[] menuOptions = { "<<<<  Add New Task  >>>>", "<<<< View Your Task >>>>", "<<<<   Edit tasks   >>>>", "<<<<    About Me    >>>>", "<<<<      Help      >>>>", "<<<<  Exit Program  >>>>" };
        do
        {
            Console.Clear();
            Console.WriteLine("+-------------------------------------+");
            Console.WriteLine("|        Welcome to OrganizeMe!       |");
            Console.WriteLine("+-------------------------------------+");
            Console.WriteLine("       What would you like to do?      ");
            Console.Write("\n");
            DisplayChoices(menuOptions, indexSelector);
            Console.Write("Time: {0}", dateTime.ToString("hh:mmtt"));
            Console.WriteLine("         Date: {0}", dateTime.ToString("MMM/dd/yyyy"));
            keyPressed = Console.ReadKey();
            switch (keyPressed.Key)
            {
                case ConsoleKey.UpArrow: indexSelector--; break;
                case ConsoleKey.DownArrow: indexSelector++; break;
            }

            if (indexSelector == menuOptions.Length) { indexSelector = 0; }
            else if (indexSelector < 0) { indexSelector = menuOptions.Length - 1; }
        }
        while (keyPressed.Key != ConsoleKey.Enter);

        switch (indexSelector)
        {
            case 0: NewTask(); break;
            case 1: ViewTask(); break;
            case 2: EditTask(); break;
            case 3: AboutMe(); break;
            case 4: Help(); break;
            case 5: ExitProg(); break;
        }
    }

    static void NewTask()
    {
        Console.Clear();
        string choice = " ";
        do
        {
            Console.WriteLine("+-------------------------------------+");
            Console.WriteLine("|             Add New Task            |");
            Console.WriteLine("+-------------------------------------+");

            Console.Write("\n");
            Console.Write("Enter the Title of the Task: ");
            string taskName = Console.ReadLine();

            Console.Write("\n");
            Console.Write("Enter the Deadline Date of the Task: ");
            DateTime taskDate = Convert.ToDateTime(Console.ReadLine());

            Console.Write("\n");
            Console.Write("Enter the Deadline Time of the Task: ");
            DateTime taskTime = Convert.ToDateTime(Console.ReadLine());

            Console.Write("\n");
            Console.Write("Do you want to add another task?(Y/N): ");
            choice = Console.ReadLine();
            Console.Write("\n");

            Console.Clear();

            multipleVal NewTask = new multipleVal
            {
                name = taskName,
                date = taskDate,
                time = taskTime
            };

            MainList.Add(NewTask);
        }
        while (choice == "Y" || choice == "y");
        
        Main(); 
    }

    static void ViewTask()
    {
        indexSelector = 0;
        MainList.Sort((task1, task2) => task1.date.CompareTo(task2.date));

        if (MainList.Count == 0)
        {
                indexSelector = 0;
            do
            {
                Console.Clear();
                PrintTask();
                Console.Write("\n");
                DisplayChoices(defaultOption, indexSelector);
                keyPressed = Console.ReadKey();
                switch (keyPressed.Key)
                {
                    case ConsoleKey.UpArrow: indexSelector--; break;
                    case ConsoleKey.DownArrow: indexSelector++; break;
                }

                if (indexSelector == defaultOption.Length) { indexSelector = 0; }
                else if (indexSelector < 0) { indexSelector = defaultOption.Length - 1; }
            }
            while (keyPressed.Key != ConsoleKey.Enter);

            if (indexSelector == 0) { Main(); }
            else if (indexSelector == 1) { ExitProg(); }
        }
        else if (MainList.Count > 0)
        {
            string[] nextPageOptions = { "<<<<  Next Page   >>>>", "<<<<     Back     >>>>", "<<<< Exit Program >>>>" };
            string[] nextPrevOptions = { "<<<<  Next Page   >>>>", "<<<<  Prev Page   >>>>", "<<<<     Back     >>>>", "<<<< Exit Program >>>>" };
            indexSelector = 0;
            do
            {
                Console.Clear();
                PrintTask();
                Console.Write("\n");
                DisplayChoices(nextPageOptions, indexSelector);
                keyPressed = Console.ReadKey();
                switch (keyPressed.Key)
                {
                    case ConsoleKey.UpArrow: indexSelector--; break;
                    case ConsoleKey.DownArrow: indexSelector++; break;
                }

                if (indexSelector == nextPageOptions.Length) { indexSelector = 0; }
                else if (indexSelector < 0) { indexSelector = nextPageOptions.Length - 1; }
            }
            while (keyPressed.Key != ConsoleKey.Enter);

            page:
            if (indexSelector == 0)
            {
                int newTaskCount = defaultTask.Count, repeat = 0, limit = 4;
                
                next:
                do
                {
                    Console.Clear();
                    PrintTask();
                    Console.Clear();
                    Console.WriteLine("+-------------------------------------+");
                    Console.WriteLine("|              Your Tasks             |");
                    Console.WriteLine("+-------------------------------------+");
                    Console.Write("\n");
                    
                    for (int i = repeat; i < MainList.Count && i < limit; i++)
                    {
                        if (i == 0 || i == 4 || i == 8 || i == 12 || i == 16 || i == 20 || i == 24 || i == 28) { Console.WriteLine("======================================="); }

                        colorCoding(i);

                        int taskCount = newTaskCount + i + 1;
                        Console.WriteLine($"Task Number {taskCount}");
                        Console.WriteLine("\tName: {0}", MainList[i].name);
                        Console.WriteLine("\tDate: {0}", MainList[i].date.ToString("MMMM dd, yyyy"));
                        Console.WriteLine("\tTime: {0}", MainList[i].time.ToString("hh:mmtt"));
                        Console.ResetColor();
                        Console.WriteLine("=======================================");
                    }
                    Console.Write("\n");
                    DisplayChoices(nextPrevOptions, indexSelector);
                    keyPressed = Console.ReadKey();
                    switch (keyPressed.Key)
                    {
                        case ConsoleKey.UpArrow: indexSelector--; break;
                        case ConsoleKey.DownArrow: indexSelector++; break;
                    }

                    if (indexSelector == nextPrevOptions.Length) { indexSelector = 0; }
                    else if (indexSelector < 0) { indexSelector = nextPrevOptions.Length - 1; }
                }
                while (keyPressed.Key != ConsoleKey.Enter);

                if (indexSelector == 0) { repeat += 4; limit += 4; goto next; }
                else if (indexSelector == 1)
                {
                    if (repeat < 4)
                    {
                        indexSelector = 0;
                        do
                        {
                            Console.Clear();
                            PrintTask();
                            Console.Write("\n");
                            DisplayChoices(nextPageOptions, indexSelector);
                            keyPressed = Console.ReadKey();
                            switch (keyPressed.Key)
                            {
                                case ConsoleKey.UpArrow: indexSelector--; break;
                                case ConsoleKey.DownArrow: indexSelector++; break;
                            }

                            if (indexSelector == nextPageOptions.Length) { indexSelector = 0; }
                            else if (indexSelector < 0) { indexSelector = nextPageOptions.Length - 1; }
                        }
                        while (keyPressed.Key != ConsoleKey.Enter);

                        if (indexSelector == 0) { goto page; }
                        else if (indexSelector == 1) { Main(); }
                        else if (indexSelector == 2) { ExitProg(); }

                    }
                    else
                    {
                        repeat -= 4;
                        limit -= 4;
                        goto next;
                    }
                }
                else if (indexSelector == 2) { Main();  }
                else if (indexSelector == 3) { ExitProg(); }// medyo buggy pa to irecheck mo dapat 
            }
            else if (indexSelector == 1) { Main();  }
            else if (indexSelector == 2) { ExitProg(); }
        }
        Console.ReadKey();
    }

    static void EditTask()
    {
        Console.Clear();
        indexSelector = 0;

        int editName = 0, editDate = 0, editTime = 0, removeTask = 0, returnTask = 0;
        string[] editOptions = { "<<<<   Task Name   >>>>", "<<<< Task Deadline >>>>", "<<<<   Task Time   >>>>", "<<<<  Remove Task  >>>>", "<<<<     Back      >>>>" };
        int totalTaskCount = MainList.Count + defaultTask.Count;
        do
        {
            Console.Clear();
            Console.WriteLine("+-------------------------------------+");
            Console.WriteLine("|      What Do You Want To Edit?      |");
            Console.WriteLine("+-------------------------------------+");
            Console.Write("\n");
            DisplayChoices(editOptions, indexSelector);
            keyPressed = Console.ReadKey();
            switch (keyPressed.Key)
            {
                case ConsoleKey.UpArrow: indexSelector--; break;
                case ConsoleKey.DownArrow: indexSelector++; break;
            }

            if (indexSelector == editOptions.Length) { indexSelector = 0; }
            else if (indexSelector < 0) { indexSelector = editOptions.Length - 1; }
        }
        while (keyPressed.Key != ConsoleKey.Enter);

        if (indexSelector == 0 && defaultTask.Count == 0 || indexSelector == 1 && defaultTask.Count == 0 || indexSelector == 2 && defaultTask.Count == 0 || indexSelector == 3 && defaultTask.Count == 0)
        {
            Console.WriteLine("You do not have any task.");

            do
            {
                DisplayChoices(editOptions, indexSelector);
                keyPressed = Console.ReadKey();
                switch (keyPressed.Key)
                {
                    case ConsoleKey.UpArrow: indexSelector--; break;
                    case ConsoleKey.DownArrow: indexSelector++; break;
                }

                if (indexSelector == editOptions.Length) { indexSelector = 0; }
                else if (indexSelector < 0) { indexSelector = editOptions.Length - 1; }
            }
            while (keyPressed.Key != ConsoleKey.Enter);
        }

        if (indexSelector == 0)
        {
            PrintTask();
            nonDefaultTask();
            errorTask:
            Console.Write("\nEnter the Task Number that You Want to edit: ");
            editName = Convert.ToInt32(Console.ReadLine());  

            if (editName > totalTaskCount || editName <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n<<< Invalid Input. Please Try Again >>>\n");
                Console.ResetColor();
                goto errorTask;
            }

            string Name = " ";
            if (editName > 4)
            {
                Console.Clear();
                Console.WriteLine("+-------------------------------------+");
                Console.WriteLine("|             Update Task             |");
                Console.WriteLine("+-------------------------------------+");

                Console.Write("\nEnter the New Task Name: ");
                Name = Console.ReadLine();
                
                MainList[editName - 5].name = Name;
                loop:
                Console.Write("\nSuccessfully Updated!! Press 2 to view your Tasks: ");
                returnTask = Convert.ToInt32(Console.ReadLine());

                if (returnTask == 2)
                {
                    ViewTask();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\n<<< Invalid Input. Please Try Again >>>\n");
                    Console.ResetColor();
                    goto loop;
                }
            }
            else if (editName > 0 && editName <= 4)
            {
                Console.Clear();

                defaultTask.RemoveAt(editName - 1);
                Console.WriteLine("+-------------------------------------+");
                Console.WriteLine("|             Update Task             |");
                Console.WriteLine("+-------------------------------------+");

                Console.Write("\nEnter the New Task Name: ");
                Name = Console.ReadLine();

                defaultTask.Insert(editName - 1, Name);
                loop1:
                Console.Write("\nSuccessfully Updated!! Press 2 to view your Tasks: ");
                returnTask = Convert.ToInt32(Console.ReadLine());

                if (returnTask == 2)
                {
                    ViewTask();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\n<<< Invalid Input. Please Try Again >>>\n");
                    Console.ResetColor();
                    goto loop1;
                }
            }
            
        }

        else if (indexSelector == 1)
        {
            DateTime Date = DateTime.Now.Date;
            PrintTask();
            nonDefaultTask();
            errorDate:
            Console.Write("\nEnter the task Number that You Want to Edit: ");
            editDate = Convert.ToInt32(Console.ReadLine());

            if (editDate > totalTaskCount || editDate <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n<<< Invalid Input. Please Try Again >>>\n");
                Console.ResetColor();
                goto errorDate;
            }

            if (editDate > 4)
            {
                Console.Clear();
                Console.WriteLine("+-------------------------------------+");
                Console.WriteLine("|             Update Task             |");
                Console.WriteLine("+-------------------------------------+");

                Console.Write("\nEnter the New Date of the Deadline: ");
                Date = Convert.ToDateTime(Console.ReadLine());

                MainList[editDate - 5].date = Date;

                loop:
                Console.Write("\nSuccessfully Updated!! Press 2 to view your Tasks: \n");
                returnTask = Convert.ToInt32(Console.ReadLine());

                if (returnTask == 2)
                {
                    ViewTask();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("<<< Invalid Input. Please Try Again >>>\n");
                    Console.ResetColor();
                    goto loop;
                }
            }
            else if (editDate > 0 && editDate <= 4)
            {
                Console.Clear();
                defaultDate.RemoveAt(editDate - 1);
                Console.WriteLine("+-------------------------------------+");
                Console.WriteLine("|             Update Task             |");
                Console.WriteLine("+-------------------------------------+");

                Console.Write("\nEnter the New Date of the Deadline: ");
                Date = Convert.ToDateTime(Console.ReadLine());

                defaultDate.Insert(editDate - 1, Date.ToString("MMMM dd, yyyy"));

                loop2:
                Console.Write("\nSuccessfully Updated!! Press 2 to view your Tasks: \n");
                returnTask = Convert.ToInt32(Console.ReadLine());

                if (returnTask == 2)
                {
                    ViewTask();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("<<< Invalid Input. Please Try Again >>>\n");
                    Console.ResetColor();
                    goto loop2;
                }
            }  
        }

        else if (indexSelector == 2)
        {
            DateTime Time = DateTime.Now;
            PrintTask();
            nonDefaultTask();
            errorTime:
            Console.Write("\nEnter the task Number that You Want to Edit: ");
            editTime = Convert.ToInt32(Console.ReadLine());

            if (editTime > totalTaskCount || editTime <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n<<< Invalid Input. Please Try Again >>>\n");
                Console.ResetColor();
                goto errorTime;
            }

            if (editTime > 4)
            {
                Console.Clear();
                Console.WriteLine("+-------------------------------------+");
                Console.WriteLine("|             Update Task             |");
                Console.WriteLine("+-------------------------------------+");

                Console.Write("\nEnter the New Time of the Deadline: ");
                Time = Convert.ToDateTime(Console.ReadLine());

                MainList[editTime - 5].time = Time;

                loop3:
                Console.Write("\nSuccessfully Updated!! Press 2 to view your Tasks: ");
                returnTask = Convert.ToInt32(Console.ReadLine());

                if (returnTask == 2)
                {
                    ViewTask();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("<<< Invalid Input. Please Try Again >>>\n");
                    Console.ResetColor();
                    goto loop3;
                }
            }

            else if (editTime > 0 || editTime <= 4)
            {
                Console.Clear();
                defaultTime.RemoveAt(editTime - 1);
                Console.WriteLine("+-------------------------------------+");
                Console.WriteLine("|             Update Task             |");
                Console.WriteLine("+-------------------------------------+");

                Console.Write("\nEnter the New Time of the Deadline: ");
                Time = Convert.ToDateTime(Console.ReadLine());

                defaultTime.Insert(editTime - 1, Time.ToString("hh:mmtt"));

                loop4:
                Console.Write("\nSuccessfully Updated!! Press 2 to view your Tasks: ");
                returnTask = Convert.ToInt32(Console.ReadLine());

                if (returnTask == 2)
                {
                    ViewTask();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("<<< Invalid Input. Please Try Again >>>\n");
                    Console.ResetColor();
                    goto loop4;
                }
            }   
        }

        else if (indexSelector == 3)
        {
            PrintTask();
            nonDefaultTask();
            errorRemove:
            Console.Write("\nEnter the task Number that You Want to Edit: ");
            removeTask = Convert.ToInt32(Console.ReadLine());
            
            if (removeTask > totalTaskCount || removeTask <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n<<< Invalid Input. Please Try Again >>>\n");
                Console.ResetColor();
                goto errorRemove;
            }

            if (removeTask > 4)
            {
                MainList.RemoveAt(removeTask - 5);

                loop4:
                Console.Write("\nSuccessfully Removed!! Press 2 to view your Tasks: ");
                returnTask = Convert.ToInt32(Console.ReadLine());

                if (returnTask == 2)
                {
                    ViewTask();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("<<< Invalid Input. Please Try Again >>>\n");
                    Console.ResetColor();
                    goto loop4;
                }
            }

            else if (removeTask > 0 || removeTask <= 4)
            {

                defaultTask.RemoveAt(removeTask - 1); defaultDate.RemoveAt(removeTask - 1); defaultTime.RemoveAt(removeTask - 1);

                loop5:
                Console.Write("\nSuccessfully Removed!! Press 2 to view your Tasks: ");
                returnTask = Convert.ToInt32(Console.ReadLine());

                if (returnTask == 2)
                {
                    ViewTask();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("<<< Invalid Input. Please Try Again >>>\n");
                    Console.ResetColor();
                    goto loop5;
                }
            }
        }

        else if (indexSelector == 4)
        {
            Console.Clear();
            Main();
        }

        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("<<< Invalid Input. Please Try Again >>>\n");
            Console.ResetColor();
        }
    }

    static void AboutMe()
    {
        Console.Clear();
        indexSelector = 0;
        do
        {
            Console.Clear();
            Console.WriteLine("+-------------------------------------+");
            Console.WriteLine("|             What Am I?              |");
            Console.WriteLine("+-------------------------------------+");
            Console.WriteLine("|                                     |");
            Console.WriteLine("| OrganizeMe is a Console Application |");
            Console.WriteLine("| created  by a  First  Year Computer |");
            Console.WriteLine("| Science student.  It is a tool that |");
            Console.WriteLine("| provides a user-friendly  interface |");
            Console.WriteLine("| through  console   application  for |");
            Console.WriteLine("| adding,  viewing,  organizing  task |");
            Console.WriteLine("| and many more. User is prompted  to |");
            Console.WriteLine("| enter the task  name, deadline, and |");
            Console.WriteLine("| also the time.  Then,  the  program |");
            Console.WriteLine("| will  automatically  organize  them |");
            Console.WriteLine("| based on  their corresponding dead- |");
            Console.WriteLine("| lines.  The  program  also includes |");
            Console.WriteLine("| color coded indicators  for each of |");
            Console.WriteLine("| the tasks.  This simple application |");
            Console.WriteLine("| aims to  provide a simple-to-handle |");
            Console.WriteLine("| program that will allow the user to |");
            Console.WriteLine("| manage and organize their tasks.    |");
            Console.WriteLine("|                                     |");
            Console.WriteLine("+-------------------------------------+");
            Console.Write("\n");
            DisplayChoices(defaultOption, indexSelector);
            keyPressed = Console.ReadKey();
            switch (keyPressed.Key)
            {
                case ConsoleKey.UpArrow: indexSelector--; break;
                case ConsoleKey.DownArrow: indexSelector++; break;
            }

            if (indexSelector == defaultOption.Length) { indexSelector = 0; }
            else if (indexSelector < 0) { indexSelector = defaultOption.Length - 1; }
        }
        while (keyPressed.Key != ConsoleKey.Enter);

        if (indexSelector == 0) { Main(); }
        else if (indexSelector == 1) { ExitProg(); }

        Console.ReadKey();
    }

    static void ExitProg()
    {
        Console.Clear();
        Console.WriteLine("+-------------------------------------+");
        Console.WriteLine("|           Goodbye, Buddy!           |");
        Console.WriteLine("+-------------------------------------+");
        Console.Write("|");
        Console.WriteLine("                                     |");
        Console.WriteLine("| > Thank You For Using OrganizeMe! < |");
        Console.WriteLine("|  > Hope To See You Again Soon! <    |");
        Console.Write("|");
        Console.WriteLine("                                     |");
        Console.WriteLine("+-------------------------------------+");
        Console.ReadKey();
    }


    static void PrintTask()
    {
        Console.Clear();
        Console.WriteLine("+-------------------------------------+");
        Console.WriteLine("|              Your Tasks             |");
        Console.WriteLine("+-------------------------------------+");
        Console.Write("\n");
        
        int taskCount = 0;
        for (int j = 0; j < defaultTask.Count; j++)
        {
            if (j == 0) { Console.WriteLine("======================================="); }
            if (j == 0) { Console.ForegroundColor = ConsoleColor.Red; }
            else if (j == 1) { Console.ForegroundColor = ConsoleColor.Yellow; }
            else if (j == 2) { Console.ForegroundColor = ConsoleColor.Yellow; }
            else if (j == 3) { Console.ForegroundColor = ConsoleColor.Green; }
            taskCount++;
            Console.WriteLine($"Task Number {taskCount}");
            Console.WriteLine("\tName: {0}", defaultTask[j]);
            for (int k = j; k == j; k++)
            {
                Console.WriteLine("\tDate: {0}", defaultDate[k]);
            }
            for (int z = j; z == j; z++)
            {
                Console.WriteLine("\tTime: {0}", defaultTime[z]);
            }
            Console.ResetColor();
            Console.WriteLine("=======================================");
        }  
    }

    static void DisplayChoices(string[] choices, int indexSelector)
    {
        for (int i = 0; i < choices.Length; i++)
        {
            if (i == indexSelector)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
            }

            Console.WriteLine($"\t{choices[i]}");
            Console.ResetColor();
        }
        Console.Write("\n");
        Console.WriteLine("+-------------------------------------+");
    }

    static void nonDefaultTask()
    {
        int nonDefaultTaskCount = defaultTask.Count;
        if (MainList.Count > 0)
        {
            for (int i = 0; i < MainList.Count; i++)
            {
                if ((MainList[i].date - DateTime.Now).Days <= 3)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if ((MainList[i].date - DateTime.Now).Days > 3 && (MainList[i].date - DateTime.Now).Days <= 7)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else if ((MainList[i].date - DateTime.Now).Days > 7)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                nonDefaultTaskCount++;
                Console.WriteLine($"Task Number {nonDefaultTaskCount}");
                Console.WriteLine("\tName: {0}", MainList[i].name);
                Console.WriteLine("\tDate: {0}", MainList[i].date.ToString("MMMM dd, yyyy"));
                Console.WriteLine("\tTime: {0}", MainList[i].time.ToString("hh:mmtt"));
                Console.ResetColor();
                Console.WriteLine("=======================================");
            }
        }
    }

    static void Help()
    {
        indexSelector = 0;
        do
        {
            Console.Clear();
            Console.WriteLine("+-------------------------------------+");
            Console.WriteLine("|       Color Coding Indicators       |");
            Console.WriteLine("+-------------------------------------+");
            Console.WriteLine("                                       ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  The Red  Color  indicates  that the  ");
            Console.WriteLine("  specific   task's  deadline  is/are  ");
            Console.WriteLine("  less than or  equal to three  days.  ");
            Console.WriteLine("                                       ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  The Yellow Color indicates that the  ");
            Console.WriteLine("  specific   task's  deadline  is/are  ");
            Console.WriteLine("  between  four  days  to seven days.  ");
            Console.WriteLine("                                       ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  The Green Color indicates  that the  ");
            Console.WriteLine("  specific   task's  deadline  is/are  ");
            Console.WriteLine("  more than seven days.                ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("                                       ");
            Console.WriteLine("+-------------------------------------+");
            Console.WriteLine("                                       ");
            DisplayChoices(defaultOption, indexSelector);
            keyPressed = Console.ReadKey();
            switch (keyPressed.Key)
            {
                case ConsoleKey.UpArrow: indexSelector--; break;
                case ConsoleKey.DownArrow: indexSelector++; break;
            }

            if (indexSelector == defaultOption.Length) { indexSelector = 0; }
            else if (indexSelector < 0) { indexSelector = defaultOption.Length - 1; }
        }
        while (keyPressed.Key != ConsoleKey.Enter);

        if (indexSelector == 0) { Main(); }
        else if (indexSelector == 1) { ExitProg(); }

        Console.ReadKey();
    }

    static void colorCoding(int i)
    {
        if ((MainList[i].date - DateTime.Now).Days <= 3)
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        else if ((MainList[i].date - DateTime.Now).Days > 3 && (MainList[i].date - DateTime.Now).Days <= 7)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        else if ((MainList[i].date - DateTime.Now).Days > 7)
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }
    }
}
