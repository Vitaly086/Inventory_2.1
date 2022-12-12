namespace Inventory_2._1;

public class Program
{
    static void Main(string[] args)
    {
        // 1. Создаем компанию:
        var company = new Company();

        // 2. Создаем инвентарь:
        var table = new Furniture("Стол", "Белый");
        var chair = new Furniture("Стул", "Коричневый");
        var laptop = new Technique("Ноутбук", "Asus");
        var printer = new Technique("Принтер", "Canon");
        var phone = new Technique("Телефон", "Nokia");

        // 3. Добавляем инвентарь в компанию:
        company.AddInventories(table, chair, laptop, printer, phone);

        // 4. Создаем сотрудников:
        var firstEmployee = new Employee("Владимир", "Директор");
        var secondEmployee = new Employee("Виктор", "Инженер");
        var thirdEmployee = new Employee("Дмитрий", "Менеджер");
        
        // 5. Добавляем сотрудников в компанию:
        company.AddEmployees(firstEmployee, secondEmployee, thirdEmployee);

        // 6. Выдаем инвентарь сотрудникам:
        company.GiveInventoryToEmployee(firstEmployee, table, phone);
        company.GiveInventoryToEmployee(secondEmployee, chair);
        company.GiveInventoryToEmployee(thirdEmployee, laptop, printer);
   
        // 7. Проводим инвентаризацию:
        Console.WriteLine();
        Console.WriteLine("Инвентаризация");
        company.MakeInventory();
    }
}

/// <summary>
/// Класс Компания содержит:
/// Два приватных поля: склад, динамический массив сотрудников.
/// Четыре публичных метода:
/// Добавить сотрудников в список сотрудников.
/// Добавить инвентарь на склад.
/// Выдать инвентарь сотруднику.
/// Провести инвентаризацию.
/// </summary>

// Класс Компания.
public class Company
{
    // Поле Склад.
    private Storage _storage = new Storage();
    // Лист сотрудников компании.
    private List<Employee> _employees = new List<Employee>();
    
    
    // Метод добавляет сотрудников в лист сотрудников компании.
    public void AddEmployees(params Employee[] employees)
    {
        foreach (var employee in employees)
        {
            // Добавляем сотрудника в динамический массив сотрудников.
            _employees.Add(employee);
        }
    }
   
    // Метод добавляет инвентарь на склад.
    public void AddInventories(params Inventory[] inventories)
    {
        foreach (var inventory in inventories)
        {
            // Добавляем инвентарь на склад.
            _storage.AddInventory(inventory);
        }
    }

    // Метод выдает инвентарь сотруднику и удаляет его со склада.
    public void GiveInventoryToEmployee(Employee employee, params Inventory[] inventories)
    {
        foreach (var inventory in inventories)
        {
            // Добавляем инвентарь сотруднику.
            employee.AddInventory(inventory);
            // Удаляем инвентарь со склада.
            _storage.RemoveInventory(inventory);
        }
    }
    
    // Метод проводит инвентаризацию предметов у сотрудников.
    public void MakeInventory()
    {
        foreach (var employee in _employees)
        {
            // Выводим на экран сотрудника и его должность.
            Console.WriteLine(
                $"Сотрудник {employee.Name}, должность {employee.JobTitle}, получил следующие предметы: ");
            // Выводим на экран предметы выданные сотруднику.
            employee.PrintInventory();
            Console.WriteLine();
        }
    }
}

/// <summary>
/// Класс Склад содержит: 
/// Два приватных поля: динамический массив с инвентарем и целочисленное значение инвентарного номера.
/// Два публичных метода:
/// Добавить инвентарь, удалить инвентарь.
/// </summary>

// Класс Склад
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
   
    // Метод удаляет инвентарь со склада.
    public void RemoveInventory(Inventory inventory)
    {
        // Удаляем инвентарь с динамического массива.
        _inventories.Remove(inventory);
    }
}

/// <summary>
/// Класс Сотрудник содержит:
/// Два публичных свойства с геттером: Name и JobTile.
/// Приватный динамический массив предметов сотрудника.
/// Публичный конструктор, принимающий и сохраняющий в свойства: имя и должность сотрудника.
/// Два публичных метода:
/// Добавить инвентарь сотруднику.
/// Вывести на экран весь инвентарь сотрудника.
/// </summary>

// Класс Сотрудник.
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

    // Метод выводит на экран весь выданный инвентарь данного сотрудника.
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
}

/// <summary>
/// Абстрактный класс Инвентарь содержит:
/// Защищеное свойство Инвентарный номер с геттером и приватным сеттером.
/// Приватное поле название предмета.
/// Защищеный конструктор класса, принимающий и сохраняющий в поле название предмета.
/// Публичный метод присваивающий инвентарный метод предмету.
/// Публичный виртуальный метод, выводящий на экран базовую информацию о предмете.
/// </summary>

// Абстрактный класс Инвентарь.
public abstract class Inventory
{
    // Свойство Инвентарный номер.
    protected int InventoryNumber { get; private set; }
    // Поле название инвентаря. 
    private string _name;

    // Конструктор класса Инвентарь, принимающий и сохраняющий в поле название инвентаря.
    protected Inventory(string name)
    {
        _name = name;
    }

    // Метод записывает инвентарный номер.
    public void SetInventoryNumber(int inventoryNumber)
    {
        InventoryNumber = inventoryNumber;
    }

    // Виртуальный метод выводит базовую информацию об инвентаре.
    public virtual void Print()
    {
        Console.Write($"Предмет: {_name}. ");
    }
}

/// <summary>
/// Класс Мебель наследуется от класса Инвентарь, содержит:
/// Приватное поле цвет.
/// Публичный конструктор, принимающий и сохраняющий в поле цвет.
/// Публичный переопределённый метод, вызывающий базовый метод класса и выводящий на экран информацию о цвете мебели.
/// </summary>

// Унаследованный класс Мебель от класса Инвентарь.
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
/// Класс Техника наследуется от класса Инвентарь, содержит:
/// Приватное поле модель.
/// Публичный конструктор, принимающий и сохраняющий в поле модель.
/// Публичный переопределённый метод, вызывающий базовый метод класса и выводящий на экран информацию о модели техники.
/// </summary>

// Унаследованный класс Техника от класса Инвентарь.
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