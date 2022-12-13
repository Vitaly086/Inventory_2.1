public class Program
{
    static void Main(string[] args)
    {
        // Создаем компанию:
        var company = new Company();

        // Вызываем меню действий.
        ShowSelectionMenu(company);
    }

    // Метод показывающий меню действий.
    private static void ShowSelectionMenu(Company company)
    {
        // В бесконечном цикле будем принимать запросы и отвечать на них.
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Выберите нужный пункт меню:");
            Console.WriteLine("1 - Добавить инвентарь на склад компании.\n" +
                              "2 - Добавить сотрудника в компанию.\n" +
                              "3 - Выдать сотруднику инвентарь.\n" +
                              "4 - Провести инвентаризацию.\n");

            // Считываем выбор пользователя.
            var userChoice = Convert.ToInt32(Console.ReadLine());
            // Отвечаем на запрос.
            ProcessSelectedMenuAction((MainMenuChoice) userChoice, company);
        }
    }

    // Метод определяющий, какое действие выполнять.
    private static void ProcessSelectedMenuAction(MainMenuChoice userChoice, Company company)
    {
        // В зависимости от выбора пользователя - вызываем нужный метод.
        // В case для удобства чтения и понимания кода используем перечисление MainMenuChoice.
        switch (userChoice)
        {
            // Кейс добавления нового инвентаря.
            case MainMenuChoice.AddNewInventory:
                AddNewInventory(company);
                return;

            // Кейс добавления нового сотрудника.
            case MainMenuChoice.AddNewEmployee:
                AddNewEmployee(company);
                return;

            // Кейс выдачи инвентаря сотруднику.
            case MainMenuChoice.GiveInventoryToEmployee:
                GiveInventoryToEmployee(company);
                return;
            // Кейс проводит инвентаризацию.
            case MainMenuChoice.MakeInventory:
                company.MakeInventory();
                break;

            // Кейс по умолчанию, в случае неправильного ввода.
            default:
                Console.WriteLine($"Неверный запрос, повторите\n");
                break;
        }
    }

    // Метод считывает выбор пользователя и добавляет выбранному сотруднику - выбранный инвентарь.
    private static void GiveInventoryToEmployee(Company company)
    {
        // Проверяем, есть ли в компании сотрудники.
        if (!company.HasEmployees())
        {
            Console.WriteLine("В компании нет сотрудников, сначала добавьте сотрудника.");
            return;
        }

        // Проверяем, есть ли в компании инвентарь.
        if (!company.HasInventories())
        {
            Console.WriteLine("На складе нет предметов, сначала добавьте предмет.");
            return;
        }

        // Выбираем индекс инвентаря, который будем добавлять сотруднику.
        var employee = ChooseEmployee(company);
        // Выбираем индекс сотруднка, которому будем добавлять инвентарь.
        var inventory = ChooseInventory(company);

        // Выдаем инветарь сотруднику.
        company.GiveInventoryToEmployee(employee, inventory);
    }

    // Метод считывает выбор пользователя и добавляет нового сотрудника.
    private static void AddNewEmployee(Company company)
    {
        Console.WriteLine("Выберите нужный пункт меню:");
        Console.WriteLine("1 - Добавить нового сотрудника.\n" +
                          "2 - Вернуться обратно.\n");


        // Считываем выбор пользователя.
        var employeeChoice = (EmployeeChoice) Convert.ToInt32(Console.ReadLine());

        switch (employeeChoice)
        {
            // Кейс создания нового сотрудника.
            case EmployeeChoice.Employee:
                var employee = CreateEmployee();

                // Компании добавляем нового сотрудника.
                company.AddEmployee(employee);
                break;

            // Кейс выхода.
            case EmployeeChoice.Exit:
                return;

            // Кейс по умолчанию, в случае неправильного ввода.
            default:
                Console.WriteLine($"Неверный запрос.\n" +
                                  $"Возврат в основное меню.\n");
                break;
        }
    }

    // Метод считывает выбор пользователя и добавляет новый инвентарь.
    private static void AddNewInventory(Company company)
    {
        Console.WriteLine("Выберите нужный пункт меню:");
        Console.WriteLine("1 - Добавить Мебель.\n" +
                          "2 - Добавить Технику.\n" +
                          "3 - Вернуться обратно.\n");

        // Считываем выбор пользователя.
        var inventoryChoice = (InventoryChoice) Convert.ToInt32(Console.ReadLine());
        // Выходим, если пользователь выбрал даннйы кейс.
        if (inventoryChoice == InventoryChoice.Exit)
        {
            return;
        }

        // Создаем новый инвентарь.
        var inventory = CreateInventory(inventoryChoice);

        // Компании добавляем новый инвентарь.
        company.AddInventory(inventory);
    }

    // Метод создает новый инветарь.
    private static Inventory CreateInventory(InventoryChoice choice)
    {
        // Создаем пустую переменную типа Инвентарь, чтобы записать в нее новый ивентарь.
        Inventory inventory = null;
        switch (choice)
        {
            // Кейс создания инвентаря - Мебель.
            case InventoryChoice.Furniture:
                Console.WriteLine("Введите название мебели.");
                // Пользователь вводит имя мебели.
                var furnitureName = Console.ReadLine();
                Console.WriteLine("Введите цвет мебели.");
                // Пользователь вводит цвет мебели.
                var color = Console.ReadLine();
                // Создаем объект класса Мебель.
                inventory = new Furniture(furnitureName, color);
                break;

            // Кейс создания инвентаря - Техника.
            case InventoryChoice.Technique:
                Console.WriteLine("Введите название техники.");
                // Пользователь вводит имя техники.
                var techniqueName = Console.ReadLine();
                Console.WriteLine("Введите модель техники.");
                // Пользователь вводит модель техники.
                var model = Console.ReadLine();
                // Создаем объект класса Техника.
                inventory = new Technique(techniqueName, model);
                break;

            // Кейс по умолчанию, в случае неправильного ввода.
            default:
                Console.WriteLine($"Неверный запрос.\n" +
                                  $"Возврат в основное меню.\n");
                break;
        }

        // Возвращаем объект класса Инветарь.
        return inventory;
    }

    // Метод создает нового сотрудника.
    private static Employee CreateEmployee()
    {
        // Кейс создания нового сотрудника.
        Console.WriteLine("Введите имя сотрудника.");
        // Пользователь вводит имя сотрудника.
        var name = Console.ReadLine();
        Console.WriteLine("Введите должность сотрудника.");
        // Пользователь вводит должность сотрудника.
        var jomTitle = Console.ReadLine();
        // Создаем объект класса Сотрудник.
        var employee = new Employee(name, jomTitle);

        // Возвращаем объект класса Сотрудник.
        return employee;
    }

    // Метод выбирает сотрудника.
    private static Employee ChooseEmployee(Company company)
    {
        Console.WriteLine("Выберите сотрудника, которому выдадут предмет:");

        int employeeIndex;
        // Бесконечный цикл, для выбора индекса сотрудника.
        while (true)
        {
            // Выводим на экран всех сотрудников компании.
            company.PrintAllEmployees();
            // Пользователь вводит индекс, отнимаем 1, так как моссив начинается с 0 индекса.
            employeeIndex = Convert.ToInt32(Console.ReadLine()) - 1;

            // Проверяем, правильный ли индекс.
            if (company.IsRightEmployeeIndex(employeeIndex))
            {
                // Останавливаем цикл, если индекс правильный.
                break;
            }

            // В случае ошибки выводим сообщение на экран.
            Console.WriteLine("Номер сотрудника выбран не верно, выберите еще раз.");
        }

        // Возвращаем индекс сотрудника.
        return company.GetEmployee(employeeIndex);
    }

    // Метод выбирает инвентарь.
    private static Inventory ChooseInventory(Company company)
    {
        Console.WriteLine("Выберите предмет:");

        // Переменная для хранения индекса инвентаря.
        int inventoryIndex;
        // Бесконечный цикл, для выбора индекса инвентаря.
        while (true)
        {
            // Выводим весь инвентарь компании.
            company.PrintAllInventories();
            // Пользователь вводит индекс, отнимаем 1, так как моссив начинается с 0 индекса.
            inventoryIndex = Convert.ToInt32(Console.ReadLine()) - 1;
            // Проверяем, правильный ли индекс.
            if (company.IsRightInventoryIndex(inventoryIndex))
            {
                // Останавливаем цикл, если индекс правильный.
                break;
            }

            // В случае ошибки выводим сообщение на экран.
            Console.WriteLine("Номер предмета выбран не верно, выберите еще раз.");
        }

        // Возвращаем инвентарь.
        return company.GetInventory(inventoryIndex);
    }
}

