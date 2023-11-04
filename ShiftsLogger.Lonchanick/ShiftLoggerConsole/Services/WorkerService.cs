
using Newtonsoft.Json;
using ShiftsLoggerConsole.Models;
using Spectre.Console;
using System.Net.Http.Json;

namespace ShiftLoggerConsole.Services;

public class WorkerService
{
        
    public HttpClient client = new();
    public static readonly string getUrl="http://localhost:5180/api/Worker";
    public static readonly string getByIdUrl="http://localhost:5180/api/Worker/";
    public static readonly string updateUrl="http://localhost:5180/api/Worker/";
    public WorkerService(){}
    

    public async Task<IEnumerable<Worker>?> GetAsync()
    {

        List<Worker>? workers=null;

        HttpResponseMessage response = await client.GetAsync(getUrl);

        if(response is not null)
        {
            var data = response.Content.ReadAsStringAsync().Result;
            workers = JsonConvert.DeserializeObject<List<Worker>>(data);
            return workers;
        }

        return workers;
    }

    public async Task<Worker?> GetById(int id)
    {
        string temp=getByIdUrl+id;
        Worker? worker=null;
        HttpResponseMessage response = await client.GetAsync(temp);

        if(response is not null)
        {
            var data = response.Content.ReadAsStringAsync().Result;
            worker = JsonConvert.DeserializeObject<Worker>(data);
            return worker;
        }

        return worker;
    }

    public async Task Add(Worker worker)
    {
        string tempUrl=getUrl;
        await client.PostAsJsonAsync(tempUrl,worker);
    }

    public async Task Delete(int id)
    {
        string tempUrl = updateUrl+id;
        await client.DeleteAsync(tempUrl);
    }

    public async Task Update(int id)
    {
        string tempUrl = updateUrl+id;
        //how to make an update request?
        string newName = AnsiConsole.Ask<string>("New Name: ");
        Worker newWorker = new(){
            Id=id,
            Name=newName
        };
        await client.PutAsJsonAsync(tempUrl,newWorker);
    }

}