using GameServices.Model.Map;

var map = new Map(48, 27);
Console.WriteLine(map.AsJson());

Console.ReadKey();


abstract class Creator
{
    public abstract IProduct FactoryMethod();

    public string SomeOperation()
    {
        // Call the factory method to create a Product object.
        var product = FactoryMethod();
        // Now, use the product.
        var result = "Creator: The same creator's code has just worked with "
            + product.Operation();

        return result;
    }
}

class ConcreteCreator1 : Creator
{
    public override IProduct FactoryMethod()
    {
        return new ConcreteProduct1();
    }
}

class ConcreteCreator2 : Creator
{
    public override IProduct FactoryMethod()
    {
        return new ConcreteProduct2();
    }
}

public interface IProduct
{
    string Operation();
}

class ConcreteProduct1 : IProduct
{
    public string Operation()
    {
        return "{Result of ConcreteProduct1}";
    }
}

class ConcreteProduct2 : IProduct
{
    public string Operation()
    {
        return "{Result of ConcreteProduct2}";
    }
}

class Client
{
    public void Main()
    {
        Console.WriteLine("App: Launched with the ConcreteCreator1.");
        ClientCode(new ConcreteCreator1());

        Console.WriteLine("");

        Console.WriteLine("App: Launched with the ConcreteCreator2.");
        ClientCode(new ConcreteCreator2());
    }

    // The client code works with an instance of a concrete creator, albeit
    // through its base interface. As long as the client keeps working with
    // the creator via the base interface, you can pass it any creator's
    // subclass.
    public void ClientCode(Creator creator)
    {
        // ...
        Console.WriteLine("Client: I'm not aware of the creator's class," +
            "but it still works.\n" + creator.SomeOperation());
        // ...
    }
}

class Program
{
    static void Main(string[] args)
    {
        new Client().Main();
    }
}