/// <summary>
/// Enum с выбором основного меню.
/// </summary>
public enum MainMenuChoice
{
    AddNewInventory = 1,
    AddNewEmployee = 2,
    GiveInventoryToEmployee = 3,
    MakeInventory = 4
}

/// <summary>
/// Enum с выбором инвентаря.
/// </summary>
public enum InventoryChoice
{
    Furniture = 1,
    Technique = 2,
    Exit = 3
}

/// <summary>
/// Enum с выбором сотрудника.
/// </summary>
public enum EmployeeChoice
{
    Employee = 1,
    Exit = 2
}

/// <summary>
/// Класс Компания содержит склад и список сотрудников.
/// Выдает сотрудникам инвентарь, проводит инвентаризацию.
/// </summary>
public class Company
{
    // Поле Склад.
    private Storage _storage = new Storage();
    // Лист сотрудников компании.
    private List<Employee> _employees = new List<Employee>();


    // Метод добавляет сотрудников в лист сотрудников компании.
    public void AddEmployee(Employee employee)
    {
        // Добавляем сотрудника в динамический массив сотрудников.
        _employees.Add(employee);
    }

    // Метод добавляет инвентарь на склад.
    public void AddInventory(Inventory inventory)
    {
        // Добавляем инвентарь на склад.
        _storage.AddInventory(inventory);
    }

