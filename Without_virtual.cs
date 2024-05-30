using System;
using System.Collections.Generic;

abstract class Building
{
    public string Name { get; set; }
    public string Address { get; set; }
    public double Area { get; set; }
    public int NumberOfFloors { get; set; }
    public int YearBuilt { get; set; }

    public Building(string name, string address, double area, int numberOfFloors, int yearBuilt)
    {
        Name = name;
        Address = address;
        Area = area;
        NumberOfFloors = numberOfFloors;
        YearBuilt = yearBuilt;
    }

    public abstract string GetAdditionalInfo();

    public override string ToString()
    {
        return $"Name: {Name}, Address: {Address}, Area: {Area}, Floors: {NumberOfFloors}, Year Built: {YearBuilt}, {GetAdditionalInfo()}";
    }
}

class ResidentialBuilding : Building
{
    public int NumberOfApartments { get; set; }
    public string ApartmentType { get; set; }

    public ResidentialBuilding(string name, string address, double area, int numberOfFloors, int yearBuilt, int numberOfApartments, string apartmentType)
        : base(name, address, area, numberOfFloors, yearBuilt)
    {
        NumberOfApartments = numberOfApartments;
        ApartmentType = apartmentType;
    }

    public override string GetAdditionalInfo()
    {
        return $"Apartments: {NumberOfApartments}, Apartment Type: {ApartmentType}";
    }
}

class CommercialBuilding : Building
{
    public string BuildingType { get; set; }

    public CommercialBuilding(string name, string address, double area, int numberOfFloors, int yearBuilt, string buildingType)
        : base(name, address, area, numberOfFloors, yearBuilt)
    {
        BuildingType = buildingType;
    }

    public override string GetAdditionalInfo()
    {
        return $"Building Type: {BuildingType}";
    }
}

class BuildingProject
{
    public string Description { get; set; }
    public List<Building> Buildings { get; set; }

    public BuildingProject(string description)
    {
        Description = description;
        Buildings = new List<Building>();
    }

    public void AddBuilding(Building building)
    {
        Buildings.Add(building);
    }

    public override string ToString()
    {
        string projectInfo = $"Project Description: {Description}\nBuildings:\n";
        foreach (var building in Buildings)
        {
            projectInfo += building.ToString() + "\n";
        }
        return projectInfo;
    }
}

class BuildingGenerator
{
    private Random random = new Random();

    public ResidentialBuilding GenerateResidentialBuilding()
    {
        string[] apartmentTypes = { "Studio", "1 Bedroom", "2 Bedrooms", "3 Bedrooms" };
        string name = "Residential Building";
        string address = GenerateAddress();
        double area = random.Next(500, 2000);
        int numberOfFloors = random.Next(3, 20);
        int yearBuilt = random.Next(1950, 2020);
        int numberOfApartments = random.Next(10, 100);
        string apartmentType = apartmentTypes[random.Next(apartmentTypes.Length)];

        return new ResidentialBuilding(name, address, area, numberOfFloors, yearBuilt, numberOfApartments, apartmentType);
    }

    public CommercialBuilding GenerateCommercialBuilding()
    {
        string[] buildingTypes = { "Office", "Retail", "Restaurant", "Hotel" };
        string name = "Commercial Building";
        string address = GenerateAddress();
        double area = random.Next(1000, 5000);
        int numberOfFloors = random.Next(1, 10);
        int yearBuilt = random.Next(1970, 2020);
        string buildingType = buildingTypes[random.Next(buildingTypes.Length)];

        return new CommercialBuilding(name, address, area, numberOfFloors, yearBuilt, buildingType);
    }

    private string GenerateAddress()
    {
        return $"{random.Next(1, 1000)} Street, City";
    }
}

class Program
{
    static void Main(string[] args)
    {
        BuildingGenerator generator = new BuildingGenerator();

        int choice;
        do
        {
            Console.WriteLine("Select building type:");
            Console.WriteLine("1. Residential Building");
            Console.WriteLine("2. Commercial Building");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice: ");
        } while (!int.TryParse(Console.ReadLine(), out choice) || (choice < 0 || choice > 2));

        if (choice == 0)
        {
            Console.WriteLine("Exiting program...");
            return;
        }

        Building building = null;

        if (choice == 1)
        {
            building = generator.GenerateResidentialBuilding();
        }
        else if (choice == 2)
        {
            building = generator.GenerateCommercialBuilding();
        }

        Console.WriteLine($"\nGenerated {building.GetType().Name}:");
        Console.WriteLine(building);

        Console.WriteLine("\nEnter project description:");
        string projectDescription = Console.ReadLine();

        BuildingProject project = new BuildingProject(projectDescription);
        project.AddBuilding(building);

        Console.WriteLine("\nDo you want to add more buildings to the project? (yes/no)");
        string addMore = Console.ReadLine().ToLower();

        while (addMore == "yes")
        {
            do
            {
                Console.WriteLine("\nSelect building type:");
                Console.WriteLine("1. Residential Building");
                Console.WriteLine("2. Commercial Building");
                Console.WriteLine("0. Finish and view project");
                Console.Write("Enter your choice: ");
            } while (!int.TryParse(Console.ReadLine(), out choice) || (choice < 0 || choice > 2));

            if (choice == 0)
                break;

            if (choice == 1)
            {
                building = generator.GenerateResidentialBuilding();
            }
            else if (choice == 2)
            {
                building = generator.GenerateCommercialBuilding();
            }

            Console.WriteLine($"\nGenerated {building.GetType().Name}:");
            Console.WriteLine(building);

            project.AddBuilding(building);

            Console.WriteLine("\nDo you want to add more buildings to the project? (yes/no)");
            addMore = Console.ReadLine().ToLower();
        }

        Console.WriteLine($"\nFinal Building Project:");
        Console.WriteLine(project);
    }
}
