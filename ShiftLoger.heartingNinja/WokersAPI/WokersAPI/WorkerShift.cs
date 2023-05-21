namespace WokersAPI;

public class WorkerShift
{
    public int Id { get; set; }
    public int SuperHeroId { get; set; }
    public string Name { get; set; }
    public DateTime LoginTime { get; set; }
    public DateTime LogoutTime { get; set; }
}