    // Метод выдает инвентарь сотруднику.
    public void GiveInventoryToEmployee(Employee employee, Inventory inventory)
    {
        // Добавляем инвентарь работнику.
        employee.AddInventory(inventory);
        // Выводим на экран данные сотрудника и предмета.
        Console.WriteLine($"Сотруднику {employee.Name} выдан предмет {inventory.Name}," +
                          $" инвентарный номер {inventory.InventoryNumber}.");
    }

    // Метод проводит инвентаризацию предметов у сотрудников.
    public void MakeInventory()
    {
        // Проверяем, есть ли сорудники в компании.
        if (!HasEmployees())
        {
            Console.WriteLine("В компании нет сотрудников, сначала добавьте сотрудника.");
            return;
        }

        foreach (var employee in _employees)
        {
            // Проверяем, выдан ли сотруднику предмет.
            if (!employee.HasInventory())
            {
                // Если не выдан выводим сообщение на экран.
                Console.WriteLine($"Сотрудник {employee.Name}, должность {employee.JobTitle}, предметы не получал.");
                // Пропускаем иттерацию в цикле.
                continue;
            }

            // Выводим на экран сотрудника и его должность.
            Console.WriteLine(
                $"Сотрудник {employee.Name}, должность {employee.JobTitle}, получил следующие предметы: ");
            // Выводим на экран предметы выданные сотруднику.
            employee.PrintInventory();
            Console.WriteLine();
        }
    }

    // Метод обертка, вызывает проверку у склада, есть ли инвентарь.
    public bool HasInventories()
    {
        return _storage.HasInventories();
    }

    // Метод проверяет, есть ли в компании сотрудники.
    public bool HasEmployees()
    {
        // Если сотрудников нет, выводим сообщение на экран.
        return _employees.Count != 0;
    }

    // Метод выводит на экран всех сотрудников компании.
    public void PrintAllEmployees()
    {
        // Проверяем наличие сотрудников в списке.
        if (!HasEmployees())
        {
            Console.WriteLine("Сотрудники отсутствуют в компании.");
        }

        for (var index = 0; index < _employees.Count; index++)
        {
            // Выводим на экран сотрудника и его должность.
            Console.WriteLine(
                $"{index + 1}. Сотрудник {_employees[index].Name} в должности {_employees[index].JobTitle}.");
        }
    }

    // Метод проверяет, есть ли такой индекс в массиве сотрудников.
    public bool IsRightEmployeeIndex(int employeeIndex)
    {
        return employeeIndex >= 0 && employeeIndex < _employees.Count;
    }

    // Метод обертка, вызывает метод печати всего инвентаря у склада. 
    public void PrintAllInventories()
    {
        _storage.PrintAllInventories();
    }

    // Метод проверяет, есть ли такой индекс в массиве сотрудников.
    public bool IsRightInventoryIndex(int inventoryIndex)
    {
        return _storage.IsRightInventoryIndex(inventoryIndex);
    }

    // Метод возвращает сотрудника.
    public Employee GetEmployee(int employeeIndex)
    {
        return _employees[employeeIndex];
    }

    // Метод обертка, вызывает метод получения инвентаря у склада. 
    public Inventory GetInventory(int inventoryIndex)
    {
        return _storage.GetInventory(inventoryIndex);
    }
}

/// <summary>
/// Класс Склад отвечает за хранение инвентаря.
/// </summary>
public class Storage
{
    // Динамический массив предметов инвентаря.
    private List<Inventory> _inventories = new List<Inventory>();
    // Инвентарный номер предметов.
    private int _inventoryNumber = 1;

    // Метод добавления инвентаря на склад.
    public void AddInventory(Inventory inventory)
    {
        // Записываем предмету его инвентарный номер.
        inventory.SetInventoryNumber(_inventoryNumber);
        // Добавляем предмет в динамический массив.
        _inventories.Add(inventory);
        // Увеличиваем инвентарный номер.
        _inventoryNumber++;
    }

