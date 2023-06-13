namespace CodebridgeTest.Domain.Entities;

public class Dog : BaseEntity
{
    private const string NegativeValueExceptionMessage = "Value cannot be negative!";
    private const string EmptyValueExceptionMessage = "Value cannot be empty!";
    
    public string Name { get; set; }
    public string Color { get; set; }
    public int TailLength { get; set; }
    public int Weight { get; set; }

    public Dog(string name, string color, int tailLength, int weight):
        this(0 , name, color, tailLength, weight)
    {
        if(string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(EmptyValueExceptionMessage, nameof(name));
        
        if(string.IsNullOrWhiteSpace(color))
            throw new ArgumentException(EmptyValueExceptionMessage, nameof(color));
        
        if (tailLength < 0)
            throw new ArgumentException(NegativeValueExceptionMessage, nameof(tailLength));
        
        if (weight < 0)
            throw new ArgumentException(NegativeValueExceptionMessage, nameof(weight));
    }

    private Dog(int id, string name, string color, int tailLength, int weight)
    {
        Id = id;
        Name = name;
        Color = color;
        TailLength = tailLength;
        Weight = weight;
    }
}