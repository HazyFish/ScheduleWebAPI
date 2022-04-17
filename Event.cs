using Swashbuckle.AspNetCore.Annotations;

namespace ScheduleWebAPI;

public class Event
{
    [SwaggerSchema(ReadOnly = true)]
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Type { get; set; } = "";
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string Location { get; set; } = "";
}

