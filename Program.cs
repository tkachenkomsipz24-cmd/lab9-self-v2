using System.Text;
using Lab9SelfV2;

Console.OutputEncoding = Encoding.UTF8;

// ============================================================
// Колекція 1: List<int>
// ============================================================
Console.WriteLine("=== Колекція 1: List<int> ===");
var numbers = new List<int> { 5, 12, 7, 20, 3, 18 };
Console.WriteLine($"Вихідні дані: {string.Join(", ", numbers)}");

var squares = CollectionUtils.Map(numbers, n => n * n);
Console.WriteLine($"Map (квадрати): {string.Join(", ", squares)}");

var evens = CollectionUtils.Filter(numbers, n => n % 2 == 0);
Console.WriteLine($"Filter (парні): {string.Join(", ", evens)}");

int max = CollectionUtils.MaxBy(numbers, n => n);
Console.WriteLine($"MaxBy (найбільше): {max}");

int farthest = CollectionUtils.MaxBy(numbers, n => Math.Abs(n - 10));
Console.WriteLine($"MaxBy (найдальше від 10): {farthest}");

Console.Write("ForEach (числа через пробіл): ");
CollectionUtils.ForEach(numbers, n => Console.Write($"{n} "));
Console.WriteLine();

// ============================================================
// Колекція 2: string[] (масив)
// ============================================================
Console.WriteLine();
Console.WriteLine("=== Колекція 2: string[] ===");
string[] words = { "apple", "kiwi", "banana", "fig", "cherry" };
Console.WriteLine($"Вихідні дані: {string.Join(", ", words)}");

var upper = CollectionUtils.Map(words, w => w.ToUpper());
Console.WriteLine($"Map (верхній регістр): {string.Join(", ", upper)}");

var lengths = CollectionUtils.Map(words, w => w.Length); // string -> int
Console.WriteLine($"Map (довжини, string -> int): {string.Join(", ", lengths)}");

var longWords = CollectionUtils.Filter(words, w => w.Length > 4);
Console.WriteLine($"Filter (довжина > 4): {string.Join(", ", longWords)}");

string longest = CollectionUtils.MaxBy(words, w => w.Length);
Console.WriteLine($"MaxBy (найдовше слово): {longest}");

Console.WriteLine("ForEach (кожне слово з нового рядка):");
CollectionUtils.ForEach(words, w => Console.WriteLine($"  - {w}"));

// ============================================================
// Колекція 3: List<Product> (власний клас)
// ============================================================
Console.WriteLine();
Console.WriteLine("=== Колекція 3: List<Product> ===");
var products = new List<Product>
{
    new Product { Name = "Laptop",   Price = 25000 },
    new Product { Name = "Mouse",    Price = 500 },
    new Product { Name = "Keyboard", Price = 1500 }
};

var names = CollectionUtils.Map(products, p => p.Name);
Console.WriteLine($"Names: {string.Join(", ", names)}");

var mostExpensive = CollectionUtils.MaxBy(products, p => p.Price);
Console.WriteLine($"Most expensive: {mostExpensive.Name} - {mostExpensive.Price}");

var expensive = CollectionUtils.Filter(products, p => p.Price > 1000);
Console.WriteLine("Expensive products:");
CollectionUtils.ForEach(expensive, p => Console.WriteLine($"  - {p.Name}"));

Console.WriteLine("Усі продукти:");
CollectionUtils.ForEach(products, p => Console.WriteLine($"  {p.Name}: {p.Price} грн"));

// ============================================================
// Типи делегатів: Func, Predicate, Action; лямбда з замиканням
// ============================================================
Console.WriteLine();
Console.WriteLine("=== Делегати як змінні та замикання ===");

Func<Product, string> label = p => $"[{p.Name}]";
Predicate<Product> isCheap = p => p.Price < 1000;
Action<string> print = s => Console.WriteLine($"  > {s}");

CollectionUtils.ForEach(
    CollectionUtils.Map(CollectionUtils.Filter(products, isCheap), label),
    print);

decimal threshold = 1200; // змінна, захоплена лямбдою (замикання)
var aboveThreshold = CollectionUtils.Filter(products, p => p.Price > threshold);
Console.WriteLine($"Дорожче за {threshold}: {string.Join(", ", aboveThreshold)}");

// ============================================================
// Порівняння з LINQ
// ============================================================
Console.WriteLine();
Console.WriteLine("=== Порівняння з LINQ ===");
Console.WriteLine($"Map    == Select : {CollectionUtils.Map(products, p => p.Name).SequenceEqual(products.Select(p => p.Name))}");
Console.WriteLine($"Filter == Where  : {CollectionUtils.Filter(numbers, n => n % 2 == 0).SequenceEqual(numbers.Where(n => n % 2 == 0))}");
Console.WriteLine($"MaxBy  == MaxBy  : {object.ReferenceEquals(CollectionUtils.MaxBy(products, p => p.Price), products.MaxBy(p => p.Price))}");

// ============================================================
// Порожня колекція
// ============================================================
Console.WriteLine();
Console.WriteLine("=== Порожня колекція ===");
try
{
    CollectionUtils.MaxBy(new List<int>(), n => n);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"InvalidOperationException: {ex.Message}");
}
