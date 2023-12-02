using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Travel_Agency_Core;
using Travel_Agency_DataBase;
using Newtonsoft.Json;

namespace Travel_Agency_Seed.Seeders;

public abstract class SeederBase<TData> where TData : Entity
{
    protected List<TData> Data { get; set; }

    protected SeederBase(string path)
    {
        Data = new List<TData>();
        InitData(path);
    }

    private void InitData(string path)
    {
        var json = File.ReadAllText($"../../../Data/{path}.json");
        this.Data = JsonConvert.DeserializeObject<List<TData>>(json) ?? new List<TData>();
    }

    public async Task Execute(TravelAgencyContext dbContext)
    {
        if (await dbContext.Set<TData>().AnyAsync())
            return;

        await ConfigureSeed(dbContext);
    }

    protected abstract Task ConfigureSeed(TravelAgencyContext dbContext);
}