    // Метод печает все предметы на складе.
    public void PrintAllInventories()
    {
        // Проверяем есть ли предметы на складе.
        if (!HasInventories())
        {
            Console.WriteLine("На складе нет предметов.");
        }

        for (var i = 0; i < _inventories.Count; i++)
        {
            // Выводим на экран предмет и его инвентарный номер.
            Console.WriteLine($"На складе находятся предметы:\n" +
                              $"{i + 1}. {_inventories[i].Name}," +
                              $" Инвентарный номер: {_inventories[i].InventoryNumber}.");
        }
    }

    // Метод проверяет, есть ли такой индекс в массиве инвентарь.
    public bool IsRightInventoryIndex(int inventoryIndex)
    {
        return inventoryIndex >= 0 && inventoryIndex < _inventories.Count;
    }

    // Метод проверяет, есть ли в инветарь на складе.
    public bool HasInventories()
    {
        return _inventories.Count != 0;
    }

    // Метод возвращает инвентарь со склада.
    public Inventory GetInventory(int inventoryIndex)
    {
        // Получаем инвентарь по индексу.
        var inventory = _inventories[inventoryIndex];
        // Удаляем инвентарь из массива.
        _inventories.RemoveAt(inventoryIndex);
        // Возвращаем инвентарь.
        return inventory;
    }
}

/// <summary>
/// Класс Сотрудник хранит список инвентаря сотрудника.
/// </summary>
public class Employee
{
    // Свойство Имя сотрудника.
    public string Name { get; }

    // Свойство Должность сотрудника.
    public string JobTitle { get; }

    // Динамический массив предметов сотрудника.
    private List<Inventory> _inventories = new List<Inventory>();

    // Конструктор класса Сотрудник, который принимает и сохраняет в поле класса - Имя и Должность.
    public Employee(string name, string jobTitle)
    {
        Name = name;
        JobTitle = jobTitle;
    }

    // Метод добавляет инвентарь сотруднику.
    public void AddInventory(Inventory inventory)
    {
        // Добавляем инвентарь в динамеческий массив.
        _inventories.Add(inventory);
    }

    // Метод выводит на экран выданный инвентарь сотрудника.
    public void PrintInventory()
    {
        for (var i = 0; i < _inventories.Count; i++)
        {
            // Вывод индекса для нумерации.
            Console.Write($"{i + 1}. ");
            // Выводим данные инвентаря.
            _inventories[i].Print();
        }
    }

    // Метод проверяет, есть ли у сотрудника инвентарь.
    public bool HasInventory()
    {
        return _inventories.Count > 0;
    }
}

/// <summary>
/// Абстрактный класс Инвентарь отвечает за базовые характеристики инвентаря.
/// Выводит общую информацию об инвентаре.
/// </summary>
public abstract class Inventory
{
    // Свойство Инвентарный номер.
    public int InventoryNumber { get; private set; }

    // Свойство Название инвентаря. 
    public string Name { get; }

    // Конструктор класса Инвентарь, принимающий и сохраняющий в свойство название инвентаря.
    protected Inventory(string name)
    {
        Name = name;
    }

    // Метод записывает инвентарный номер.
    public void SetInventoryNumber(int inventoryNumber)
    {
        InventoryNumber = inventoryNumber;
    }

    // Виртуальный метод выводит базовую информацию об инвентаре.
    public virtual void Print()
    {
        Console.Write($"Предмет: {Name}. ");
    }
}

/// <summary>
/// Класс Мебель содержит более подробную информацию об инвентаре типа Мебель.
/// </summary>
public class Furniture : Inventory
{
    // Поле цвет мебели.
    private string _color;

    // Конструктор класса Мебель, принимающий и сохраняющий цвет.
    public Furniture(string name, string color) : base(name)
    {
        _color = color;
    }

    // Переопределенный метод вызывает базовый метод класса и выводит дополнительную информацию о мебели.
    public override void Print()
    {
        // Вызов базового метода класса.
        base.Print();
        Console.WriteLine($"Цвет: {_color}, Инвентарный номер: {InventoryNumber}.");
    }
}

/// <summary>
/// Класс Техника содержит более подробную информацию об инвентаре типа Техника.
/// </summary>
public class Technique : Inventory
{
    // Поле модель техники.
    private string _model;

    // Конструктор класса Техника, принимающий и сохраняющий модель.
    public Technique(string name, string model) : base(name)
    {
        _model = model;
    }

    // Переопределенный метод вызывает базовый метод класса и выводит дополнительную информацию о технике.
    public override void Print()
    {
        // Вызов базового метода класса.
        base.Print();
        Console.WriteLine($"Модель: {_model}, Инвентарный номер: {InventoryNumber}.");
    }
}