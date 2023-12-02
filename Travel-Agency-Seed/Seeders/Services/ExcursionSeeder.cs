using Microsoft.EntityFrameworkCore;
using Travel_Agency_DataBase;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_Seed.Seeders.Services;

public class ExcursionSeeder : SeederBase<Excursion>
{
    public ExcursionSeeder() : base("Services/excursion")
    {
    }

    protected override async Task ConfigureSeed(TravelAgencyContext dbContext)
    {
        foreach (var item in Data)
        {
            var image = await dbContext.Images.Where(x => x.Name == item.Image.Name).SingleOrDefaultAsync();
            if (image is null)
                throw new Exception($"Image with name {item.Name} not found");

            item.Image = image;

            var places = item.Places.ToList();
            for (var i = 0; i < places.Count; i++)
            {
                var i1 = i;
                var place = await dbContext.TouristPlaces.Where(x => x.Name == places[i1].Name)
                    .SingleOrDefaultAsync();

                places[i] = place ?? throw new Exception($"TouristPlace with name {places[i].Name} not found");
            }

            var activities = item.Activities.ToList();
            for (var i = 0; i < activities.Count; i++)
            {
                var i1 = i;
                var activity = await dbContext.TouristActivities.Where(x => x.Name == activities[i1].Name)
                    .SingleOrDefaultAsync();

                activities[i] = activity ??
                                throw new Exception($"TouristActivity with name {activities[i].Name} not found");
            }

            var hotels = item.Hotels.ToList();
            for (var i = 0; i < hotels.Count; i++)
            {
                var i1 = i;
                var hotel = await dbContext.Hotels.Where(x => x.Name == activities[i1].Name)
                    .SingleOrDefaultAsync();

                hotels[i] = hotel ??
                            throw new Exception($"Hotel with name {activities[i].Name} not found");
            }

            item.Places = places;
            item.Activities = activities;
            item.Hotels = hotels;
        }

        await SingleData(dbContext);
    }
}