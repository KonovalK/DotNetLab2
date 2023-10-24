using System;
using System.Collections.Generic;
using System.Linq;

public static class StringExtensions
{
    public static string Reverse(this string input)
    {
        char[] charArray = input.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    public static int CountOccurrences(this string input, char character)
    {
        return input.Count(c => c == character);
    }
}

public static class ArrayExtensions
{
    public static int CountOccurrences<T>(this T[] array, T value)
    {
        return array.Count(x => x.Equals(value));
    }

    public static T[] Distinct<T>(this T[] array)
    {
        List<T> distinctList = new List<T>();

        foreach (T item in array)
        {
            if (!distinctList.Contains(item))
            {
                distinctList.Add(item);
            }
        }

        return distinctList.ToArray();
    }


}

public class ExtendedDictionaryElement<T, U, V>
{
    public T Key { get; }
    public U Value1 { get; }
    public V Value2 { get; }

    public ExtendedDictionaryElement(T key, U value1, V value2)
    {
        Key = key;
        Value1 = value1;
        Value2 = value2;
    }
}

public class ExtendedDictionary<T, U, V>
{
    private List<ExtendedDictionaryElement<T, U, V>> elements = new List<ExtendedDictionaryElement<T, U, V>>();

    public void AddElement(T key, U value1, V value2)
    {
        var element = new ExtendedDictionaryElement<T, U, V>(key, value1, value2);
        elements.Add(element);
    }

    public bool RemoveElement(T key)
    {
        var elementToRemove = elements.FirstOrDefault(e => e.Key.Equals(key));
        if (elementToRemove != null)
        {
            elements.Remove(elementToRemove);
            return true;
        }
        return false;
    }

    public bool ContainsKey(T key)
    {
        return elements.Any(e => e.Key.Equals(key));
    }

    public bool ContainsValue(U value1, V value2)
    {
        return elements.Any(e => e.Value1.Equals(value1) && e.Value2.Equals(value2));
    }

    public ExtendedDictionaryElement<T, U, V> this[T key]
    {
        get { return elements.FirstOrDefault(e => e.Key.Equals(key)); }
    }

    public int Count
    {
        get { return elements.Count; }
    }

    public IEnumerable<ExtendedDictionaryElement<T, U, V>> GetAllElements()
    {
        return elements;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        string str = "Друга лабка";
        Console.WriteLine($"Перевернутий рядок:  {str.Reverse()}");
        Console.WriteLine($"Кількість символів а у рядку:  {str.CountOccurrences('а')}");

        int[] intArray = { 1, 2, 2, 3, 3, 3 };
        Console.WriteLine($"Скільки раз значення 2 зустрічається в масиві:  {intArray.CountOccurrences(2)}");
        int[] distinctArray = intArray.Distinct();
        Console.WriteLine(string.Join(", ", distinctArray));

        var dictionary = new ExtendedDictionary<string, int, double>();
        dictionary.AddElement("key1", 10, 5.5);
        dictionary.AddElement("key2", 20, 3.0);

        if (dictionary.ContainsKey("key1"))
            Console.WriteLine($"Значення1: {dictionary["key1"].Value1}, Значення2: {dictionary["key1"].Value2}");

        Console.WriteLine($"Загальна кількість елементів: {dictionary.Count}");

        foreach (var element in dictionary.GetAllElements())
        {
            Console.WriteLine($"Ключ: {element.Key}, Значення1: {element.Value1}, Значення2: {element.Value2}");
        }
    }
}

