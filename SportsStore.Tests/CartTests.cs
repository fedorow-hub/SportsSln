using SportsStore.Models;

namespace SportsStore.Tests;

public class CartTests
{
    [Fact]
    public void Can_Add_New_Lines()
    {
        //Arrange create products
        Product product1 = new Product { ProductID = 1, Name = "P1" };
        Product product2 = new Product { ProductID = 2, Name = "P2" };

        //Arrange create a new cart
        Cart target = new Cart();

        //Act
        target.AddItem(product1, 1);
        target.AddItem(product2, 1);
        CartLine[] results = target.Lines.ToArray();

        //Assert
        Assert.Equal(2, results.Length);
        Assert.Equal(product1, results[0].Product);
        Assert.Equal(product2, results[1].Product);
    }

    [Fact]
    public void Can_Add_Quantity_For_Existing_Lines()
    {
        //Arrange create products
        Product product1 = new Product { ProductID = 1, Name = "P1" };
        Product product2 = new Product { ProductID = 2, Name = "P2" };

        //Arrange create a new cart
        Cart target = new Cart();

        //Act
        target.AddItem(product1, 1);
        target.AddItem(product2, 1);
        target.AddItem(product1, 10);
        CartLine[] results = (target.Lines ?? new())
            .OrderBy(c => c.Product.ProductID).ToArray();

        // Assert
        Assert.Equal(2, results.Length);
        Assert.Equal(11, results[0].Quantity);
        Assert.Equal(1, results[1].Quantity);
    }

    [Fact]
    public void Can_Remove_Line()
    {
        //Arrange create some test products
        Product product1 = new Product { ProductID = 1, Name = "P1" };
        Product product2 = new Product { ProductID = 2, Name = "P2" };
        Product product3 = new Product { ProductID = 3, Name = "P3" };

        //Arrange create cart
        Cart target = new Cart();

        //Arrange add some product to the cart
        target.AddItem(product1, 1);
        target.AddItem(product2, 3);
        target.AddItem(product3, 5);
        target.AddItem(product2, 1);

        //Act
        target.RemoveLine(product2);

        //Assert
        Assert.Empty(target.Lines.Where(c => c.Product == product2));
        Assert.Equal(2, target.Lines.Count());
    }

    [Fact]
    public void Calculate_Total_Count()
    {
        //Arrange create some test products
        Product product1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
        Product product2 = new Product { ProductID = 2, Name = "P2", Price = 50M };

        //Arrange create cart
        Cart target = new Cart();

        //Act
        target.AddItem(product1, 1);
        target.AddItem(product2, 1);
        target.AddItem(product1, 3);
        decimal result = target.ComputeTotalValue();

        //Assert
        Assert.Equal(450M, result);
    }

    [Fact]
    public void Can_Clean_Content()
    {
        //Arrange create some test products
        Product product1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
        Product product2 = new Product { ProductID = 2, Name = "P2", Price = 50M };

        //Arrange create cart
        Cart target = new Cart();

        //Arrange add some product
        target.AddItem(product1, 1);
        target.AddItem(product2, 1);

        //Act
        target.Clear();

        //Assert
        Assert.Empty(target.Lines);
    }
}