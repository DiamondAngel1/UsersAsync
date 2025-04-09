using System.ComponentModel.DataAnnotations;
using Bogus;

namespace MyMvcApp.Data;

public class Manager{

    public void AddUsers(Contexta context)
    {
        var userFaker = new Faker<User>("uk")
    .RuleFor(u => u.Sex, f => f.PickRandom<bool>(true, false))
    .RuleFor(u => u.FirstName, (f, u) =>
        u.Sex ? f.Name.FirstName(Bogus.DataSets.Name.Gender.Male) : f.Name.FirstName(Bogus.DataSets.Name.Gender.Female))
    .RuleFor(u => u.LastName, (f, u) =>
        u.Sex ? f.Name.LastName(Bogus.DataSets.Name.Gender.Male) : f.Name.LastName(Bogus.DataSets.Name.Gender.Female))
    .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber("+380#########"))
    .RuleFor(u => u.Image, (f, u) =>
        $"https://randomuser.me/api/portraits/{(u.Sex ? "men" : "women")}/{f.Random.Number(1, 99)}.jpg");

        for (int i = 0; i < 20; i++)
        {
            var b = userFaker.Generate(1);
            context.Users.Add(b[0]);
            context.SaveChanges();

        }
    }
}